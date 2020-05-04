using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using System.Threading;

namespace Lights
{
    public class Lights
    {
        OutputPort led;
        private int blinkTime = 0;
        private bool isStrob;
        private bool isOn;

        ThreadStart ts;
        Thread threadStrob;

        public Lights(Cpu.Pin port, int blinkTm)
        {

            this.led = new OutputPort(port, false);
            blinkTime = blinkTm;
        }

        public void On()
        {
            isOn = true;

            led.Write(isOn);
        }

        public void Strobe()
        {
            isOn = true;
            ts = new ThreadStart(ThreadStrobWork);
            threadStrob = new Thread(ts);
            threadStrob.Start();
        }

        public void Off()
        {
            isOn = false;
            led.Write(isOn);
            if (threadStrob != null && threadStrob.IsAlive)
            {
                //threadStrob.Suspend();
                threadStrob.Abort();
            }
        }

        private void ThreadStrobWork()
        {
            while (isOn)
            {
                led.Write(true);
                Thread.Sleep(50);
                led.Write(false);

                Thread.Sleep(500);

                led.Write(true);
                Thread.Sleep(50);
                led.Write(false);

                Thread.Sleep(100);

                led.Write(true);
                Thread.Sleep(50);
                led.Write(false);

                Thread.Sleep(blinkTime);
            }
        }

    }
}
