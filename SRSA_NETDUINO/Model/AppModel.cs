using System;
using Microsoft.SPOT;
using System.IO.Ports;
using Gps;
using System.Text;
using Entity;
using System.Collections;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using System.Threading;
using Acelerometro;
using ServoClass;
using ModuloRcReader;
using Lights;
using model;

namespace Model
{
    public class AppModel
    {
        #region Variaveis Privadas
        private static SerialPort commPort;
        private InterfaceGps interfaceGps;
        private static Acelerometer acelerometro = new Acelerometer(Pins.GPIO_PIN_A5, Pins.GPIO_PIN_A4, 268, 778, 390, 905, 0, 0);
        private String[] commando = new String[0];
        private byte[] buffer = new byte[100];
        private byte[] mensagemRetorno = new byte[100];
        private UTF8Encoding encoding = new UTF8Encoding();

        static private PlaneInfo planeInfo = new PlaneInfo();
        private static Localizacao localizacaoHome = new Localizacao();

        private ArrayList wayPoints = new ArrayList();
        private Boolean lvlHoldModel = true;
        private Boolean wayPointMode = false;
        private WayPoint wayPointAtual = new WayPoint();
        private Double autopilotHdg = 0;

        private int maxRollAngleAutopilor = 45;
        private int maxPitchAngleAutopilot = 45;

        private static Servo servoAlerion;
        private static Servo servoElevetor;
        private static Servo servoPower;
        private int maxAlerionActivationAngle = 105;

        private ArrayList listaWayPoint = new ArrayList();

        private static ArrayList listaMensagem = new ArrayList();
        private static int threadSendMsgSleepTime = 150;

        private String GPRMC = "";
        private String GPGGA = "";
        private String GPGLL = "";

        static ModuloRcReader.RXReader rxReader = new ModuloRcReader.RXReader();

        private float valorAntigoCanal3 = 0;
        private float novoValorCanal3 = 0;

        private Lights.Lights farol = new Lights.Lights(Pins.GPIO_PIN_D11, 0);
        private Lights.Lights navLight = new Lights.Lights(Pins.GPIO_PIN_D12, 0);
        private Lights.Lights strobLight = new Lights.Lights(Pins.GPIO_PIN_D13, 500);

        ThreadStart ts;
        Thread threadLeituraDados;

        ThreadStart tsMsgSender;
        Thread threadMsgSender;

        ThreadStart tsControleAeronave;
        Thread threadControleAero;

        static int fatorCorrecao = 115;
        #endregion

        #region Metodos Publicos

        public AppModel(String PortaComm, String PortaGps)
        {
            Debug.Print("Iniciando ...........................");
            //PINOS 0,1,2,3 RESERVADOR PARA COM1/COM2

            //MAPEAMENTO DOS CANAIS:
            //1RX -> 4 NETDUINO
            //2RX -> 7 NETDUINO
            //3RX ->  NETDUINO
            //4RX ->  NETDUINO
            //rxReader.addChannel(Pins.GPIO_PIN_D1, 1);
            //rxReader.addChannel(Pins.GPIO_PIN_D2, 2);
            //rxReader.addChannel(Pins.GPIO_PIN_D3, 3);
            try
            {
                //rxReader.addChannel(Pins.GPIO_PIN_D4, 1); //ALERION
                //rxReader.addChannel(Pins.GPIO_PIN_D7, 2); //ELEVATOR
                //rxReader3.addChannel(Pins.GPIO_PIN_D7, 3); //ESC
                //rxReader.addChannel(Pins.GPIO_PIN_D8, 5); //DIGITAL (ON-OFF)
                //rxReader3.addChannel(Pins.GPIO_PIN_D9, 6); //CANAL 6
                //rxReader2.addChannel(Pins.GPIO_PIN_D4, 1); 
                //rxReader.channelUpdated += new RXReader.ChannelUpdated(reader_channelUpdated);
            }
            catch
            {

            }

            ts = new ThreadStart(ThreadLeituraDados);
            threadLeituraDados = new Thread(ts);
            threadLeituraDados.Start();

            tsMsgSender = new ThreadStart(ThreadSendMsg);
            threadMsgSender = new Thread(tsMsgSender);
            threadMsgSender.Start();

            tsControleAeronave = new ThreadStart(ThreadControleAero);
            threadControleAero = new Thread(tsControleAeronave);
            threadControleAero.Start();

            commPort = new SerialPort(PortaComm, 9600);
            commPort.ReadTimeout = 30;
            commPort.Open();

            //Four of the output ports (Digital pins 5, 6, 9 and 10) 
            servoAlerion = new Servo(Pins.GPIO_PIN_D5);
            servoElevetor = new Servo(Pins.GPIO_PIN_D6);
            servoPower = new Servo(Pins.GPIO_PIN_D10);

            servoAlerion.ServoAlerionCenterPos = 105;
            servoElevetor.ServoAlerionCenterPos = 105;
        }

