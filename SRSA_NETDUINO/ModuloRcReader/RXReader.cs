using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Collections;

namespace ModuloRcReader
{
    public class RXReader
    {
        #region "Update Event"
        public delegate void ChannelUpdated(ChannelInfo channel, float oldValue, float newValue);
        public event ChannelUpdated channelUpdated;
        private static int countChannels = 0;

        public ArrayList listaChannelMap = new ArrayList();
        protected virtual void OnChannelUpdate(ChannelInfo channel, float oldValue, float newValue)
        {
            if (channelUpdated != null)
                channelUpdated(channel, oldValue, newValue);
        }
        #endregion

        //Constant to convert ticks to microseconds, appears to always be 10 on the Netduino
        private const long ticksToMicroseconds = (long)(TimeSpan.TicksPerMillisecond / 1000);
        public ChannelInfo[] channels;
        public bool isCalibrating = false;


        public RXReader(int quantChannels)
        {
            //Initialize the channel array, uses a mostly-empty array
            //to avoid costly lookups of elements with the trade-off
            //of using additional RAM.
            channels = new ChannelInfo[quantChannels];
        }

        /// <summary>
        /// Begins monitoring a channel on the receiver given a pin index
        /// </summary>
        /// <param name="pinIndex">Index of the digital pin to monitor for this channel</param>
        /// <param name="channelID">ID number of the receiver channel this pin represents</param>
        public ChannelInfo addChannel(Cpu.Pin pin, int channelID)
        {
            channelMap cm = new channelMap();
            cm.channelId = channelID;
            cm.channelPin = int.Parse(pin.ToString());
            listaChannelMap.Add(cm);

            ChannelInfo c = new ChannelInfo();
            c.negativeRange = false;
            c.channelID = channelID;

            c.pin = new InterruptPort(pin, false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);
            try
            {
                c.pin.OnInterrupt += new NativeEventHandler(port_OnInterrupt);
            }
            catch
            {

            }
            channels[countChannels] = c;
            countChannels++;
            return c;
        }


        /// <summary>
        /// Retrieves the ChannelInfo object for a given pin
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public ChannelInfo getChannel(int channelID)
        {
            return channels[channelID];
        }


        private int findChannelPinByChannelId(uint id)
        {
            foreach (channelMap cm in listaChannelMap)
            {
                if (cm.channelId == id)
                {
                    return cm.channelPin;
                }
            }
            return 0;
        }

        /// <summary>
        /// Called when an intterupt event occurs on one of the monitored pins
        /// </summary>
        private void port_OnInterrupt(uint data1, uint state, DateTime time)
        {
            try
            {
                //ChannelInfo channel = channels[data1];
                ChannelInfo channel;
                channel = channels[findChannelPinByChannelId(data1)];

                if (state == 0 && channel != null)
                {
                    long pulseWidth = time.Ticks - channel.lastTick;
                    int pulseWidthMicroseconds = (int)(pulseWidth / ticksToMicroseconds);
                    pulseMeasured(channel, pulseWidthMicroseconds);
                }
                else if (channel != null)
                {
                    channel.lastTick = time.Ticks;
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void pulseMeasured(ChannelInfo channel, int microseconds)
        {
            float val = convertPulseToRange(channel, microseconds);

            if (isCalibrating)
            {
                channel.maxValue = System.Math.Max(channel.maxValue, microseconds);
                channel.minValue = System.Math.Min(channel.minValue, microseconds);
            }
            else
            {
                channel.pulseWidth = microseconds;

                if (floatAbs(val - channel.currentValue) > channel.epsilon)
                {
                    float cVal = channel.currentValue;
                    channel.currentValue = val;
                    OnChannelUpdate(channel, cVal, val);
                }
                channel.lastPulseWidth = microseconds;
            }
        }

        /// <summary>
        /// Converts a measured pulse-width to a range based on the channel's settings
        /// </summary>
        private float convertPulseToRange(ChannelInfo channel, int microseconds)
        {
            float val = 0.0f;

            if (!channel.negativeRange)
            {
                val = (float)(microseconds - channel.minValue) / (float)(channel.maxValue - channel.minValue);

                //if (channel.negativeRange)
                //    val = (val - 0.5f) * 2.0f;
            }
            else
            {
                if (microseconds > channel.zeroPoint)
                {
                    val = (float)(microseconds - channel.zeroPoint) / (float)(channel.maxValue - channel.zeroPoint);
                }
                else
                {
                    val = -1 * ((float)(channel.zeroPoint - microseconds) / (float)(channel.zeroPoint - channel.minValue));
                }
            }

            if (channel.clamp)
                val = clamp(val, channel.negativeRange);

            return val;
        }

        #region "Calibration"

        /// <summary>
        /// Begin calibration mode. Min/Max values are recorded, zeroPoint is set to current state,
        /// and no update events will fire. All RC controls should be in neutral position.
        /// </summary>
        public void startCalibration()
        {
            isCalibrating = true;

            ChannelInfo c;
            for (int i = 0; i < channels.Length; i++)
            {
                c = channels[i];
                if (!(c == null))
                {
                    c.zeroPoint = c.pulseWidth;
                    c.maxValue = c.pulseWidth + 1;
                    c.minValue = c.pulseWidth - 1;
                }
            }
        }

        /// <summary>
        /// End calibration mode and resume event firing
        /// </summary>
        public void endCalibration()
        {
            isCalibrating = false;

            ChannelInfo c;
            for (int i = 0; i < channels.Length; i++)
            {
                c = channels[i];
                if (!(c == null))
                {
                    Debug.Print("obj.channels[" + c.pin.Id.ToString() + "].minValue = " + c.minValue.ToString() + ";");
                    Debug.Print("obj.channels[" + c.pin.Id.ToString() + "].maxValue = " + c.maxValue.ToString() + ";");
                    Debug.Print("obj.channels[" + c.pin.Id.ToString() + "].zeroPoint = " + c.zeroPoint.ToString() + ";");
                    Debug.Print("");
                }
            }
        }
        #endregion

        /// <summary>
        /// Clamps the supplied value to the specified range
        /// </summary>
        private float clamp(float val, bool negativeRange)
        {
            if (val > 1.0f) val = 1.0f;

            if (negativeRange)
            {
                if (val < -1.0f) val = -1.0f;
            }
            else
            {
                if (val < 0.0f) val = 0.0f;
            }

            return val;
        }

        /// <summary>
        /// Apparently there's no Abs() overload in the System.Math class
        /// for float values.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private float floatAbs(float x)
        {
            if (x < 0) return -1.0f * x;
            return x;
        }
    }
}
