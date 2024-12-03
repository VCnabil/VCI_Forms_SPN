using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            checkBox_autosend.Checked = true;
            _doAutosend = checkBox_autosend.Checked;


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

                    string onetimemessage = textBoxSend.Text;
                    serialPort.Write(onetimemessage);
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
                    decodeSerialMessage(message);
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


        int _Intsteer_Enable = 0;
        int _Autocal_CMD = 0;
        int _Set1_Set2_Mode = 0;
        int _Position_Capture_Request = 0;
        void decodeSerialMessage1(string msg) {

            //PVCC,1,0,0,0,0,0,0,0*07
            //NMEA_HEADER, Intsteer_Enable, Autocal_CMD, Set1_Set2_Mode, Position_Capture_Request,0 ,0 ,0 ,0  * checksum

            lbl_Intsteer_Enable.Text = _Intsteer_Enable.ToString();
            lbl_Autocal_CMD.Text = _Autocal_CMD.ToString();
            lbl_Set1_Set2_Mode.Text = _Set1_Set2_Mode.ToString();
            lbl_Position_Capture_Request.Text = _Position_Capture_Request.ToString();
        }

        void decodeSerialMessage(string msg)
        {
            int checksumIndex = msg.IndexOf('*');
            if (checksumIndex != -1)
            {
                msg = msg.Substring(0, checksumIndex); 
            }
            string[] parts = msg.Split(',');
            if (parts.Length >= 5 && parts[0] == "$PVCC")
            {
                
                int.TryParse(parts[1], out _Intsteer_Enable);
                int.TryParse(parts[2], out _Autocal_CMD);
                int.TryParse(parts[3], out _Set1_Set2_Mode);
                int.TryParse(parts[4], out _Position_Capture_Request);

                lbl_Intsteer_Enable.Text = _Intsteer_Enable.ToString();
                lbl_Autocal_CMD.Text = _Autocal_CMD.ToString();
                lbl_Set1_Set2_Mode.Text = _Set1_Set2_Mode.ToString();
                lbl_Position_Capture_Request.Text = _Position_Capture_Request.ToString();
                Debug.WriteLine("OK.");

                if (_Autocal_CMD == 11)
                {
                    lbl_Command.Text = "init autocal";
                }
                else if (_Autocal_CMD == 22)
                {
                    lbl_Command.Text = "finish autocal";
                }
                else if (_Autocal_CMD == 33)
                {
                    lbl_Command.Text = "abort autocal";
                }
                else if (_Autocal_CMD == 66 || _Autocal_CMD == 88)
                {
                    lbl_Command.Text = "pos cap req acked";
                }
                else {
                    lbl_Command.Text = "autocal Off";
                }
            }
            else
            {
               Debug.WriteLine("Invalid message format received.");
            }
        }

        private async void looptimer_Tick(object sender, EventArgs e)
        {
            if (!_doAutosend || !isPortOpen) return;

            try
            {
                if (chkToggleImplementation.Checked)
                {
                    await RunImplementation1();
                }
                else
                {
                    if (cb_c3iConfig.Checked)
                    {
                        await RunImplementation3();
                    }
                    else { 
                    
                    await RunImplementation2();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending auto data: " + ex.Message);
            }
        }

        private async Task RunImplementation1()
        {
            await Task.Run(() =>
            {
                int XMIT_1_PortNoz_Scaled = ucXMIT_PortNoz_Scaled.Value;
                int XMIT_2_StbdNoz_Scaled = ucXMIT_StbdNoz_Scaled.Value;
                int XMIT_3_PortBkt_Scaled = ucXMIT_PortBkt_Scaled.Value;
                int XMIT_4_StbdBkt_Scaled = ucXMIT_StbdBkt_Scaled.Value;
                int XMIT_5_PortTab_Scaled = ucXMIT_PortTab_Scaled.Value;
                int XMIT_6_StbdTab_Scaled = ucXMIT_StbdTab_Scaled.Value;
                int XMIT_7_SensorFaultError = ucXMIT_SensorFaultError.Value;
                int XMIT_8_NonFollowError = ucXMIT_NonFollowError.Value;
                int XMIT_9_CmdFaultError_STA1 = ucXMIT_CmdFaultError_STA1.Value;
                int XMIT_10CmdFaultError_STA2 = ucXMIT_CmdFaultError_STA2.Value;
                int XMIT_11CmdFaultError_STA3 = ucXMIT_CmdFaultError_STA3.Value;
                int XMIT_12Autocal_TrimRoll_Error = ucXMIT_Autocal_TrimRoll_Error.Value;
                int XMIT_13Dockmode_Interlock_Error = ucXMIT_Dockmode_Interlock_Error.Value;
                int XMIT_14_Status_CT = uc_MODE.Value;
                int XMIT_15StopRequest = 0;
                int XMIT_16 = ucXMIT_16.Value;
                int XMIT_17 = ucXMIT_17.Value;
                int XMIT_18CANFault = ucXMIT_CANFault.Value;

                string messageWithoutChecksum = $"$PVCI," +
                $"{XMIT_1_PortNoz_Scaled}," +
                $"{XMIT_2_StbdNoz_Scaled}," +
                $"{XMIT_3_PortBkt_Scaled}," +
                $"{XMIT_4_StbdBkt_Scaled}," +
                $"{XMIT_5_PortTab_Scaled}," +
                $"{XMIT_6_StbdTab_Scaled}," +
                $"{XMIT_7_SensorFaultError}," +
                $"{XMIT_8_NonFollowError}," +
                $"{XMIT_9_CmdFaultError_STA1}," +
                $"{XMIT_10CmdFaultError_STA2}," +
                $"{XMIT_11CmdFaultError_STA3}," +
                $"{XMIT_12Autocal_TrimRoll_Error}," +
                $"{XMIT_13Dockmode_Interlock_Error}," +
                $"{XMIT_14_Status_CT}," +
                $"{XMIT_15StopRequest}," +
                $"{XMIT_16}," +
                $"{XMIT_17}," +
                $"{XMIT_18CANFault}";

                string checksum = CalculateChecksum(messageWithoutChecksum);
                string fullMessage = $"{messageWithoutChecksum}*{checksum}\r";
                serialPort.Write(fullMessage);
                Invoke(new Action(() =>
                {
                    textBoxSend.Clear();
                    textBoxSend.AppendText(fullMessage);
                }));
            });
        }
        //sending a sample string 
        // $PVCI,0,0,500,500,0,0,7,0,14,0,0,0,0,3,0,0,65,2*0C

        private async Task RunImplementation2()
        {
            await Task.Run(() =>
            {
                int XMIT_0_Insteer = uc0Cantrak_Intsteer_Enable.Value;
                int XMIT_1_CalCommand = uc1CT_Autocal_Flag.Value;
                int XMIT_2_Set12Mode = uc2CT_Set1_Set2_Mode.Value;
                int XMIT_3__Set12Flag = uc3CT_Set1_Set2_Flag.Value;
                int XMIT_4_z = 0;
                int XMIT_5_z = 0;
                int XMIT_6_z = 0;
                int XMIT_7_z = 0;
               // int XMIT_8_z = 0;

                string messageWithoutChecksum = $"$PVCC," +
                $"{XMIT_0_Insteer}," +
                $"{XMIT_1_CalCommand}," +
                $"{XMIT_2_Set12Mode}," +
                $"{XMIT_3__Set12Flag}," +
                $"{XMIT_4_z}," +
                $"{XMIT_5_z}," +
                $"{XMIT_6_z}," +
                $"{XMIT_7_z}";

                string checksum = CalculateChecksum(messageWithoutChecksum);
                string fullMessage = $"{messageWithoutChecksum}*{checksum}\r";
                serialPort.Write(fullMessage);
                Invoke(new Action(() =>
                {
                    textBoxSend.Clear();
                    textBoxSend.AppendText(fullMessage);
                }));
            });
        }

        //implementation3 sends "$config,1" 
        private async Task RunImplementation3()
        {
            await Task.Run(() =>
            {
                int XMIT_0_Config = vCinc_Config.Value;

                // int XMIT_8_z = 0;

                string messageWithoutChecksum = $"$config," +$"{XMIT_0_Config}";

               
                string fullMessage = $"{messageWithoutChecksum}\r";
                serialPort.Write(fullMessage);
                Invoke(new Action(() =>
                {
                    textBoxSend.Clear();
                    textBoxSend.AppendText(fullMessage);
                }));
            });
        }


        private async void looptimer_Tick1(object sender, EventArgs e)
        {
            if (!_doAutosend || !isPortOpen) return;

            try
            {
                // Run the data sending on a separate thread to avoid blocking the UI
                await Task.Run(() =>
                {
                    //1
                    int XMIT_1_PortNoz_Scaled = ucXMIT_PortNoz_Scaled.Value;
                    //2
                    int XMIT_2_StbdNoz_Scaled = ucXMIT_StbdNoz_Scaled.Value;
                    //3
                    int XMIT_3_PortBkt_Scaled = ucXMIT_PortBkt_Scaled.Value;
                    //4
                    int XMIT_4_StbdBkt_Scaled = ucXMIT_StbdBkt_Scaled.Value;
                    //5
                    int XMIT_5_PortTab_Scaled = ucXMIT_PortTab_Scaled.Value;
                    //6
                    int XMIT_6_StbdTab_Scaled = ucXMIT_StbdTab_Scaled.Value;

                    //faults

                    //7
                    int XMIT_7_SensorFaultError = ucXMIT_SensorFaultError.Value;
                    //8
                    int XMIT_8_NonFollowError = ucXMIT_NonFollowError.Value;
                    //9
                    int XMIT_9_CmdFaultError_STA1 = ucXMIT_CmdFaultError_STA1.Value;
                    //10
                    int XMIT_10CmdFaultError_STA2 = ucXMIT_CmdFaultError_STA2.Value;
                    //11
                    int XMIT_11CmdFaultError_STA3 = ucXMIT_CmdFaultError_STA3.Value;
                    //12
                    int XMIT_12Autocal_TrimRoll_Error = ucXMIT_Autocal_TrimRoll_Error.Value;  
                    //13
                    int XMIT_13Dockmode_Interlock_Error =  ucXMIT_Dockmode_Interlock_Error.Value;
                    //14
                    int XMIT_14_Status_CT = uc_MODE.Value;
                    //15
                    int XMIT_15StopRequest =  0;
               
                    int XMIT_16 = ucXMIT_16.Value;
                    //17
                    int XMIT_17 = ucXMIT_17.Value;
                    int XMIT_18CANFault = ucXMIT_CANFault.Value;
                   // string messageWithoutChecksum = $"$PVCI,{XMIT_PortBkt_Scaled},{XMIT_StbdBkt_Scaled},{XMIT_PortNoz_Scaled},{XMIT_StbdNoz_Scaled},{XMIT_PortTab_Scaled},{XMIT_StbdTab_Scaled},{XMIT_SensorFaultError},{XMIT_NonFollowError},{XMIT_CmdFaultError_STA1},{XMIT_CmdFaultError_STA2},{XMIT_CmdFaultError_STA3},{XMIT_Autocal_TrimRoll_Error},{XMIT_Dockmode_Interlock_Error},{cXMIT_Status_CT},{cXMIT_StopRequest},{XMIT_CANFault}";
                    string messageWithoutChecksum = $"$PVCI," +
                    $"{XMIT_1_PortNoz_Scaled}," +
                    $"{XMIT_2_StbdNoz_Scaled}," +
                    $"{XMIT_3_PortBkt_Scaled}," +
                    $"{XMIT_4_StbdBkt_Scaled}," +
                    $"{XMIT_5_PortTab_Scaled}," +
                    $"{XMIT_6_StbdTab_Scaled}," +
                    $"{XMIT_7_SensorFaultError}," +
                    $"{XMIT_8_NonFollowError}," +
                    $"{XMIT_9_CmdFaultError_STA1}," +
                    $"{XMIT_10CmdFaultError_STA2}," +
                    $"{XMIT_11CmdFaultError_STA3}," +
                    $"{XMIT_12Autocal_TrimRoll_Error}," +
                    $"{XMIT_13Dockmode_Interlock_Error}," +
                    $"{XMIT_14_Status_CT}," +
                    $"{XMIT_15StopRequest}," +
                    $"{XMIT_16}," +
                    $"{XMIT_17}," +
                    $"{XMIT_18CANFault}";

           
                    string checksum = CalculateChecksum(messageWithoutChecksum);
                    string fullMessage = $"{messageWithoutChecksum}*{checksum}\r";
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
        private async void looptimer_Tick2(object sender, EventArgs e)
        {
            if (!_doAutosend || !isPortOpen) return;

            try
            {
                // Run the data sending on a separate thread to avoid blocking the UI
                await Task.Run(() =>
                {
                    int XMIT_0_Insteer = uc0Cantrak_Intsteer_Enable.Value;
                    int XMIT_1_CalCommand = uc1CT_Autocal_Flag.Value;
                    int XMIT_2_Set12Mode = uc2CT_Set1_Set2_Mode.Value;
                    int XMIT_3__Set12Flag = uc3CT_Set1_Set2_Flag.Value;
                    int XMIT_4_z = 0;
                    int XMIT_5_z = 0;
                    int XMIT_6_z = 0;
                    int XMIT_7_z = 0;



                    string messageWithoutChecksum = $"$PVCC," +
                    $"{XMIT_0_Insteer}," +
                    $"{XMIT_1_CalCommand}," +
                    $"{XMIT_2_Set12Mode}," +
                    $"{XMIT_3__Set12Flag}," +
                    $"{XMIT_4_z}," +
                    $"{XMIT_5_z}," +
                    $"{XMIT_6_z}," +
                    $"{XMIT_7_z},";



                    string checksum = CalculateChecksum(messageWithoutChecksum);
                    string fullMessage = $"{messageWithoutChecksum}*{checksum}\r";
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

        private void Serial_C3_Load(object sender, EventArgs e)
        {

        }
    }

}