        static void reader_channelUpdated(ChannelInfo channel, float oldValue, float newValue)
        {
            try
            {
                int anlge = (int)(((newValue * fatorCorrecao) * 180) / 100);
                if (channel.channelID == 1)
                {
                    if (planeInfo.Autopilot == false)
                    {
                        servoAlerion.SetAngle(anlge);
                    }
                }
                else if (channel.channelID == 2)
                {
                    if (planeInfo.Autopilot == false)
                    {
                         servoElevetor.SetAngle(anlge);
                    }
                }
                else if (channel.channelID == 3)
                {
                    if (planeInfo.Autopilot == false)
                    {
                        servoPower.SetAngle(anlge);
                    }
                }
                else if (channel.channelID == 5)
                {
                    if (newValue < 1)
                    {
                        planeInfo.Autopilot = false;
                    }
                    else
                    {
                        planeInfo.Autopilot = true;
                    }
                }
            }
            catch
            {

            }
        }

        public void IniciarPortaComunicacao(String porta)
        {
            commPort.Open();
        }

        public void DesligarPortaComunicacao()
        {
            commPort.Close();
        }

        public void DesligarGps()
        {
            interfaceGps.DesligarPorta();
        }

        private String TratarComandosRecebidos(String leitura)
        {
            try
            {
                String[] commando = new String[0];
                commando = leitura.Split('#');

                for (int i = 0; i < commando.Length; i++)
                {
                    if (commando[i].ToString().IndexOf("*") != -1)
                    {
                        return commando[i].ToString().Substring(2, commando[i].ToString().Length - 2);
                    }
                }
            }
            catch
            {
                return String.Empty;
            }
            return String.Empty;
        }

