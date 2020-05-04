using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace ModuloRcReader
{
    /// <summary>
    /// Represents the settings for a monitored channel
    /// </summary>
    public class ChannelInfo
    {
        public InterruptPort pin;

        //Retains the time in ticks the last time a change was
        //observer on this channel
        public long lastTick = -1;
        public int channelID = -1;

        //min- and max- pulse width values expected from the receiver
        //public int minValue = 1000;
        //public int maxValue = 3000;
        public int minValue = 1000;
        public int maxValue = 2000;
        //Stores the last reported value and minimum change required to
        //trigger a change notification
        public float currentValue = -100.0f;
        public float epsilon = 0.01f;
        public int pulseWidth = -1;
        public int lastPulseWidth = -1;
        //Negative range will return the value in the -1.0f - +1.0f range
        //by default set to false for a 0.0f - +1.0f range
        public bool negativeRange = false;

        //If negative range is used this represents the neutral point, should
        //normally be in the middle of the min/max range but could be tweaked
        //during calibration
        //public int zeroPoint = 1500;
        public int zeroPoint = 1500;

        //If clamp is set to true then values returned will NEVER exceed the
        //expected bounds of the range
        public bool clamp = true;
    }
}
