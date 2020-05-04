using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using GEPlugin;
using OSD_LIB;
using Entity;

namespace COCKPIT
{
    [ComVisibleAttribute(true)]
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public partial class FormFpv : Form
    {
        private const string PLUGIN_URL =
      @"http://earth-api-samples.googlecode.com/svn/trunk/demos/desktop-embedded/pluginhost.html";

        private IGEPlugin m_ge = null;


        OSD osdSpeedDisplay = new OSD();
        OSD osdAltDisplay = new OSD();
        OSD osdHdgDisplay = new OSD();
        OSD osdBatDisplay = new OSD();
        OSD osdFuelDisplay = new OSD();
        OSD osdLatLongDisplay = new OSD();
        OSD osdAutoPilotDisplay = new OSD();
        OSD osdThrottleDisplay = new OSD();

        static PlaneInfo planeInfo = new PlaneInfo();

        public FormFpv()
        {
            InitializeComponent();

            DrawOsdInfo(planeInfo);

        }

        private void DrawOsdInfo(PlaneInfo planeInfo)
        {
            Font fontDisplay = new Font("Lucida Console", 9);
            Point fpvPos = FindLocation(fpv);
            Color corFuel = new Color();
            Color corBat = new Color();
            Color corAp = new Color();
            osdSpeedDisplay.Draw(fpvPos.X + 10, fpvPos.Y + 200, 255, Color.Green, 10, 1, "Vel. (km/h): " + planeInfo.Velocidadekmh.ToString(), fontDisplay);
            osdAltDisplay.Draw(fpvPos.X + 10, fpvPos.Y + 220, 255, Color.Green, 10, 1, "Alt. (m): " + planeInfo.AltitudeMetros.ToString(), fontDisplay);
            osdHdgDisplay.Draw(fpvPos.X + (fpv.Size.Width / 2), fpvPos.Y + 40, 255, Color.Green, 10, 1, "Hdg: " + planeInfo.Hdg.ToString(), fontDisplay);
            osdThrottleDisplay.Draw(fpvPos.X + 10, fpvPos.Y + fpv.Height - 25, 255, Color.Green, 10, 1, "Throttle (%): " + planeInfo.Throttle.ToString(), fontDisplay);
            if (planeInfo.PercentualCombustivel >= 50)
            {
                corFuel = Color.Green;
            }
            else if (planeInfo.PercentualCombustivel >= 20)
            {
                corFuel = Color.Yellow;
            }
            else
            {
                corFuel = Color.Red;
            }

            if (planeInfo.PercentualBateria >= 1150)
            {
                corBat = Color.Green;
            }
            else if (planeInfo.PercentualBateria >= 1050)
            {
                corBat = Color.Yellow;
            }
            else
            {
                corBat = Color.Red;
            }

            if (planeInfo.AutoPilot)
            {
                corAp = Color.Green;
            }
            else
            {
                corAp = Color.Red;
            }
            osdAutoPilotDisplay.Draw(fpvPos.X + (fpv.Size.Width / 2), fpvPos.Y + 100, 255, corAp, 10, 1, "Auto Pilot: " + planeInfo.StatusAutoPilot(), fontDisplay);
            //osdFuelDisplay.Draw(fpvPos.X + (fpv.Size.Width / 2), fpvPos.Y + fpv.Size.Height, 255, corFuel, 10, 1, "Combustível (%): " + planeInfo.PercentualCombustivel.ToString(), fontDisplay);
            osdBatDisplay.Draw(fpvPos.X + (fpv.Size.Width / 2), fpvPos.Y + (fpv.Size.Height - 20), 255, corBat, 10, 1, "Bateria (V): " + planeInfo.PercentualBateria.ToString(), fontDisplay);
        }

        private static Point FindLocation(Control ctrl)
        {
            Point p;
            for (p = ctrl.Location; ctrl.Parent != null; ctrl = ctrl.Parent)
                p.Offset(ctrl.Parent.Location);
            return p;
        }

        private void ShowMap()
        {
            try
            {
                int w = this.groupBox1.Width * 35 / 100;
                int h = this.groupBox1.Height * 35 / 100;

                // webBrowser.Width = w;
                // webBrowser.Height = h;
                webBrowser.Refresh();
                webBrowser.Navigate(PLUGIN_URL);
                webBrowser.ObjectForScripting = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao carregar o mapa:" + ex.ToString());
            }

        }

        private void FormFpv_Load(object sender, EventArgs e)
        {
            ShowMap();
        }

        private void btnStartStopRecording_Click(object sender, EventArgs e)
        {
            DrawOsdInfo(planeInfo);
        }

    }
}
