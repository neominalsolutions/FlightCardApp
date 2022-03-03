using FlightCardApp.libs.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.domain
{

    /*
     * 
     * Id:string GUID // Uçuş Planlama Id; 
 List<FlightTypePrices>; // Bussiness ve Economy Fiyatı
 ConnectingFlight: boolean; // Aktarmalı mı
 Issuing Company : Company; // Uçuş planlayan şirket
 FightDate: Datetime; // Uçuş tarihi 10.10.2022
 List<Flight> Details  // Uçuştaki tüm uçuş detayı burada olucak
     * 
     */

    /// <summary>
    /// Planlanan uçuşun durum bilgisini bu enum üzerinde tutacağız
    /// </summary>
    public enum FlightPlaningStatus
    {
        Canceled = 100, // uçuş iptal edildi
        Submitted = 101, // Operasyon amiri tarafından onaylandı,
        Closed = 102, // Bilet satışına kapalı
        Created = 99
    }

    /// <summary>
    /// Bu enum ile uçuş için kaç farklı fiyat tarifesi tanımlayabileceğimizi kontrol edebileceğiz.
    /// </summary>
    public enum FlightClass
    {
        Economy,
        Bussiness
    }

    // Bilet satışı için uçuşların listelendiği ekranda sadece Submitted olan uçuşlar görüntülenecek.

    // Not: Domain driven desing kavramında ilişkiler çift taraflı değil. tek taraflı olmalıdır. travelsal direction association tek yönlü aggregate root içerisinden diğer sınıflara bağlanmalıdır. bidirection association çift yönlü olmamalıdır

    public class FlightPlaning : Entity, IAggregateRoot
    {

        public string TotalTravelTime
        {
            get
            {
                DateTime departureTime = Flights.OrderBy(x => x.DepartureTime).Take(1).FirstOrDefault().DepartureTime;

                DateTime arrivalTime = Flights.OrderByDescending(x => x.ArrivalTime).Take(1).FirstOrDefault().ArrivalTime;

                return TravelTimeCalculator.GetTravelTime(arrivalTime, departureTime);
            }
        }


        private string _fromCode;

        public string FromCode { get { return _fromCode; } private set {
                _fromCode = value;
            } }

        private void SetFromCode()
        {
            if (ConnectionFlight)
            {
                var firstFlight = Flights.OrderBy(x => x.DepartureTime).Take(1).FirstOrDefault();

                _fromCode = firstFlight?.From?.ShortCode;
            }
            else
            {  // direkt uçuşlardaki from bilgisi 
                var firstFlight = Flights.Take(1).FirstOrDefault(); // select top(1)

                _fromCode = firstFlight?.From?.ShortCode;
            }
        }

        private string _toCode; // field ile class içi değişiklik yaptık

        public string ToCode{ get { return _toCode; } private set { _toCode = value; } } // field üzerinde hesaplanan değeri property'e bağladık

        private void SetToCode()
        {
            // aktramalı uçuştaki From bilgisi
            if (ConnectionFlight)
            {
                var lastFlight = Flights.OrderByDescending(x => x.ArrivalTime).Take(1).FirstOrDefault();

                _toCode = lastFlight?.To?.ShortCode;
            }
            else
            {  // direkt uçuşlardaki from bilgisi 
                var lastFlight = Flights.Take(1).FirstOrDefault(); // select top(1)

                _toCode = lastFlight?.To?.ShortCode;
            }
        }


        public FlightPlaning()
        {

        }

        public FlightPlaningStatus Status { get; private set; } = FlightPlaningStatus.Created; // İlk değerimiz created olucak.

        public bool ConnectionFlight { get; set; } = false;

        /// <summary>
        /// Flight Date uçuş için zorulu bir alandır. private set contructor üzerinden göndericez.
        /// </summary>
        public DateTime FlightDate { get; private set; }

        // Field da kullanıyoruz. field üzerinden set yaparız
        private List<Flight> _flights; // traversal association tek taraflı ilişki

        /// <summary>
        /// FlightPlaning e girilen uçuşlar
        /// </summary>
        public IReadOnlyCollection<Flight> Flights // property üzerinden get yaparız
        {
            get
            {
                return _flights;
            }
        }

        private List<FlightTypePrice> _prices; // macarcase

        /// <summary>
        /// Property field bağlanmış oldu yukarıdaki get ile yazım arasında bir fark yoktur.
        /// </summary>
        public IReadOnlyCollection<FlightTypePrice> Prices => _prices;

        public FlightPlaning(DateTime flightDate)
        {
            _flights = new List<Flight>();
            _prices = new List<FlightTypePrice>();
            SetFlightDate(flightDate);  // Uçuş planlama gününü encapsulate etmiş olduk.

        }

        /// <summary>
        /// Minimum 2 gün sonrasına uçuş planlaması yapılabilir. 
        /// 
        /// </summary>
        private void SetFlightDate(DateTime flightDate)
        {
            var today = DateTime.Now;
            var dayDiff = (flightDate - today).Days;

            // aynı güne uçuş planlanamaz
            if (flightDate.Year == today.Year && flightDate.Month == today.Month && flightDate.Day == today.Day)
            {
                throw new Exception("Aynı gün içerisinde uçuş planlaması yapılamaz");
            }

            // minimum 2 gün sonrası için uçuş planlanmalıdır
            if (dayDiff < 2)
            {
                throw new Exception("Uçuş minimum 2 gün önceden planlanması lazım");
            }

            // planlanacak olan uçuş makismum 15 günü geçemez

            if (dayDiff > 15)
                throw new Exception("Uçuş makimum 15 gün içerisinde planlanabilir");


            FlightDate = flightDate;

        }

        /// <summary>
        /// Uçuş için gidiş dönüş tanımlaması yapmamızı sağlar. Aktarmalı ve aktarmasız uçuşlarda farklı logicler uygulanacaktır.
        /// </summary>
        /// <param name="flight"></param>
        public void AddFlight(Flight flight)
        {
            // aktarmalı uçuş 
            if (ConnectionFlight)
            {
                if (Flights.Count >= 3)
                {
                    throw new Exception("En fazla 3 farklı aktarma tanımlanabilir");
                    // not 4. aktarmayı eklemeyeceğiz
                }
                else
                {
                    ValidateFlightTime(flight);

                    // son uçuş bilgisine göre saatler arasında bir çakışma var mı kontrolü
                    ValidateConnectingFlightDepartureTimeConflict(flight.DepartureTime);

                    _flights.Add(flight);

                }
            }
            else
            {
                if (Flights.Count() == 0)
                {
                    // direkt uçuş
                    ValidateFlightTime(flight);
                    _flights.Add(flight);
                }
                else
                {
                    throw new Exception("Direkt uçuşlara sadece 1 adet uçuş planlabilir");
                }
            }


            SetFromCode();
            SetToCode();

        }

        /// <summary>
        /// Aktarmalı olanlarda kalkış zamanında bir çakışma var mı kontrol eder.
        /// </summary>
        /// <param name="departureTime"></param>
        private void ValidateConnectingFlightDepartureTimeConflict(DateTime departureTime)
        {

            // uçak daha inmeden nereye kalkıyor
            if (Flights.Where(x => x.ArrivalTime > departureTime).Count() > 1)
            {

                throw new Exception("Kalkış saati varış saatinden sonra seçilmelidir");
            }

            // son aktarmanın varış saati ile yeni eklenecek olan aktarmanın kalkış saati arasında minimum 1 saatlik fark olmalıdır.


            var lastFlight = Flights.OrderByDescending(x => x.ArrivalTime).Take(1).FirstOrDefault();

            if (lastFlight != null)
            {

                // adam uçağa yetişsin
                if ((departureTime - lastFlight.ArrivalTime).Hours < 1)
                {
                    throw new Exception("İki aktarma arasında minimum 1 saat olmalıdır!");
                }

                // adam hava limanında çok beklemesin
                if ((departureTime - lastFlight.ArrivalTime).Hours > 4)
                {
                    throw new Exception("İki aktarma arasında maksimum 4 saat bekleme olmalıdır!");
                }

            }


        }

        /// <summary>
        /// Kalkış variş tarih kontrolünü yaptık.
        /// </summary>
        /// <param name="flight"></param>
        private void ValidateFlightTime(Flight flight)
        {
            if (flight.ArrivalTime == default(DateTime) || flight.DepartureTime == default(DateTime))
            {
                throw new Exception("Kalkış variş saati default datetime olarak girildi!");
            }

            if (flight.ArrivalTime <= flight.DepartureTime)
            {
                throw new Exception("Kalkış saati ile varış saatinden küçük veya eşit girildi!");
            }
        }


        /// <summary>
        /// Bir uçuşa farklı tiplerde fiyat eklememizi sağlar
        /// </summary>
        /// <param name="newPrice"></param>
        public void AddFlightPrice(FlightTypePrice newPrice)
        {

            // makimum 1 flight planing için kaç farklı fiyat tarifesi girebilirim değeri
            int maximumFlightPriceTypeThreshhold = Enum.GetValues(typeof(FlightClass)).Length;

            if(Prices.Count() < maximumFlightPriceTypeThreshhold)
            {
                // Bussiness fiyatı Econmoy fiyatından düşük olamaz

                bool priceExist = Prices.Any(x => x.Name == newPrice.Name); 
                // Economy vardı listede daha önceden sisteme girilmişti.

                if (priceExist)
                {
                    throw new Exception($"{newPrice.Name} zaten tanımladınız!");
                }
                else
                {
                    if(newPrice.Name == FlightClass.Economy.ToString())
                    {
                        var bussinessExist = Prices.FirstOrDefault(x => x.Name == FlightClass.Bussiness.ToString());

                        // economy fiyatı bussiness fiyattan fazla olamaz
                      
                           if(newPrice?.ListPrice >= bussinessExist?.ListPrice)
                            {
                                throw new Exception("Economy fiyatı Bussiness fiyattan yüksek veya aynı olmamalıdır!");
                            } 
                            else
                            {
                                _prices.Add(newPrice);
                            }
                        
                    }
                    else if(newPrice.Name == FlightClass.Bussiness.ToString())
                    {
                        var economyExist = Prices.FirstOrDefault(x => x.Name == FlightClass.Economy.ToString());

       

                        if (newPrice?.ListPrice <= economyExist?.ListPrice)
                        {
                            throw new Exception("Bussiness fiyatı Economy fiyattan küçük veya aynı olmamalıdır!");
                        }
                        else
                        {
                            _prices.Add(newPrice);
                        }
                    }
                }

            }
            else
            {
                throw new Exception("Tüm fiyat tarifleri uygulandı!");
            }
      
        }

        /// <summary>
        /// Operasyon Amiri tarafından uçuş onaylandı
        /// </summary>
        public void FlightSubmit()
        {
            // close yapılan cancel yapılan bir uçuşu submit edemeyiz.
            if (Status == FlightPlaningStatus.Created)
            {
                Status = FlightPlaningStatus.Submitted;
                // submitted olan uçuşlar listenebilecek.
            }
        }

        /// <summary>
        /// Uçuşa hazır artık bilet satışı yok demek.
        /// </summary>
        public void FlightClosed()
        {
            // durumu submitted olan bir uçuş sadece close olabilir.  
            if (Status == FlightPlaningStatus.Submitted)
            {
                Status = FlightPlaningStatus.Closed;
            }
        }

        /// <summary>
        /// Operasyon amiri tarafından onaylanan veya onaylanmayan uçuşlarda iptal politikası uygulanabilir.
        /// </summary>
        public void FlightCanceled(string cancelationReason)
       {

            if (Status == FlightPlaningStatus.Submitted)
            {
                // bu durumda bilet alınmış olabilir bir event fırlatmamız lazım
                Status = FlightPlaningStatus.Canceled;
                AddEvents(new FlightCanceled(Id, cancelationReason));
                // bu kısımda eventin sürece dahil edileceğini söyledik. Event direkt olarak göndermedik. veri tabanına kayıt aşamasından önce eventi göndereceğiz. bunu dispatcher yapacak.
            }

        }


    }
}