        public void Executar()
        {
            String input = "";
            String strBuffer = "";
            int bytesToRead;
            int idComando = 0;
            byte[] buffer;
            String leitura;

            planeInfo.Autopilot = false;
            threadSendMsgSleepTime = 150;

            strobLight.Strobe();
            while (true)
            {
                if (!commPort.IsOpen)
                {
                    commPort.Open();
                }

                Boolean executarComando = false;

                try
                {
                   

                    //ControleAeronave();
                    bytesToRead = commPort.BytesToRead;
                    if (bytesToRead > 0)
                    {
                        buffer = new byte[bytesToRead];
                        commPort.Read(buffer, 0, buffer.Length);
                        char[] chars = Encoding.UTF8.GetChars(buffer);
                        leitura = new String(chars);

                        strBuffer += leitura;

                        if (strBuffer.IndexOf("\n") != -1)
                        {
                            input = TratarComandosRecebidos(strBuffer);
                            strBuffer = "";
                        }

                        //input = TratarComandosRecebidos(leitura);

                        if (input != String.Empty && input.Length > 2)
                        {
                            //input = cryEngine.Decrypt(input);
                            string[] stmp = input.Split(';');
                            int id = int.Parse(stmp[0]);

                            if (id != idComando)
                            {
                                executarComando = true;
                            }
                            else
                            {
                                executarComando = false;
                            }
                        }
                        else
                        {
                            executarComando = false;
                        }

                        if (executarComando)
                        {
                            input = MetodosEstaticos.Replace(input, '#', ' ');
                            input = MetodosEstaticos.Replace(input, '*', ' ');
                            commando = input.Split(';');

                            idComando = int.Parse(commando[0].ToString());

                            if (commando[1].ToUpper().Trim() == "AUTOPILOT")
                            {
                                if (commando[2].Trim() == "ON")
                                {
                                    planeInfo.Autopilot = true;
                                    threadSendMsgSleepTime = 150;
                                }
                                else
                                {
                                    planeInfo.Autopilot = false;
                                    threadSendMsgSleepTime = 1000;
                                }
                            }
                            else if (commando[1].ToUpper().Trim() == "ADDWAYPOINT" && listaWayPoint.Count <= 10)
                            {
                                WayPoint wayPoint = new WayPoint(listaWayPoint.Count + 1, Double.Parse(commando[3].Trim()), Double.Parse(commando[4].Trim()), int.Parse(commando[5].Trim()));
                                listaWayPoint.Add(wayPoint);
                            }
                            else if (commando[1].ToUpper().Trim() == "ADDHOMEPOINT")
                            {
                                WayPoint wayPoint = new WayPoint(0, Double.Parse(commando[3].Trim()), Double.Parse(commando[4].Trim()), int.Parse(commando[5].Trim()));
                                localizacaoHome = wayPoint;
                                listaWayPoint.Add(wayPoint);
                            }
                            else if (commando[1].ToUpper().Trim() == "REMOVERWAYPOINT")
                            {
                                int id = int.Parse(commando[2].Trim());
                                WayPoint wayPointRemove = new WayPoint();
                                foreach (WayPoint wayPoint in listaWayPoint)
                                {
                                    if (wayPoint.Id == id)
                                    {
                                        wayPointRemove = wayPoint;
                                        break;
                                    }
                                }
                                listaWayPoint.Remove(wayPointRemove);
                            }
                            else if (commando[1].ToUpper().Trim() == "REMOVERWAYPOINTS")
                            {
                                listaWayPoint.Clear();
                            }
                            else if (commando[1].ToUpper().Trim() == "STATUS")
                            {
                                mensagemRetorno = encoding.GetBytes(planeInfo.getRawData());
                                commPort.Write(mensagemRetorno, 0, mensagemRetorno.Length);
                            }
                            else if (commando[1].ToUpper().Trim() == "WAYPOINTINFO")
                            {
                                Thread.Sleep(1500);
                                listaMensagem.Add(Util.GerarRawWayPoints(listaWayPoint));
                                Thread.Sleep(1500);
                            }
                            else if (commando[1].ToUpper().Trim() == "SETSERVOALERION")
                            {
                                int pos = int.Parse(commando[2].Trim());
                                servoAlerion.SetAngle(pos);
                            }
                            else if (commando[1].ToUpper().Trim() == "SETSERVOELEVATORPOS")
                            {
                                int pos = int.Parse(commando[2].Trim());
                                servoElevetor.SetAngle(pos);
                            }
                            else if (commando[1].ToUpper().Trim() == "SETSERVOPOWERPOS")
                            {
                                int pos = int.Parse(commando[2].Trim());
                                servoPower.SetAngle(pos);
                            }
                            else if (commando[1].ToUpper().Trim() == "SETSTROBE")
                            {
                                if (commando[2].Trim() == "ON")
                                {
                                    strobLight.Strobe();
                                }
                                else if (commando[2].Trim() == "OFF")
                                {
                                    strobLight.Off();
                                }
                            }
                            else if (commando[1].ToUpper().Trim() == "SETNAVLIGHT")
                            {
                                if (commando[2].Trim() == "ON")
                                {
                                    navLight.On();
                                }
                                else if (commando[2].Trim() == "OFF")
                                {
                                    navLight.Off();
                                }
                            }
                            else if (commando[1].ToUpper().Trim() == "SETFAROL")
                            {
                                if (commando[2].Trim() == "ON")
                                {
                                    farol.On();
                                }
                                else if (commando[2].Trim() == "OFF")
                                {
                                    farol.Off();
                                }
                            }
                            else
                            {
                                listaMensagem.Add("Comando não reconhecido !:" + input);
                            }

                            input = "";
                        }

                    }

                    //Send Status                   
                    listaMensagem.Add(planeInfo.getRawData());
                }
                catch (Exception ex)
                {
                    Debug.Print("Erro causado pela input: " + input);
                }
                finally
                {
                    
                    Thread.Sleep(50);
                }
            }
        }


        public Double AutopilotHdg
        {
            get { return autopilotHdg; }
            set { autopilotHdg = value; }
        }

        public int MaxAlerionActivationAngle
        {
            get { return maxAlerionActivationAngle; }
            set { maxAlerionActivationAngle = value; }
        }

        #endregion

        #region MetodosPrivados

