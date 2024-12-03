using System;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VCI_Forms_SPN
{
    public partial class SerialForm : Form
    {



        // Synchronize NumericUpDown with TrackBar values
        //numericUpDownPortBucket.ValueChanged += (s, e) => { trackBar1.Value = (int)numericUpDownPortBucket.Value; };
        //numericUpDownStbdBucket.ValueChanged += (s, e) => { trackBar2.Value = (int)numericUpDownStbdBucket.Value; };
        //numericUpDownPortNoz.ValueChanged += (s, e) => { trackBar3.Value = (int)numericUpDownPortNoz.Value; };
        //numericUpDownStbdNoz.ValueChanged += (s, e) => { trackBar4.Value = (int)numericUpDownStbdNoz.Value; };
        //numericUpDownPortInterceptor.ValueChanged += (s, e) => { trackBar5.Value = (int)numericUpDownPortInterceptor.Value; };
        //numericUpDownStbdInterceptor.ValueChanged += (s, e) => { trackBar6.Value = (int)numericUpDownStbdInterceptor.Value; };
        private SerialPort serialPort;
        private bool isPortOpen = false;
        Timer looptimer;
        Timer pingPongTimer;  // Timer for ping-pong effect
        bool _doAutosend = false;
        bool increasing1 = true, increasing2 = true, increasing3 = true, increasing4 = true, increasing5 = true, increasing6 = true; // Direction flags for each trackbar

        Random rnd;

        public SerialForm()
        {
            looptimer = new Timer();
            pingPongTimer = new Timer(); // Initialize ping-pong Timer

            rnd = new Random();

            InitializeComponent();
            GetAvailablePorts();
            buttonStartStop.Click += buttonStartStop_Click;
            buttonSend.Click += buttonSend_Click;
            this.FormClosing += SerialForm_FormClosing;
            checkBox_autosend.CheckedChanged += checkBox_autosend_CheckedChanged;
            checkBox_pingPong.CheckedChanged += CheckBox_pingPong_CheckedChanged;  

            // PingPong Timer Interval
            pingPongTimer.Interval = 10; // Adjust for the desired speed of the effect
            pingPongTimer.Tick += PingPongTimer_Tick;



            // Initial Timer Interval
            looptimer.Interval = (int)numupdown_sendrate.Value;
            looptimer.Tick += looptimer_Tick;
            looptimer.Start();

            // Handle numericUpDown_sendrate change to update timer interval
            numupdown_sendrate.ValueChanged += NumericUpDown_sendrate_ValueChanged;

            trackBar1.ValueChanged += TrackBar1_ValueChanged;
            trackBar2.ValueChanged += TrackBar2_ValueChanged;
            trackBar3.ValueChanged += TrackBar3_ValueChanged;
            trackBar4.ValueChanged += TrackBar4_ValueChanged;
            trackBar5.ValueChanged += TrackBar5_ValueChanged;
            trackBar6.ValueChanged += TrackBar6_ValueChanged;
        }
        private void PingPongTimer_Tick(object sender, EventArgs e)
        {
            // Ping-Pong logic for each trackbar
            PingPongTrackBar(trackBar1, ref increasing1);
            PingPongTrackBar(trackBar2, ref increasing2);
            PingPongTrackBar(trackBar3, ref increasing3);
            PingPongTrackBar(trackBar4, ref increasing4);
            PingPongTrackBar(trackBar5, ref increasing5);
            PingPongTrackBar(trackBar6, ref increasing6);
        }
        private void PingPongTrackBar(System.Windows.Forms.TrackBar trackBar, ref bool increasing)
        {
            if (trackBar == null) // Check if trackBar is valid
            {
                throw new ArgumentNullException(nameof(trackBar), "TrackBar cannot be null.");
            }

            // Adjust the value based on the direction
            if (increasing)
            {
                trackBar.Value = Math.Min(trackBar.Value + 5, trackBar.Maximum);
                if (trackBar.Value >= trackBar.Maximum)
                {
                    increasing = false; // Change direction
                }
            }
            else
            {
                trackBar.Value = Math.Max(trackBar.Value - 5, trackBar.Minimum);
                if (trackBar.Value <= trackBar.Minimum)
                {
                    increasing = true; // Change direction
                }
            }
        }

        private void CheckBox_pingPong_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_pingPong.Checked)
            {
                pingPongTimer.Start(); // Start the ping-pong effect
            }
            else
            {
                pingPongTimer.Stop(); // Stop the ping-pong effect
            }
        }


        private void TrackBar6_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownStbdInterceptor.Value = trackBar6.Value;
        }

        private void TrackBar5_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownPortInterceptor.Value = trackBar5.Value;
        }

        private void TrackBar4_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownStbdNoz.Value = trackBar4.Value;
        }

        private void TrackBar3_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownPortNoz.Value = trackBar3.Value;
        }

        private void TrackBar2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownStbdBucket.Value = trackBar2.Value;
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownPortBucket.Value = trackBar1.Value;
        }

        private void NumericUpDown_sendrate_ValueChanged(object sender, EventArgs e)
        {
            looptimer.Interval = (int)numupdown_sendrate.Value;
        }

        private async void looptimer_Tick(object sender, EventArgs e)
        {
            if (!_doAutosend || !isPortOpen) return;

            try
            {
                // Run the data sending on a separate thread to avoid blocking the UI
                await Task.Run(() =>
                {
                    // Collect values for sending
                    int portNoz = (int)numericUpDownPortNoz.Value;
                    int stbdNoz = (int)numericUpDownStbdNoz.Value;
                    int portBucket = (int)numericUpDownPortBucket.Value;
                    int stbdBucket = (int)numericUpDownStbdBucket.Value;
                    int portInterceptor = (int)numericUpDownPortInterceptor.Value;
                    int stbdInterceptor = (int)numericUpDownStbdInterceptor.Value;

                    // Construct the message
                    string messageWithoutChecksum = $"$PVCI,{portBucket},{stbdBucket},{portNoz},{stbdNoz},{portInterceptor},{stbdInterceptor},7,8,9,10,11,12,13,14,15,16,17,18";

                    // Calculate checksum
                    string checksum = CalculateChecksum(messageWithoutChecksum);

                    // Final message
                    string fullMessage = $"{messageWithoutChecksum}*{checksum}\n";

                    // Send the message to the serial port
                    serialPort.Write(fullMessage);

                    // Update the UI safely from the background thread
                    Invoke(new Action(() =>
                    {
                        textBoxSend.Clear();
                        textBoxSend.AppendText(fullMessage);  // Display the message in the textbox
                    }));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending auto data: " + ex.Message);
            }
        }

        private void checkBox_autosend_CheckedChanged(object sender, EventArgs e)
        {
            _doAutosend = checkBox_autosend.Checked;
        }

        private void GetAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();
            comboBoxPorts.Items.AddRange(ports);
            if (ports.Length > 0)
                comboBoxPorts.SelectedIndex = 0;
        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            if (!isPortOpen)
            {
                try
                {
                    serialPort = new SerialPort(comboBoxPorts.SelectedItem.ToString(), 19200, Parity.None, 8, StopBits.One);
                    serialPort.ReadTimeout = 500;
                    serialPort.WriteTimeout = 500;
                    serialPort.NewLine = "\r\n";
                    serialPort.DataReceived += SerialPort_DataReceived;
                    serialPort.Open();
                    isPortOpen = true;
                    buttonStartStop.Text = "Stop";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening port: " + ex.Message);
                }
            }
            else
            {
                serialPort.DataReceived -= SerialPort_DataReceived;
                serialPort.Close();
                isPortOpen = false;
                buttonStartStop.Text = "Start";
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string message = serialPort.ReadExisting();
                Invoke(new Action(() =>
                {
                    textBoxReceive.AppendText(message);
                }));
            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show("Error reading from port: " + ex.Message);
                }));
            }
        }

        private void buttonSend_Click_textbox(object sender, EventArgs e)
        {
            if (isPortOpen)
            {
                try
                {
                    string textToSend = textBoxSend.Text;
                    serialPort.WriteLine(textToSend);
                    textBoxSend.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending data: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Serial port is not open.");
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (isPortOpen)
            {
                try
                {
                    int portBucket = (int)numericUpDownPortBucket.Value;
                    int stbdBucket = (int)numericUpDownStbdBucket.Value;
                    int portNoz = (int)numericUpDownPortNoz.Value;
                    int stbdNoz = (int)numericUpDownStbdNoz.Value;
                    int portInterceptor = (int)numericUpDownPortInterceptor.Value;
                    int stbdInterceptor = (int)numericUpDownStbdInterceptor.Value;

                    string messageWithoutChecksum = $"$PVCI,{portBucket},{stbdBucket},{portNoz},{stbdNoz},{portInterceptor},{stbdInterceptor}";

                    string checksum = CalculateChecksum(messageWithoutChecksum);
                    string fullMessage = $"{messageWithoutChecksum}*{checksum}\r\n";

                    serialPort.Write(fullMessage);
                    textBoxSend.Clear();
                    textBoxSend.AppendText(fullMessage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending data: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Serial port is not open.");
            }
        }

        private void SerialForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isPortOpen)
            {
                serialPort.DataReceived -= SerialPort_DataReceived;
                serialPort.Close();
            }
        }

        private string CalculateChecksum(string sentence)
        {
            if (sentence.StartsWith("$"))
                sentence = sentence.Substring(1);

            int checksum = 0;
            foreach (char c in sentence)
            {
                if (c == '*')
                    break;
                checksum ^= Convert.ToByte(c);
            }
            return checksum.ToString("X2");
        }

    }
}
