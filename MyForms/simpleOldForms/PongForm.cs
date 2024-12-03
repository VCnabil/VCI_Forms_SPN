using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VCI_Forms_SPN._GLobalz;
using VCI_Forms_SPN._Managers;

namespace VCI_Forms_SPN.MyForms
{
    public partial class PongForm : Form
    {
        private Rectangle ball;
        private Rectangle paddleP1;
        private Rectangle paddleP2;
        private int ballSpeedX = 4;
        private int ballSpeedY = 4;
        private Timer gameTimer;

        #region TemplateVariables
        PGN_MANAGER _myPGNManager;
        Queue<string> messageQueue;
        StringBuilder messageBuffer;
        const int MaxMessages = 12;
        int _OScreenCount = 0;
        bool _isOnCanBus;
        Dictionary<string, string> uniqueMessages = new Dictionary<string, string>();
        #endregion

        private bool isGameStarte;
        public PongForm()
        {
            InitializeComponent();

            // Enable double buffering for panel1
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, panel1, new object[] { true });

            // Initialize ball and paddles
            ball = new Rectangle(panel1.Width / 2 - 5, panel1.Height / 2 - 5, 10, 10);
            paddleP1 = new Rectangle(10, tb_P1.Value, 10, 100);
            paddleP2 = new Rectangle(panel1.Width - 20, tb_P2.Value, 10, 100);

            // Set up Paint event for the panel
            panel1.Paint += Panel1_Paint;

            // Initialize game timer
            gameTimer = new Timer
            {
                Interval = 24 // ~60 FPS
            };
            gameTimer.Tick += GameLoop;

            // Start/stop button handler
            btn_Start.Click += (sender, e) => ToggleGameStarted();

            #region TemplateInitialize
            cb_uniqueOn.Checked = true;
            lbl_OnScreenCount.BackColor = Color.Transparent;
            lbl_OnScreenCount.ForeColor = Color.Black;
            lbl_onBus.BackColor = Color.Transparent;
            lbl_onBus.ForeColor = Color.Black;
            btn_Validate.Click += Btn_Validate_Click;
            btn_RunStop.Click += Btn_RunStop_Click;
 
            messageBuffer = new StringBuilder();
            messageQueue = new Queue<string>(MaxMessages);
            KvsrManager.Instance.OnMessageReceived += KvsrManager_OnMessageReceived;
            #endregion
        }
        #region TemplateFunctions
        private void ToggleGameStarted()
        {
            isGameStarte = !isGameStarte;

            if (isGameStarte)
            {
                gameTimer.Start();
                btn_Start.Text = "Stop Pong";
            }
            else
            {
                gameTimer.Stop();
                btn_Start.Text = "Start Pong";
            }
        }
        private void GameLoop(object sender, EventArgs e)
        {
            // Update paddle positions
            paddleP1.Y = tb_P1.Value;
            paddleP2.Y = tb_P2.Value;

            // Move the ball
            ball.X += ballSpeedX;
            ball.Y += ballSpeedY;

            // Handle ball collisions
            HandleCollisions();

            // Update UI elements
            UpdateLabelsAndControls();

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
            if (_OScreenCount == 0) { return; }
            if (_myPGNManager != null)
            {

                _myPGNManager.LoadByteArraysForGroups();
                var pgnByteArrays = _myPGNManager.GetPgnByteArrays();
                foreach (var entry in pgnByteArrays.Values)
                {
                    int pgn = entry.pgn;
                    byte[] data = entry.data;
                    KvsrManager.Instance.SendPGN_withStatus(1, pgn, data);
                }
            }
            else
            {
                Debug.WriteLine("[DEBUG] PGN Manager is not initialized");
            }

            // Redraw the panel
            panel1.Invalidate();
        }
        private void HandleCollisions()
        {
            // Ball collision with top and bottom
            if (ball.Top <= 0 || ball.Bottom >= panel1.Height)
                ballSpeedY = -ballSpeedY;

            // Ball collision with paddles
            if (ball.IntersectsWith(paddleP1) || ball.IntersectsWith(paddleP2))
                ballSpeedX = -ballSpeedX;

            // Ball out of bounds
            if (ball.Left <= 0 || ball.Right >= panel1.Width)
            {
                ball.X = panel1.Width / 2 - 5;
                ball.Y = panel1.Height / 2 - 5;
            }
        }
        private void UpdateLabelsAndControls()
        {
         

            if (checkBox1.Checked)
            {
                // Update VCI user controls with pong values
                vCinc_BallX.Value = ball.X;
                vCinc_BallY.Value = ball.Y;
                vCinc_P1y.Value = paddleP1.Y;
                vCinc_P2y.Value = paddleP2.Y;

                //labels
                lbl_ballXpos.Text = $"Ball X: {vCinc_BallX.Value}";
                lbl_ballYpos.Text = $"Ball Y: {vCinc_BallY.Value}";
                lbl_P1pos.Text = $"P1: {vCinc_P1y.Value}";
                lbl_P2pos.Text = $"P2: {vCinc_P2y.Value}";
            }
            else {
                lbl_ballXpos.Text = $"Ball X: {ball.X}";
                lbl_ballYpos.Text = $"Ball Y: {ball.Y}";
                lbl_P1pos.Text = $"P1: {paddleP1.Y}";
                lbl_P2pos.Text = $"P2: {paddleP2.Y}";
            }
        }


  
        private void KvsrManager_OnMessageReceived(string message)
        {
            Debug.WriteLine($"[DEBUG] received: {message}");
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
            if (_isOnCanBus)
            {

                KvsrManager.Instance.Close();
                KvsrManager.Instance.OnMessageReceived -= KvsrManager_OnMessageReceived;
            }
            base.OnFormClosing(e);
            Debug.WriteLine("[DEBUG] CAN manager closed and resources cleaned up.");
        }
      

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Clear the panel
            g.Clear(Color.Black);

            // Draw paddles
            using (Brush redBrush = new SolidBrush(Color.Red))
                g.FillRectangle(redBrush, paddleP1);
            using (Brush blueBrush = new SolidBrush(Color.Blue))
                g.FillRectangle(blueBrush, paddleP2);

            // Draw ball
            using (Brush orangeBrush = new SolidBrush(Color.Orange))
                g.FillRectangle(orangeBrush, ball);




        }
    }
}