        private void ControleAeronave()
        {
            //CapturarVetorAtual();
            if (planeInfo.Autopilot)
            {
                //SetPower(130);
                //Caso o avião esteja desestabilizado (pitch angle maior que o angulo de segurança)
                //Este algoritmo o estabiliza
                if (System.Math.Abs(planeInfo.PitchAngle) > maxPitchAngleAutopilot)
                {
                    if (planeInfo.PitchAngle > 0)
                    {
                        Pitch(planeInfo.PitchAngle - maxPitchAngleAutopilot);
                    }
                    else
                    {
                        Pitch(planeInfo.PitchAngle + maxPitchAngleAutopilot);
                    }
                }
                else if (System.Math.Abs(planeInfo.RollAngle) > maxRollAngleAutopilor)
                {
                    if (planeInfo.RollAngle > 0)
                    {
                        Roll(planeInfo.RollAngle - maxRollAngleAutopilor);
                    }
                    else
                    {
                        Roll(planeInfo.RollAngle + maxRollAngleAutopilor);
                    }
                }
                else if (lvlHoldModel)
                {
                    EstabilizacaoVertical();
                    EstabilizacaoHorizontal();
                }
                else if (wayPointMode)
                {
                    CalcularProximoWayPoint();
                    autopilotHdg = CalcularDirecaoWayPoint(wayPointAtual);
                    if (planeInfo.Hdg >= autopilotHdg)
                    {
                        Double d = planeInfo.Hdg - autopilotHdg;
                        if (d <= 5)
                        {
                            Roll(0);
                        }
                        else if (d >= 180)
                        {
                            Roll(90 + maxAlerionActivationAngle);
                        }
                        else
                        {
                            Roll(90 - maxAlerionActivationAngle);
                        }
                    }
                    else
                    {
                        Double d = autopilotHdg - planeInfo.Hdg;
                        if (d <= 5)
                        {
                            Roll(0);
                        }
                        else if (d >= 180)
                        {
                            Roll(90 + maxAlerionActivationAngle);
                        }
                        else
                        {
                            Roll(90 - maxAlerionActivationAngle);
                        }
                    }
                }
            }
        }

        private void EstabilizacaoVertical()
        {
            if (System.Math.Abs(planeInfo.RollAngle) > 0)
            {
                Roll(planeInfo.RollAngle);
            }
            Thread.Sleep(5);
        }

        private void EstabilizacaoHorizontal()
        {
            if (System.Math.Abs(planeInfo.PitchAngle) > 0)
            {
                Pitch(planeInfo.PitchAngle);

            }
            Thread.Sleep(5);
        }

        private void Roll(int angle)
        {
            servoAlerion.SetAngle(System.Math.Abs(servoAlerion.ServoAlerionCenterPos - angle));
        }

        private void Pitch(int angle)
        {
            servoElevetor.SetAngle(System.Math.Abs(servoElevetor.ServoAlerionCenterPos - angle));
        }

        private void SetPower(int valor)
        {
            servoPower.SetAngle(servoPower.ServoAlerionCenterPos + valor);
        }

        private void CapturarVetorAtual()
        {
            /*$GPRMC
            eg3. $GPRMC,220516,A,5133.82,N,00042.24,W,173.8,231.8,130694,004.2,W*70
                     0    1    2    3    4     5    6    7      8     9  10   11

              1   220516     Time Stamp
              2   A          validity - A-ok, V-invalid
              3   5133.82    current Latitude
              4   N          North/South
              5   00042.24   current Longitude
              6   W          East/West
              7   173.8      Speed in knots
              8   231.8      True course
              9   130694     Date Stamp
              10  004.2      Variation
              11  W          East/West
              12  *70        checksum
             * */
            planeInfo.DataHoraMedicao = DateTime.Now;

            String temp = interfaceGps.RetornarPosicaoAtual("$GPRMC");
            if (temp != String.Empty)
            {
                GPRMC = temp;
            }

            temp = interfaceGps.RetornarPosicaoAtual("$GPGGA");
            if (temp != String.Empty)
            {
                GPGGA = temp;
            }

            if (GPRMC != String.Empty && GPGGA != String.Empty)
            {
                String[] dataGPRMC = GPRMC.Split(',');
                String[] dataGPGGA = GPGGA.Split(',');

                planeInfo.LocalizacaoAtual.Latitude = Double.Parse(dataGPRMC[3]);
                planeInfo.LocalizacaoAtual.Longitude = Double.Parse(dataGPRMC[5]);

                if (dataGPRMC[4] == "S")
                {
                    planeInfo.LocalizacaoAtual.Latitude = planeInfo.LocalizacaoAtual.Latitude * -1;
                }

                //planeInfo.LocalizacaoAtual.LatitudeAbsoluta = planeInfo.LocalizacaoAtual.Latitude + 9000.0000;

                if (dataGPRMC[6] == "W")
                {
                    planeInfo.LocalizacaoAtual.Longitude = planeInfo.LocalizacaoAtual.Longitude * -1;
                }
                planeInfo.AltitudeMetros = Double.Parse(dataGPGGA[9]);
                //planeInfo.LocalizacaoAtual.LatitudeAbsoluta = planeInfo.LocalizacaoAtual.Longitude + 18000.0000;

                planeInfo.Velocidadeknots = Double.Parse(dataGPRMC[7]);
                planeInfo.Hdg = Double.Parse(dataGPRMC[8]);
            }
        }

