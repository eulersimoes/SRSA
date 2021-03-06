#include <SoftwareSerial.h>
#include <Servo.h>


// Assign your channel in pins
#define AP_IN_PIN 8
#define THROTTLE_IN_PIN 7
#define ALERON_IN_PIN 5
#define ELEVON_IN_PIN 6
#define FAILSAFE_IN_PIN 9 //TODO

#define BAT_METER_IN_PIN A1 //MEDIDOR_BATERIA
#define ACELEROMETER_X_IN_PIN A4 //ANALOGIC
#define ACELEROMETER_Y_IN_PIN A6 //ANALOGIC


// Assign your channel out pins
#define THROTTLE_OUT_PIN 10
#define ALERON_OUT_PIN 9
#define ELEVON_OUT_PIN 11

#define BUFF_SIZE 200
#define GPS_BUFF_SIZE 100

#define NAV_LIGHT_PIN 5
#define STROBE_PIN 6
#define FAROL_PIN 7

#define ROLL_ANGLE_LIMITE 45
#define PITCH_ANGLE_LIMITE 45

#define ROLL_ANGLE_TURNS 35
#define PITCH_ANGLE_CLIMBS 35

// These bit flags are set in bUpdateFlagsShared to indicate which
// channels have new signals
#define THROTTLE_FLAG 1
#define ALERON_FLAG 2
#define ELEVON_FLAG 4
#define AP_FLAG 5

#define MIN_ALERON 1088
#define MAX_ALERON 1812

#define MIN_ELEVON 1242
#define MAX_ELEVON 1868

#define MIN_THROTTLE 1088
#define MAX_THROTTLE 1750

// holds the update flags defined above
volatile uint8_t bUpdateFlagsShared;

// shared variables are updated by the ISR and read by loop.
// In loop we immediatley take local copies so that the ISR can keep ownership of the 
// shared ones. To access these in loop
// we first turn interrupts off with noInterrupts
// we take a copy to use in loop and the turn interrupts back on
// as quickly as possible, this ensures that we are always able to receive new signals
volatile uint16_t unThrottleInShared;
volatile uint16_t unSteeringInShared;
volatile uint16_t unAuxInShared;
volatile uint16_t unApInShared;
// These are used to record the rising edge of a pulse in the calcInput functions
// They do not need to be volatile as they are only used in the ISR. If we wanted
// to refer to these in loop and the ISR then they would need to be declared volatile
uint32_t ulThrottleStart;
uint32_t ulSteeringStart;
uint32_t ulAuxStart;
uint32_t ulApStart;
// Servo objects generate the signals expected by Electronic Speed Controllers and Servos
// We will use the objects to output the signals we read in
// this example code provides a straight pass through of the signal with no custom processing
Servo servoThrottle;
Servo servoAleron;
Servo servoElevon;

int loopCount =0;
bool autoPilot = true;
bool planeInfoMessage = false;
int autoPilotMode = 0; //0: establization; 1: waypoint
int batMeter = 0;

char charMessageBuffer[BUFF_SIZE];
char charMessageGpsBuffer[GPS_BUFF_SIZE];

int  countMessageBuffer;
int countGpsMessageBuffer;
bool flagMessageRecebida=false;
bool receivingMessage = false;
bool dataAcelerometerReady = false;

int countAcelerometer =0;
int mediaEixoX=0;
int mediaEixoY=0;

int eixoX=0;
int eixoY=0;

int startMillis =0;
int startMillisRc =0;
int startMillisAp=0;
int startMillisGps=0;
int currentMillis =0;
int startMillisInfo =0;

bool flagDispatchEvent =false;
bool flagGpsReady =false;
bool receivingGpsMessage=false;

bool readingGps = false;
String comando = "";

double currentLatitude =0;
double currentLongitude =0;

double homeLatitude =-19.80540;
double homeLongitude =-43.18337;

int currentAltitude =0;
int currentHdg =0;
int targetHdg =0;
double currentSpeed=0;

String listWayPoint[10];
int countWayPoint =0;
int nextWayPoint=0;

bool farol = false;
bool navLight = false;
bool strobe = false;
bool serialLock = false;

int ch1;
int ch2;
int ch3;
int ch6;

SoftwareSerial gpsSerial(10, 11); // RX, TX
SoftwareSerial navSerial(11, 12); // RX, TX

