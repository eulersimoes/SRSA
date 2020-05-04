#define RX_SIGNAL_TIMEOUT 15000 // 15mS
#define NUM_OF_RXCHANNELS 4

#define CHANNEL1_INPUT 2
#define CHANNEL2_INPUT 3
#define CHANNEL3_INPUT 4
#define CHANNEL4_INPUT 5

enum radio_states_e
{
POLLCHANNELS
,SENDDATA
};

enum radio_channels_e
{
CHANNEL1
,CHANNEL2
,CHANNEL3
,CHANNEL4
,DEFAULT_CH
};

enum radio_states_e enRadioStates;
enum radio_channels_e enRadioChannels;

unsigned long ulToggleTimer,ulPrevToggleTimer;
unsigned int uiPrevPinValue;
boolean blSignalTimeout;
byte bChannelData[NUM_OF_RXCHANNELS];

byte convertData(unsigned long ulElapsedTime)
{
ulElapsedTime = ulElapsedTime / 10;

if(ulElapsedTime > 250)
{
ulElapsedTime = 250;
}
else if(ulElapsedTime < 70)
{
ulElapsedTime = 70;
}
else
{
/* Do Nothing */
}

return (byte)ulElapsedTime;
}

void checkChannels()
{
unsigned int uiTemp;

switch(enRadioChannels)
{
case CHANNEL1:
{
// High to Low Transition of Channel 1
if((HIGH == uiPrevPinValue) && (LOW == digitalRead(CHANNEL1_INPUT)))
{
ulPrevToggleTimer = ulToggleTimer;
ulToggleTimer = micros(); // mark start of channel 2

#if 1
uiTemp = bChannelData[0] << 1;
uiTemp += bChannelData[0];
uiTemp += convertData(ulToggleTimer - ulPrevToggleTimer);

bChannelData[0] = (byte)(uiTemp >> 2);
#else
bChannelData[0] = convertData(ulToggleTimer - ulPrevToggleTimer);
#endif

enRadioChannels = CHANNEL3;
}
break;
}
case CHANNEL2:
{
// High to Low Transition of Channel 2
if((HIGH == uiPrevPinValue) && (LOW == digitalRead(CHANNEL2_INPUT)))
{
ulPrevToggleTimer = ulToggleTimer;
ulToggleTimer = micros(); // mark start of channel 3

#if 1
uiTemp = bChannelData[1] << 1;
uiTemp += bChannelData[1];
uiTemp += convertData(ulToggleTimer - ulPrevToggleTimer);

bChannelData[1] = (byte)(uiTemp >> 2);
#else
bChannelData[1] = convertData(ulToggleTimer - ulPrevToggleTimer);
#endif

enRadioChannels = CHANNEL4;
}
break;
}
case CHANNEL3:
{
// High to Low Transition of Channel 3
if((HIGH == uiPrevPinValue) && (LOW == digitalRead(CHANNEL3_INPUT)))
{
ulPrevToggleTimer = ulToggleTimer;
ulToggleTimer = micros(); // mark start of channel 4

#if 1
uiTemp = bChannelData[2] << 1;
uiTemp += bChannelData[2];
uiTemp += convertData(ulToggleTimer - ulPrevToggleTimer);

bChannelData[2] = (byte)(uiTemp >> 2);
#else
bChannelData[2] = convertData(ulToggleTimer - ulPrevToggleTimer);
#endif

enRadioChannels = CHANNEL2;
}
break;
}
case CHANNEL4:
{
// High to Low Transition of Channel 4
if((HIGH == uiPrevPinValue) && (LOW == digitalRead(CHANNEL4_INPUT)))
{
ulPrevToggleTimer = ulToggleTimer;
ulToggleTimer = micros();

#if 1
uiTemp = bChannelData[3] << 1;
uiTemp += bChannelData[3];
uiTemp += convertData(ulToggleTimer - ulPrevToggleTimer);

bChannelData[3] = (byte)(uiTemp >> 2);
#else
bChannelData[3] = convertData(ulToggleTimer - ulPrevToggleTimer);
#endif

uiPrevPinValue = LOW;
enRadioChannels = DEFAULT_CH;
blSignalTimeout = false;
enRadioStates = SENDDATA;
}
break;
}
default:
{
/* Do Nothing */
break;
}
}
}

void setup()
{
Serial.begin(9600);
ulToggleTimer = micros();

pinMode(CHANNEL1_INPUT,INPUT);
pinMode(CHANNEL2_INPUT,INPUT);
pinMode(CHANNEL3_INPUT,INPUT);
pinMode(CHANNEL4_INPUT,INPUT);

enRadioStates = POLLCHANNELS;
enRadioChannels = DEFAULT_CH;
}

void loop()
{
byte bTemp;

switch(enRadioStates)
{
/* Poll Channels */
case POLLCHANNELS:
{
/* Check Channel 1 High to Low Transition */
if(blSignalTimeout)
{
/* Low to High Transition of Channel 1 */
if((LOW == uiPrevPinValue) && (HIGH == digitalRead(CHANNEL1_INPUT)))
{
ulToggleTimer = micros(); // mark start of channel 1
uiPrevPinValue = HIGH;
enRadioChannels = CHANNEL1; // poll Channel 1 first
}

checkChannels();
}
/* Wait for Timeout */
else if(false == digitalRead(CHANNEL1_INPUT))
{
if((micros() - ulToggleTimer) >= RX_SIGNAL_TIMEOUT)
{
blSignalTimeout = true;
uiPrevPinValue = LOW;
enRadioChannels = DEFAULT_CH;
}
}
else
{
ulToggleTimer = micros(); // mark timeout start
}
break;
}
/* Send Data */
case SENDDATA:
{
// 0xF0 + NUM_OF_RXCHANNELS
Serial.write(0xF0+NUM_OF_RXCHANNELS);

// 0x00
Serial.write((byte)0x00);

// 100 = 1mS, 200 = 2mS.
for(bTemp = 0; bTemp < NUM_OF_RXCHANNELS; bTemp++)
{
Serial.write(bChannelData[bTemp]);
}

enRadioStates = POLLCHANNELS;
ulToggleTimer = micros(); // mark timeout start
break;
}
default:
{
/* error state, must not be reached */
break;
}
}
}
