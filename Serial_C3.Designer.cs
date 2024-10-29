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
            this.ucXMIT_StbdTab_Scaled = new VCI_Forms_LIB.VCinc_uc();
            this.ucXMIT_PortTab_Scaled = new VCI_Forms_LIB.VCinc_uc();
            this.uc_MODE = new VCI_Forms_LIB.VCinc_uc();
            this.ucXMIT_16 = new VCI_Forms_LIB.VCinc_uc();
            this.ucXMIT_17 = new VCI_Forms_LIB.VCinc_uc();
            this.lbl_Intsteer_Enable = new System.Windows.Forms.Label();
            this.lbl_Autocal_CMD = new System.Windows.Forms.Label();
            this.lbl_Set1_Set2_Mode = new System.Windows.Forms.Label();
            this.lbl_Position_Capture_Request = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_Command = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numupdown_sendrate)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_autosend
            // 
            this.checkBox_autosend.AutoSize = true;
            this.checkBox_autosend.Location = new System.Drawing.Point(2013, 577);
            this.checkBox_autosend.Name = "checkBox_autosend";
            this.checkBox_autosend.Size = new System.Drawing.Size(86, 29);
            this.checkBox_autosend.TabIndex = 29;
            this.checkBox_autosend.Text = "auto";
            this.checkBox_autosend.UseVisualStyleBackColor = true;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(1454, 612);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(767, 38);
            this.buttonSend.TabIndex = 28;
            this.buttonSend.Text = "send1";
            this.buttonSend.UseVisualStyleBackColor = true;
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Location = new System.Drawing.Point(1877, 572);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(130, 34);
            this.buttonStartStop.TabIndex = 25;
            this.buttonStartStop.Text = "StartCom";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            // 
            // numupdown_sendrate
            // 
            this.numupdown_sendrate.Location = new System.Drawing.Point(2101, 575);
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
            this.ucXMIT_SensorFaultError.Location = new System.Drawing.Point(9, 297);
            this.ucXMIT_SensorFaultError.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_SensorFaultError.Name = "ucXMIT_SensorFaultError";
            this.ucXMIT_SensorFaultError.PGN = "0000";
            this.ucXMIT_SensorFaultError.Size = new System.Drawing.Size(191, 260);
            this.ucXMIT_SensorFaultError.SPNName = "XMIT_SensorFaultError";
            this.ucXMIT_SensorFaultError.TabIndex = 31;
            this.ucXMIT_SensorFaultError.Value = 15;
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
            this.ucXMIT_NonFollowError.Location = new System.Drawing.Point(200, 297);
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
            this.ucXMIT_CmdFaultError_STA1.Location = new System.Drawing.Point(403, 297);
            this.ucXMIT_CmdFaultError_STA1.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_CmdFaultError_STA1.Name = "ucXMIT_CmdFaultError_STA1";
            this.ucXMIT_CmdFaultError_STA1.PGN = "0000";
            this.ucXMIT_CmdFaultError_STA1.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_CmdFaultError_STA1.SPNName = "XMIT_CmdFaultError_STA1";
            this.ucXMIT_CmdFaultError_STA1.TabIndex = 33;
            this.ucXMIT_CmdFaultError_STA1.Value = 14;
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
            this.ucXMIT_CmdFaultError_STA2.Location = new System.Drawing.Point(604, 297);
            this.ucXMIT_CmdFaultError_STA2.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_CmdFaultError_STA2.Name = "ucXMIT_CmdFaultError_STA2";
            this.ucXMIT_CmdFaultError_STA2.PGN = "0000";
            this.ucXMIT_CmdFaultError_STA2.Size = new System.Drawing.Size(199, 260);
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
            this.ucXMIT_CmdFaultError_STA3.Location = new System.Drawing.Point(812, 297);
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
            this.ucXMIT_Autocal_TrimRoll_Error.Location = new System.Drawing.Point(1012, 297);
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
            this.comboBoxPorts.Location = new System.Drawing.Point(1608, 571);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(263, 33);
            this.comboBoxPorts.TabIndex = 24;
            // 
            // textBoxReceive
            // 
            this.textBoxReceive.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textBoxReceive.ForeColor = System.Drawing.Color.LimeGreen;
            this.textBoxReceive.Location = new System.Drawing.Point(1454, 693);
            this.textBoxReceive.Multiline = true;
            this.textBoxReceive.Name = "textBoxReceive";
            this.textBoxReceive.Size = new System.Drawing.Size(767, 286);
            this.textBoxReceive.TabIndex = 26;
            this.textBoxReceive.Text = "ergregewg";
            // 
            // textBoxSend
            // 
            this.textBoxSend.Location = new System.Drawing.Point(1454, 656);
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(762, 31);
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
            this.ucXMIT_Dockmode_Interlock_Error.Location = new System.Drawing.Point(1212, 297);
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
            this.ucXMIT_CANFault.Location = new System.Drawing.Point(2021, 297);
            this.ucXMIT_CANFault.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_CANFault.Name = "ucXMIT_CANFault";
            this.ucXMIT_CANFault.PGN = "0000";
            this.ucXMIT_CANFault.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_CANFault.SPNName = "XMIT_CANFault";
            this.ucXMIT_CANFault.TabIndex = 38;
            this.ucXMIT_CANFault.Value = 2;
            // 
            // ucXMIT_PortNoz_Scaled
            // 
            this.ucXMIT_PortNoz_Scaled.Address = "00";
            this.ucXMIT_PortNoz_Scaled.BackColor = System.Drawing.Color.LightCyan;
            this.ucXMIT_PortNoz_Scaled.Bit0Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit1Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit2Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit3Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit4Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit5Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit6Title = "";
            this.ucXMIT_PortNoz_Scaled.Bit7Title = "";
            this.ucXMIT_PortNoz_Scaled.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.ucXMIT_PortNoz_Scaled.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_PortNoz_Scaled.Location = new System.Drawing.Point(9, 9);
            this.ucXMIT_PortNoz_Scaled.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_PortNoz_Scaled.MaxValue = 65535;
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
            this.ucXMIT_StbdNoz_Scaled.BackColor = System.Drawing.Color.LightPink;
            this.ucXMIT_StbdNoz_Scaled.Bit0Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit1Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit2Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit3Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit4Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit5Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit6Title = "";
            this.ucXMIT_StbdNoz_Scaled.Bit7Title = "";
            this.ucXMIT_StbdNoz_Scaled.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.ucXMIT_StbdNoz_Scaled.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_StbdNoz_Scaled.Location = new System.Drawing.Point(209, 9);
            this.ucXMIT_StbdNoz_Scaled.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_StbdNoz_Scaled.MaxValue = 65535;
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
            this.ucXMIT_PortBkt_Scaled.BackColor = System.Drawing.Color.LightCyan;
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
            this.ucXMIT_PortBkt_Scaled.Location = new System.Drawing.Point(409, 9);
            this.ucXMIT_PortBkt_Scaled.m_ticks = 6;
            this.ucXMIT_PortBkt_Scaled.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_PortBkt_Scaled.MaxValue = 65535;
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
            this.ucXMIT_StbdBkt_Scaled.BackColor = System.Drawing.Color.LightPink;
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
            this.ucXMIT_StbdBkt_Scaled.G_EndAngle = 48;
            this.ucXMIT_StbdBkt_Scaled.G_StartAngle = 315;
            this.ucXMIT_StbdBkt_Scaled.Location = new System.Drawing.Point(610, 9);
            this.ucXMIT_StbdBkt_Scaled.m_ticks = 6;
            this.ucXMIT_StbdBkt_Scaled.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_StbdBkt_Scaled.MaxValue = 65535;
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
            this.btnLabjack.Location = new System.Drawing.Point(1455, 571);
            this.btnLabjack.Name = "btnLabjack";
            this.btnLabjack.Size = new System.Drawing.Size(147, 34);
            this.btnLabjack.TabIndex = 43;
            this.btnLabjack.Text = "Sim I/O";
            this.btnLabjack.UseVisualStyleBackColor = true;
            // 
            // ucXMIT_StbdTab_Scaled
            // 
            this.ucXMIT_StbdTab_Scaled.Address = "00";
            this.ucXMIT_StbdTab_Scaled.BackColor = System.Drawing.Color.LightPink;
            this.ucXMIT_StbdTab_Scaled.Bit0Title = "";
            this.ucXMIT_StbdTab_Scaled.Bit1Title = "";
            this.ucXMIT_StbdTab_Scaled.Bit2Title = "";
            this.ucXMIT_StbdTab_Scaled.Bit3Title = "";
            this.ucXMIT_StbdTab_Scaled.Bit4Title = "";
            this.ucXMIT_StbdTab_Scaled.Bit5Title = "";
            this.ucXMIT_StbdTab_Scaled.Bit6Title = "";
            this.ucXMIT_StbdTab_Scaled.Bit7Title = "";
            this.ucXMIT_StbdTab_Scaled.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.ucXMIT_StbdTab_Scaled.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_StbdTab_Scaled.Location = new System.Drawing.Point(1010, 9);
            this.ucXMIT_StbdTab_Scaled.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_StbdTab_Scaled.MaxValue = 65535;
            this.ucXMIT_StbdTab_Scaled.Name = "ucXMIT_StbdTab_Scaled";
            this.ucXMIT_StbdTab_Scaled.NumberOfBytes = 2;
            this.ucXMIT_StbdTab_Scaled.PGN = "0000";
            this.ucXMIT_StbdTab_Scaled.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_StbdTab_Scaled.SPNName = "XMIT_StbdTab_Scaled";
            this.ucXMIT_StbdTab_Scaled.TabIndex = 45;
            this.ucXMIT_StbdTab_Scaled.Value = 10;
            // 
            // ucXMIT_PortTab_Scaled
            // 
            this.ucXMIT_PortTab_Scaled.Address = "00";
            this.ucXMIT_PortTab_Scaled.BackColor = System.Drawing.Color.LightCyan;
            this.ucXMIT_PortTab_Scaled.Bit0Title = "";
            this.ucXMIT_PortTab_Scaled.Bit1Title = "";
            this.ucXMIT_PortTab_Scaled.Bit2Title = "";
            this.ucXMIT_PortTab_Scaled.Bit3Title = "";
            this.ucXMIT_PortTab_Scaled.Bit4Title = "";
            this.ucXMIT_PortTab_Scaled.Bit5Title = "";
            this.ucXMIT_PortTab_Scaled.Bit6Title = "";
            this.ucXMIT_PortTab_Scaled.Bit7Title = "";
            this.ucXMIT_PortTab_Scaled.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.ucXMIT_PortTab_Scaled.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_PortTab_Scaled.Location = new System.Drawing.Point(810, 9);
            this.ucXMIT_PortTab_Scaled.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_PortTab_Scaled.MaxValue = 65535;
            this.ucXMIT_PortTab_Scaled.Name = "ucXMIT_PortTab_Scaled";
            this.ucXMIT_PortTab_Scaled.NumberOfBytes = 2;
            this.ucXMIT_PortTab_Scaled.PGN = "0000";
            this.ucXMIT_PortTab_Scaled.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_PortTab_Scaled.SPNName = "XMIT_PortTab_Scaled";
            this.ucXMIT_PortTab_Scaled.TabIndex = 44;
            this.ucXMIT_PortTab_Scaled.Value = 10;
            // 
            // uc_MODE
            // 
            this.uc_MODE.Address = "00";
            this.uc_MODE.BackColor = System.Drawing.Color.Linen;
            this.uc_MODE.Bit0Title = "";
            this.uc_MODE.Bit1Title = "";
            this.uc_MODE.Bit2Title = "";
            this.uc_MODE.Bit3Title = "";
            this.uc_MODE.Bit4Title = "";
            this.uc_MODE.Bit5Title = "";
            this.uc_MODE.Bit6Title = "";
            this.uc_MODE.Bit7Title = "";
            this.uc_MODE.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.uc_MODE.Location = new System.Drawing.Point(1421, 297);
            this.uc_MODE.Margin = new System.Windows.Forms.Padding(0);
            this.uc_MODE.Name = "uc_MODE";
            this.uc_MODE.PGN = "0000";
            this.uc_MODE.Size = new System.Drawing.Size(200, 260);
            this.uc_MODE.TabIndex = 46;
            this.uc_MODE.Value = 3;
            // 
            // ucXMIT_16
            // 
            this.ucXMIT_16.Address = "00";
            this.ucXMIT_16.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_16.Bit0Title = " ";
            this.ucXMIT_16.Bit1Title = " ";
            this.ucXMIT_16.Bit2Title = " ";
            this.ucXMIT_16.Bit3Title = " ";
            this.ucXMIT_16.Bit4Title = " ";
            this.ucXMIT_16.Bit5Title = " ";
            this.ucXMIT_16.Bit6Title = "";
            this.ucXMIT_16.Bit7Title = "";
            this.ucXMIT_16.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_16.Location = new System.Drawing.Point(1621, 297);
            this.ucXMIT_16.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_16.Name = "ucXMIT_16";
            this.ucXMIT_16.PGN = "0000";
            this.ucXMIT_16.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_16.SPNName = "x16";
            this.ucXMIT_16.TabIndex = 47;
            // 
            // ucXMIT_17
            // 
            this.ucXMIT_17.Address = "00";
            this.ucXMIT_17.BackColor = System.Drawing.Color.Linen;
            this.ucXMIT_17.Bit0Title = " ";
            this.ucXMIT_17.Bit1Title = " ";
            this.ucXMIT_17.Bit2Title = " ";
            this.ucXMIT_17.Bit3Title = " ";
            this.ucXMIT_17.Bit4Title = " ";
            this.ucXMIT_17.Bit5Title = " ";
            this.ucXMIT_17.Bit6Title = "";
            this.ucXMIT_17.Bit7Title = "";
            this.ucXMIT_17.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.ucXMIT_17.Location = new System.Drawing.Point(1821, 297);
            this.ucXMIT_17.Margin = new System.Windows.Forms.Padding(0);
            this.ucXMIT_17.Name = "ucXMIT_17";
            this.ucXMIT_17.PGN = "0000";
            this.ucXMIT_17.Size = new System.Drawing.Size(200, 260);
            this.ucXMIT_17.SPNName = "xmi17";
            this.ucXMIT_17.TabIndex = 48;
            this.ucXMIT_17.Value = 65;
            // 
            // lbl_Intsteer_Enable
            // 
            this.lbl_Intsteer_Enable.AutoSize = true;
            this.lbl_Intsteer_Enable.Location = new System.Drawing.Point(1416, 9);
            this.lbl_Intsteer_Enable.Name = "lbl_Intsteer_Enable";
            this.lbl_Intsteer_Enable.Size = new System.Drawing.Size(162, 25);
            this.lbl_Intsteer_Enable.TabIndex = 49;
            this.lbl_Intsteer_Enable.Text = "Intsteer_Enable";
            // 
            // lbl_Autocal_CMD
            // 
            this.lbl_Autocal_CMD.AutoSize = true;
            this.lbl_Autocal_CMD.Location = new System.Drawing.Point(1416, 34);
            this.lbl_Autocal_CMD.Name = "lbl_Autocal_CMD";
            this.lbl_Autocal_CMD.Size = new System.Drawing.Size(144, 25);
            this.lbl_Autocal_CMD.TabIndex = 50;
            this.lbl_Autocal_CMD.Text = "Autocal_CMD";
            // 
            // lbl_Set1_Set2_Mode
            // 
            this.lbl_Set1_Set2_Mode.AutoSize = true;
            this.lbl_Set1_Set2_Mode.Location = new System.Drawing.Point(1416, 59);
            this.lbl_Set1_Set2_Mode.Name = "lbl_Set1_Set2_Mode";
            this.lbl_Set1_Set2_Mode.Size = new System.Drawing.Size(178, 25);
            this.lbl_Set1_Set2_Mode.TabIndex = 51;
            this.lbl_Set1_Set2_Mode.Text = "Set1_Set2_Mode";
            // 
            // lbl_Position_Capture_Request
            // 
            this.lbl_Position_Capture_Request.AutoSize = true;
            this.lbl_Position_Capture_Request.Location = new System.Drawing.Point(1416, 84);
            this.lbl_Position_Capture_Request.Name = "lbl_Position_Capture_Request";
            this.lbl_Position_Capture_Request.Size = new System.Drawing.Size(269, 25);
            this.lbl_Position_Capture_Request.TabIndex = 52;
            this.lbl_Position_Capture_Request.Text = "Position_Capture_Request";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1243, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 25);
            this.label1.TabIndex = 53;
            this.label1.Text = "Intsteer_Enable";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1243, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 25);
            this.label2.TabIndex = 54;
            this.label2.Text = "Autocal_CMD";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1243, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 25);
            this.label3.TabIndex = 55;
            this.label3.Text = "Set1_Set2_Md";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1243, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 25);
            this.label4.TabIndex = 56;
            this.label4.Text = "Pos_Cap_Requ";
            // 
            // lbl_Command
            // 
            this.lbl_Command.AutoSize = true;
            this.lbl_Command.Location = new System.Drawing.Point(1624, 34);
            this.lbl_Command.Name = "lbl_Command";
            this.lbl_Command.Size = new System.Drawing.Size(52, 25);
            this.lbl_Command.TabIndex = 57;
            this.lbl_Command.Text = "cmd";
            // 
            // Serial_C3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2231, 991);
            this.Controls.Add(this.lbl_Command);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_Position_Capture_Request);
            this.Controls.Add(this.lbl_Set1_Set2_Mode);
            this.Controls.Add(this.lbl_Autocal_CMD);
            this.Controls.Add(this.lbl_Intsteer_Enable);
            this.Controls.Add(this.ucXMIT_17);
            this.Controls.Add(this.ucXMIT_16);
            this.Controls.Add(this.uc_MODE);
            this.Controls.Add(this.ucXMIT_StbdTab_Scaled);
            this.Controls.Add(this.ucXMIT_PortTab_Scaled);
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
            this.Load += new System.EventHandler(this.Serial_C3_Load);
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
        private VCI_Forms_LIB.VCinc_uc ucXMIT_StbdTab_Scaled;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_PortTab_Scaled;
        private VCI_Forms_LIB.VCinc_uc uc_MODE;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_16;
        private VCI_Forms_LIB.VCinc_uc ucXMIT_17;
        private System.Windows.Forms.Label lbl_Intsteer_Enable;
        private System.Windows.Forms.Label lbl_Autocal_CMD;
        private System.Windows.Forms.Label lbl_Set1_Set2_Mode;
        private System.Windows.Forms.Label lbl_Position_Capture_Request;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_Command;
    }
}