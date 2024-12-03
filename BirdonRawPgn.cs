using Newtonsoft.Json;
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
using VCI_Forms_SPN._BackEndDataOBJs;
using VCI_Forms_SPN._GLobalz;
using VCI_Forms_SPN._Managers;

namespace VCI_Forms_SPN
{
    public partial class BirdonRawPgn : Form
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
        VC_LOCATION VESSEL_LOC;
        double VESSEL_HEADING;
        VC_LOCATION WAYPOINT_LOC;
        private PipeManager _pipeManager;
        private bool _pipeIsOpen = false;
        private Timer pipeTimer;
        private readonly object _syncLock = new object();
        double _myJET_ANG = 50;
        double _myTHRUST = 0;
        public BirdonRawPgn()
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
            VESSEL_LOC = new VC_LOCATION();
            WAYPOINT_LOC = new VC_LOCATION();
         
       

            pipeTimer = new Timer();
            pipeTimer.Interval = 320;
            pipeTimer.Tick += PipeTimer_Tick;

            _pipeManager = new PipeManager();
            _pipeManager.OnMessageReceived += PipeManager_OnMessageReceived;

          
 
            
        

            InitializeControls_StationControls();
        }

        public void InitializeControls_StationControls()
        {
         
        }
        void Read_Controls()
        {

    


        }
    

        private void PipeManager_OnMessageReceived(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => PipeManager_OnMessageReceived(message)));
                return;
            }
            try
            {
                if (!_pipeIsOpen)
                {
                    return;
                }
                ShipStatus shipStatus = JsonConvert.DeserializeObject<ShipStatus>(message);
                if (shipStatus != null)
                {
                    lock (_syncLock)
                    {
                        VESSEL_LOC.Latitude = shipStatus.ActualLat;
                        VESSEL_LOC.Longitude = shipStatus.ActualLon;
                        VESSEL_HEADING = shipStatus.ShipHeading;
                    }
                }
            }
            catch (JsonSerializationException ex)
            {
                Debug.WriteLine($"[DEBUG] Error deserializing JSON: {ex.Message}");
            }
        }
  
       
    
        private async void PipeTimer_Tick(object sender, EventArgs e)
        {
            var dataToSend = new
            {
                Thrust = _myTHRUST,
                Jet1Angle = _myJET_ANG,
                Jet2Angle = _myJET_ANG,
                WaypointLat = WAYPOINT_LOC.Latitude,
                WaypointLon = WAYPOINT_LOC.Longitude
            };
            string message = JsonConvert.SerializeObject(dataToSend);
            await _pipeManager.SendMessageAsync(message);
        }
     
   
        private void Looptimer_Tick(object sender, EventArgs e)
        {
            lock (_syncLock)
            {
         
                Read_Controls();
                //PGN_Controlled();

 
           
          
                SendAllPgnMessages();
            }
        }
  
        #region can and stuff
        private void KvsrManager_OnMessageReceived(string message)
        {
            if (IsDisposed || Disposing)
                return;
            if (InvokeRequired)
            {
                try
                {
                    Invoke(new Action(() => KvsrManager_OnMessageReceived(message)));
                }
                catch (ObjectDisposedException)
                {
                    Debug.WriteLine("Attempted to invoke on a disposed object.");
                }
                return;
            }
            string id = message.Substring(3, 8);
            if (uniqueMessages.ContainsKey(id))
            {
                uniqueMessages[id] = message;
            }
            else
            {
                uniqueMessages.Add(id, message);
            }
            string _uniquePgns = string.Join(Environment.NewLine, uniqueMessages.Values);
            string _quedPgns = string.Join(Environment.NewLine, messageQueue);
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
            int busNumber = 1;
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
            if (_myPGNManager != null)
            {
                _myPGNManager.LoadByteArraysForGroups();
                var pgnByteArrays = _myPGNManager.GetPgnByteArrays();
                foreach (var entry in pgnByteArrays.Values)
                {
                    KvsrManager.Instance.SendPGN_withStatus(1, entry.pgn, entry.data);
                }
            }
            else
            {
                Debug.WriteLine("[DEBUG] PGN Manager is not initialized");
            }
         
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_pipeIsOpen)
            {
                _pipeManager.StopPipeServer();
                pipeTimer.Stop();
            }
            looptimer.Stop();
            if (_isOnCanBus)
            {
                _isOnCanBus = false;
                KvsrManager.Instance.OnMessageReceived -= KvsrManager_OnMessageReceived;
                KvsrManager.Instance.Close();
            }
            base.OnFormClosing(e);
            Debug.WriteLine("[DEBUG] CAN manager closed and resources cleaned up.");
        }
        #endregion

    }
}
