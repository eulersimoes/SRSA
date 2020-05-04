#include <PinChangeInt.h>
#include <Servo.h>

// MultiChannels
//
// rcarduino.blogspot.com
//
// A simple approach for reading three RC Channels using pin change interrupts
//
// See related posts - 
// http://rcarduino.blogspot.co.uk/2012/01/how-to-read-rc-receiver-with.html
// http://rcarduino.blogspot.co.uk/2012/03/need-more-interrupts-to-read-more.html
// http://rcarduino.blogspot.co.uk/2012/01/can-i-control-more-than-x-servos-with.html
//
// rcarduino.blogspot.com
//

// include the pinchangeint library - see the links in the related topics section above for details


// Assign your channel in pins
#define AP_IN_PIN 5
#define THROTTLE_IN_PIN 4
#define ALERON_IN_PIN 2
#define ELEVON_IN_PIN 3
//#define FAILSAFE_IN_PIN 6 //TODO

// Assign your channel out pins
#define ALERON_OUT_PIN 9
#define ELEVON_OUT_PIN 10
#define THROTTLE_OUT_PIN 11

#define BUFF_SIZE 200
#define GPS_BUFF_SIZE 100

#define NAV_LIGHT_PIN 5
#define STROBE_PIN 6
#define FAROL_PIN 7

#define ROLL_ANGLE_TURNS 15
#define PITCH_ANGLE_CLIMBS 15

#define ACELEROMETER_X_IN_PIN A4 //ANALOGIC
#define ACELEROMETER_Y_IN_PIN A6 //ANALOGIC

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

#define MAX_ROLL_ANGLE 15
#define MAX_PITCH_ANGLE 15

Servo servoThrottle;
Servo servoAleron;
Servo servoElevon;

int ch1;
int ch2;
int ch3;
int ch4;
int ch5;
int ch6;

int loopCount =0;
bool autoPilot = false;
bool planeInfoMessage = false;
int autoPilotMode = 0; //0: establization; 1: return to home; 2: waypoint
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
int startMillisSerial =0;
int currentMillis =0;
int startMillisRc =0;

bool flagDispatchEvent =false;
bool flagGpsReady =false;
bool receivingGpsMessage=false;
bool levelingAircraft = false;
bool readingGps = false;
String comando = "";

float currentLatitude =0;
float currentLongitude =0;
int currentAltitude =0;
int targetAltitude = 3000;
int currentHdg =0;
int targetHdg =210;
double currentSpeed=0;

String listWayPoint[10];
int countWayPoint =0;
int nextWayPoint=0;

bool farol = false;
bool navLight = false;
bool strobe = false;
bool serialLock = false;
bool lostRcSinal = false;

void setup()
{
  Serial.begin(9600);
  //commGps.begin(9600);

  Serial.println("Start");

  // attach servo objects, these will generate the correct 
  // pulses for driving Electronic speed controllers, servos or other devices
  // designed to interface directly with RC Receivers  
  servoThrottle.attach(THROTTLE_OUT_PIN);
  servoAleron.attach(ALERON_OUT_PIN);
  servoElevon.attach(ELEVON_OUT_PIN);

  pinMode(2, INPUT); //  ALERON_IN_PIN 2


  currentMillis = millis();
  flagGpsReady = true;

  Serial.println("Loop");
}

void loop()
{
  currentMillis = millis();
  //flightData();

  if(currentMillis - startMillisRc >= 25)
  {
    startMillisRc = millis();
    ch1 = pulseIn(ALERON_IN_PIN, HIGH, 25000); 
    ch2 = pulseIn(ELEVON_IN_PIN, HIGH, 25000); 
    ch3 = pulseIn(THROTTLE_IN_PIN, HIGH, 25000);  
    ch5 = pulseIn(AP_IN_PIN, HIGH, 25000); 
  }

  configFlightParameters();
  flightControl();

  serialRead();
}


// simple interrupt service routine
void calcThrottle()
{

}

void calcAleron()
{

}

void calcElevon()
{

}

void calcAp()
{

}

