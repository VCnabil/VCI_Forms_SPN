namespace VCI_Forms_SPN
{
    partial class Serial_C3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBox_autosend = new System.Windows.Forms.CheckBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.numupdown_sendrate = new System.Windows.Forms.NumericUpDown();
            this.ucXMIT_SensorFaultError = new VCI_Forms_LIB.VCinc_uc();
            this.ucXMIT_NonFollowError = new VCI_Forms_LIB.VCinc_uc();
            this.ucXMIT_CmdFaultError_STA1 = new VCI_Forms_LIB.VCinc_uc();
            this.ucXMIT_CmdFaultError_STA2 = new VCI_Forms_LIB.VCinc_uc();
            this.ucXMIT_CmdFaultError_STA3 = new VCI_Forms_LIB.VCinc_uc();
            this.ucXMIT_Autocal_TrimRoll_Error = new VCI_Forms_LIB.VCinc_uc();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.textBoxReceive = new System.Windows.Forms.TextBox();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.ucXMIT_Dockmode_Interlock_Error = new VCI_Forms_LIB.VCinc_uc();
            this.ucXMIT_CANFault = new VCI_Forms_LIB.VCinc_uc();
            this.ucXMIT_PortNoz_Scaled = new VCI_Forms_LIB.VCinc_uc();
            this.ucXMIT_StbdNoz_Scaled = new VCI_Forms_LIB.VCinc_uc();
            this.ucXMIT_PortBkt_Scaled = new VCI_Forms_LIB.VCinc_uc();
            this.ucXMIT_StbdBkt_Scaled = new VCI_Forms_LIB.VCinc_uc();
            this.btnLabjack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numupdown_sendrate)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_autosend
            // 
            this.checkBox_autosend.AutoSize = true;
            this.checkBox_autosend.Location = new System.Drawing.Point(2298, 12);
            this.checkBox_autosend.Name = "checkBox_autosend";
            this.checkBox_autosend.Size = new System.Drawing.Size(150, 29);
            this.checkBox_autosend.TabIndex = 29;
            this.checkBox_autosend.Text = "checkBox1";
            this.checkBox_autosend.UseVisualStyleBackColor = true;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(2107, 9);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(185, 31);
            this.buttonSend.TabIndex = 28;
            this.buttonSend.Text = "buttonSend";
            this.buttonSend.UseVisualStyleBackColor = true;
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Location = new System.Drawing.Point(1950, 9);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(151, 33);
            this.buttonStartStop.TabIndex = 25;
            this.buttonStartStop.Text = "button1";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            // 
            // numupdown_sendrate
            // 
            this.numupdown_sendrate.Location = new System.Drawing.Point(2383, 56);
            this.numupdown_sendrate.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numupdown_sendrate.Name = "numupdown_sendrate";
            this.numupdown_sendrate.Size = new System.Drawing.Size(120, 31);
            this.numupdown_sendrate.TabIndex = 30;
            this.numupdown_sendrate.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // ucXMIT_SensorFaultError
            // 
            this.ucXMIT_SensorFaultError.Address = "00";
            this.ucXMIT_SensorFaultError.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_SensorFaultError.Bit0Title = "bBkt2_Fault";
            this.ucXMIT_SensorFaultError.Bit1Title = "bBkt1_Fault";
            this.ucXMIT_SensorFaultError.Bit2Title = "bNoz2_Fault";
            this.ucXMIT_SensorFaultError.Bit3Title = "bNoz1_Fault";
            this.ucXMIT_SensorFaultError.Bit4Title = "bTab2_Fault";
            this.ucXMIT_SensorFaultError.Bit5Title = "bTab1_Fault";
            this.ucXMIT_SensorFaultError.Bit6Title = "";
            this.ucXMIT_SensorFaultError.Bit7Title = "";
            this.ucXMIT_SensorFaultError.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_SensorFaultError.Location = new System.Drawing.Point(18, 22);
            this.ucXMIT_SensorFaultError.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_SensorFaultError.Name = "ucXMIT_SensorFaultError";
            this.ucXMIT_SensorFaultError.PGN = "0000";
            this.ucXMIT_SensorFaultError.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_SensorFaultError.SPNName = "XMIT_SensorFaultError";
            this.ucXMIT_SensorFaultError.TabIndex = 31;
            // 
            // ucXMIT_NonFollowError
            // 
            this.ucXMIT_NonFollowError.Address = "00";
            this.ucXMIT_NonFollowError.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_NonFollowError.Bit0Title = "bStbdBktNFU_Fault";
            this.ucXMIT_NonFollowError.Bit1Title = "bPortBktNFU_Fault";
            this.ucXMIT_NonFollowError.Bit2Title = "bStbdNozNFU_Fault";
            this.ucXMIT_NonFollowError.Bit3Title = "bPortNozNFU_Fault";
            this.ucXMIT_NonFollowError.Bit4Title = "bStbdTabNFU_Fault";
            this.ucXMIT_NonFollowError.Bit5Title = "bPortTabNFU_Fault";
            this.ucXMIT_NonFollowError.Bit6Title = "";
            this.ucXMIT_NonFollowError.Bit7Title = "";
            this.ucXMIT_NonFollowError.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_NonFollowError.Location = new System.Drawing.Point(218, 22);
            this.ucXMIT_NonFollowError.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_NonFollowError.Name = "ucXMIT_NonFollowError";
            this.ucXMIT_NonFollowError.PGN = "0000";
            this.ucXMIT_NonFollowError.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_NonFollowError.SPNName = "XMIT_NonFollowError";
            this.ucXMIT_NonFollowError.TabIndex = 32;
            // 
            // ucXMIT_CmdFaultError_STA1
            // 
            this.ucXMIT_CmdFaultError_STA1.Address = "00";
            this.ucXMIT_CmdFaultError_STA1.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_CmdFaultError_STA1.Bit0Title = "bIdle1_Fault";
            this.ucXMIT_CmdFaultError_STA1.Bit1Title = "bHelm1_Fault";
            this.ucXMIT_CmdFaultError_STA1.Bit2Title = "bSLev1_Fault";
            this.ucXMIT_CmdFaultError_STA1.Bit3Title = "bPLev1_Fault";
            this.ucXMIT_CmdFaultError_STA1.Bit4Title = "bJoyY1_Fault";
            this.ucXMIT_CmdFaultError_STA1.Bit5Title = "bJoyX1_Fault";
            this.ucXMIT_CmdFaultError_STA1.Bit6Title = "";
            this.ucXMIT_CmdFaultError_STA1.Bit7Title = "";
            this.ucXMIT_CmdFaultError_STA1.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_CmdFaultError_STA1.Location = new System.Drawing.Point(418, 22);
            this.ucXMIT_CmdFaultError_STA1.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_CmdFaultError_STA1.Name = "ucXMIT_CmdFaultError_STA1";
            this.ucXMIT_CmdFaultError_STA1.PGN = "0000";
            this.ucXMIT_CmdFaultError_STA1.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_CmdFaultError_STA1.SPNName = "XMIT_CmdFaultError_STA1";
            this.ucXMIT_CmdFaultError_STA1.TabIndex = 33;
            // 
            // ucXMIT_CmdFaultError_STA2
            // 
            this.ucXMIT_CmdFaultError_STA2.Address = "00";
            this.ucXMIT_CmdFaultError_STA2.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_CmdFaultError_STA2.Bit0Title = "bIdle2_Fault";
            this.ucXMIT_CmdFaultError_STA2.Bit1Title = "bHelm2_Fault";
            this.ucXMIT_CmdFaultError_STA2.Bit2Title = "bSLev2_Fault";
            this.ucXMIT_CmdFaultError_STA2.Bit3Title = "bPLev2_Fault";
            this.ucXMIT_CmdFaultError_STA2.Bit4Title = "bJoyY2_Fault";
            this.ucXMIT_CmdFaultError_STA2.Bit5Title = "bJoyX2_Fault";
            this.ucXMIT_CmdFaultError_STA2.Bit6Title = "";
            this.ucXMIT_CmdFaultError_STA2.Bit7Title = "";
            this.ucXMIT_CmdFaultError_STA2.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_CmdFaultError_STA2.Location = new System.Drawing.Point(618, 22);
            this.ucXMIT_CmdFaultError_STA2.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_CmdFaultError_STA2.Name = "ucXMIT_CmdFaultError_STA2";
            this.ucXMIT_CmdFaultError_STA2.PGN = "0000";
            this.ucXMIT_CmdFaultError_STA2.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_CmdFaultError_STA2.SPNName = "XMIT_CmdFaultError_STA2";
            this.ucXMIT_CmdFaultError_STA2.TabIndex = 34;
            // 
            // ucXMIT_CmdFaultError_STA3
            // 
            this.ucXMIT_CmdFaultError_STA3.Address = "00";
            this.ucXMIT_CmdFaultError_STA3.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_CmdFaultError_STA3.Bit0Title = "bIdle3_Fault";
            this.ucXMIT_CmdFaultError_STA3.Bit1Title = "bHelm3_Fault";
            this.ucXMIT_CmdFaultError_STA3.Bit2Title = "bSLev2_Fault";
            this.ucXMIT_CmdFaultError_STA3.Bit3Title = "bJoyYX3_Fault";
            this.ucXMIT_CmdFaultError_STA3.Bit4Title = "bJoyY3_Fault";
            this.ucXMIT_CmdFaultError_STA3.Bit5Title = "bJoyX3_Fault";
            this.ucXMIT_CmdFaultError_STA3.Bit6Title = "";
            this.ucXMIT_CmdFaultError_STA3.Bit7Title = "";
            this.ucXMIT_CmdFaultError_STA3.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_CmdFaultError_STA3.Location = new System.Drawing.Point(818, 22);
            this.ucXMIT_CmdFaultError_STA3.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_CmdFaultError_STA3.Name = "ucXMIT_CmdFaultError_STA3";
            this.ucXMIT_CmdFaultError_STA3.PGN = "0000";
            this.ucXMIT_CmdFaultError_STA3.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_CmdFaultError_STA3.SPNName = "XMIT_CmdFaultError_STA3";
            this.ucXMIT_CmdFaultError_STA3.TabIndex = 35;
            // 
            // ucXMIT_Autocal_TrimRoll_Error
            // 
            this.ucXMIT_Autocal_TrimRoll_Error.Address = "00";
            this.ucXMIT_Autocal_TrimRoll_Error.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_Autocal_TrimRoll_Error.Bit0Title = "bTrim1_Fault";
            this.ucXMIT_Autocal_TrimRoll_Error.Bit1Title = "bRoll1_Fault";
            this.ucXMIT_Autocal_TrimRoll_Error.Bit2Title = "bAutocal_Fault";
            this.ucXMIT_Autocal_TrimRoll_Error.Bit3Title = "bAP_OOR_Fault";
            this.ucXMIT_Autocal_TrimRoll_Error.Bit4Title = "bTrim2_Fault";
            this.ucXMIT_Autocal_TrimRoll_Error.Bit5Title = "bRoll2_Fault";
            this.ucXMIT_Autocal_TrimRoll_Error.Bit6Title = "";
            this.ucXMIT_Autocal_TrimRoll_Error.Bit7Title = "";
            this.ucXMIT_Autocal_TrimRoll_Error.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_Autocal_TrimRoll_Error.Location = new System.Drawing.Point(1018, 22);
            this.ucXMIT_Autocal_TrimRoll_Error.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_Autocal_TrimRoll_Error.Name = "ucXMIT_Autocal_TrimRoll_Error";
            this.ucXMIT_Autocal_TrimRoll_Error.PGN = "0000";
            this.ucXMIT_Autocal_TrimRoll_Error.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_Autocal_TrimRoll_Error.SPNName = "XMIT_Autocal_TrimRoll_Error";
            this.ucXMIT_Autocal_TrimRoll_Error.TabIndex = 36;
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(1672, 10);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(263, 33);
            this.comboBoxPorts.TabIndex = 24;
            // 
            // textBoxReceive
            // 
            this.textBoxReceive.Location = new System.Drawing.Point(1672, 106);
            this.textBoxReceive.Multiline = true;
            this.textBoxReceive.Name = "textBoxReceive";
            this.textBoxReceive.Size = new System.Drawing.Size(831, 534);
            this.textBoxReceive.TabIndex = 26;
            // 
            // textBoxSend
            // 
            this.textBoxSend.Location = new System.Drawing.Point(1672, 58);
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(705, 31);
            this.textBoxSend.TabIndex = 27;
            // 
            // ucXMIT_Dockmode_Interlock_Error
            // 
            this.ucXMIT_Dockmode_Interlock_Error.Address = "00";
            this.ucXMIT_Dockmode_Interlock_Error.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_Dockmode_Interlock_Error.Bit0Title = "bDockmodeInhibit";
            this.ucXMIT_Dockmode_Interlock_Error.Bit1Title = "whi eith clutch ingaed";
            this.ucXMIT_Dockmode_Interlock_Error.Bit2Title = "bInboardClutchInterlock";
            this.ucXMIT_Dockmode_Interlock_Error.Bit3Title = "";
            this.ucXMIT_Dockmode_Interlock_Error.Bit4Title = "";
            this.ucXMIT_Dockmode_Interlock_Error.Bit5Title = "";
            this.ucXMIT_Dockmode_Interlock_Error.Bit6Title = "";
            this.ucXMIT_Dockmode_Interlock_Error.Bit7Title = "";
            this.ucXMIT_Dockmode_Interlock_Error.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_Dockmode_Interlock_Error.Location = new System.Drawing.Point(1218, 22);
            this.ucXMIT_Dockmode_Interlock_Error.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_Dockmode_Interlock_Error.Name = "ucXMIT_Dockmode_Interlock_Error";
            this.ucXMIT_Dockmode_Interlock_Error.PGN = "0000";
            this.ucXMIT_Dockmode_Interlock_Error.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_Dockmode_Interlock_Error.SPNName = "XMIT_Dockmode_Interlock_Error";
            this.ucXMIT_Dockmode_Interlock_Error.TabIndex = 37;
            // 
            // ucXMIT_CANFault
            // 
            this.ucXMIT_CANFault.Address = "00";
            this.ucXMIT_CANFault.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_CANFault.Bit0Title = "bCAN2_Fault";
            this.ucXMIT_CANFault.Bit1Title = "option1";
            this.ucXMIT_CANFault.Bit2Title = "option2";
            this.ucXMIT_CANFault.Bit3Title = "op3";
            this.ucXMIT_CANFault.Bit4Title = "bStation3_CAN_FAULT";
            this.ucXMIT_CANFault.Bit5Title = "";
            this.ucXMIT_CANFault.Bit6Title = "";
            this.ucXMIT_CANFault.Bit7Title = "";
            this.ucXMIT_CANFault.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_CANFault.Location = new System.Drawing.Point(1427, 22);
            this.ucXMIT_CANFault.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_CANFault.Name = "ucXMIT_CANFault";
            this.ucXMIT_CANFault.PGN = "0000";
            this.ucXMIT_CANFault.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_CANFault.SPNName = "XMIT_CANFault";
            this.ucXMIT_CANFault.TabIndex = 38;
            // 
            // ucXMIT_PortNoz_Scaled
            // 
            this.ucXMIT_PortNoz_Scaled.Address = "00";
            this.ucXMIT_PortNoz_Scaled.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_PortNoz_Scaled.Bit0Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit1Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit2Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit3Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit4Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit5Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit6Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit7Title = "";
            this.ucXMIT_PortNoz_Scaled.ControlMode = VCI_Forms_LIB.ControlModes.Gauge;
            this.ucXMIT_PortNoz_Scaled.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_PortNoz_Scaled.Location = new System.Drawing.Point(18, 401);
            this.ucXMIT_PortNoz_Scaled.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_PortNoz_Scaled.MaxValue = 999;
            this.ucXMIT_PortNoz_Scaled.Name = "ucXMIT_PortNoz_Scaled";
            this.ucXMIT_PortNoz_Scaled.NumberOfBytes = 2;
            this.ucXMIT_PortNoz_Scaled.PGN = "0000";
            this.ucXMIT_PortNoz_Scaled.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_PortNoz_Scaled.SPNName = "XMIT_PortNoz_Scaled";
            this.ucXMIT_PortNoz_Scaled.TabIndex = 39;
            this.ucXMIT_PortNoz_Scaled.Value = 500;
            // 
            // ucXMIT_StbdNoz_Scaled
            // 
            this.ucXMIT_StbdNoz_Scaled.Address = "00";
            this.ucXMIT_StbdNoz_Scaled.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_StbdNoz_Scaled.Bit0Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit1Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit2Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit3Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit4Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit5Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit6Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit7Title = "";
            this.ucXMIT_StbdNoz_Scaled.ControlMode = VCI_Forms_LIB.ControlModes.Gauge;
            this.ucXMIT_StbdNoz_Scaled.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_StbdNoz_Scaled.Location = new System.Drawing.Point(885, 401);
            this.ucXMIT_StbdNoz_Scaled.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_StbdNoz_Scaled.MaxValue = 999;
            this.ucXMIT_StbdNoz_Scaled.Name = "ucXMIT_StbdNoz_Scaled";
            this.ucXMIT_StbdNoz_Scaled.NumberOfBytes = 2;
            this.ucXMIT_StbdNoz_Scaled.PGN = "0000";
            this.ucXMIT_StbdNoz_Scaled.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_StbdNoz_Scaled.SPNName = "XMIT_StbdNoz_Scaled";
            this.ucXMIT_StbdNoz_Scaled.TabIndex = 40;
            this.ucXMIT_StbdNoz_Scaled.Value = 500;
            // 
            // ucXMIT_PortBkt_Scaled
            // 
            this.ucXMIT_PortBkt_Scaled.Address = "00";
            this.ucXMIT_PortBkt_Scaled.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_PortBkt_Scaled.Bit0Title = "";
            this.ucXMIT_PortBkt_Scaled.Bit1Title = "";
            this.ucXMIT_PortBkt_Scaled.Bit2Title = "";
            this.ucXMIT_PortBkt_Scaled.Bit3Title = "";
            this.ucXMIT_PortBkt_Scaled.Bit4Title = "";
            this.ucXMIT_PortBkt_Scaled.Bit5Title = "";
            this.ucXMIT_PortBkt_Scaled.Bit6Title = "";
            this.ucXMIT_PortBkt_Scaled.Bit7Title = "";
            this.ucXMIT_PortBkt_Scaled.ControlMode = VCI_Forms_LIB.ControlModes.Gauge;
            this.ucXMIT_PortBkt_Scaled.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_PortBkt_Scaled.Location = new System.Drawing.Point(352, 401);
            this.ucXMIT_PortBkt_Scaled.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_PortBkt_Scaled.MaxValue = 999;
            this.ucXMIT_PortBkt_Scaled.Name = "ucXMIT_PortBkt_Scaled";
            this.ucXMIT_PortBkt_Scaled.NumberOfBytes = 2;
            this.ucXMIT_PortBkt_Scaled.PGN = "0000";
            this.ucXMIT_PortBkt_Scaled.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_PortBkt_Scaled.SPNName = "XMIT_PortBkt_Scaled";
            this.ucXMIT_PortBkt_Scaled.TabIndex = 41;
            this.ucXMIT_PortBkt_Scaled.Value = 500;
            // 
            // ucXMIT_StbdBkt_Scaled
            // 
            this.ucXMIT_StbdBkt_Scaled.Address = "00";
            this.ucXMIT_StbdBkt_Scaled.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_StbdBkt_Scaled.Bit0Title = "";
            this.ucXMIT_StbdBkt_Scaled.Bit1Title = "";
            this.ucXMIT_StbdBkt_Scaled.Bit2Title = "";
            this.ucXMIT_StbdBkt_Scaled.Bit3Title = "";
            this.ucXMIT_StbdBkt_Scaled.Bit4Title = "";
            this.ucXMIT_StbdBkt_Scaled.Bit5Title = "";
            this.ucXMIT_StbdBkt_Scaled.Bit6Title = "";
            this.ucXMIT_StbdBkt_Scaled.Bit7Title = "";
            this.ucXMIT_StbdBkt_Scaled.ControlMode = VCI_Forms_LIB.ControlModes.Gauge;
            this.ucXMIT_StbdBkt_Scaled.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_StbdBkt_Scaled.Location = new System.Drawing.Point(574, 401);
            this.ucXMIT_StbdBkt_Scaled.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_StbdBkt_Scaled.MaxValue = 999;
            this.ucXMIT_StbdBkt_Scaled.Name = "ucXMIT_StbdBkt_Scaled";
            this.ucXMIT_StbdBkt_Scaled.NumberOfBytes = 2;
            this.ucXMIT_StbdBkt_Scaled.PGN = "0000";
            this.ucXMIT_StbdBkt_Scaled.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_StbdBkt_Scaled.SPNName = "XMIT_StbdBkt_Scaled";
            this.ucXMIT_StbdBkt_Scaled.TabIndex = 42;
            this.ucXMIT_StbdBkt_Scaled.Value = 500;
            // 
            // btnLabjack
            // 
            this.btnLabjack.Location = new System.Drawing.Point(1442, 322);
            this.btnLabjack.Name = "btnLabjack";
            this.btnLabjack.Size = new System.Drawing.Size(185, 31);
            this.btnLabjack.TabIndex = 43;
            this.btnLabjack.Text = "button1";
            this.btnLabjack.UseVisualStyleBackColor = true;
            // 
            // Serial_C3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2515, 670);
            this.Controls.Add(this.btnLabjack);
            this.Controls.Add(this.ucXMIT_StbdBkt_Scaled);
            this.Controls.Add(this.ucXMIT_PortBkt_Scaled);
            this.Controls.Add(this.ucXMIT_StbdNoz_Scaled);
            this.Controls.Add(this.ucXMIT_PortNoz_Scaled);
            this.Controls.Add(this.ucXMIT_CANFault);
            this.Controls.Add(this.ucXMIT_Dockmode_Interlock_Error);
            this.Controls.Add(this.ucXMIT_Autocal_TrimRoll_Error);
            this.Controls.Add(this.ucXMIT_CmdFaultError_STA3);
            this.Controls.Add(this.ucXMIT_CmdFaultError_STA2);
            this.Controls.Add(this.ucXMIT_CmdFaultError_STA1);
            this.Controls.Add(this.ucXMIT_NonFollowError);
            this.Controls.Add(this.ucXMIT_SensorFaultError);
            this.Controls.Add(this.numupdown_sendrate);
            this.Controls.Add(this.checkBox_autosend);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxSend);
            this.Controls.Add(this.textBoxReceive);
            this.Controls.Add(this.buttonStartStop);
            this.Controls.Add(this.comboBoxPorts);
            this.Name = "Serial_C3";
            this.Text = "3031";
            ((System.ComponentModel.ISupportInitialize)(this.numupdown_sendrate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_autosend;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.NumericUpDown numupdown_sendrate;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_SensorFaultError;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_NonFollowError;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_CmdFaultError_STA1;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_CmdFaultError_STA2;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_CmdFaultError_STA3;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_Autocal_TrimRoll_Error;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.TextBox textBoxReceive;
        private System.Windows.Forms.TextBox textBoxSend;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_Dockmode_Interlock_Error;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_CANFault;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_PortNoz_Scaled;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_StbdNoz_Scaled;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_PortBkt_Scaled;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_StbdBkt_Scaled;
        private System.Windows.Forms.Button btnLabjack;
    }
}