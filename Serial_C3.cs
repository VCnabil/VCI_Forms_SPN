using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VCI_Forms_SPN
{
    public partial class Serial_C3 : Form
    {
        private SerialPort serialPort;
        private bool isPortOpen = false;
        Timer looptimer;
        bool _doAutosend = false;
        private void GetAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();
            comboBoxPorts.Items.AddRange(ports);
            if (ports.Length > 0)
                comboBoxPorts.SelectedIndex = 0;
        }

        public Serial_C3()
        {
            looptimer = new Timer();
            InitializeComponent();
            GetAvailablePorts();
            buttonStartStop.Click += buttonStartStop_Click;
            buttonSend.Click += buttonSend_Click;
            this.FormClosing += SerialForm_FormClosing;
            checkBox_autosend.CheckedChanged += checkBox_autosend_CheckedChanged;
            

            // Initial Timer Interval
            looptimer.Interval = (int)numupdown_sendrate.Value;
            looptimer.Tick += looptimer_Tick;
            looptimer.Start();
            numupdown_sendrate.ValueChanged += NumericUpDown_sendrate_ValueChanged;
            btnLabjack.Click += BtnLabjack_Click;
 
        }

        private void BtnLabjack_Click(object sender, EventArgs e)
        {
            LabJ_v1 formjack = new LabJ_v1();
            formjack.Show();
            btnLabjack.Enabled = false;
        }

        private void checkBox_autosend_CheckedChanged(object sender, EventArgs e)
        {
            _doAutosend = checkBox_autosend.Checked;
        }

        private void NumericUpDown_sendrate_ValueChanged(object sender, EventArgs e)
        {
            looptimer.Interval = (int)numupdown_sendrate.Value;
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
        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (isPortOpen)
            {
                try
                {
                    //$PVCI,999,999,778,999,0,0,13,2,14,0,0,0,0,3,0,0,65*24 <----here is a real example of message 
                    //      pBu,sBu,pNo,sNo,0, 0,,13,2,14,0,0,0,0,3,0,0,65 

                    // Simulated values for the other parameters
                    //int portNoz = 999;
                    //int stbdNoz = 999;
                    //int portBucket = 888;
                    //int stbdBucket = 777;
                    //int _UNUSED_portTAB = 0;
                    //int _UNUSED_stbdTAB = 0;
                    //int XMIT_SensorFaultError = 13;
                    //int XMIT_NonFollowError = 2;
                    //int XMIT_CmdFaultError_STA1 =14;
                    //int XMIT_CmdFaultError_STA2 = 0;
                    //int XMIT_CmdFaultError_STA3 = 0;
                    //int XMIT_Autocal_TrimRoll_Error = 0;
                    //int XMIT_Dockmode_Interlock_Error = 3;
                    //int cXMIT_StopRequest = 0;
                    //int cXMIT_Status_CT = 0;
                    //int XMIT_CANFault = 0;
             

                    int XMIT_PortNoz_Scaled = ucXMIT_PortNoz_Scaled.Value;
                    int XMIT_StbdNoz_Scaled = ucXMIT_StbdNoz_Scaled.Value;
                    int XMIT_PortBkt_Scaled = ucXMIT_PortBkt_Scaled.Value;
                    int XMIT_StbdBkt_Scaled = ucXMIT_StbdBkt_Scaled.Value;
                    int _UNUSED_portTAB = 0;
                    int _UNUSED_stbdTAB = 0;
                    int XMIT_SensorFaultError = ucXMIT_SensorFaultError.Value;
                    int XMIT_NonFollowError = ucXMIT_NonFollowError.Value;
                    int XMIT_CmdFaultError_STA1 = ucXMIT_CmdFaultError_STA1.Value;
                    int XMIT_CmdFaultError_STA2 = ucXMIT_CmdFaultError_STA2.Value;
                    int XMIT_CmdFaultError_STA3 = ucXMIT_CmdFaultError_STA3.Value;
                    int XMIT_Autocal_TrimRoll_Error = ucXMIT_Autocal_TrimRoll_Error.Value;
                    int XMIT_Dockmode_Interlock_Error = ucXMIT_Dockmode_Interlock_Error.Value;
                    int cXMIT_StopRequest = 0;
                    int cXMIT_Status_CT = 0;
                    int XMIT_CANFault = ucXMIT_CANFault.Value;

                    string messageWithoutChecksum = $"$PVCI,{XMIT_PortBkt_Scaled},{XMIT_StbdBkt_Scaled},{XMIT_PortNoz_Scaled},{XMIT_StbdNoz_Scaled},{_UNUSED_portTAB},{_UNUSED_stbdTAB},{XMIT_SensorFaultError},{XMIT_NonFollowError},{XMIT_CmdFaultError_STA1},{XMIT_CmdFaultError_STA2},{XMIT_CmdFaultError_STA3},{XMIT_Autocal_TrimRoll_Error},{XMIT_Dockmode_Interlock_Error},{cXMIT_StopRequest},{cXMIT_Status_CT},{XMIT_CANFault}";

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

        private async void looptimer_Tick(object sender, EventArgs e)
        {
            if (!_doAutosend || !isPortOpen) return;

            try
            {
                // Run the data sending on a separate thread to avoid blocking the UI
                await Task.Run(() =>
                {
                    int XMIT_PortNoz_Scaled = ucXMIT_PortNoz_Scaled.Value;
                    int XMIT_StbdNoz_Scaled = ucXMIT_StbdNoz_Scaled.Value;
                    int XMIT_PortBkt_Scaled = ucXMIT_PortBkt_Scaled.Value;
                    int XMIT_StbdBkt_Scaled = ucXMIT_StbdBkt_Scaled.Value;
                    int _UNUSED_portTAB = 0;
                    int _UNUSED_stbdTAB = 0;
                    int XMIT_SensorFaultError = ucXMIT_SensorFaultError.Value;
                    int XMIT_NonFollowError = ucXMIT_NonFollowError.Value;
                    int XMIT_CmdFaultError_STA1 = ucXMIT_CmdFaultError_STA1.Value;
                    int XMIT_CmdFaultError_STA2 = ucXMIT_CmdFaultError_STA2.Value;
                    int XMIT_CmdFaultError_STA3 = ucXMIT_CmdFaultError_STA3.Value;
                    int XMIT_Autocal_TrimRoll_Error = ucXMIT_Autocal_TrimRoll_Error.Value;
                    int XMIT_Dockmode_Interlock_Error = ucXMIT_Dockmode_Interlock_Error.Value;
                    int cXMIT_StopRequest = 0;
                    int cXMIT_Status_CT = 0;
                    int XMIT_CANFault = ucXMIT_CANFault.Value;
                

                    string messageWithoutChecksum = $"$PVCI,{XMIT_PortBkt_Scaled},{XMIT_StbdBkt_Scaled},{XMIT_PortNoz_Scaled},{XMIT_StbdNoz_Scaled},{_UNUSED_portTAB},{_UNUSED_stbdTAB},{XMIT_SensorFaultError},{XMIT_NonFollowError},{XMIT_CmdFaultError_STA1},{XMIT_CmdFaultError_STA2},{XMIT_CmdFaultError_STA3},{XMIT_Autocal_TrimRoll_Error},{XMIT_Dockmode_Interlock_Error},{cXMIT_StopRequest},{cXMIT_Status_CT},{XMIT_CANFault}";

                    string checksum = CalculateChecksum(messageWithoutChecksum);
                    string fullMessage = $"{messageWithoutChecksum}*{checksum}\n";
                    serialPort.Write(fullMessage);
                    Invoke(new Action(() =>
                    {
                        textBoxSend.Clear();
                        textBoxSend.AppendText(fullMessage); 
                    }));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending auto data: " + ex.Message);
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


        private void SerialForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isPortOpen)
            {
                serialPort.DataReceived -= SerialPort_DataReceived;
                serialPort.Close();
            }
        }

    }

}