void configFlightParameters()
{
  //AP ON
  if(ch5 >= 1700)
  {
    //Return to home
    autoPilot = true;
    autoPilotMode = 1;
  }
  else if(ch5 >= 1200)
  {
    //Estab
    autoPilot = true;
    autoPilotMode = 0;
  }

  if(lostRcSinal)
  {
    autoPilot = true;
    autoPilotMode = 1;
  }
  else if(ch5 <= 1200){
    autoPilot = false; 
  }
}  

void flightControl()
{
  verifyIfAircraftIsAboveMaxAngle();

  if(autoPilot)
  {
    if(levelingAircraft || autoPilotMode == 0)
    {
      estabAirCraft();
    }
    else
    {
      turnAirCraft();
    }

  }
  else
  {
    servoAleron.writeMicroseconds(ch1);
    servoElevon.writeMicroseconds(ch2);
    servoThrottle.writeMicroseconds(ch3);
  }
}


void verifyIfAircraftIsAboveMaxAngle()
{
  if(abs(eixoX) > MAX_PITCH_ANGLE ||abs (eixoY) > MAX_ROLL_ANGLE )
  {
    levelingAircraft = true;
  }
  else
  {
    levelingAircraft  = false; 
  }
}

//Obtem dados dos sensores de vÃ´o
void flightData()
{

  int leituraX = analogRead(ACELEROMETER_X_IN_PIN);
  if(leituraX < 100 || leituraX > 800) //Neste caso ignora o valor pois Ã© lixo
  {
    return;  
  }

  int leituraY =  analogRead(ACELEROMETER_Y_IN_PIN);

  if(leituraY < 200 || leituraY > 800) //Neste caso ignora o valor pois Ã© lixo
  {
    return;  
  }

  mediaEixoX +=  leituraX;
  mediaEixoY +=  leituraY;

  //if(countAcelerometer == 2)
  //{
  eixoX = mediaEixoX ;
  eixoY = mediaEixoY ; 
  //eixoX = map(eixoX, 160, 530, -90, 90);
  eixoX = map(eixoX, 200, 623, -90, 90);
  eixoY = map(eixoY, 325, 748, -90, 90);
  countAcelerometer = 0; 
  mediaEixoX =0;
  mediaEixoY =0;
  dataAcelerometerReady = true;
  //}

  countAcelerometer++;
}

void turnAirCraft()
{
  //Turn the aircraft to targetdg
  //Determine if is the aircraft will turn to right or left
  bool turnLeft = false;
  int origem = currentHdg;
  int destino =targetHdg;

  int deltaAlt = targetAltitude -currentAltitude  ;
  //Margem de 10
  //Avião deve subir
  if(abs(deltaAlt) >10 && deltaAlt >0){
    if(abs(eixoX) <= MAX_PITCH_ANGLE )
    {
      servoElevon.writeMicroseconds(1300); 
    }
    else{
      servoElevon.writeMicroseconds(1500);
    }
    //Avião deve descer  
  }
  else if(abs(deltaAlt) >10 && deltaAlt <0){
    if(abs(eixoX) <= MAX_PITCH_ANGLE )
    {
      servoElevon.writeMicroseconds(1700); 
    }
    else{
      servoElevon.writeMicroseconds(1500);
    }
  }


  if(abs(origem - destino) <= 5 && abs(deltaAlt) <= 10)
  {
    estabAirCraft();
    return; 
  } 

  int grausDireita;
  int grausEsquerda;
  if (destino >= origem)
  {
    grausDireita= destino - origem;
    grausEsquerda = 360 - destino;
  }
  else
  {
    grausEsquerda = origem - destino;
    grausDireita = 360 - grausEsquerda;
  }
  if (grausDireita > grausEsquerda)
  {
    turnLeft = true;
  }
  else
  {
    turnLeft = false;
  }


  if(turnLeft){
    if(abs(eixoX) < ROLL_ANGLE_TURNS)
    {
      servoAleron.writeMicroseconds(1400);
    }
    else{
      servoAleron.writeMicroseconds(1500);
    }
  }
  else{
    if(abs(eixoX) < ROLL_ANGLE_TURNS)
    {
      servoAleron.writeMicroseconds(1600);
    }
    else{
      servoAleron.writeMicroseconds(1500);
    }
  } 
}

