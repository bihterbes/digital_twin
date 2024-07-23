#include <ESP8266WiFi.h>
//#include <WiFiUdp.h>
//#include <NTPClient.h>


//#define WIFI_SSID "simit"
//#define WIFI_PASSWORD "kamisama"

#define LED_Time D1 // NodeMCU'da LED'in bağlanacağı pini tanımlıyoruz.
#define LED_Stop D2
// defines pin numbers
#define trigPin D4
#define echoPin D5
// defines variables
float duration;
float distance;

//WiFiUDP ntpUDP;
//NTPClient timeClient(ntpUDP, "pool.ntp.org", 10800, 60000); // NTP sunucusu, UTC +3 saat dilimi ve 60 saniyede bir güncelleme



void setup() {
  
  Serial.begin(9600);
  delay(1000);
  // connect to wifi.
 /* WiFi.begin(WIFI_SSID, WIFI_PASSWORD);
  Serial.print("connecting"); 
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(500);
   }
  Serial.println();
  Serial.print("connected: ");
  Serial.println(WiFi.localIP()); 
*/
  pinMode(LED_Time, OUTPUT);
  pinMode(LED_Stop, OUTPUT);
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
  
   // NTP istemcisini başlatma
  // timeClient.begin();
}

void loop() {/*
   timeClient.update();
  unsigned long currentTime = timeClient.getEpochTime(); // Mevcut zamanı al
  struct tm *ptm = gmtime((time_t *)&currentTime); // Zamanı yapılandır

  ptm->tm_hour += 7; // UTC +3 saat dilimi için saati güncelle
  if (ptm->tm_hour >= 24) {
    ptm->tm_hour -= 24; // 24 saati geçerse sıfırdan başlat
  }

  int currentHour = ptm->tm_hour; // Mevcut saat
  int currentMinute = ptm->tm_min; // Mevcut dakika
  int currentSecond = ptm->tm_sec; // Mevcut saniye
*/
  // Mevcut zamanı Seri Monitör'e yazdırma
  //Serial.print("Current time: ");
  //Serial.print(currentHour);
  //Serial.print(":");
  //Serial.print(currentMinute);
  //Serial.print(":");
  //Serial.println(currentSecond);

  
  // clears the trigPin
  digitalWrite(trigPin, LOW);
  delayMicroseconds(2);

  //sets the trigPin on HIGH state for 10 micro seconds
  digitalWrite(trigPin, HIGH),
               delayMicroseconds(10);
  digitalWrite(trigPin, LOW);

  // Reads the echoPin, returns the sound wave travel time in microseconds
  duration = pulseIn(echoPin, HIGH);

  //Calculating the distance
  distance = duration * 0.034 / 2; //s=t*v
  //Prints the distance on the Serial Monitor
  Serial.print("Distance: ");
  Serial.println(distance);

  
// Belirli saatlerde LED_Time'i yakıp söndürme
  String unityData = Serial.readString();

  if(unityData[0] == 'y')
  {
    digitalWrite(LED_Time, HIGH);
  }
  if(unityData[0] == 'n')
  {
    
    digitalWrite(LED_Time, LOW);
  }
  /*
  if ((currentSecond >= 0 && currentSecond < 30)) { // Saat 19:35 ile 19:37 arasında LED'i yak
    digitalWrite(LED_Time, HIGH);
    
  } else {
    digitalWrite(LED_Time, LOW);
   
    
  }*/

// Uzaklığa göre LED_Stop yakıp söndür

if(distance<=10){
   digitalWrite(LED_Stop, HIGH);
  }else {
     digitalWrite(LED_Stop, LOW);
    
    }

  delay(10); // 1 saniye bekle ve tekrar kontrol et

  
}
