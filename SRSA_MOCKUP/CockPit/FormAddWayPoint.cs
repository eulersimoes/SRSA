using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Entity;
using GEPlugin;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using SHAREDCLASSES;

namespace COCKPIT
{
    // Apply a demand for full trust in order
    // to limit access by partially trusted code.
    [ComVisibleAttribute(true)]
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public partial class FormAddWayPoint : Form
    {
        private Double latitude;
        private Double longitude;
        private int altitude;
        private IList<WayPoint> listaWayPoint = new List<WayPoint>();

        public IList<WayPoint> ListaWayPoint
        {
            get { return listaWayPoint; }
            set { listaWayPoint = value; }
        }
        private const string PLUGIN_URL = @"http://localhost:7354/WebContent/point/point.html";

        public Double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public Double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public int Altitude
        {
            get { return altitude; }
            set { altitude = value; }
        }

        public FormAddWayPoint()
        {
            InitializeComponent();
            webBrowser.Navigate(PLUGIN_URL);
            webBrowser.Refresh();
            webBrowser.ObjectForScripting = this;
            //this.m_ge = m_ge;
        }


        private void btIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                String rawWpData = Util.TratarRetornoHtmlWayPoint(webBrowser.Document.GetElementById("hListWp").OuterHtml);
                String[] arrayCoordenada = rawWpData.Split(';');
                for (int i = 0; i < arrayCoordenada.Length -1; i++)
                {
                    String[] latLong = arrayCoordenada[i].Split(',');
                    WayPoint wp = new WayPoint();
                    wp.Latitude = double.Parse(latLong[0]);
                    wp.Latitude = Math.Round(wp.Latitude, 3);
                    wp.Longitude = double.Parse(latLong[1]);
                    wp.Longitude = Math.Round(wp.Longitude, 3);
                    wp.Altitude = double.Parse(txtAltitude.Text);
                    listaWayPoint.Add(wp);
                }
                //novoWayPoint.Latitude = Double.Parse(txtLat.Text);
                //novoWayPoint.Longitude = Double.Parse(txtLong.Text);
                //novoWayPoint.Altitude = Double.Parse(txtAlt.Text);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                listaWayPoint.Clear();
            }

        }

        private void FormAddWayPoint_Load(object sender, EventArgs e)
        {
            //webBrowser.ObjectForScripting = this;
        }

        private void createPlacemarkBtn_Click(object sender, EventArgs e)
        {
            webBrowser.Document.InvokeScript("JSCreatePlacemarkAtCameraCenter",
                new object[] { "Teste" });

            // NOTE: because on Windows, the Google Earth Plug-in exposes COM interfaces,
            // we could've also done this directly in C#:
            /*
            if (m_ge != null)
            {
                KmlLookAtCoClass lookAt = m_ge.getView().copyAsLookAt(m_ge.ALTITUDE_RELATIVE_TO_GROUND);

                // create a point
                KmlPointCoClass point = m_ge.createPoint("");
                point.setLatitude(lookAt.getLatitude());
                point.setLongitude(lookAt.getLongitude());

                // create a placemark
                KmlPlacemarkCoClass placemark = m_ge.createPlacemark("");
                placemark.setName(placemarkNameTxt.Text);
                placemark.setDescription("This was created from .NET");
                placemark.setGeometry(point);

                // add the placemark to the plugin
                m_ge.getFeatures().appendChild(placemark);
            }
            */
        }
    }
}
