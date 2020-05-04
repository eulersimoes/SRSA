using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using System.Threading;
using System;

namespace ServoClass
{
    public class Servo
    {
        public PWM servo;
        int servoAlerionCenterPos = 90;

        public int ServoAlerionCenterPos
        {
            get { return servoAlerionCenterPos; }
            set { servoAlerionCenterPos = value; }
        }

        public Servo(Cpu.Pin pin)
        {
            try
            {
                servo = new PWM(pin);
                servo.SetDutyCycle(dutyCycle: 0);
            }
            catch
            {

            }
        }

        private uint _angle = 0;

        private uint Angle
        {
            get
            {
                return _angle;
            }

            set
            {
                const uint degreeMin = 0;
                const uint degreeMax = 180;
                const uint durationMin = 500;  // 480  
                const uint durationMax = 2300;  // 2450  
                _angle = value;
                if (_angle < degreeMin) _angle = degreeMin;
                if (_angle > degreeMax) _angle = degreeMax;
                uint dur = (_angle - degreeMin) * (durationMax - durationMin) / (degreeMax - degreeMin) + durationMin;
                servo.SetPulse(period: 20000, duration: dur);
            }
        }

        public void SetAngle(int valor)
        {
            try
            {
                if (valor <= 180 && valor >= 0)
                {
                    Angle = uint.Parse(valor.ToString());
                }
                else if (valor > 180)
                {
                    Angle = uint.Parse("180");
                }
                else if (valor < 0)
                {
                    Angle = uint.Parse("0");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}