using LabJack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCI_Forms_SPN._GLobalz;
using static Kvaser.KvmLib.Kvmlib;

namespace VCI_Forms_SPN
{
    public partial class LabJ_v1 : Form
    {
        bool firstmessage = false;
        private LabJManager labJManager;
        private int selectedMUXChannel = 1;  // Default to MUX channel 1
        private double minVoltage = 2.44;
        private double maxVoltage = 3.02;
        private bool Connected = false;

        public LabJ_v1()
        {
            InitializeComponent();
            labJManager = LabJManager.Instance;
            labJManager.aLabJackDataReceived += LabJManager_DataReceived;

            trackBar1.Minimum = 0;
            trackBar1.Maximum = 500;
            trackBar1.ValueChanged += TrackBar1_ValueChanged;

            textBoxMin.Text = "2.44";
            textBoxMax.Text = "3.02";
            textBoxMin.TextChanged += TextBoxMin_TextChanged;
            textBoxMax.TextChanged += TextBoxMax_TextChanged;

            btn_MUX1.Click += (s, e) => SelectMUXChannel(1, btn_MUX1);
            btn_MUX2.Click += (s, e) => SelectMUXChannel(2, btn_MUX2);
            btn_MUX4.Click += (s, e) => SelectMUXChannel(4, btn_MUX4);
            btn_MUX5.Click += (s, e) => SelectMUXChannel(5, btn_MUX5);
            btn_MUX1.BackColor = Color.Green;
            labJManager.INIT_CON_LABJACK();
        }

        private void SelectMUXChannel(int channel, Button selectedButton)
        {
            selectedMUXChannel = channel;

            // Reset all buttons to default color
            btn_MUX1.BackColor = DefaultBackColor;
            btn_MUX2.BackColor = DefaultBackColor;
            btn_MUX4.BackColor = DefaultBackColor;
            btn_MUX5.BackColor = DefaultBackColor;
            selectedButton.BackColor = Color.Green;
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            double scaledValue = ScaleTrackbarValueToVoltage(trackBar1.Value);
            labelVoltage.Text = scaledValue.ToString("0.00") + " V";

            if (Connected)
            {
                labJManager.WRITEDATA_MUXDAC(selectedMUXChannel, scaledValue);
            }
        }
        private void TextBoxMin_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBoxMin.Text, out double minVal))
            {
                minVoltage = Math.Max(0.0, Math.Min(5.0, minVal)); 
                UpdateTrackBarLimits();
            }
        }
        private void TextBoxMax_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBoxMax.Text, out double maxVal))
            {
                maxVoltage = Math.Min(5.0, Math.Max(0.0, maxVal));  
                UpdateTrackBarLimits();
            }
        }
        private void UpdateTrackBarLimits()
        {
            if (minVoltage < maxVoltage)
            {
                // No need to change trackbar limits, just the output scaling
                TrackBar1_ValueChanged(trackBar1, EventArgs.Empty); // Trigger an update
            }
        }

        private double ScaleTrackbarValueToVoltage(int trackbarValue)
        {
            double normalizedValue = (double)trackbarValue / 500.0; // Normalize trackbar to 0-1 range
            return minVoltage + (maxVoltage - minVoltage) * normalizedValue;
        }

        private void LabJManager_FirstMessageReceived(string version)
        {
            if (!firstmessage) { 
                MessageBox.Show($"LabJack version received: {version}");
                firstmessage = true;
            }
        }

        private void LabJManager_DataReceived(string serial, string firmware)
        {
            if (!Connected)
            {
                MessageBox.Show($"LabJack Data Received\nSerial: {serial}\nFirmware: {firmware}");
                Connected = true;
            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            labJManager.FirstMEssageWasReceived -= LabJManager_FirstMessageReceived;
            labJManager.aLabJackDataReceived -= LabJManager_DataReceived;
            base.OnFormClosed(e);
        }

    }
}
