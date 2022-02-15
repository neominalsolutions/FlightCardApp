FightPlaning  class
---------------------------
Id:string GUID // Uçuş Planlama Id; 
List<FlightTypePrices>; // Bussiness ve Economy Fiyatı
ConnectingFlight: boolean; // Aktarmalı mı
Issuing Company : Company; // Uçuş planlayan şirket
FightDate: Datetime; // Uçuş tarihi 10.10.2022
List<Flight> Details  // Uçuştaki tüm uçuş detayı burada olucak
Status (Canceled, Submitted, Closed)
//Methods


Closed Uçuştan 2 saat önce uçuş kapansın. 
Submitted olması için son olarak uçuşun onayı verilir.
Canceled olmak için 2 saat öncesinden iptal edilebilir olmalıdır. 


AddFlight(Flight f);
Cancel(string cancelationReason); // Kötü hava koşulları, Uçak Arızası, Pandemi vs...


FlightCanceled Event
cancelationReason
fightPlaningId

----------------------

Flight class
----------------------

Flight Number : string; (Key) Uçuş Numarası
From : AirPort           Kalkacağı Havalimanı
To: AirPort             Ineceği Havalimanı
Company: Company        Hangi Şirkete ait bir uçuş olduğu
DepartureTime: TimeSpan Kalkış zamanı
ArrivalTime : TimeSpan  Varış zamanı
FlightDate : DateTime   Uçuş Tarihi

------------------------------------------------

FightPlaningDto => Uçuş kartlarını ekranda göstereceğimiz sınıf

Not: Direkt uçuş oluştururken ilk uçuşun kalkışı ile son uçuşun inişi baz alınacak.
Aktarmalı uçuşlarda ise Flight Detail alanı dolu olucak.

From (IST)
To (BKK)
Arrival Time 14:25
Arrival Date (string) 1 Mart Pazartesi
Total Time 2 saat 30 dakika
Departure Time: 11:10
Departure Date (string) 2 Mart Salı
connectingFlightText (1 aktarmalı veya Direkt)
List<FlightPrice> FlightPrices;
List<FlightDto>  FightDetails
Arrival Time 14:25
Departure Time 09:45
Total Time: 10 saat 20 dk
FromAirPort: İstanbul Hava Limanı
ToAirPort: İzmir Adnan Menderes Hav Limanı

--------------------------------------


Not: FlightCanceled Event fırlatılınca uçuş için satın alınan biletler açık bilete çekilecek. Bu bileti alan kullanıcılara mail gönderilecek.

----------------------------------------------------------
FlightTicket // Son kullanıcının sistemden aldığı bilet
-----------------------------------------------------------

TicketNumber
NameOfPassenger: string (Mert Alptekin)
IdentityNumber: (TC No)
FlightNumber: string
Class (Business, Economy) : enum
Date (Uçuş Tarihi): DateTime
FlightPlaningId: string
Status (Check-in, ReadyForCheck-in, Check-out, OpenTicket): Enum
// Bilet durumu hakkında buradaki status değeri üzerinden bilet ile alakalı bilgi sahibi olacağız.

CheckIn(); Bu işlem ile Ticket Boarding Pass’e dönüşür

ConvertToOpenTicket(); 
// Eğer uçuş iptal olursa Status Open Ticket dönüşür.
OpenTicket olan biletlerin FligthNumber,Date gibi alanları ileri tarihli olduğundan boş bırakılır.

Not: FlightPlaning ilk oluştuğunda ReadyForChecking olarak ayarlanır.


CheckOut(); Uçuş başladıktan sonra veya Boarding Pass gişe memuru tarafından okutulduktan sonra bilet "checkout" olarak işaretlenir. 


Not: Flight Ticket checkin yapıldıktan sonra Boarding Pass’e dönüşmüştür.


Boarding Pass class (Uçağa biniş kartı)
---------------------------------------
TicketNumber: string  -> THY-021332301
FlightNumber: string  -> THY-201
BoardingTime: string  -> 10:20
Gate: string -> 201
Seat:string  ->  4A
From: string -> IST
To:string    -> ADB
Class        -> Bussines
Date         -> DateTime -> 10.10.2022

Read();  Gişe görevlisi tarafından okutulan bilet checkout olarak işaretlenir. 

Read işlemi sonrasında bilet onaylandıktan sonra "BoardingPassReadedBoardingPassReaded" eventi fırlatılır ve FlightTicket CheckOut olarak işaretlenir.
// Checkout işleminde ise biletimizi expire ediyoruz. Bu bilet artık kullanılmaz oluyor.