void setup()
{
  Serial.println("Start");
  Serial.begin(9600);
  gpsSerial.begin(9600);
  navSerial.begin(9600);
  Serial.println("Loop");
}

void loop()
{
  //Executa a Ação a cada xms
  
  currentMillis = millis();

  serialRead();
  flightData();
  tratarComando();
  //flightControl();
  
  
  if(dataAcelerometerReady == true)
  {
    dataAcelerometerReady = false;
  }
  
  if (flagMessageRecebida ==true)
  { 
     //TODO: TRATAR COMANDO RECEBIDO
     flagMessageRecebida = false;
  }
  
  //Executa a Ação a cada xms
  if(currentMillis - startMillis >= 150)
  {
    startMillis = millis();
    gerarPlaneInfoText();    
  }
  
  //Realiza leitura do GPS a cada 1s
  if(currentMillis - startMillisGps >= 1000)
  {
    startMillisGps =millis();
    gpsRead();  
  }
  
  //Passa informações para a placa nav a cada 2s
  if(currentMillis - startMillisInfo >= 2000)
  {
    startMillisInfo =millis();
    gerarInfoText();  
  }
  
}

void flightData()
{
 int leituraBat =  analogRead(BAT_METER_IN_PIN);
 batMeter = map(leituraBat, 0, 1023, 0, 2996);
 
 int leituraX = analogRead(ACELEROMETER_X_IN_PIN);
 if(leituraX < 100 || leituraX > 700) //Neste caso ignora o valor pois é lixo
 {
    return;  
 }
 
 int leituraY =  analogRead(ACELEROMETER_Y_IN_PIN);
 if(leituraY < 200 || leituraY > 650) //Neste caso ignora o valor pois é lixo
 {
    return;  
 }
 
 mediaEixoX +=  leituraX;
 mediaEixoY +=  leituraY;

 if(countAcelerometer == 50)
 {
  eixoX = mediaEixoX/50;
  eixoY = mediaEixoY/50; 
  //eixoX = map(eixoX, 160, 530, -90, 90);
  eixoX = map(eixoX, 194, 613, -90, 90);
  eixoY = map(eixoY, 330, 775, -90, 90);
  countAcelerometer = 0; 
  mediaEixoX =0;
  mediaEixoY =0;
  dataAcelerometerReady = true;
 }
 countAcelerometer++;
 
 calcHdgToTarget(currentLatitude,currentLongitude,homeLatitude,homeLongitude);
 calcDistance(currentLatitude,currentLongitude,homeLatitude,homeLongitude);
}

void serialRead()
{
  /*              
  if(Serial1.available() && flagMessageRecebida ==false && serialLock == false)
  {
    // read the incoming byte:
         
                serialLock = true;
                int incomingByte = Serial1.read();
                //char myChar2 = incomingByte;
                //Serial.print(myChar2);
                
                if(incomingByte == 10)
                {
                   serialLock = false;
                   return;  
                }
                char myChar = incomingByte;
                if(myChar == '#')
                {
                  planeInfoMessage = true; 
                }
                
                charMessageBuffer[countMessageBuffer] = myChar;
                countMessageBuffer++;
                
                if(countMessageBuffer == BUFF_SIZE || incomingByte == 13 || (planeInfoMessage == true && myChar=='*'))
                {
                  planeInfoMessage = false;
                  countMessageBuffer = 0; 
                  comando = String(charMessageBuffer);

                  for(int i=0; i< BUFF_SIZE;i++)
                  {
                    //CLEANING THE BUFFER FOR NEXT TURN
                    charMessageBuffer[i] =' ';
                  }
                  flagMessageRecebida = true;
                }
               serialLock=false;  
  } 
  */
}


void gpsRead()
{

  while(gpsSerial.available() >0)
  {
    // read the incoming byte:
         
                serialLock = true;
                int incomingByte = gpsSerial.read();
                //char myChar2 = incomingByte;
                //Serial.print(myChar2);
                
                if(incomingByte == 10)
                {
                   serialLock = false;
                   return;  
                }
                char myChar = incomingByte;
                if(myChar == '#')
                {
                  planeInfoMessage = true; 
                }
                
                charMessageBuffer[countMessageBuffer] = myChar;
                countMessageBuffer++;
                
                if(countMessageBuffer == BUFF_SIZE || incomingByte == 13 || (planeInfoMessage == true && myChar=='*'))
                {
                  planeInfoMessage = false;
                  countMessageBuffer = 0; 
                  comando = String(charMessageBuffer);

                  for(int i=0; i< BUFF_SIZE;i++)
                  {
                    //CLEANING THE BUFFER FOR NEXT TURN
                    charMessageBuffer[i] =' ';
                  }
                  flagMessageRecebida = true;
                }
               serialLock=false;  
  } 
}



