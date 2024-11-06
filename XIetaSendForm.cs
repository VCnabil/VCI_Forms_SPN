using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCI_Forms_LIB;
using VCI_Forms_SPN._GLobalz;
using VCI_Forms_SPN._Managers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VCI_Forms_SPN
{
    public partial class XIetaSendForm : Form
    {
        #region TemplateVariavles
        Timer looptimer = new Timer();
        PGN_MANAGER _myPGNManager;
        Queue<string> messageQueue;
        StringBuilder messageBuffer;
        const int MaxMessages = 22;
        int _OScreenCount = 0;
 
        bool _isOnCanBus;
        Dictionary<string, string> uniqueMessages = new Dictionary<string, string>();
        #endregion

        private Dictionary<int, byte[]> pgnDataDictionary = new Dictionary<int, byte[]>()
        {
            { 0x09F1127F, new byte[] { 0x00, 0x3A, 0x19, 0x00, 0x00, 0x00, 0x00, 0x00 } },
            { 0x09F8017F, new byte[] { 0x9A, 0x87, 0x01, 0x00, 0xDB, 0x00, 0x00, 0x00 } },
            { 0x18FF6729, new byte[] { 0x80, 0xF9, 0x64, 0x3F, 0xE4, 0x7B, 0x41, 0x41 } },
            { 0x18FEFC29, new byte[] { 0x80, 0x00, 0x80, 0x80, 0x00, 0x80, 0x00, 0x00 } },
            { 0x18FF4929, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } },
            { 0x18FF5329, new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } },
            { 0x18FF5929, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } },
            { 0x18FF6529, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } },
            { 0x18FF6629, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x68, 0x01, 0x64, 0x00 } },
            { 0x18FF7300, new byte[] { 0xE7, 0x03, 0x00, 0x00, 0x19, 0xFC, 0xFF, 0xFF } }
        };


        XIETAvariants xIETAvariants = new XIETAvariants();
        private const float GridSquaresPerEdge = 4f;
        double gridSquareSizeInPixels = 1.0;
        float pixelsPerUnit = 1.0f;
        private PointF _panelCenter;
        double _centermap_LAT = 0.0;
        double _centermap_LON = 0.0;
        double _WP_LAT = 0.0;
        double _WP_LON = 0.0;
        double _myheading = 0.0;
        double _screenWP_LAT = 0.0;
        double _screenWP_LON = 0.0;
        private bool _unitsInMeters = false;
        double gridSquareEdgeLength_regardlessOfFeetorMeters = 1.0;

        public XIetaSendForm()
        {
            InitializeComponent();
            #region TemplateInitialize
            lbl_OnScreenCount.BackColor = Color.Transparent;
            lbl_OnScreenCount.ForeColor = Color.Black;
            lbl_onBus.BackColor = Color.Transparent;
            lbl_onBus.ForeColor = Color.Black;
            btn_Validate.Click += Btn_Validate_Click;
            btn_RunStop.Click += Btn_RunStop_Click;
            messageBuffer = new StringBuilder();
            messageQueue = new Queue<string>(MaxMessages);
            looptimer.Interval = 200;
            looptimer.Tick += Looptimer_Tick;
            looptimer.Start();
            #endregion
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, mapPanel2, new object[] { true });
            unitsCheckBox.CheckedChanged += (s, e) =>
            {
                _unitsInMeters = unitsCheckBox.Checked;
                mapPanel2.Invalidate();
            };
            if (!double.TryParse(gridSquareSizeBox.Text, out gridSquareSizeInPixels) || gridSquareSizeInPixels <= 0)
            {
                gridSquareEdgeLength_regardlessOfFeetorMeters = 1.0;
            }
            gridSquareSizeBox.TextChanged += (s, e) => {
                CalculateScaling();
                mapPanel2.Invalidate();

            };
            mapPanel2.Paint += MapPanel_Paint;
            mapPanel2.Resize += MapPanel2_Resize;
            mapPanel2.MouseClick += MapPanel2_MouseClick;
            this.Load += XIetaSendForm_Load;
        }
        private void MapPanel2_Resize(object sender, EventArgs e)
        {
            CalculateScaling();
            mapPanel2.Invalidate();
        }
  
        private void CalculateScaling()
        {
            if (mapPanel2.Width == 0 || mapPanel2.Height == 0)
                return;

            _panelCenter = new PointF(mapPanel2.Width / 2f, mapPanel2.Height / 2f);
            if (double.TryParse(gridSquareSizeBox.Text, out double squareSize))
            {
                gridSquareEdgeLength_regardlessOfFeetorMeters = squareSize;
            }
            else
            {
                gridSquareEdgeLength_regardlessOfFeetorMeters = 1.0;  
            }
            double realSizeInCurrentUnits = gridSquareEdgeLength_regardlessOfFeetorMeters;
            if (_unitsInMeters)
            {
                realSizeInCurrentUnits *= 0.3048; 
            }
            gridSquareSizeInPixels = mapPanel2.Width / GridSquaresPerEdge;
            pixelsPerUnit = (float)(gridSquareSizeInPixels / realSizeInCurrentUnits);
        }
  
        private (double lat, double lon) ScreenToWorldLatLon(Point screenPoint)
        {
            // Calculate offset from panel center
            float deltaX = screenPoint.X - _panelCenter.X;
            float deltaY = _panelCenter.Y - screenPoint.Y; // Y axis is inverted in screen coordinates

            // Convert pixel offset to real-world distance
            double offsetInFeetX = deltaX / pixelsPerUnit;
            double offsetInFeetY = deltaY / pixelsPerUnit;

            // Convert feet to meters if necessary
            if (_unitsInMeters)
            {
                offsetInFeetX *= 0.3048;
                offsetInFeetY *= 0.3048;
            }

            // Convert to latitude and longitude based on offsets
            double lat = _centermap_LAT + (offsetInFeetY / xIETAvariants.Get_radiusEarthEquatorialFt() * 180 / Math.PI);
            double lon = _centermap_LON + (offsetInFeetX / (xIETAvariants.Get_radiusEarthEquatorialFt() * Math.Cos(_centermap_LAT * Math.PI / 180)) * 180 / Math.PI);

            return (lat, lon);
        }
        private PointF WorldLatLonToScreen(double lat, double lon)
        {
            // Calculate the real-world distance offsets in feet or meters from the center map position
            double deltaLatInFeet = (lat - _centermap_LAT) * xIETAvariants.Get_radiusEarthEquatorialFt() * Math.PI / 180;
            double deltaLonInFeet = (lon - _centermap_LON) * xIETAvariants.Get_radiusEarthEquatorialFt() * Math.PI / 180 / Math.Cos(_centermap_LAT * Math.PI / 180);

            // Convert to pixels based on pixelsPerUnit
            if (_unitsInMeters)
            {
                deltaLatInFeet /= 0.3048;
                deltaLonInFeet /= 0.3048;
            }

            float x = (float)(_panelCenter.X + deltaLonInFeet * pixelsPerUnit);
            float y = (float)(_panelCenter.Y - deltaLatInFeet * pixelsPerUnit);

            return new PointF(x, y);
        }


        private void MapPanel2_MouseClick(object sender, MouseEventArgs e)
        {
            var (lat, lon) = ScreenToWorldLatLon(e.Location);
            _screenWP_LAT = lat;
            _screenWP_LON = lon;
            mapPanel2.Invalidate();
        }

        private void XIetaSendForm_Load(object sender, EventArgs e)
        {
            CalculateScaling();
        }
        private void DrawGrid(Graphics g)
        {
            float totalGridSize = (float)(GridSquaresPerEdge * gridSquareSizeInPixels);
            float gridOriginX = _panelCenter.X - totalGridSize / 2f;
            float gridOriginY = _panelCenter.Y - totalGridSize / 2f;
            using (Pen gridPen = new Pen(Color.LightGray))
            {
                for (int i = 0; i <= GridSquaresPerEdge; i++)
                {
                    float offset = i * (float)gridSquareSizeInPixels;
                    g.DrawLine(gridPen, gridOriginX + offset, gridOriginY, gridOriginX + offset, gridOriginY + totalGridSize);
                    g.DrawLine(gridPen, gridOriginX, gridOriginY + offset, gridOriginX + totalGridSize, gridOriginY + offset);
                }
            }
        }
        private void DrawDiskAtWaypoint(Graphics g, double[] argDeltaXy)
        {
            if (argDeltaXy == null) { return; }
            double deltaX = argDeltaXy[0];
            double deltaY = argDeltaXy[1];
            if (_unitsInMeters)
            {
                deltaX *= 0.3048;
                deltaY *= 0.3048;
            }
            float x = (float)(_panelCenter.X + deltaX * pixelsPerUnit);
            float y = (float)(_panelCenter.Y - deltaY * pixelsPerUnit);
            float radius = 10.0f;
            using (Brush brush = new SolidBrush(Color.Red))
            {
                g.FillEllipse(brush, x - radius, y - radius, 2 * radius, 2 * radius);
            }
        }
        private void DrawDiskAtWaypoint(Graphics g)
        {
            PointF screenPos = WorldLatLonToScreen(_screenWP_LAT, _screenWP_LON);
            if (screenPos.X < 0 || screenPos.X > mapPanel2.Width || screenPos.Y < 0 || screenPos.Y > mapPanel2.Height)
                return;

            float radius = 10.0f;
            using (Brush brush = new SolidBrush(Color.Blue))
            {
                g.FillEllipse(brush, screenPos.X - radius, screenPos.Y - radius, 2 * radius, 2 * radius);
            }
        }

        private void MapPanel_Paint(object sender, PaintEventArgs e)
        {
            if (pixelsPerUnit == 0 || _panelCenter == PointF.Empty)
            {
                CalculateScaling();
            }
            DrawGrid(e.Graphics);
            xIETAvariants.UpdateParameters(_centermap_LAT, _centermap_LON, _WP_LAT, _WP_LON, _myheading);
            double[] deltaXY = xIETAvariants.Get_DeltaXY_matrixcalc_inFT();
            DrawDiskAtWaypoint(e.Graphics); // Draw disk using real-to-screen calculation
            //not this   DrawDiskAtWaypoint(e.Graphics, deltaXY);
        }

        private void Looptimer_Tick(object sender, EventArgs e)
        {
            if (cb_useTbLat.Checked) { 
                vCinc_LatLon_mapCnter.SetLatitude(trackBarLat.Value / 1000.000);
            }
            if (cb_useTbLon.Checked) {
                vCinc_LatLon_mapCnter.SetLongitude(trackBarLon.Value / 1000.000);
            }


            _centermap_LAT = vCinc_LatLon_mapCnter.LatitudeDecimal;
            _centermap_LON = vCinc_LatLon_mapCnter.LongitudeDecimal;
            _WP_LAT = vCinc_LatLon_waypoint.LatitudeDecimal;
            _WP_LON = vCinc_LatLon_waypoint.LongitudeDecimal;
            _myheading = trackBar1.Value / 100.00;
            ushort heading = (ushort)Math.Round(_myheading * Math.PI / 180 * 10000);
            label1.Text = _myheading.ToString();
            byte lowByte = (byte)heading;
            byte highByte = (byte)(heading >> 8);
            if (pgnDataDictionary.ContainsKey(0x09F1127F))
            {
                pgnDataDictionary[0x09F1127F][1] = lowByte;
                pgnDataDictionary[0x09F1127F][2] = highByte;
            }
            uint _my_COORD_LAT_x1E7 = (uint)(vCinc_LatLon_waypoint.LatitudeDecimal * 1E7);
            uint _my_COORD_LON_x1E7 = (uint)(vCinc_LatLon_waypoint.LongitudeDecimal * 1E7);
            if (pgnDataDictionary.ContainsKey(0x09F8017F))
            {
                pgnDataDictionary[0x09F8017F][0] = (byte)_my_COORD_LAT_x1E7;
                pgnDataDictionary[0x09F8017F][1] = (byte)(_my_COORD_LAT_x1E7 >> 8);
                pgnDataDictionary[0x09F8017F][2] = (byte)(_my_COORD_LAT_x1E7 >> 16);
                pgnDataDictionary[0x09F8017F][3] = (byte)(_my_COORD_LAT_x1E7 >> 24);
                pgnDataDictionary[0x09F8017F][4] = (byte)_my_COORD_LON_x1E7;
                pgnDataDictionary[0x09F8017F][5] = (byte)(_my_COORD_LON_x1E7 >> 8);
                pgnDataDictionary[0x09F8017F][6] = (byte)(_my_COORD_LON_x1E7 >> 16);
                pgnDataDictionary[0x09F8017F][7] = (byte)(_my_COORD_LON_x1E7 >> 24);
            }
            int my32bitvalue_XI = 0;
            int my32bitvalue_ETA = 0;
            string textXI = tb_XI.Text;
            if (textXI != "") { my32bitvalue_XI = Convert.ToInt32(textXI); }
            string textETA = tb_ETA.Text;
            if (textETA != "") { my32bitvalue_ETA = Convert.ToInt32(textETA); }
            if (pgnDataDictionary.ContainsKey(0x18FF6729))
            {
                pgnDataDictionary[0x18FF6729][0] = (byte)my32bitvalue_XI;
                pgnDataDictionary[0x18FF6729][1] = (byte)(my32bitvalue_XI >> 8);
                pgnDataDictionary[0x18FF6729][2] = (byte)(my32bitvalue_XI >> 16);
                pgnDataDictionary[0x18FF6729][3] = (byte)(my32bitvalue_XI >> 24);
                pgnDataDictionary[0x18FF6729][4] = (byte)my32bitvalue_ETA;
                pgnDataDictionary[0x18FF6729][5] = (byte)(my32bitvalue_ETA >> 8);
                pgnDataDictionary[0x18FF6729][6] = (byte)(my32bitvalue_ETA >> 16);
                pgnDataDictionary[0x18FF6729][7] = (byte)(my32bitvalue_ETA >> 24);
            }
            mapPanel2.Invalidate();
        }


        private void Looptimer_Tick_working(object sender, EventArgs e)
        {
            //max 90000 min -90000
            double trackerLat = trackBarLat.Value / 1000.000;
            double trackerLon = trackBarLon.Value / 1000.000;

            vCinc_LatLon_mapCnter.SetLatitude(trackerLat);
            vCinc_LatLon_mapCnter.SetLongitude(trackerLon);

            // trackBar1 max 36000
            double _myheading = trackBar1.Value / 100.00;
            ushort heading = (ushort)Math.Round(_myheading * Math.PI / 180 * 10000);
             
            label1.Text = _myheading.ToString();
            byte lowByte = (byte)heading;
            byte highByte = (byte)(heading >> 8);
            if (pgnDataDictionary.ContainsKey(0x09F1127F))
            {
                pgnDataDictionary[0x09F1127F][1] = lowByte;
                pgnDataDictionary[0x09F1127F][2] = highByte;
            }

            uint _my_COORD_LAT_x1E7 = (uint)(vCinc_LatLon_mapCnter.LatitudeDecimal * 1E7);
            uint _my_COORD_LON_x1E7 = (uint)(vCinc_LatLon_mapCnter.LongitudeDecimal * 1E7);

            byte byt3 = (byte)(_my_COORD_LAT_x1E7 >> 24);
            byte byt2 = (byte)(_my_COORD_LAT_x1E7 >> 16);
            byte byt1 = (byte)(_my_COORD_LAT_x1E7 >> 8);
            byte byt0 = (byte)_my_COORD_LAT_x1E7;

            byte byt7 = (byte)(_my_COORD_LON_x1E7 >> 24);
            byte byt6 = (byte)(_my_COORD_LON_x1E7 >> 16);
            byte byt5 = (byte)(_my_COORD_LON_x1E7 >> 8);
            byte byt4 = (byte)_my_COORD_LON_x1E7;

            if (pgnDataDictionary.ContainsKey(0x09F8017F))
            {
                pgnDataDictionary[0x09F8017F][0] = byt0;
                pgnDataDictionary[0x09F8017F][1] = byt1;
                pgnDataDictionary[0x09F8017F][2] = byt2;
                pgnDataDictionary[0x09F8017F][3] = byt3;

                pgnDataDictionary[0x09F8017F][4] = byt4;
                pgnDataDictionary[0x09F8017F][5] = byt5;
                pgnDataDictionary[0x09F8017F][6] = byt6;
                pgnDataDictionary[0x09F8017F][7] = byt7;
            }

            int my32bitvalue_XI = 12254855;
             

            string textXI = tb_XI.Text;
            if (textXI != "")
            {
                my32bitvalue_XI = Convert.ToInt32(textXI);
            }



            byte byte3 = (byte)(my32bitvalue_XI >> 24);
            byte byte2 = (byte)(my32bitvalue_XI >> 16);
            byte byte1 = (byte)(my32bitvalue_XI >> 8);
            byte byte0 = (byte)my32bitvalue_XI;

            int my32bitvalue_ETA = 5520000;

            string textETA = tb_ETA.Text;
            if (textETA != "")
            {
                my32bitvalue_ETA = Convert.ToInt32(textETA);
            }
            byte byte7 = (byte)(my32bitvalue_ETA >> 24);
            byte byte6 = (byte)(my32bitvalue_ETA >> 16);
            byte byte5 = (byte)(my32bitvalue_ETA >> 8);
            byte byte4 = (byte)my32bitvalue_ETA;

            if (pgnDataDictionary.ContainsKey(0x18FF6729))
            {
                pgnDataDictionary[0x18FF6729][0] = byte0;
                pgnDataDictionary[0x18FF6729][1] = byte1;
                pgnDataDictionary[0x18FF6729][2] = byte2;
                pgnDataDictionary[0x18FF6729][3] = byte3;

                pgnDataDictionary[0x18FF6729][4] = byte4;
                pgnDataDictionary[0x18FF6729][5] = byte5;
                pgnDataDictionary[0x18FF6729][6] = byte6;
                pgnDataDictionary[0x18FF6729][7] = byte7;
            }

            _isOnCanBus = KvsrManager.Instance.GetIsOnBus();
            if (_isOnCanBus)
            {
                lbl_onBus.BackColor = Color.Green;
                lbl_onBus.ForeColor = Color.White;
                lbl_onBus.Text = "ON BUS";
            }
            else
            {
                lbl_onBus.BackColor = Color.Red;
                lbl_onBus.ForeColor = Color.White;
                lbl_onBus.Text = "OFF BUS";
            }    
            if (!_isOnCanBus) { return; }
            SendAllPgnMessages();
            if (_OScreenCount == 0) { return; }

  

        }

        #region can and stuff
        private void KvsrManager_OnMessageReceived(string message)
        {
            // Debug.WriteLine($"[DEBUG] received: {message}");
            string id = message.Substring(3, 8); // "ID=18EA0028" extracts the part between 'ID=' and ','

            string _uniquePgns = "";
            string _quedPgns = "";
            if (InvokeRequired)
            {
                Invoke(new Action(() => KvsrManager_OnMessageReceived(message)));
                return;
            }
            // Store or replace the message for the specific ID
            if (uniqueMessages.ContainsKey(id))
            {
                uniqueMessages[id] = message;
            }
            else
            {
                uniqueMessages.Add(id, message);
            }
            _uniquePgns = string.Join(Environment.NewLine, uniqueMessages.Values);

            if (messageQueue.Count >= MaxMessages)
            {
                messageQueue.Dequeue();
            }
            messageQueue.Enqueue(message);
            _quedPgns = string.Join(Environment.NewLine, messageQueue);

            if (cb_uniqueOn.Checked)
            {
                tb_CAN_Bus_View.Text = _uniquePgns;
            }
            else
            {
                tb_CAN_Bus_View.Text = _quedPgns;
            }
        }
        private void Btn_Validate_Click(object sender, EventArgs e)
        {
            _OScreenCount = 0;
            lbl_OnScreenCount.BackColor = Color.Transparent;
            lbl_OnScreenCount.ForeColor = Color.Black;
            Call_PgnManager_GatherOnscreen();
            if (_OScreenCount > 0)
            {
                _myPGNManager.First_Call();
            }
            else
            {
                MessageBox.Show("No SPN_Control found to serialize.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void Call_PgnManager_GatherOnscreen()
        {
            if (_myPGNManager == null)
            {
                _myPGNManager = new PGN_MANAGER(this);
            }
            _OScreenCount = _myPGNManager.Get__numberOfunique_PGNADRSS();
            lbl_OnScreenCount.Text = " On Screen UCS: " + _OScreenCount.ToString();
            if (_OScreenCount == 0)
            {
                lbl_OnScreenCount.BackColor = Color.Red;
                lbl_OnScreenCount.ForeColor = Color.White;
                lbl_OnScreenCount.Text += " ZERO ? ";
                return;
            }
            else
            {
                lbl_OnScreenCount.BackColor = Color.Green;
                lbl_OnScreenCount.ForeColor = Color.White;
            }
        }
        private void Btn_RunStop_Click(object sender, EventArgs e)
        {
            if (!_isOnCanBus)
            {

                KvsrManager.Instance.Init();
                KvsrManager.Instance.OnMessageReceived += KvsrManager_OnMessageReceived;
            }
            else
            {
                KvsrManager.Instance.Close();
                KvsrManager.Instance.OnMessageReceived -= KvsrManager_OnMessageReceived;
            }
        }
        private void SendAllPgnMessages()
        {
            int busNumber = 1; // Assuming bus number is 1

            foreach (var entry in pgnDataDictionary)
            {
                int pgn = entry.Key;
                byte[] data = entry.Value;

                // Send the message using your method
                KvsrManager.Instance.SendPGN_withStatus(busNumber, pgn, data);
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            mapPanel2.Paint -= MapPanel_Paint;
            if (_isOnCanBus)
            {

                KvsrManager.Instance.Close();
                KvsrManager.Instance.OnMessageReceived -= KvsrManager_OnMessageReceived;
            }
            base.OnFormClosing(e);
            Debug.WriteLine("[DEBUG] CAN manager closed and resources cleaned up.");
        }
        #endregion
    }
}