void estabAirCraft()
{         
  int anglesYAboveLimite =  abs(eixoY) - MAX_ROLL_ANGLE;
  int anglesXAboveLimite = abs(eixoX) - MAX_PITCH_ANGLE;

  //Movimento generico independete se roll é positivo ou negativo
  //Somente para sistemas que tem leitura do acelerometro correto.
  servoAleron.writeMicroseconds(1500 - (eixoY*8));
  servoElevon.writeMicroseconds(1500 +(eixoX*10));
  /* 
   //if(eixoY >5)
   if(eixoY >0)
   { 
   if(anglesYAboveLimite <= 10)
   {
   servoAleron.writeMicroseconds(1400);
   }
   else if(anglesYAboveLimite <= 20)
   {
   servoAleron.writeMicroseconds(1300);
   }
   else if(anglesYAboveLimite <= 30)
   {
   
   servoAleron.writeMicroseconds(1200);
   }
   else if(anglesYAboveLimite <= 40)
   {
   servoAleron.writeMicroseconds(1100);
   }
   else{
   
   servoAleron.writeMicroseconds(1000);
   }
   }
   */

  //else if(eixoY < 5){  
  /*
    else{
   
   if(anglesYAboveLimite <= 10)
   {
   servoAleron.writeMicroseconds(1600);
   }
   else if(anglesYAboveLimite <= 20)
   {
   servoAleron.writeMicroseconds(1700);
   }
   else if(anglesYAboveLimite <= 30)
   {
   servoAleron.writeMicroseconds(1800);
   }
   else if(anglesYAboveLimite <= 40)
   {
   servoAleron.writeMicroseconds(1900);
   }
   else{
   servoAleron.writeMicroseconds(2000);
   }
   }
   */


  /*
  if(eixoX >2){
   
   if(anglesXAboveLimite <= 10)
   {
   servoElevon.writeMicroseconds(1600);
   }
   else if(anglesXAboveLimite <= 20)
   {
   servoElevon.writeMicroseconds(1700);
   }
   else if(anglesXAboveLimite <= 30)
   {
   servoElevon.writeMicroseconds(1800);
   }
   else if(anglesXAboveLimite <= 40)
   {
   servoElevon.writeMicroseconds(1900);
   }
   else{
   servoElevon.writeMicroseconds(2000);
   }
   }
   else if(eixoX < 2){ 
   
   if(anglesXAboveLimite <= 10)
   {
   servoElevon.writeMicroseconds(1400);
   }
   else if(anglesXAboveLimite <= 20)
   {
   servoElevon.writeMicroseconds(1300);
   }
   else if(anglesXAboveLimite <= 30)
   {
   
   servoElevon.writeMicroseconds(1200);
   }
   else if(anglesXAboveLimite <= 40)
   {
   servoElevon.writeMicroseconds(1100);
   }
   else{
   
   servoElevon.writeMicroseconds(1000);
   }
   }
   */
}

void serialRead()
{

  while(Serial.available() >0)
  {
    // read the incoming byte:

    int incomingByte = Serial.read();
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
      tratarComando();
    }    
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
      }
      else if( (comando.substring(1,6)) == "GPVTG" ){
        //SPEED AND HEADING
        comando.substring(7,12).toCharArray(charBuf, 20);
        currentHdg = atoi(charBuf);

        comando.substring(26,31).toCharArray(charBuf, 20);
        currentSpeed = atof(charBuf);
      } 
    }
    else if((comando.substring(0,1)) == "#" )
    {
      int count =0;
      for (int i=1; i< 200; i++)
      {
        if(comando[i] != ';' && comando[i] != '*'){
          cmd[count] += comando[i];
        }
        else if(comando[i] == '*'){
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
  if(cmd[0] == "INFO")
  {     
    currentHdg = stringToInt(cmd[1]);
    targetHdg = stringToInt(cmd[2]);
    currentAltitude = stringToInt(cmd[3]);
    currentSpeed= stringToInt(cmd[4]);
  }
  else if(cmd[0] == "DEBUG")
  {
    eixoX = stringToInt(cmd[1]);
    eixoY = stringToInt(cmd[2]);
    currentAltitude = stringToInt(cmd[3]);
    currentHdg = stringToInt(cmd[4]);
  }
}

int stringToInt(String minhaString) 
{
  int numero = minhaString.toInt();
  return numero;
}







