using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace Acelerometro
{
    public class Acelerometer
    {
        private int minX = 0;
        private int maxX = 0;
        private int minY = 0;
        private int maxY = 0;
        private int minZ = 0;
        private int maxZ = 0;
        private AnalogInput eixoX;
        private AnalogInput eixoY;
        //private AnalogInput eixoZ;
        /// <summary>
        /// Used internally to map a value of one scale to another
        /// </summary>
        /// <param name="x"></param>
        /// <param name="in_min"></param>
        /// <param name="in_max"></param>
        /// <param name="out_min"></param>
        /// <param name="out_max"></param>
        /// <returns></returns>
        private int map(int x, int in_min, int in_max, int out_min, int out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        public Acelerometer(Microsoft.SPOT.Hardware.Cpu.Pin pinX, Microsoft.SPOT.Hardware.Cpu.Pin pinY, int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
        {
            eixoX = new AnalogInput(pinX);
            eixoY = new AnalogInput(pinY);
            //eixoZ = new AnalogInput(pinZ);
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
            this.minZ = minZ;
            this.maxZ = maxZ;
        }

        public int ObterLeituraEixoX()
        {
            bool leitura = false;
            int valorAnterior = 0;
            int count = 0;
            while (!leitura && count < 5)
            {
                if (valorAnterior == map(eixoX.Read(), minX, maxX, -90, 90))
                {
                    break;
                }
                else
                {
                    valorAnterior = map(eixoX.Read(), minX, maxX, -90, 90);
                }
                count++;
            }

            return map(eixoX.Read(), minX, maxX, -90, 90);
        }

        public int ObterLeituraEixoY()
        {
            bool leitura = false;
            int valorAnterior = 0;
            int count = 0;
            while (!leitura && count < 5)
            {
                if (valorAnterior == map(eixoY.Read(), minY, maxY, -90, 90))
                {
                    break;
                }
                else
                {
                    valorAnterior = map(eixoY.Read(), minY, maxY, -90, 90);
                }
                count++;
            }
            return map(eixoY.Read(), minY, maxY, -90, 90);
        }
    }
}
