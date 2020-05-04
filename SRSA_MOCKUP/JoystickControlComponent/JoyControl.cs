using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JoystickControlComponent
{
    public partial class JoyControl : UserControl
    {
        #region Declaracao de Váriaveis

        private int posXstickRight = 0;
        private int posYStickRight = 0;
        private int posXstickLeft = 0;
        private int posYStickLeft = 0;

        private int minPosServoCrossX = 0;
        private int maxPosServoCrossX = 180;

        private int minPosServoCrossY = 0;
        private int maxPosServoCrossY = 180;

        private int minPosServoPowerX = 0;
        private int maxPosServoPowerX = 180;

        private int minPosServoPowerY = 0;
        private int maxPosServoPowerY = 180;

        public event EventHandler Changed;

        Vector2 lastSitckRight = new Vector2();
        #endregion

        #region Metodos Publicos

        System.Threading.Thread threadInfo;
        System.Threading.ThreadStart tsInfo;


        public int MinPosServoPowerY
        {
            get { return minPosServoPowerY; }
            set { minPosServoPowerY = value; }
        }

        public int MaxPosServoPowerY
        {
            get { return maxPosServoPowerY; }
            set { maxPosServoPowerY = value; }
        }

        public int MinPosServoPowerX
        {
            get { return minPosServoPowerX; }
            set { minPosServoPowerX = value; }
        }

        public int MaxPosServoPowerX
        {
            get { return maxPosServoPowerX; }
            set { maxPosServoPowerX = value; }
        }

        public int MinPosServoCrossY
        {
            get { return minPosServoCrossY; }
            set { minPosServoCrossY = value; }
        }

        public int MaxPosServoCrossY
        {
            get { return maxPosServoCrossY; }
            set { maxPosServoCrossY = value; }
        }

        public int MinPosServoCrossX
        {
            get { return minPosServoCrossX; }
            set { minPosServoCrossX = value; }
        }

        public int MaxPosServoCrossX
        {
            get { return maxPosServoCrossX; }
            set { maxPosServoCrossX = value; }
        }

        public int PosXstickRight
        {
            get { return posXstickRight; }
            set { posXstickRight = value; }
        }

        public int PosYStickRight
        {
            get { return posYStickRight; }
            set { posYStickRight = value; }
        }

        public int PosXstickLeft
        {
            get { return posXstickLeft; }
            set { posXstickLeft = value; }
        }

        public int PosYStickLeft
        {
            get { return posYStickLeft; }
            set { posYStickLeft = value; }
        }

        public JoyControl()
        {
            InitializeComponent();
            System.Drawing.Point pointLocationLbl = new System.Drawing.Point();
            pointLocationLbl.X = this.Width / 2;
            pointLocationLbl.Y = this.Height / 2;
            lblCross.Location = pointLocationLbl;

        }

        public void Start()
        {
            tsInfo = new System.Threading.ThreadStart(Execucao);
            threadInfo = new System.Threading.Thread(tsInfo);
            threadInfo.Start();
        }

        public void Stop()
        {
            threadInfo.Suspend();
        }

        #endregion

        #region Metodos Privados

        private void UpdateComponente()
        {
            this.lblCross.Refresh();
        }

        private void Executar()
        {
            System.Drawing.Point pointCross = new System.Drawing.Point();
            Vector2 sitckRight = new Vector2();
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

                sitckRight = currentState.ThumbSticks.Right;
                try
                {
                    float x = sitckRight.X * 100;
                    float y = sitckRight.Y * 100 * -1;
                    pointCross = TransformValue(x, y);

                    int sX = (int)((sitckRight.X * 100) + 100);
                    int sY = (int)((sitckRight.X * 100) + 100);

                    System.Drawing.Point pServoPosCross = TransforValueServo(this.minPosServoCrossX, this.maxPosServoCrossX, sX, sY);

                    Double posEixoX = Double.Parse(sX.ToString()) * (Double.Parse(MaxPosServoCrossX.ToString()) / 200);
                    Double posEixoY = Double.Parse(sY.ToString()) * (Double.Parse(MaxPosServoCrossY.ToString()) / 200);

                    this.posXstickRight = (int)posEixoX;
                    this.posYStickRight = (int)posEixoY;
                    if (lastSitckRight != currentState.ThumbSticks.Right)
                    {
                        lastSitckRight = currentState.ThumbSticks.Right;
                        this.Changed(this, new EventArgs());
                    }
                }
                catch (Exception ex)
                {

                }

            lblCross.Location = pointCross;
            lblCross.Refresh();
        }

        private System.Drawing.Point TransformValue(float x, float y)
        {
            System.Drawing.Point p = new System.Drawing.Point();
            float mx = this.Width;
            float my = this.Height;

            x = x + 100;
            y = y + 100;

            float fatorCorrecaoX = (mx * 0.9f) / 200f;
            float fatorCorrecaoY = (my * 0.9f) / 200f;


            float posX = (int)x * fatorCorrecaoX;
            float posY = (int)y * fatorCorrecaoY;

            p.X = (int)posX;
            p.Y = (int)posY;

            return p;
        }

        private System.Drawing.Point TransforValueServo(double minPosServo, double maxPosServo, double posX, double posY)
        {
            double FatorCorrecao = ((maxPosServo - minPosServo) / 200);
            System.Drawing.Point pReturn = new System.Drawing.Point();

            pReturn.X = (int)(posX * FatorCorrecao);
            pReturn.Y = (int)(posY * FatorCorrecao);

            return pReturn;

        }
        private void Execucao()
        {
            try
            {
                while (true)
                {
                    if (!this.IsDisposed && this.IsHandleCreated)
                    {

                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Executar();
                        });
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void JoyControl_Load(object sender, EventArgs e)
        {
            //Start();
        }
        #endregion

    }
}
