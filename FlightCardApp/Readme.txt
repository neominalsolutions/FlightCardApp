FightPlaning  class
---------------------------
Id:string GUID
List<FlightTypePrices>;
ConnectingFlight: boolean;
Issuing Company : Company;
FightDate: Datetime; 
List<Flight> Details 

//Methods

AddFlight(Flight f);
Cancel(string description); // Kötü hava koşulları, Uçak Arızası, Pandemi vs...

----------------------

Flight class
----------------------

Flight Number : string; (Key)
From : AirPort
To: AirPort
Company: Company
DepartureTime: TimeSpan 
ArrivalTime : TimeSpan
FlightDate : DateTime

-------------------

FlightPlaningFactory sınıfı aktarmalı ve aktarmasız uçuş oluşturur.

ConnectionFlightCard
DirectFlightCard

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
ListPrice 1400 TL
List<FlightDto>  FightDetails
Arrival Time 14:25
Departure Time 09:45
Total Time: 10 saat 20 dk
FromAirPort: İstanbul Hava Limanı

--------------------------------------


Not: FlightCanceled Event fırlatılınca uçuş için satın alınan biletler açık bilete çekilecek. Bu bileti alan kullanıcılara mail gönderilecek.

----------------------------------------------------------
FlightTicket // Son kullanıcının sistemden aldığı bilet
-----------------------------------------------------------

TicketNumber
NameOfPassenger: string (Mert Alptekin)
FlightNumber: string
Class (Business, Economy) : enum
Date (Uçuş Tarihi): DateTime
Flight (Flight Number): string
FlightPlaningId: string
Status (Check-in, ReadyForCheck-in, Check-out, OpenTicket): Enum

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

Read işlemi sonrasında bilet onaylandıktan sonra "BoardingPassReaded" eventi fırlatılır ve FlightTicket CheckOut olarak işaretlenir.