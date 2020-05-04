#include <SoftwareSerial.h>
#include <string.h>
#include <ctype.h>
int ledPin = 13;                  // LED test pin
int byteGPS=-1;
char linea[300] = "";
char comandoGPR[7] = "$GPRMC";
int cont=0;
int bien=0;
int conta=0;
int indices[13];

int altitude=0;
double latitude=0;
double longitude=0;

String lat ="";
String latOrientation ="";

String longt ="";
String longtOrientation ="";
String speedKnots="";
String hdg="";
String alt="";
String dateTime="";
String planeInfo="";
SoftwareSerial mySerialwIFI(0, 1); // RX, TX
SoftwareSerial mySerialGps(2, 3); // RX, TX

void setup() {
  // set the data rate for the SoftwareSerial port
  mySerialGps.begin(9600);
  pinMode(ledPin, OUTPUT);       // Initialize LED pin
  Serial.begin(9600);
  for (int i=0;i<300;i++){       // Initialize a buffer for received data
    linea[i]=' ';
  }   
}
void loop() {
   digitalWrite(ledPin, HIGH);
   byteGPS=mySerialGps.read();         // Read a byte of the serial port
   if (byteGPS == -1) {           // See if the port is empty yet
     delay(100); 
   } else {
     linea[conta]=byteGPS;        // If there is serial port data, it is put in the buffer
     conta++;                      
     //Serial.print(byteGPS, BYTE); 
     if (byteGPS==13){            // If the received byte is = to 13, end of transmission
       digitalWrite(ledPin, LOW); 
       cont=0;
       bien=0;
       for (int i=1;i<7;i++){     // Verifies if the received command starts with $GPR
         if (linea[i]==comandoGPR[i-1]){
           bien++;
         }
       }
       if(bien==6){               // If yes, continue and process the data
         for (int i=0;i<300;i++){
           if (linea[i]==','){    // check for the position of the  "," separator
             indices[cont]=i;
             cont++;
           }
           if (linea[i]=='*'){    // ... and the "*"
             indices[12]=i;
             cont++;
           }
         }
        
         String sTime="";
         String sLat ="";
         String sLatD = "";
         String sLong ="";
         String sLongD = "";
         String sHdg="";
         String sAlt="";
         String sSpeedK ="";
         
         for (int i=0;i<12;i++){
           switch(i)
           {
             case 0 ://Serial.print("Time in UTC (HhMmSs): ");
             break;
             
             
             case 1 :/*Serial.print("Status (A=OK,V=KO): ");*/break;
             case 2 ://Serial.print("Latitude: ");
                for (int j=indices[i];j<(indices[i+1]-1);j++)
                {
                   sLat += linea[j+1];
                   //Serial.print(linea[j+1]); 
                }
             break;
             case 3 :/*Serial.print("Direction (N/S): ");*/               
                for (int j=indices[i];j<(indices[i+1]-1);j++)
                {
                   sLatD += linea[j+1];
                   //Serial.print(linea[j+1]); 
                }
             break;
             case 4 :/*Serial.print("Longitude: ");*/
                for (int j=indices[i];j<(indices[i+1]-1);j++)
                {
                   sLong += linea[j+1];
                   //Serial.print(linea[j+1]); 
                }
             break;
             case 5 :/*Serial.print("Direction (E/W): ");*/
                for (int j=indices[i];j<(indices[i+1]-1);j++)
                {
                   sLongD += linea[j+1];
                   //Serial.print(linea[j+1]); 
                }
             break;
             case 6 :/*Serial.print("Velocity in knots: ");*/
                for (int j=indices[i];j<(indices[i+1]-1);j++)
                {
                   sSpeedK += linea[j+1];
                   //Serial.print(linea[j+1]); 
                }
             break;
             case 7 :/*Serial.print("Heading in degrees: ");*/
                for (int j=indices[i];j<(indices[i+1]-1);j++)
                {
                   sHdg += linea[j+1];
                   //Serial.print(linea[j+1]); 
                }
             break;
             case 8 :/*Serial.print("Date UTC (DdMmAa): ");*/
                for (int j=indices[i];j<(indices[i+1]-1);j++)
                {
                   sTime += linea[j+1];
                   //Serial.print(linea[j+1]); 
                }
                   
             break;
             case 9 :/*Serial.print("Magnetic degrees: ");*/break;
             case 10 :/*Serial.print("(E/W): ");*/break;
             case 11 :/*Serial.print("Mode: ");*/break;
             case 12 :/*Serial.print("Checksum: ");*/break;
           }
         }
         Serial.print("planeInfo");
         Serial.print(";");
         Serial.print(sTime);
         Serial.print(";");
         //Acelerometro
         Serial.print(0);
         Serial.print(";");
         Serial.print(0);
         Serial.print(";");
         //---------------
         //ALT DATA
         Serial.print(0);
         Serial.print(";");
         //---------------
         Serial.print(sHdg);
         Serial.print(";");
         
         Serial.print(sSpeedK);
         Serial.print(";");
         
         if(sLatD == 'S')
         {
            Serial.print('-');
         }
         
         Serial.print(sLat);
         Serial.print(";");
   
         if(sLongD == 'W')
         {
            Serial.print('-');
         }
         
         Serial.print(sLong);
         Serial.print(";");
         
         Serial.print("0;0;0;FALSE");
         
         Serial.println(" ");
       }
       conta=0;                    // Reset the buffer
       for (int i=0;i<300;i++){    //  
         linea[i]=' ';             
       }                 
     }
   }
 }