        private void CapturarDadosAcelerometro()
        {
            planeInfo.PitchAngle = acelerometro.ObterLeituraEixoX();
            planeInfo.RollAngle = acelerometro.ObterLeituraEixoY();
        }

        private void CapturarDadosMedidorCombustivel()
        {


        }

        private void CapturarDadosMedidorBateria()
        {

        }

        private void ThreadLeituraDados()
        {
            interfaceGps = new InterfaceGps("COM2");
            while (true)
            {
                CapturarVetorAtual();
                CapturarDadosAcelerometro();
                Thread.Sleep(100);
            }
        }

        private void ThreadSendMsg()
        {
            while (true)
            {
                if (listaMensagem.Count > 0)
                {
                    byte[] mensagemRetorno = encoding.GetBytes((String)listaMensagem[0]);
                    commPort.Write(mensagemRetorno, 0, mensagemRetorno.Length);
                    listaMensagem.RemoveAt(0);
                }
                Thread.Sleep(threadSendMsgSleepTime);
            }
        }


        private void ThreadControleAero()
        {
            while (true)
            {
                try
                {
                    ControleAeronave();
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private Double CalcularDirecaoWayPoint(WayPoint way)
        {
            //TODO: IMPLEMENTAR
            int latAtual = planeInfo.LocalizacaoAtual.LatitudeE6;
            int longAtual = planeInfo.LocalizacaoAtual.LongitudeE6;

            int latDestino = way.LatitudeE6;
            int longDestino = way.LongitudeE6;

            Double cateto1 = latAtual - latDestino;
            Double cateto2 = longAtual - longDestino;
            Double hip = exMath.Sqrt(exMath.Pow(cateto1, 2) + exMath.Pow(cateto2, 2));

            Double sin = exMath.Sin(hip);
            Double angle = exMath.Asin(hip);

            return angle;
        }

        //Calulo em Metros
        private Double CalcularDistanciaWayPoint(WayPoint way)
        {
            //TODO: IMPLEMENTAR
            int latAtual = planeInfo.LocalizacaoAtual.LatitudeE6;
            int longAtual = planeInfo.LocalizacaoAtual.LongitudeE6;

            int latDestino = way.LatitudeE6;
            int longDestino = way.LongitudeE6;

            Double cateto1 = latAtual - latDestino;
            Double cateto2 = longAtual - longDestino;
            Double hip = exMath.Sqrt(exMath.Pow(cateto1, 2) + exMath.Pow(cateto2, 2));

            //Double sin = exMath.Sin(hip);
            //Double angle = exMath.Asin(hip);
            //Retorna distancia em metros
            return hip * 100000;
        }

        private void CalcularProximoWayPoint()
        {
            double distWayPoint = CalcularDistanciaWayPoint(wayPointAtual);
            if (distWayPoint <= 10)
            {
                int index = listaWayPoint.IndexOf(wayPointAtual);
                if (index >= listaWayPoint.Count)
                {
                    wayPointAtual = (WayPoint)listaWayPoint[0];
                }
                else
                {
                    wayPointAtual = (WayPoint)listaWayPoint[index + 1];
                }
            }
        }


        #endregion
    }
}
