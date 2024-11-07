using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
        #endregion


        private const float GridSquaresPerEdge = 4f;
        double gridSquareSizeInPixels = 1.0;
        float pixelsPerUnit = 1.0f;
        private PointF _panelCenter;
        private VC_LOCATION _centermap;
        private VC_LOCATION _waypoint;
        private VC_LOCATION _screenWaypoint;
        double _myheading = 0.0;
        private bool _unitsInMeters = false;
        private double gridSquareEdgeLength = 1.0;
        private XIETAobj xietaObj;
        private double radiusEarthEquatorialFt = 6378.14 * 100000 / (2.54 * 12); //20925600.787

        double xi=0.0;
        double eta=0.0;
        double dx = 0.0;
        double dy = 0.0;

        // Previous trackbar positions
        private int previousLatTrackBarValue = 0;
        private int previousLonTrackBarValue = 0;

        private Timer debounceTimer;
        public XIetaSendForm()
        {
            InitializeComponent();
            debounceTimer = new Timer();
            debounceTimer.Interval = 500; // 500 milliseconds delay
            debounceTimer.Tick += (s, e) =>
            {
                debounceTimer.Stop(); // Stop the timer after it triggers
                CalculateScaling();
                mapPanel2.Invalidate();
            };

            gridSquareSizeBox.TextChanged += (s, e) =>
            {
                debounceTimer.Stop(); // Reset the timer every time the event is triggered
                debounceTimer.Start(); // Start the timer again
            };

            _centermap = new VC_LOCATION();
            _waypoint = new VC_LOCATION();
            _screenWaypoint = new VC_LOCATION();
            xietaObj = new XIETAobj();

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

            gridSquareSizeBox.TextChanged += (s, e) =>
            {
                CalculateScaling();
                mapPanel2.Invalidate();
            };

            mapPanel2.Paint += MapPanel_Paint;
            mapPanel2.Resize += MapPanel2_Resize;
            mapPanel2.MouseClick += MapPanel2_MouseClick;
            this.Load += XIetaSendForm_Load;


            vCinc_LatLon_mapCnter.SetLatitude(42.36487);
            vCinc_LatLon_mapCnter.SetLongitude(-71.0545);

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
                gridSquareEdgeLength = squareSize;
            }
            else
            {
                gridSquareEdgeLength = 1.0;
            }

            // Convert grid square size to pixels per unit
            double realSizeInCurrentUnits = _unitsInMeters ? gridSquareEdgeLength * 0.3048 : gridSquareEdgeLength;
            gridSquareSizeInPixels = Math.Min(mapPanel2.Width, mapPanel2.Height) / GridSquaresPerEdge;
            pixelsPerUnit = (float)(gridSquareSizeInPixels / realSizeInCurrentUnits);
        }
        private VC_LOCATION ScreenToWorldLatLon(Point screenPoint)
        {
            return xietaObj.ScreenToWorldLatLon(screenPoint, _centermap, _panelCenter, pixelsPerUnit, _unitsInMeters, radiusEarthEquatorialFt);
        }


        private PointF WorldLatLonToScreen(VC_LOCATION location)
        {
            return xietaObj.WorldLatLonToScreen(location, _centermap, _panelCenter, pixelsPerUnit, _unitsInMeters, radiusEarthEquatorialFt);
        }



        private void MapPanel2_MouseClick(object sender, MouseEventArgs e)
        {
            _screenWaypoint = ScreenToWorldLatLon(e.Location);
            mapPanel2.Invalidate();
        }

        private void XIetaSendForm_Load(object sender, EventArgs e)
        {
            if (double.TryParse(gridSquareSizeBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double squareSize) && squareSize > 0)
            {
                gridSquareEdgeLength = squareSize;
            }
            else
            {
                gridSquareEdgeLength = 1.0; // Default value if not provided
            }

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

        private void DrawDiskAtWaypoint(Graphics g)
        {
            PointF screenPos = WorldLatLonToScreen(_screenWaypoint);
            if (screenPos.X < 0 || screenPos.X > mapPanel2.Width || screenPos.Y < 0 || screenPos.Y > mapPanel2.Height)
                return;

            float radius = 10.0f;
            using (Brush brush = new SolidBrush(Color.Blue))
            {
                g.FillEllipse(brush, screenPos.X - radius, screenPos.Y - radius, 2 * radius, 2 * radius);
            }
        }

        private void DrawShipWithHeading(Graphics g) {

            float angleRadians = (float)(_myheading * (Math.PI / 180f));
            float shipSize = 20f;
            float frontX = mapPanel2.Width / 2f + shipSize * (float)Math.Sin(angleRadians);
            float frontY = mapPanel2.Height / 2f - shipSize * (float)Math.Cos(angleRadians);
            float rearLeftAngle = angleRadians + (float)(135 * Math.PI / 180f);
            float rearLeftX = mapPanel2.Width / 2f + shipSize * 0.5f * (float)Math.Sin(rearLeftAngle);
            float rearLeftY = mapPanel2.Height / 2f - shipSize * 0.5f * (float)Math.Cos(rearLeftAngle);
            float rearRightAngle = angleRadians - (float)(135 * Math.PI / 180f);
            float rearRightX = mapPanel2.Width / 2f + shipSize * 0.5f * (float)Math.Sin(rearRightAngle);
            float rearRightY = mapPanel2.Height / 2f - shipSize * 0.5f * (float)Math.Cos(rearRightAngle);
            PointF[] shipPoints = new PointF[]
            {
                new PointF(frontX, frontY),
                new PointF(rearLeftX, rearLeftY),
                new PointF(rearRightX, rearRightY)
            };
            using (SolidBrush shipBrush = new SolidBrush(Color.Blue))
            {
                g.FillPolygon(shipBrush, shipPoints);
            }
            using (Pen shipPen = new Pen(Color.Black))
            {
               g.DrawPolygon(shipPen, shipPoints);
            }
        }

        private void Draw_XIETAlines(Graphics g)
        {
            //use xietaObj.Get_offsetsArray shiploc wploc heading 
            // the returned array is a double[4] xiFt etaFt dxFt dyFt
            double[] results = xietaObj.Get_offsetsArray(_centermap, _waypoint, _myheading);
            double xiFt = results[0];
            double etaFt = results[1];
            double dxFt = results[2];
            double dyFt = results[3];

            if (_unitsInMeters)
            {
                xiFt/= 0.3048;
                etaFt /= 0.3048;
                dxFt /= 0.3048;
                dyFt /= 0.3048;
            }
            xi = xiFt;    // aka _lateralDisplacement
            eta = etaFt; //  aka _transverseDisplacement
            dx = dxFt;
            dy = dyFt;
     
            float angleRadians = (float)(_myheading * (Math.PI / 180f));
        
            float transverseDisplacementPixels = (float)(eta * pixelsPerUnit);
            float endX = (mapPanel2.Width / 2f) + transverseDisplacementPixels * (float)Math.Sin(angleRadians);
            float endY = (mapPanel2.Height / 2f) - transverseDisplacementPixels * (float)Math.Cos(angleRadians);
            PointF endPoint = new PointF(endX, endY);
            using (Pen bluePen = new Pen(Color.Blue, 2))
            {
                g.DrawLine(bluePen, new PointF((mapPanel2.Width / 2f), (mapPanel2.Height / 2f)), endPoint);
            }

            if (xi != 0) {

                float perpendicularAngleRadians = angleRadians + ((xi >= 0) ? (float)(Math.PI / 2) : (float)(-Math.PI / 2));
                float lateralDisplacementPixels = (float)(Math.Abs(xi) * pixelsPerUnit);
                float lateralEndX = endX + lateralDisplacementPixels * (float)Math.Sin(perpendicularAngleRadians);
                float lateralEndY = endY - lateralDisplacementPixels * (float)Math.Cos(perpendicularAngleRadians);
                PointF lateralEndPoint = new PointF(lateralEndX, lateralEndY);
                using (Pen cyanPen = new Pen(Color.Cyan, 2))
                {
                    g.DrawLine(cyanPen, endPoint, lateralEndPoint);
                }
            }

        }

        private void MapPanel_Paint(object sender, PaintEventArgs e)
        {
            if (pixelsPerUnit == 0 || _panelCenter == PointF.Empty)
            {
                CalculateScaling();
            }

            DrawGrid(e.Graphics);
            DrawDiskAtWaypoint(e.Graphics);
            DrawShipWithHeading(e.Graphics);
            Draw_XIETAlines(e.Graphics);
        }

 
        private void Looptimer_Tick(object sender, EventArgs e)
        {
         
            // Update _centermap and _waypoint locations
            _centermap.Latitude = vCinc_LatLon_mapCnter.LatitudeDecimal;
            _centermap.Longitude = vCinc_LatLon_mapCnter.LongitudeDecimal;
            _waypoint.Latitude = _screenWaypoint.Latitude;
            _waypoint.Longitude = _screenWaypoint.Longitude;

            //vCinc_LatLon_waypoint.SetLatitude(_waypoint.Latitude);
            //vCinc_LatLon_waypoint.SetLongitude(_waypoint.Longitude);



            // Heading calculations and PGN data updates remain the same
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
            int my32bitvalue_XI = (int)(xi * 10000);
            int my32bitvalue_ETA = (int)(eta * 10000);
            tb_XI.Text= my32bitvalue_XI.ToString();
            tb_ETA.Text = my32bitvalue_ETA.ToString();
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

            SendAllPgnMessages();
            mapPanel2.Invalidate();
           
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
