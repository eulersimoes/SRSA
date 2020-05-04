/*****************************************************************************/
/* Project  : AvionicsInstrumentControlDemo                                  */
/* File     : AttitudeIndicatorInstrumentControl.cs                          */
/* Version  : 1                                                              */
/* Language : C#                                                             */
/* Summary  : The attitude indicator instrument control                      */
/* Creation : 22/06/2008                                                     */
/* Autor    : Guillaume CHOUTEAU                                             */
/* History  :                                                                */
/*****************************************************************************/

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Data;

namespace AvionicsInstrumentControlDemo
{
    class AttitudeIndicatorInstrumentControl : InstrumentControl
    {
        #region Fields

        // Parameters
        private double pitchAngle = 0; // Phi
        private double rollAngle = 0; // Theta

        // Parameters
        private double lastPitchAngle = 0; // Phi
        private double lastRollAngle = 0; // Theta


        // Images
        Bitmap bmpCadran = new Bitmap(COCKPIT.AvionicsInstrumentsControls.AvionicsInstrumentsControlsRessources.Horizon_Background);
        Bitmap bmpBoule = new Bitmap(COCKPIT.AvionicsInstrumentsControls.AvionicsInstrumentsControlsRessources.Horizon_GroundSky);
        Bitmap bmpAvion = new Bitmap(COCKPIT.AvionicsInstrumentsControls.AvionicsInstrumentsControlsRessources.Maquette_Avion);

        #endregion

        #region Contructor

        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public AttitudeIndicatorInstrumentControl()
		{
			// Double bufferisation
			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint, true);
        }

        #endregion

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        #region Paint

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Calling the base class OnPaint
            base.OnPaint(pe);

            // Pre Display computings

            Point ptBoule = new Point(25, - 210);
            Point ptRotation = new Point(150, 150);

            float scale = (float)this.Width / bmpCadran.Width;

            // Affichages - - - - - - - - - - - - - - - - - - - - - - 

            bmpCadran.MakeTransparent(Color.Yellow);
            bmpAvion.MakeTransparent(Color.Yellow);

            // display Horizon
            RotateAndTranslate(pe, bmpBoule, rollAngle, 0, ptBoule, (int)(4*pitchAngle), ptRotation, scale);

            // diplay mask
            Pen maskPen = new Pen(this.BackColor,30*scale);
            pe.Graphics.DrawRectangle(maskPen, 0, 0, bmpCadran.Width * scale, bmpCadran.Height * scale);

            // display cadran
            pe.Graphics.DrawImage(bmpCadran, 0, 0, (float)(bmpCadran.Width * scale), (float)(bmpCadran.Height * scale));

            // display aircraft symbol
            pe.Graphics.DrawImage(bmpAvion, (float)((0.5 * bmpCadran.Width - 0.5 * bmpAvion.Width) * scale), (float)((0.5 * bmpCadran.Height - 0.5 * bmpAvion.Height) * scale), (float)(bmpAvion.Width * scale), (float)(bmpAvion.Height * scale));


        }

        #endregion

        #region Methods

        /// <summary>
        /// Define the physical value to be displayed on the indicator
        /// </summary>
        /// <param name="aircraftPitchAngle">The aircraft pitch angle in °deg</param>
        /// <param name="aircraftRollAngle">The aircraft roll angle in °deg</param
        public void SetAttitudeIndicatorParameters(double aircraftPitchAngle, double aircraftRollAngle)
        {
            pitchAngle = aircraftPitchAngle;
            rollAngle = aircraftRollAngle * Math.PI / 180;

            this.Refresh();
        }

        public double PitchAngle
        {
            get { return pitchAngle; }
            set { //pitchAngle = value;
            //this.Refresh();
                PitchAngleAtualizator(value);
            }
        }

        public double RollAngle
        {
            get { return rollAngle; }
            set {
                //RollAngleAtualizator(value * Math.PI / 180);
                rollAngle = value * Math.PI / 180;
                this.Refresh();
            }
        }

        private void PitchAngleAtualizator(double value)
        {
            if (value == 0)
            {
                return;
            }

            if (value < lastPitchAngle)
            {
                int auxAngle = (int)lastPitchAngle;
                while (auxAngle != value)
                {
                    pitchAngle = auxAngle;
                    this.Refresh();
                    auxAngle--;
                }
            }
            else
            {
                int auxAngle = (int)lastPitchAngle;
                while (auxAngle != value)
                {
                    pitchAngle = auxAngle;
                    this.Refresh();
                    auxAngle++;
                }
            }

            lastPitchAngle = pitchAngle;
        }

        private void RollAngleAtualizator(double value)
        {
            value = value * 10;
            if (value == 0)
            {
                return;
            }

            if (value < lastRollAngle)
            {
                double auxAngle = lastRollAngle;
                while (auxAngle >= value)
                {
                    rollAngle = auxAngle;
                    this.Refresh();
                    auxAngle--;
                }
            }
            else
            {
                double auxAngle = lastRollAngle;
                while (auxAngle <= value)
                {
                    rollAngle = auxAngle;
                    this.Refresh();
                    auxAngle++;
                }
            }

            lastRollAngle = rollAngle;
        }

        #endregion
    }
}