void tratarComando()
{
   char charBuf[20];
   String cmd[20];
   //STARTING BUFFER
   for (int i=0; i<20; i++)
   {
     charBuf[i] = ' ';
   }
 
   if(flagMessageRecebida == true)
   { 
     if(comando.substring(0,1) == "$")
     {
       //gps
       if((comando.substring(1,6)) == "GPGGA" ) 
       {
         //Latitude
         //comando.substring(18,27).toCharArray(charBuf, 20);
         //CONVERSION TO GOOGLEMAPS FORMAT
         comando.substring(18,20).toCharArray(charBuf, 20);
         int dd = atoi(charBuf);
         comando.substring(20,27).toCharArray(charBuf, 20);
         double mmmm = atof(charBuf);
         mmmm =mmmm/60;
         currentLatitude = dd + mmmm;
         //currentLatitude = atof(charBuf);
         if((comando.substring(28,29)) == "S")
         {
           currentLatitude = currentLatitude * -1;
         }
         
         //Longitude
         //CONVERSION TO GOOGLEMAPS FORMAT
         comando.substring(30,33).toCharArray(charBuf, 20);
         dd = atoi(charBuf);
         comando.substring(33,40).toCharArray(charBuf, 20);
         mmmm = atof(charBuf);
         mmmm =mmmm/60;
         currentLongitude = dd + mmmm;
         
         if((comando.substring(41,42)) == "W")
         {
           currentLongitude = currentLongitude * -1;
         }
         
         comando.substring(52,56).toCharArray(charBuf, 20);
         currentAltitude = atoi(charBuf);
       }else if( (comando.substring(1,6)) == "GPVTG" ){
         //SPEED AND HEADING
         comando.substring(7,12).toCharArray(charBuf, 20);
         currentHdg = atoi(charBuf);
         
         comando.substring(26,31).toCharArray(charBuf, 20);
         currentSpeed = atof(charBuf);
      } 
     }else if((comando.substring(0,1)) == "#" )
     {
        int count =0;
        for (int i=1; i< 200; i++)
        {
            if(comando[i] != ';' && comando[i] != '*'){
              cmd[count] += comando[i];
            }else if(comando[i] == '*'){
              break;
            }
            else{
              count++;
            }
        }
        executeCommand(cmd);
     }
     flagMessageRecebida = false;
   }  
}

void executeCommand(String cmd[])
{
  if(cmd[1] == "SETFAROL")
  {
    if(cmd[2] == "ON")
    {
      farol = true;
    }else{
     farol = false;     
    }
     
  }else if(cmd[1] == "SETNAVLIGHT"){
    if(cmd[2] == "ON")
    {
      navLight = true;
    }else{
     navLight = false;     
    }  
  }else if(cmd[1] == "SETSTROBE"){
    if(cmd[2] == "ON")
    {
      strobe = true;
    }else{
     strobe = false;     
    }     
  }else if(cmd[1] == "ADDWAYPOINT"){
    if(countWayPoint < 10)
    {
       String wayP = String(countWayPoint) + ";" + cmd[3] + ";" + cmd[4] + ";" + cmd[5];
       listWayPoint[countWayPoint] = wayP;
       countWayPoint++;
       Serial.print("WAYPOINT ADDED:");
       Serial.print(wayP);
       Serial.print(" Pos:");
       Serial.println(countWayPoint);
    }else{
       Serial.println("WAYPOINTS FULL");
    }
 }else if(cmd[1] == "SETAUTOPILOTMODE"){
    //WayPoint 
    if(cmd[2] == "NAV"){
      Serial.println("AP SET TO NAV");
      autoPilotMode = 1;
    }else{
      Serial.println("AP SET TO LEVEL");
      autoPilotMode =0;
    }
 }else if(cmd[1] == "SETNEXTWAYPOINT"){
    //WayPoint 
    char charBuff[10];
    cmd[2].toCharArray(charBuff, 10);
    Serial.println(cmd[2]);
    int nextP = atoi(charBuff);
    if(nextP <= countWayPoint){
      nextWayPoint = nextP;
      Serial.print("NEXT WAYPOINT SETED TO:");
      Serial.println(listWayPoint[nextWayPoint]);
    }else{
      Serial.println("NO WAYPOINT FOUND");
    }
 }
}
    

