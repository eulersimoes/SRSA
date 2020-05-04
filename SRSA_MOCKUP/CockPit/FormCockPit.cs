using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AvionicsInstrumentsControls;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using GEPlugin;
using COCKPIT;
using System.Threading;
using System.Threading.Tasks;
using Entity;
using OSD_LIB;
using System.Security.Cryptography;
using Entity;
using Microsoft.Expression.Encoder.Live;
using Microsoft.Expression.Encoder.Devices;
using FC.GEPluginCtrls;
using System.IO;
using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.Profiles;
namespace AvionicsInstrumentControlDemo
{
    [ComVisibleAttribute(true)]
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public partial class FormCockPit : Form
    {
        private const string PLUGIN_URL =
        @"http://earth-api-samples.googlecode.com/svn/trunk/demos/desktop-embedded/pluginhost.html";

        private IGEPlugin m_ge = null;
        System.Threading.Thread threadPlaneInfo;
        System.Threading.ThreadStart tsPlaneInfo;

        System.Threading.Thread threadEnviarMensagem;
        System.Threading.ThreadStart tsEnviarMensagem;

        System.Threading.Thread threadReceberMensagem;
        System.Threading.ThreadStart tsReceberMensagem;

        System.Threading.Thread threadBroadVideo;
        System.Threading.ThreadStart tsBroadVideo;

        OSD osdSpeedDisplay = new OSD();
        OSD osdAltDisplay = new OSD();
        OSD osdHdgDisplay = new OSD();
        OSD osdBatDisplay = new OSD();
        OSD osdFuelDisplay = new OSD();
        OSD osdLatLongDisplay = new OSD();
        OSD osdAutoPilotDisplay = new OSD();
        OSD osdThrottleDisplay = new OSD();
        private static Boolean isExecutandoComandoSerial = false;
        private static Boolean isExecutandoThreadPlaneInfo = false;
        private static ModelControle modelControle = new ModelControle();
        private static ModelFpv modelFpv = new ModelFpv();
        private static Boolean ligarFarol = true;
        private static Boolean ligarStrobe = true;
        private static Boolean ligarNavLight = true;
        private static ModelLeituraDados modelLeituraDados = new ModelLeituraDados();
        /// <summary>
        /// Creates job for capture of live source
        /// </summary>
        private LiveJob _job;
        /// <summary>
        /// Device for live source
        /// </summary>
        private LiveDeviceSource _deviceSource;
        private bool _bStartedRecording = false;

        private static float countFrame = 0;
        public FormCockPit()
        {
            InitializeComponent();
        }

        private void DrawOsdInfo(PlaneInfo planeInfo)
        {
            Font fontDisplay = new Font("Lucida Console", 9);
            Point fpvPos = FindLocation(fpv);
            Color corFuel = new Color();
            Color corBat = new Color();
            Color corAp = new Color();
            osdSpeedDisplay.Draw(fpvPos.X + 10, fpvPos.Y + 40, 255, Color.Green, 10, 1, "Vel. (km/h): " + planeInfo.Velocidadekmh.ToString(), fontDisplay);
            osdAltDisplay.Draw(fpvPos.X + 10, fpvPos.Y + 60, 255, Color.Green, 10, 1, "Alt. (m): " + planeInfo.AltitudeMetros.ToString(), fontDisplay);
            osdHdgDisplay.Draw(fpvPos.X + (fpv.Size.Width / 2), fpvPos.Y + 40, 255, Color.Green, 10, 1, "Hdg: " + planeInfo.Hdg.ToString(), fontDisplay);
            osdThrottleDisplay.Draw(fpvPos.X + 10, fpvPos.Y + fpv.Height -25, 255, Color.Green, 10, 1, "Throttle (%): " + planeInfo.Throttle.ToString(), fontDisplay);
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

        private void FormCockPit_Load(object sender, EventArgs e)
        {
            ShowMap();
            this.bdsModel.DataSource = modelControle;

            tsPlaneInfo = new System.Threading.ThreadStart(PlaneInfo);
            threadPlaneInfo = new System.Threading.Thread(tsPlaneInfo);
            threadPlaneInfo.Start();

            tsEnviarMensagem = new System.Threading.ThreadStart(EnviarMensagem);
            threadEnviarMensagem = new System.Threading.Thread(tsEnviarMensagem);
            threadEnviarMensagem.Start();

            tsReceberMensagem = new System.Threading.ThreadStart(ReceberMensagem);
            threadReceberMensagem = new System.Threading.Thread(tsReceberMensagem);
            threadReceberMensagem.Start();

            
            cmbVideoDevices.Items.Clear();
            foreach (EncoderDevice edv in EncoderDevices.FindDevices(EncoderDeviceType.Video))
            {
                cmbVideoDevices.Items.Add(edv.Name);
            }
        }
        private void ShowMap()
        {
            try
            {
                int w = this.groupBox1.Width * 50 / 100;
                int h = this.groupBox1.Height * 95 / 100;

                webBrowser.Width = w;
                webBrowser.Height = h;
                webBrowser.Refresh();
                webBrowser.Navigate(PLUGIN_URL);
                webBrowser.ObjectForScripting = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao carregar o mapa:" + ex.ToString());
            }

        }
        // called from initCallback in JavaScript
        public void JSInitSuccessCallback_(object pluginInstance)
        {
            m_ge = (IGEPlugin)pluginInstance;
            modelControle.SetPluginGmap(m_ge);
            m_ge.getGlobe().onClickEventEnabled(1);
 
         }

        private void ShowVideo()
        {
            try
            {

                EncoderDevice video = null;
                EncoderDevice audio = null;

                // Get the selected video device            
                foreach (EncoderDevice edv in EncoderDevices.FindDevices(EncoderDeviceType.Video))
                {
                    if (String.Compare(edv.Name, cmbVideoDevices.SelectedItem.ToString()) == 0)
                    {
                        video = edv;
                        break;
                    }
                }
                StopJob();

                if (video == null)
                {
                    return;
                }

                // Starts new job for preview window
                _job = new LiveJob();

                // Create a new device source. We use the first audio and video devices on the system
                _deviceSource = _job.AddDeviceSource(video, audio);

                int w = this.groupBox1.Width * 45 / 100;
                int h = this.groupBox1.Height * 95 / 100;

                while (!(w % 4 == 0))
                {
                    w++;
                }

                while (!(h % 4 == 0))
                {
                    h++;
                }

                _deviceSource.PickBestVideoFormat(new Size(w, h), 1);

                // Get the properties of the device video
                SourceProperties sp = _deviceSource.SourcePropertiesSnapshot();

                // Resize the preview panel to match the video device resolution set
                fpv.Size = new Size(w, h);

                // Setup the output video resolution file as the preview
                _job.OutputFormat.VideoProfile.Size = new Size(w, h);


                // Sets preview window to winform panel hosted by xaml window
                _deviceSource.PreviewWindow = new PreviewWindow(new HandleRef(fpv, fpv.Handle));

                // Make this source the active one
                _job.ActivateSource(_deviceSource);

                btnStartStopRecording.Enabled = true;
                //btnGrabImage.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao carregar o video: " + ex.ToString());
            }

        }

        private void PrintImage()
        {
            try
            {
                using (Bitmap bitmap = new Bitmap(fpv.Width, fpv.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        // Get the paramters to call g.CopyFromScreen and get the image
                        Rectangle rectanglePanelVideoPreview = fpv.Bounds;
                        Point sourcePoints = fpv.PointToScreen(new Point(fpv.ClientRectangle.X, fpv.ClientRectangle.Y));
                        g.CopyFromScreen(sourcePoints, Point.Empty, rectanglePanelVideoPreview.Size);
                    }

                    string strGrabFileName = "C:\\inetpub\\wwwroot\\planeInfo\\VideoStr\\out.jpg";
                    bitmap.Save(strGrabFileName, System.Drawing.Imaging.ImageFormat.Jpeg);

                    strGrabFileName = "C:\\inetpub\\wwwroot\\planeInfo\\VideoRec\\out" + countFrame + ".jpg";
                    bitmap.Save(strGrabFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    countFrame++;
                }
            }
            catch { }
            // Create a Bitmap of the same dimension of panelVideoPreview (Width x Height)

        }
        
        void StopJob()
        {
            // Has the Job already been created ?
            if (_job != null)
            {
                // Yes
                // Is it capturing ?
                //if (_job.IsCapturing)
                if (_bStartedRecording)
                {
                    // Yes
                    // Stop Capturing
                    btnStartStopRecording.PerformClick();
                }

                _job.StopEncoding();

                // Remove the Device Source and destroy the job
                _job.RemoveDeviceSource(_deviceSource);

                // Destroy the device source
                _deviceSource.PreviewWindow = null;
                _deviceSource = null;
            }
        }

       
        private void btAdd_Click(object sender, EventArgs e)
        {
            FormAddWayPoint frmWayP = new FormAddWayPoint();
            frmWayP.ShowDialog(this);
            if (frmWayP.DialogResult == DialogResult.OK)
            {
                foreach (WayPoint wp in frmWayP.ListaWayPoint)
                {
                    modelControle.AddWayPoint(wp);
                }
            }
            //frmWayP.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modelControle.CentralizarMapa();
        }

        private void AtualizarPlaneInfo(PlaneInfo planeInfo,Boolean force)
        {
            attitudeIndicatorInstrumentControl1.RollAngle = planeInfo.RollAngle;
            attitudeIndicatorInstrumentControl1.PitchAngle = planeInfo.PitchAngle;
            airSpeedIndicatorInstrumentControl1.AirSpeed = planeInfo.Velocidadekmh;
            headingIndicatorInstrumentControl1.Heading = planeInfo.Hdg;
            modelControle.GeneratePlaneLocation(planeInfo, force);
            this.QntaBat.Text = planeInfo.PercentualBateria.ToString() ;
            this.QntFuel.Text = planeInfo.PercentualCombustivel.ToString();
        }

        private void AtualizarWayPoints(List<WayPoint> lista)
        {
            //if (modelLeituraDados.AtualizarListaWayPoint)
            //{
            //    modelControle.ListaWayPoints = lista;
            //}
        }

        private void PlaneInfo()
        {
            Thread.Sleep(5000);
            //ModelLeituraDados modelLeituraDados = new ModelLeituraDados();
            while (true)
            {
                if (!this.IsDisposed && this.IsHandleCreated && !isExecutandoComandoSerial)
                {
                    modelLeituraDados.AtualizarPlaneInfo();
                    isExecutandoThreadPlaneInfo = true;
                    this.Invoke((MethodInvoker)delegate
                   {

                       this.AtualizarPlaneInfo(modelLeituraDados.PlaneInfo,false);
                       this.DrawOsdInfo(modelLeituraDados.PlaneInfo);
                       if (modelLeituraDados.AtualizarListaWayPoint)
                       {
                           this.AtualizarWayPoints(modelLeituraDados.ListaWayPoint);
                           modelLeituraDados.AtualizarListaWayPoint = false;
                       }

                   });
                }
                else
                {
                    break;
                }
                isExecutandoThreadPlaneInfo = false;
                Thread.Sleep(150);
            }
        }

        private void BroadVideo()
        {
            while (true)
            {
                if (!this.IsDisposed && this.IsHandleCreated && !isExecutandoComandoSerial)
                {

                    this.Invoke((MethodInvoker)delegate
                    {
                        this.PrintImage();
                    });
                }
                Thread.Sleep(100);
            }
        }

        private void EnviarMensagem()
        {
            Thread.Sleep(5000);

            while (true)
            {
                //modelControle.EnviarMensagem();
                Thread.Sleep(10);
            }
        }

        private void ReceberMensagem()
        {
            Thread.Sleep(5000);

            while (true)
            {
                modelLeituraDados.GerenciarMensagensRecebidas();
                Thread.Sleep(100);
            }
        }


        private void FormCockPit_FormClosing(object sender, FormClosingEventArgs e)
        {
            threadPlaneInfo.Abort();
            threadEnviarMensagem.Abort();
            threadReceberMensagem.Abort();
        }

        private void btSync_Click(object sender, EventArgs e)
        {
            modelControle.WayPointInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            modelControle.RemoveWayPoint(modelControle.ListaWayPoints[1].Id);
        }

        private void btApOn_Click(object sender, EventArgs e)
        {
            isExecutandoComandoSerial = true;
            Thread.Sleep(500);
            modelControle.TurnAutoPilot(true);
            isExecutandoComandoSerial = false;
        }

        private void btApOff_Click(object sender, EventArgs e)
        {
            isExecutandoComandoSerial = true;
            Thread.Sleep(500);
            modelControle.TurnAutoPilot(false);
            isExecutandoComandoSerial = false;
        }
       
        private void btnStartStopRecording_Click(object sender, EventArgs e)
        {
            // Is it Recoring ?

            if (_bStartedRecording)
            {
                // Yes
                // Stops encoding
                threadBroadVideo.Suspend();
                btnStartStopRecording.Text = "Broad";
                _bStartedRecording = false;
                _job.StopEncoding();
            }
            else
            {
                // Gets timestamp and edits it for filename
                FileArchivePublishFormat fileOut = new FileArchivePublishFormat();
                _job.ApplyPreset(LivePresets.VC1HighSpeedBroadband4x3);

                string timeStamp = DateTime.Now.ToString();
                timeStamp = timeStamp.Replace("/", "-");
                timeStamp = timeStamp.Replace(":", ".");

                // Sets file path and name
                string path = "C:\\Temp\\";
                string filename = "Capture" + timeStamp + ".wmv";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                fileOut.OutputFileName = Path.Combine(path, filename);

                // Adds the format to the job. You can add additional formats as well such as
                // Publishing streams or broadcasting from a port
                _job.PublishFormats.Add(fileOut);

                // Start encoding
                _job.OutputFormat.VideoProfile.Bitrate = new ConstantBitrate(2000);
                _job.SendScriptCommand(new Microsoft.Expression.Encoder.Live.ScriptCommand("caption", "Streaming now!"));
                _job.StartEncoding();

                tsBroadVideo = new System.Threading.ThreadStart(BroadVideo);
                threadBroadVideo = new System.Threading.Thread(tsBroadVideo);
                threadBroadVideo.Start();

                btnStartStopRecording.Text = "Stop Broad";
                _bStartedRecording = true;
            }
             
        }

        private void cmbVideoDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowVideo();
        }

        private void dgvWay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex ==0) //Del
            {
                modelControle.RemoveWayPoint((int)dgvWay.CurrentRow.Cells[1].Value);
                MessageBox.Show("WayPoint removido !");
            }
        }

        private void btFarol_Click(object sender, EventArgs e)
        {
            modelControle.TurnFarol(ligarFarol);
            ligarFarol = !ligarFarol;
        }

        private void btNavLight_Click(object sender, EventArgs e)
        {
            modelControle.TurnNavLight(ligarNavLight);
            ligarNavLight = !ligarNavLight;
        }

        private void btStrobe_Click(object sender, EventArgs e)
        {
            modelControle.TurnStrobe(ligarStrobe);
            ligarStrobe = !ligarStrobe;
        }

        private void btAtivar_Click(object sender, EventArgs e)
        {
            modelControle.AutoGetPlaneInfo();
        }

        private void btAtualizar_Click(object sender, EventArgs e)
        {
            this.AtualizarPlaneInfo(modelLeituraDados.PlaneInfo,true);
            modelControle.CentralizarAviao();
        }
    }
}
