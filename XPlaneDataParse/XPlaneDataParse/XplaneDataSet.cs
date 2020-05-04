using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPlaneDataParse
{
    public class XplaneDataSet
    {
        private String indexDataSet;
        private String rawValue;

        public String RawValue
        {
            get { return this.rawValue; }
            set { this.rawValue = value; }
        }

        public String IndexDataSet
        {
            get { return indexDataSet; }
            set { indexDataSet = value; }
        }

        public void Read(byte[] data)
        {
            int dataCount=5;
            int interRegcounter = 0;
            int internalCounter = 0;
            rawValue = "#";
            Boolean adquirirValor = false;
            byte[] auxByte = new byte[32];
            int aux = 0;
            internalCounter = 36;

            rawValue += "18|";
            //Pitch
            byte[] teste = new byte[4];
            teste[0] = data[9];
            teste[1] = data[10];
            teste[2] = data[11];
            teste[3] = data[12];
            rawValue += BitConverter.ToSingle(teste, 0).ToString() + "|";

            teste[0] = data[13];
            teste[1] = data[14];
            teste[2] = data[15];
            teste[3] = data[16];
            rawValue += BitConverter.ToSingle(teste, 0).ToString() + "|";
            //Heading
            teste[0] = data[17];
            teste[1] = data[18];
            teste[2] = data[19];
            teste[3] = data[20];
            rawValue += BitConverter.ToSingle(teste, 0).ToString() + "|;";


            //Alt
            teste = new byte[8];
            teste[0] = data[117];
            teste[1] = data[118];
            teste[2] = data[119];
            teste[3] = data[120];
            teste[4] = data[121];
            teste[5] = data[122];
            teste[6] = data[123];
            teste[7] = data[124];
            rawValue += "24|";
            rawValue += BitConverter.ToSingle(teste, 0).ToString() + "|";
            rawValue += "#";
            /*
            for (int i = 5; i < data.Length; i++)
            {
                if (dataCount >= data.Length)
                {
                    rawValue += BitConverter.ToSingle(auxByte, 0).ToString() + ";";
                    break;
                }


                //if (data[dataCount] != 0 && data[dataCount + 1] == 0 && data[dataCount + 2] == 0 && data[dataCount + 3] == 0)
                if(internalCounter == 36)
                {
                    internalCounter = 0;
                    if (dataCount > 5)
                    {
                        rawValue += BitConverter.ToSingle(auxByte, 0).ToString() + "|";
                        auxByte = new byte[32];
                        aux = 0;
                    }
                    rawValue += ";" + data[dataCount].ToString() + "|";
                    
                    dataCount += 4;
                    internalCounter += 4;
                    
                    if (i > 5)
                    {
                        //adquirirValor = true;
                    }
                }


                if (!adquirirValor)
                {
                    auxByte[aux] = data[dataCount];
                    aux++;
                }

                if (dataCount + 3 < data.Length && data[dataCount] == 0 && data[dataCount + 1] == 192 && data[dataCount + 2] == 121 && data[dataCount + 3] == 196)
                {
                  
                        rawValue += BitConverter.ToSingle(auxByte, 0).ToString() + "|";
                        aux = 0;
                        auxByte = new byte[32];
                        dataCount += 4;
                        internalCounter += 4;
                    }
                else if(!adquirirValor)
                {
                    dataCount++;
                    internalCounter++;
                }

            }
             
            rawValue += "#"; * */
        }
             
    }
}
