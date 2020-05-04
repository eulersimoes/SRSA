#define ACELEROMETER_X_IN_PIN A4 //ANALOGIC
#define ACELEROMETER_Y_IN_PIN A6 //ANALOGIC
#define BAT_METER_IN_PIN A1 //MEDIDOR_BATERIA

void setup()
{
  Serial.begin(9600);
  //GPS PORT
  //Serial1.begin(9600);
  
  Serial.println("CALIBRACAO DO ACELEROMETRO");

  Serial.println("Loop");
}

void loop()
{
 int leituraX = analogRead(ACELEROMETER_X_IN_PIN);
 Serial.print("x:");
 Serial.println(leituraX);
 
 int leituraY =  analogRead(ACELEROMETER_Y_IN_PIN);
 Serial.print("y:");
 Serial.println(leituraY);
 
  int bat =  analogRead(BAT_METER_IN_PIN);
  
 int batMeter = map(bat, 0, 1023, 0, 2996);
 Serial.print("bat:");
 Serial.println(batMeter);
 
 delay(500);
  
}