void gerarPlaneInfoText()
{
  if(!serialLock){
  serialLock = true;   
  Serial.print("#PLANEINFO;");
  Serial.print("01/01/2012;");
  Serial.print(eixoY);//ROLL ANGLE
  Serial.print(";");
  Serial.print(eixoX);//PITCH ANGLE
  Serial.print(";");
  Serial.print(currentAltitude);
  Serial.print(";");
  Serial.print(currentHdg);
  Serial.print(";");
  Serial.print(currentSpeed);
  Serial.print(";");

  Serial.print(currentLatitude,6);
  Serial.print(";");
  

  Serial.print(currentLongitude,6);
  Serial.print(";");
  Serial.print("0;0;"); //Throtlher and Fuel
  Serial.print(batMeter);
  Serial.print(";");
  
  if(autoPilot == 0){
     Serial.print("FALSE");
  }else{
     Serial.print("TRUE");
  }
  Serial.print(";");
  Serial.print(nextWayPoint);
  Serial.print(";");
  Serial.print(targetHdg);
  Serial.print("*");
  Serial.print("\n");
  serialLock = false;
  }
}

void gerarInfoText()
{ 
  navSerial.print("#INFO;");
  //navSerial.print(currentHdg);
  navSerial.print(0);
  navSerial.print(";");
  
  navSerial.print(targetHdg);
  navSerial.print(";"); 
    
  navSerial.print(currentAltitude);
  navSerial.print(";");

  navSerial.print(currentSpeed);
  navSerial.print(";");

  navSerial.print("*");
  navSerial.print("\n");
}

double calcDistance(double lat1, double lon1, double lat2, double lon2)
{
  double R = 6371;                            //Radio de la tierra km
  double dLat = radians(lat2 - lat1);
  double dLong = radians(lon2 - lon1);

  double a = sin(dLat/2) * sin(dLat/2) + cos(radians(lat1)) * cos(radians(lat2)) * sin(dLong/2) * sin(dLong/2);
  double c = 2 * atan2(sqrt(a), sqrt(1-a));
  double d = R * c;

  return d * 1000;                 //Retorna distancia em metros
}

void calcHdgToTarget(double lat1, double lon1, double lat2, double lon2)
{
//Decilam to Radians  
   double rlat1 = (lat1*PI /180);
   double rlon1 = (lon1*PI /180);
   double rlat2 = (lat2*PI /180);
   double rlon2 = (lon2*PI /180);

//determine angle
   double bearing = atan2(sin(rlon2-rlon1)*cos(rlat2), (cos(rlat1)*sin(rlat2))-(sin(rlat1)*cos(rlat2)*cos(rlon2-rlon1)));
//convert to degrees
   bearing = (bearing*180/PI);
   bearing = fmod((bearing + 360.0), 360);
   targetHdg = bearing;
}

void calcHdgToWayPoint()
{
 //Get Lat Long of WayPoint  
 String wayP = listWayPoint[nextWayPoint];  
 String cmd[4];
 int count =0;
 
 for (int i=0; i< 200; i++)
 {
            if(wayP[i] != ';' && wayP[i] != '*'){
              cmd[count] += wayP[i];
            }else if(comando[i] == '*'){
              break;
            }
            else{
              count++;
            }
 }
  
 char charBuf[50];
 cmd[1].toCharArray(charBuf, 50);
 double lat2 = atof(charBuf);
 cmd[2].toCharArray(charBuf, 50);
 double lon2 = atof(charBuf);

 //Dec to Radians  
 double rlat1 = (currentLatitude*PI /180);
 double rlon1 = (currentLongitude*PI /180);
 double rlat2 = (lat2*PI /180);
 double rlon2 = (lon2*PI /180);

 //determine angle
 double bearing = atan2(sin(rlon2-rlon1)*cos(rlat2), (cos(rlat1)*sin(rlat2))-(sin(rlat1)*cos(rlat2)*cos(rlon2-rlon1)));
 //convert to degrees
 bearing = (bearing*180/PI);
 bearing = fmod((bearing + 360.0), 360);
 targetHdg = bearing;
}
