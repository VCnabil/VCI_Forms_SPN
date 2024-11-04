using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCI_Forms_LIB;
using VCI_Forms_SPN._GLobalz;
using VCI_Forms_SPN._Managers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Newtonsoft.Json;
using VCI_Forms_SPN._BackEndDataOBJs;

namespace VCI_Forms_SPN
{
    public partial class ssrsK12 : Form
    {
        #region TemplateVariavles
        PGN_MANAGER _myPGNManager;
        Queue<string> messageQueue;
        StringBuilder messageBuffer;
        const int MaxMessages = 12;
        int _OScreenCount = 0;
        Timer tempTimer;
        bool _isOnCanBus;
        Dictionary<string, string> uniqueMessages = new Dictionary<string, string>();
        #endregion
        double _unity_shiplat = 0;
        double _unity_shiplon = 0;
        double _unity_shipHeading = 0;
        private PipeManager _pipeManager;
        private bool _pipeIsOpen = false;
        double _myJET_ANG = 0;
        double _myTHRUST = 0;
        double _myWPlat = 0;
        double _myWPlon = 0;
        public ssrsK12()
        {
            InitializeComponent();
            #region TemplateInitialize
            lbl_OnScreenCount.BackColor = Color.Transparent;
            lbl_OnScreenCount.ForeColor = Color.Black;
            lbl_onBus.BackColor = Color.Transparent;
            lbl_onBus.ForeColor = Color.Black;
            btn_Validate.Click += Btn_Validate_Click;
            btn_RunStop.Click += Btn_RunStop_Click;
            tempTimer = new Timer();
            tempTimer.Interval = 200;
            tempTimer.Tick += TempTimer_Tick;
            tempTimer.Start();
            messageBuffer = new StringBuilder();
            messageQueue = new Queue<string>(MaxMessages);
            trackBar1.ValueChanged += TrackBar1_ValueChanged;

            #endregion

            tb_InitLatlon.TextChanged += TextBox1_TextChanged;


            trackBar_thrust.ValueChanged += trackBar_Thrust_ValueChanged;
            trackBar_PropulsionAngle.ValueChanged += trackBar_JetAngle_ValueChanged;

            _pipeManager = new PipeManager();
            _pipeManager.OnMessageReceived += PipeManager_OnMessageReceived;

            // Configure the single button for opening and closing the pipe
            btn_PipeToggle.Click += Btn_PipeToggle_Click;
            UpdateButtonState();
            btn_setLatLonToUnity.Click += Btn_setLatLonToUnity_Click;

        }
        private async void trackBar_Thrust_ValueChanged(object sender, EventArgs e)
        {
            _myTHRUST = trackBar_thrust.Value;

            int THRUST_0_255 = (int)_myTHRUST * 255 / 100;
            vCinc_uc23.Value = THRUST_0_255;
            vCinc_uc24.Value = THRUST_0_255;
            await SendThrustAndAngles();
        }
        private async  void trackBar_JetAngle_ValueChanged(object sender, EventArgs e)
        {
            _myJET_ANG = trackBar_PropulsionAngle.Value;

            int JET_ANG_0_255 = (int)_myJET_ANG * 255 / 100;
            vCinc_uc20.Value = JET_ANG_0_255;
            vCinc_uc21.Value = JET_ANG_0_255;
            await SendThrustAndAngles();
        }
        // Event handler for setting the initial latitude and longitude to Unity
        private void Btn_setLatLonToUnity_Click(object sender, EventArgs e)
        {
            // Read the latitude and longitude from the textbox
            string latLonText = tb_InitLatlon.Text;
            string[] parts = latLonText.Split(',');

            if (parts.Length == 2)
            {
                if (double.TryParse(parts[0], out double lat) && double.TryParse(parts[1], out double lon))
                {
                    // Create the ShipCommands object
                    var shipCommands = new ShipCommands
                    {
                        ResetCoordinatesLat = lat,
                        ResetCoordinatesLon = lon
                    };

                    // Serialize to JSON
                    string jsonMessage = JsonConvert.SerializeObject(shipCommands);

                    // Send the JSON message to Unity
                    _ = _pipeManager.SendMessageAsync(jsonMessage);
                }
                else
                {
                    MessageBox.Show("Invalid latitude or longitude format. Please enter valid numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter the latitude and longitude in the format: lat,lon", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void PipeManager_OnMessageReceived(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => PipeManager_OnMessageReceived(message)));
                return;
            }

            try
            {
                // Deserialize and update only if data has changed significantly
                ShipStatus shipStatus = JsonConvert.DeserializeObject<ShipStatus>(message);
                if (shipStatus != null &&
                    (_unity_shiplat != shipStatus.ActualLat ||
                     _unity_shiplon != shipStatus.ActualLon ||
                     _unity_shipHeading != shipStatus.ShipHeading))
                {
                    _unity_shiplat = shipStatus.ActualLat;
                    _unity_shiplon = shipStatus.ActualLon;
                    _unity_shipHeading = shipStatus.ShipHeading;

                    // Update GPS values
                    vCinc_GPS1.SetShipLocationAndHeading(_unity_shiplat, _unity_shiplon, _unity_shipHeading);
                }
            }
            catch (JsonSerializationException ex)
            {
                Debug.WriteLine($"[DEBUG] Error deserializing JSON: {ex.Message}");
            }
        }


        // Event handler for toggling the pipe connection
        private async void Btn_PipeToggle_Click(object sender, EventArgs e)
        {
            if (_pipeIsOpen)
            {
                // Close the pipe
                _pipeManager.StopPipeServer();
                _pipeIsOpen = false;
            }
            else
            {
                // Open the pipe
                await _pipeManager.StartPipeServer();
                _pipeIsOpen = true;
            }
            UpdateButtonState();
        }

        // Updates the button state based on the pipe connection status
        private void UpdateButtonState()
        {
            if (_pipeIsOpen)
            {
                btn_PipeToggle.Text = "Close Pipe";
                btn_PipeToggle.BackColor = Color.Green;
            }
            else
            {
                btn_PipeToggle.Text = "Open Pipe";
                btn_PipeToggle.BackColor = Color.Red;
            }
        }


        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (_pipeIsOpen)
            {
                //do nothing, we re sending the heading as part of the unity pipe message
            }
            else
            {
                // Update the ship heading in the GPS control
                vCinc_GPS1.SetShipHeading(trackBar1.Value);
            }
        }

   
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string _strInpiut = tb_InitLatlon.Text;
            //the inpu looks like " 42.0121,-12.005"   
            string[] _strArray = _strInpiut.Split(',');
            if (_strArray.Length == 2)
            {
                _unity_shiplat = Convert.ToDouble(_strArray[0]);
                _unity_shiplon = Convert.ToDouble(_strArray[1]);
                if (!_pipeIsOpen)
                {
                    vCinc_GPS1.SetShipLocation(_unity_shiplat, _unity_shiplon);
                }
            }
           //if piped, we dont send this to gps , we send unity pipe message
            if (!_pipeIsOpen)
            {
                label5.ForeColor = Color.Black;
            }
            else
            {
                label5.ForeColor = Color.Red;
            }
            label5.Text = _unity_shiplat.ToString() + " " + _unity_shiplon.ToString();
        }
        #region TemplateFunctions
        private void TempTimer_Tick(object sender, EventArgs e)
        {
        
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


            _myWPlat = vCinc_GPS1.Get_purpleDot_LAT();
            _myWPlon = vCinc_GPS1.Get_purpleDot_LON();

            if (!_isOnCanBus) { return; }
            if (_OScreenCount == 0) { return; }
            if (_myPGNManager != null)
            {
                
       
                _myPGNManager.LoadByteArraysForGroups();
                var pgnByteArrays = _myPGNManager.GetPgnByteArrays();
                foreach (var entry in pgnByteArrays.Values)
                {
                   
                    int pgn = entry.pgn;
                    if (pgn == 0x09F8017F)
                    {
                       // Debug.WriteLine("foundone");
                        KvsrManager.Instance.SendPGN_withStatus(1, pgn, vCinc_GPS1.Get_PGNdata_CMDCOORDINATES_09F8017F()); 
                    }
                    else { 
                    
                    
                    byte[] data = entry.data;
                    KvsrManager.Instance.SendPGN_withStatus(1, pgn, data);
                    }
                }
            }
            else
            {
                Debug.WriteLine("[DEBUG] PGN Manager is not initialized");
            }
     
        }
        private async Task SendThrustAndAngles()
        {
            var thrustAndAngles = new
            {
                Thrust = _myTHRUST,
                Jet1Angle = _myJET_ANG,
                Jet2Angle = _myJET_ANG,
                WaypointLat = _myWPlat,
                WaypointLon = _myWPlon
            };

            string message = JsonConvert.SerializeObject(thrustAndAngles);
            await _pipeManager.SendMessageAsync(message);
        }

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
        #endregion
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_pipeIsOpen)
            {
                _pipeManager.StopPipeServer();
            }
            if (_isOnCanBus) { 
            
             KvsrManager.Instance.Close();
             KvsrManager.Instance.OnMessageReceived -= KvsrManager_OnMessageReceived;
            }
            base.OnFormClosing(e);
            Debug.WriteLine("[DEBUG] CAN manager closed and resources cleaned up.");
        }

    }
}
