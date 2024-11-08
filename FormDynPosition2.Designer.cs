namespace VCI_Forms_SPN
{
    partial class FormDynPosition2
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
            this.btn_restBit1 = new System.Windows.Forms.Button();
            this.btn_restBit0 = new System.Windows.Forms.Button();
            this.cb_uniqueOn = new System.Windows.Forms.CheckBox();
            this.tb_CAN_Bus_View = new System.Windows.Forms.TextBox();
            this.btn_RunStop = new System.Windows.Forms.Button();
            this.lbl_OnScreenCount = new System.Windows.Forms.Label();
            this.lbl_onBus = new System.Windows.Forms.Label();
            this.btn_Validate = new System.Windows.Forms.Button();
            this.vCinc_DynPos1 = new VCI_Forms_LIB.VCinc_DynPos();
            this.vCinc_LatLon_waypoint = new VCI_Forms_LIB.VCinc_LatLon();
            this.vCinc_LatLon_mapCnter = new VCI_Forms_LIB.VCinc_LatLon();
            this.vCinc_uc18 = new VCI_Forms_LIB.VCinc_uc();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.vCinc_SFversion2 = new VCI_Forms_LIB.VCinc_SFversion();
            this.vCinc_SFversion1 = new VCI_Forms_LIB.VCinc_SFversion();
            this.vCinc_uc5 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc6 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc7 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc8 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc4 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc3 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc2 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc1 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc12 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc11 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc10 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc9 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc24 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc23 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc22 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc21 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc20 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc19 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc17 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc16 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc15 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc14 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc13 = new VCI_Forms_LIB.VCinc_uc();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_restBit1
            // 
            this.btn_restBit1.Location = new System.Drawing.Point(309, 51);
            this.btn_restBit1.Name = "btn_restBit1";
            this.btn_restBit1.Size = new System.Drawing.Size(140, 32);
            this.btn_restBit1.TabIndex = 251;
            this.btn_restBit1.Text = "hold rot";
            this.btn_restBit1.UseVisualStyleBackColor = true;
            // 
            // btn_restBit0
            // 
            this.btn_restBit0.Location = new System.Drawing.Point(471, 51);
            this.btn_restBit0.Name = "btn_restBit0";
            this.btn_restBit0.Size = new System.Drawing.Size(140, 32);
            this.btn_restBit0.TabIndex = 250;
            this.btn_restBit0.Text = "hold loc";
            this.btn_restBit0.UseVisualStyleBackColor = true;
            // 
            // cb_uniqueOn
            // 
            this.cb_uniqueOn.AutoSize = true;
            this.cb_uniqueOn.Location = new System.Drawing.Point(16, 51);
            this.cb_uniqueOn.Margin = new System.Windows.Forms.Padding(6);
            this.cb_uniqueOn.Name = "cb_uniqueOn";
            this.cb_uniqueOn.Size = new System.Drawing.Size(292, 29);
            this.cb_uniqueOn.TabIndex = 246;
            this.cb_uniqueOn.Text = "group messages by pgn ?";
            this.cb_uniqueOn.UseVisualStyleBackColor = true;
            // 
            // tb_CAN_Bus_View
            // 
            this.tb_CAN_Bus_View.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tb_CAN_Bus_View.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_CAN_Bus_View.ForeColor = System.Drawing.Color.Lime;
            this.tb_CAN_Bus_View.Location = new System.Drawing.Point(5, 92);
            this.tb_CAN_Bus_View.Margin = new System.Windows.Forms.Padding(6);
            this.tb_CAN_Bus_View.Multiline = true;
            this.tb_CAN_Bus_View.Name = "tb_CAN_Bus_View";
            this.tb_CAN_Bus_View.ReadOnly = true;
            this.tb_CAN_Bus_View.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_CAN_Bus_View.Size = new System.Drawing.Size(659, 727);
            this.tb_CAN_Bus_View.TabIndex = 245;
            this.tb_CAN_Bus_View.Text = ": Console Bkg.green  -c -0 ";
            // 
            // btn_RunStop
            // 
            this.btn_RunStop.Location = new System.Drawing.Point(471, 0);
            this.btn_RunStop.Margin = new System.Windows.Forms.Padding(6);
            this.btn_RunStop.Name = "btn_RunStop";
            this.btn_RunStop.Size = new System.Drawing.Size(150, 44);
            this.btn_RunStop.TabIndex = 244;
            this.btn_RunStop.Text = "Send Can";
            this.btn_RunStop.UseVisualStyleBackColor = true;
            // 
            // lbl_OnScreenCount
            // 
            this.lbl_OnScreenCount.Location = new System.Drawing.Point(157, 9);
            this.lbl_OnScreenCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_OnScreenCount.Name = "lbl_OnScreenCount";
            this.lbl_OnScreenCount.Size = new System.Drawing.Size(140, 27);
            this.lbl_OnScreenCount.TabIndex = 243;
            this.lbl_OnScreenCount.Text = "on screen ";
            this.lbl_OnScreenCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_onBus
            // 
            this.lbl_onBus.Location = new System.Drawing.Point(11, 9);
            this.lbl_onBus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_onBus.Name = "lbl_onBus";
            this.lbl_onBus.Size = new System.Drawing.Size(140, 27);
            this.lbl_onBus.TabIndex = 242;
            this.lbl_onBus.Text = "on bus";
            this.lbl_onBus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Validate
            // 
            this.btn_Validate.Location = new System.Drawing.Point(309, 0);
            this.btn_Validate.Margin = new System.Windows.Forms.Padding(6);
            this.btn_Validate.Name = "btn_Validate";
            this.btn_Validate.Size = new System.Drawing.Size(150, 44);
            this.btn_Validate.TabIndex = 241;
            this.btn_Validate.Text = "validator";
            this.btn_Validate.UseVisualStyleBackColor = true;
            // 
            // vCinc_DynPos1
            // 
            this.vCinc_DynPos1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.vCinc_DynPos1.Location = new System.Drawing.Point(673, 93);
            this.vCinc_DynPos1.Name = "vCinc_DynPos1";
            this.vCinc_DynPos1.Size = new System.Drawing.Size(684, 726);
            this.vCinc_DynPos1.TabIndex = 252;
            // 
            // vCinc_LatLon_waypoint
            // 
            this.vCinc_LatLon_waypoint.BackColor = System.Drawing.Color.RosyBrown;
            this.vCinc_LatLon_waypoint.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_LatLon_waypoint.LatitudeDecimal = 0D;
            this.vCinc_LatLon_waypoint.Location = new System.Drawing.Point(1043, -2);
            this.vCinc_LatLon_waypoint.LongitudeDecimal = 0D;
            this.vCinc_LatLon_waypoint.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_LatLon_waypoint.Name = "vCinc_LatLon_waypoint";
            this.vCinc_LatLon_waypoint.Size = new System.Drawing.Size(314, 82);
            this.vCinc_LatLon_waypoint.TabIndex = 254;
            // 
            // vCinc_LatLon_mapCnter
            // 
            this.vCinc_LatLon_mapCnter.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.vCinc_LatLon_mapCnter.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_LatLon_mapCnter.LatitudeDecimal = 0D;
            this.vCinc_LatLon_mapCnter.Location = new System.Drawing.Point(673, -2);
            this.vCinc_LatLon_mapCnter.LongitudeDecimal = 0D;
            this.vCinc_LatLon_mapCnter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_LatLon_mapCnter.Name = "vCinc_LatLon_mapCnter";
            this.vCinc_LatLon_mapCnter.Size = new System.Drawing.Size(318, 82);
            this.vCinc_LatLon_mapCnter.TabIndex = 253;
            // 
            // vCinc_uc18
            // 
            this.vCinc_uc18.Address = "29";
            this.vCinc_uc18.BackColor = System.Drawing.Color.Silver;
            this.vCinc_uc18.Bit0Title = "holdpos";
            this.vCinc_uc18.Bit1Title = "holdhead";
            this.vCinc_uc18.Bit2Title = "?";
            this.vCinc_uc18.Bit3Title = " ";
            this.vCinc_uc18.Bit4Title = " ";
            this.vCinc_uc18.Bit5Title = " ";
            this.vCinc_uc18.Bit6Title = "";
            this.vCinc_uc18.Bit7Title = "";
            this.vCinc_uc18.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc18.Location = new System.Drawing.Point(2193, 391);
            this.vCinc_uc18.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc18.Name = "vCinc_uc18";
            this.vCinc_uc18.PGN = "FF88";
            this.vCinc_uc18.Size = new System.Drawing.Size(65, 260);
            this.vCinc_uc18.SPNName = "DP?";
            this.vCinc_uc18.TabIndex = 255;
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(673, 849);
            this.trackBar1.Maximum = 72000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(684, 62);
            this.trackBar1.TabIndex = 256;
            this.trackBar1.TickFrequency = 1000;
            this.trackBar1.Value = 36000;
            // 
            // vCinc_SFversion2
            // 
            this.vCinc_SFversion2.BackColor = System.Drawing.Color.Bisque;
            this.vCinc_SFversion2.Location = new System.Drawing.Point(1362, 0);
            this.vCinc_SFversion2.Major = 87;
            this.vCinc_SFversion2.Major_firstbyteindex = 0;
            this.vCinc_SFversion2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_SFversion2.Minor = 1003;
            this.vCinc_SFversion2.Minor_firstbyteindex = 1;
            this.vCinc_SFversion2.Minor_numofbytes = 2;
            this.vCinc_SFversion2.Name = "vCinc_SFversion2";
            this.vCinc_SFversion2.PGN = "ff75";
            this.vCinc_SFversion2.PrnName = "SSRS CU";
            this.vCinc_SFversion2.Rev = 9166;
            this.vCinc_SFversion2.Rev_firstbyteindex = 3;
            this.vCinc_SFversion2.Rev_numofbytes = 2;
            this.vCinc_SFversion2.Size = new System.Drawing.Size(186, 91);
            this.vCinc_SFversion2.Source = "29";
            this.vCinc_SFversion2.TabIndex = 266;
            // 
            // vCinc_SFversion1
            // 
            this.vCinc_SFversion1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.vCinc_SFversion1.Location = new System.Drawing.Point(1552, 0);
            this.vCinc_SFversion1.Major = 18;
            this.vCinc_SFversion1.Major_firstbyteindex = 2;
            this.vCinc_SFversion1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_SFversion1.Minor = 1048;
            this.vCinc_SFversion1.Minor_firstbyteindex = 3;
            this.vCinc_SFversion1.Minor_numofbytes = 2;
            this.vCinc_SFversion1.Name = "vCinc_SFversion1";
            this.vCinc_SFversion1.PGN = "ff30";
            this.vCinc_SFversion1.PrnName = "SSRS Clutch pannel";
            this.vCinc_SFversion1.Rev = 9166;
            this.vCinc_SFversion1.Rev_firstbyteindex = 5;
            this.vCinc_SFversion1.Rev_numofbytes = 2;
            this.vCinc_SFversion1.Size = new System.Drawing.Size(185, 91);
            this.vCinc_SFversion1.Source = "29";
            this.vCinc_SFversion1.TabIndex = 265;
            // 
            // vCinc_uc5
            // 
            this.vCinc_uc5.A_FirstByteIndex = 4;
            this.vCinc_uc5.Address = "29";
            this.vCinc_uc5.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc5.Bit0Title = "RmtSta Idle Knob Fault";
            this.vCinc_uc5.Bit1Title = "RmtSta JoyStick Z Fault";
            this.vCinc_uc5.Bit2Title = "RmtSta Stbd Lever Fault";
            this.vCinc_uc5.Bit3Title = "RmtSta Port Lever Fault";
            this.vCinc_uc5.Bit4Title = "RmtSta Joy-Y Fault";
            this.vCinc_uc5.Bit5Title = "RmtSta Joy-X Fault";
            this.vCinc_uc5.Bit6Title = "";
            this.vCinc_uc5.Bit7Title = "";
            this.vCinc_uc5.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc5.Location = new System.Drawing.Point(1675, 93);
            this.vCinc_uc5.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc5.Name = "vCinc_uc5";
            this.vCinc_uc5.PGN = "FF49";
            this.vCinc_uc5.Size = new System.Drawing.Size(80, 260);
            this.vCinc_uc5.SPNName = "Faluts";
            this.vCinc_uc5.TabIndex = 264;
            // 
            // vCinc_uc6
            // 
            this.vCinc_uc6.A_FirstByteIndex = 5;
            this.vCinc_uc6.Address = "29";
            this.vCinc_uc6.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc6.Bit0Title = "Port Trim Fault";
            this.vCinc_uc6.Bit1Title = "Port Roll Fault";
            this.vCinc_uc6.Bit2Title = "AutoCal Fault";
            this.vCinc_uc6.Bit3Title = "Autopilot Out Range Fault";
            this.vCinc_uc6.Bit4Title = "Stbd Trim Joystick Fault";
            this.vCinc_uc6.Bit5Title = "Stbd Roll Joystick Fault";
            this.vCinc_uc6.Bit6Title = "Hardware Fault";
            this.vCinc_uc6.Bit7Title = "";
            this.vCinc_uc6.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc6.Location = new System.Drawing.Point(1755, 93);
            this.vCinc_uc6.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc6.Name = "vCinc_uc6";
            this.vCinc_uc6.PGN = "FF49";
            this.vCinc_uc6.Size = new System.Drawing.Size(80, 260);
            this.vCinc_uc6.SPNName = "Faluts";
            this.vCinc_uc6.TabIndex = 263;
            // 
            // vCinc_uc7
            // 
            this.vCinc_uc7.A_FirstByteIndex = 6;
            this.vCinc_uc7.Address = "29";
            this.vCinc_uc7.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc7.Bit0Title = "Dk Mod Interlock Fault";
            this.vCinc_uc7.Bit1Title = "Dk Mod Clutch Fault";
            this.vCinc_uc7.Bit2Title = "Dk Mod Inboard clu Fault";
            this.vCinc_uc7.Bit3Title = "";
            this.vCinc_uc7.Bit4Title = "";
            this.vCinc_uc7.Bit5Title = "";
            this.vCinc_uc7.Bit6Title = "";
            this.vCinc_uc7.Bit7Title = "";
            this.vCinc_uc7.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc7.Location = new System.Drawing.Point(1830, 93);
            this.vCinc_uc7.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc7.Name = "vCinc_uc7";
            this.vCinc_uc7.PGN = "FF49";
            this.vCinc_uc7.Size = new System.Drawing.Size(80, 260);
            this.vCinc_uc7.SPNName = "Faluts";
            this.vCinc_uc7.TabIndex = 262;
            // 
            // vCinc_uc8
            // 
            this.vCinc_uc8.A_FirstByteIndex = 7;
            this.vCinc_uc8.Address = "29";
            this.vCinc_uc8.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc8.Bit0Title = "CAN Fault";
            this.vCinc_uc8.Bit1Title = "1 Clutch Panel CAN Fault";
            this.vCinc_uc8.Bit2Title = "2 Clutch Panel CAN Fault";
            this.vCinc_uc8.Bit3Title = "3 Clutch Panel CAN Fault";
            this.vCinc_uc8.Bit4Title = "";
            this.vCinc_uc8.Bit5Title = " DP LCD CAN Fault";
            this.vCinc_uc8.Bit6Title = "Rmt Sta CU CAN Fault";
            this.vCinc_uc8.Bit7Title = "";
            this.vCinc_uc8.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc8.Location = new System.Drawing.Point(1910, 93);
            this.vCinc_uc8.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc8.Name = "vCinc_uc8";
            this.vCinc_uc8.PGN = "FF49";
            this.vCinc_uc8.Size = new System.Drawing.Size(80, 260);
            this.vCinc_uc8.SPNName = "Faluts";
            this.vCinc_uc8.TabIndex = 261;
            // 
            // vCinc_uc4
            // 
            this.vCinc_uc4.A_FirstByteIndex = 3;
            this.vCinc_uc4.Address = "29";
            this.vCinc_uc4.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc4.Bit0Title = "Sta 2 Idle Knob Fault";
            this.vCinc_uc4.Bit1Title = "Sta 2 JoyStick Z Fault";
            this.vCinc_uc4.Bit2Title = "Sta 2 Stbd Lever Fault";
            this.vCinc_uc4.Bit3Title = "Sta 2 Port Lever Fault";
            this.vCinc_uc4.Bit4Title = "Sta 2 Joy-X Fault";
            this.vCinc_uc4.Bit5Title = "";
            this.vCinc_uc4.Bit6Title = "";
            this.vCinc_uc4.Bit7Title = "";
            this.vCinc_uc4.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc4.Location = new System.Drawing.Point(1595, 93);
            this.vCinc_uc4.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc4.Name = "vCinc_uc4";
            this.vCinc_uc4.PGN = "FF49";
            this.vCinc_uc4.Size = new System.Drawing.Size(80, 260);
            this.vCinc_uc4.SPNName = "Faluts";
            this.vCinc_uc4.TabIndex = 260;
            // 
            // vCinc_uc3
            // 
            this.vCinc_uc3.A_FirstByteIndex = 2;
            this.vCinc_uc3.Address = "29";
            this.vCinc_uc3.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc3.Bit0Title = "Sta 1 Idle Knob Fault";
            this.vCinc_uc3.Bit1Title = "Sta 1 JoyStick Z Fault";
            this.vCinc_uc3.Bit2Title = "Sta 1 Stbd Lever Fault";
            this.vCinc_uc3.Bit3Title = "Sta 1 Port Lever Fault";
            this.vCinc_uc3.Bit4Title = "Sta 1 Joy-Y Fault";
            this.vCinc_uc3.Bit5Title = "Sta 1 Joy-X Fault";
            this.vCinc_uc3.Bit6Title = "lSta 1 Tiller 2 Fault";
            this.vCinc_uc3.Bit7Title = "";
            this.vCinc_uc3.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc3.Location = new System.Drawing.Point(1520, 93);
            this.vCinc_uc3.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc3.Name = "vCinc_uc3";
            this.vCinc_uc3.PGN = "FF49";
            this.vCinc_uc3.Size = new System.Drawing.Size(80, 260);
            this.vCinc_uc3.SPNName = "Faluts";
            this.vCinc_uc3.TabIndex = 259;
            // 
            // vCinc_uc2
            // 
            this.vCinc_uc2.A_FirstByteIndex = 1;
            this.vCinc_uc2.Address = "29";
            this.vCinc_uc2.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc2.Bit0Title = "Stbd Bucket NFU";
            this.vCinc_uc2.Bit1Title = "Port Bucket NFU";
            this.vCinc_uc2.Bit2Title = "Stbd Nozzle NFU";
            this.vCinc_uc2.Bit3Title = "Port Nozzle NFU";
            this.vCinc_uc2.Bit4Title = "Stbd Tab NFU";
            this.vCinc_uc2.Bit5Title = "Port Tab NFU";
            this.vCinc_uc2.Bit6Title = "";
            this.vCinc_uc2.Bit7Title = "";
            this.vCinc_uc2.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc2.Location = new System.Drawing.Point(1440, 93);
            this.vCinc_uc2.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc2.Name = "vCinc_uc2";
            this.vCinc_uc2.PGN = "FF49";
            this.vCinc_uc2.Size = new System.Drawing.Size(80, 260);
            this.vCinc_uc2.SPNName = "Faluts";
            this.vCinc_uc2.TabIndex = 258;
            // 
            // vCinc_uc1
            // 
            this.vCinc_uc1.Address = "29";
            this.vCinc_uc1.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc1.Bit0Title = "Stbd Bucket";
            this.vCinc_uc1.Bit1Title = "Port Bucket";
            this.vCinc_uc1.Bit2Title = "Stbd Nozzle";
            this.vCinc_uc1.Bit3Title = "Port Nozzle";
            this.vCinc_uc1.Bit4Title = "Stbd Tab";
            this.vCinc_uc1.Bit5Title = "Port Tab";
            this.vCinc_uc1.Bit6Title = "";
            this.vCinc_uc1.Bit7Title = "";
            this.vCinc_uc1.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc1.Location = new System.Drawing.Point(1360, 93);
            this.vCinc_uc1.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc1.Name = "vCinc_uc1";
            this.vCinc_uc1.PGN = "FF49";
            this.vCinc_uc1.Size = new System.Drawing.Size(80, 260);
            this.vCinc_uc1.SPNName = "Faluts";
            this.vCinc_uc1.TabIndex = 257;
            // 
            // vCinc_uc12
            // 
            this.vCinc_uc12.A_FirstByteIndex = 7;
            this.vCinc_uc12.Address = "29";
            this.vCinc_uc12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.vCinc_uc12.Bit0Title = "";
            this.vCinc_uc12.Bit1Title = " ";
            this.vCinc_uc12.Bit2Title = " ";
            this.vCinc_uc12.Bit3Title = "  Sta1 Tiller Selec";
            this.vCinc_uc12.Bit4Title = " ";
            this.vCinc_uc12.Bit5Title = " ";
            this.vCinc_uc12.Bit6Title = "Flip_Dutycycle";
            this.vCinc_uc12.Bit7Title = "";
            this.vCinc_uc12.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc12.Location = new System.Drawing.Point(2005, 93);
            this.vCinc_uc12.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc12.Name = "vCinc_uc12";
            this.vCinc_uc12.PGN = "FF59";
            this.vCinc_uc12.Size = new System.Drawing.Size(99, 260);
            this.vCinc_uc12.SPNName = "SWITCHES raw";
            this.vCinc_uc12.TabIndex = 270;
            // 
            // vCinc_uc11
            // 
            this.vCinc_uc11.A_FirstByteIndex = 6;
            this.vCinc_uc11.Address = "29";
            this.vCinc_uc11.BackColor = System.Drawing.Color.Gainsboro;
            this.vCinc_uc11.Bit0Title = "AutopilotON";
            this.vCinc_uc11.Bit1Title = " Turn to Port";
            this.vCinc_uc11.Bit2Title = " Turn to Stbd";
            this.vCinc_uc11.Bit3Title = " ";
            this.vCinc_uc11.Bit4Title = " TillerSelection";
            this.vCinc_uc11.Bit5Title = " DPSwitch_Sta2";
            this.vCinc_uc11.Bit6Title = "bAP_OverrideInEffect";
            this.vCinc_uc11.Bit7Title = "apState";
            this.vCinc_uc11.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc11.Location = new System.Drawing.Point(2389, 93);
            this.vCinc_uc11.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc11.Name = "vCinc_uc11";
            this.vCinc_uc11.PGN = "FF53";
            this.vCinc_uc11.Size = new System.Drawing.Size(167, 260);
            this.vCinc_uc11.SPNName = "AUTOPILOT";
            this.vCinc_uc11.TabIndex = 269;
            // 
            // vCinc_uc10
            // 
            this.vCinc_uc10.A_FirstByteIndex = 1;
            this.vCinc_uc10.Address = "29";
            this.vCinc_uc10.BackColor = System.Drawing.Color.Gainsboro;
            this.vCinc_uc10.Bit0Title = " ";
            this.vCinc_uc10.Bit1Title = " ";
            this.vCinc_uc10.Bit2Title = " ";
            this.vCinc_uc10.Bit3Title = " ";
            this.vCinc_uc10.Bit4Title = " ";
            this.vCinc_uc10.Bit5Title = " ";
            this.vCinc_uc10.Bit6Title = "";
            this.vCinc_uc10.Bit7Title = "";
            this.vCinc_uc10.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_uc10.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc10.Location = new System.Drawing.Point(2104, 93);
            this.vCinc_uc10.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc10.MaxValue = 3;
            this.vCinc_uc10.Name = "vCinc_uc10";
            this.vCinc_uc10.PGN = "FF53";
            this.vCinc_uc10.Size = new System.Drawing.Size(200, 260);
            this.vCinc_uc10.SPNName = "active STATION";
            this.vCinc_uc10.TabIndex = 268;
            this.vCinc_uc10.Value = 1;
            // 
            // vCinc_uc9
            // 
            this.vCinc_uc9.A_FirstByteIndex = 4;
            this.vCinc_uc9.Address = "29";
            this.vCinc_uc9.BackColor = System.Drawing.Color.Gainsboro;
            this.vCinc_uc9.Bit0Title = " tr_dk";
            this.vCinc_uc9.Bit1Title = " ";
            this.vCinc_uc9.Bit2Title = " ";
            this.vCinc_uc9.Bit3Title = " ";
            this.vCinc_uc9.Bit4Title = " ";
            this.vCinc_uc9.Bit5Title = " ";
            this.vCinc_uc9.Bit6Title = "";
            this.vCinc_uc9.Bit7Title = "";
            this.vCinc_uc9.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc9.Location = new System.Drawing.Point(2304, 93);
            this.vCinc_uc9.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc9.Name = "vCinc_uc9";
            this.vCinc_uc9.PGN = "FF53";
            this.vCinc_uc9.Size = new System.Drawing.Size(85, 260);
            this.vCinc_uc9.SPNName = "opMode";
            this.vCinc_uc9.TabIndex = 267;
            // 
            // vCinc_uc24
            // 
            this.vCinc_uc24.A_FirstByteIndex = 7;
            this.vCinc_uc24.Address = "29";
            this.vCinc_uc24.BackColor = System.Drawing.Color.Silver;
            this.vCinc_uc24.Bit0Title = " ";
            this.vCinc_uc24.Bit1Title = " ";
            this.vCinc_uc24.Bit2Title = " ";
            this.vCinc_uc24.Bit3Title = " ";
            this.vCinc_uc24.Bit4Title = " ";
            this.vCinc_uc24.Bit5Title = " ";
            this.vCinc_uc24.Bit6Title = "";
            this.vCinc_uc24.Bit7Title = "";
            this.vCinc_uc24.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_uc24.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc24.Location = new System.Drawing.Point(2258, 651);
            this.vCinc_uc24.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc24.Name = "vCinc_uc24";
            this.vCinc_uc24.PGN = "FEFC";
            this.vCinc_uc24.Size = new System.Drawing.Size(200, 260);
            this.vCinc_uc24.SPNName = "Seng";
            this.vCinc_uc24.TabIndex = 281;
            // 
            // vCinc_uc23
            // 
            this.vCinc_uc23.A_FirstByteIndex = 6;
            this.vCinc_uc23.Address = "29";
            this.vCinc_uc23.BackColor = System.Drawing.Color.Silver;
            this.vCinc_uc23.Bit0Title = " ";
            this.vCinc_uc23.Bit1Title = " ";
            this.vCinc_uc23.Bit2Title = " ";
            this.vCinc_uc23.Bit3Title = " ";
            this.vCinc_uc23.Bit4Title = " ";
            this.vCinc_uc23.Bit5Title = " ";
            this.vCinc_uc23.Bit6Title = "";
            this.vCinc_uc23.Bit7Title = "";
            this.vCinc_uc23.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_uc23.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc23.Location = new System.Drawing.Point(2076, 651);
            this.vCinc_uc23.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc23.Name = "vCinc_uc23";
            this.vCinc_uc23.PGN = "FEFC";
            this.vCinc_uc23.Size = new System.Drawing.Size(182, 260);
            this.vCinc_uc23.SPNName = "Peng";
            this.vCinc_uc23.TabIndex = 280;
            // 
            // vCinc_uc22
            // 
            this.vCinc_uc22.A_FirstByteIndex = 5;
            this.vCinc_uc22.Address = "29";
            this.vCinc_uc22.BackColor = System.Drawing.Color.Silver;
            this.vCinc_uc22.Bit0Title = " ";
            this.vCinc_uc22.Bit1Title = " ";
            this.vCinc_uc22.Bit2Title = " ";
            this.vCinc_uc22.Bit3Title = " ";
            this.vCinc_uc22.Bit4Title = " ";
            this.vCinc_uc22.Bit5Title = " ";
            this.vCinc_uc22.Bit6Title = "";
            this.vCinc_uc22.Bit7Title = "";
            this.vCinc_uc22.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_uc22.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc22.Location = new System.Drawing.Point(1910, 651);
            this.vCinc_uc22.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc22.Name = "vCinc_uc22";
            this.vCinc_uc22.PGN = "FEFC";
            this.vCinc_uc22.Size = new System.Drawing.Size(166, 260);
            this.vCinc_uc22.SPNName = "Sbuk";
            this.vCinc_uc22.TabIndex = 279;
            this.vCinc_uc22.Value = 128;
            // 
            // vCinc_uc21
            // 
            this.vCinc_uc21.A_FirstByteIndex = 3;
            this.vCinc_uc21.Address = "29";
            this.vCinc_uc21.BackColor = System.Drawing.Color.Silver;
            this.vCinc_uc21.Bit0Title = " ";
            this.vCinc_uc21.Bit1Title = " ";
            this.vCinc_uc21.Bit2Title = " ";
            this.vCinc_uc21.Bit3Title = " ";
            this.vCinc_uc21.Bit4Title = " ";
            this.vCinc_uc21.Bit5Title = " ";
            this.vCinc_uc21.Bit6Title = "";
            this.vCinc_uc21.Bit7Title = "";
            this.vCinc_uc21.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_uc21.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc21.Location = new System.Drawing.Point(1737, 651);
            this.vCinc_uc21.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc21.Name = "vCinc_uc21";
            this.vCinc_uc21.PGN = "FEFC";
            this.vCinc_uc21.Size = new System.Drawing.Size(173, 260);
            this.vCinc_uc21.SPNName = "Pnoz";
            this.vCinc_uc21.TabIndex = 278;
            this.vCinc_uc21.Value = 128;
            // 
            // vCinc_uc20
            // 
            this.vCinc_uc20.A_FirstByteIndex = 2;
            this.vCinc_uc20.Address = "29";
            this.vCinc_uc20.BackColor = System.Drawing.Color.Silver;
            this.vCinc_uc20.Bit0Title = " ";
            this.vCinc_uc20.Bit1Title = " ";
            this.vCinc_uc20.Bit2Title = " ";
            this.vCinc_uc20.Bit3Title = " ";
            this.vCinc_uc20.Bit4Title = " ";
            this.vCinc_uc20.Bit5Title = " ";
            this.vCinc_uc20.Bit6Title = "";
            this.vCinc_uc20.Bit7Title = "";
            this.vCinc_uc20.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_uc20.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc20.Location = new System.Drawing.Point(1563, 651);
            this.vCinc_uc20.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc20.Name = "vCinc_uc20";
            this.vCinc_uc20.PGN = "FEFC";
            this.vCinc_uc20.Size = new System.Drawing.Size(174, 260);
            this.vCinc_uc20.SPNName = "Snoz";
            this.vCinc_uc20.TabIndex = 277;
            this.vCinc_uc20.Value = 128;
            // 
            // vCinc_uc19
            // 
            this.vCinc_uc19.Address = "29";
            this.vCinc_uc19.BackColor = System.Drawing.Color.Silver;
            this.vCinc_uc19.Bit0Title = " ";
            this.vCinc_uc19.Bit1Title = " ";
            this.vCinc_uc19.Bit2Title = " ";
            this.vCinc_uc19.Bit3Title = " ";
            this.vCinc_uc19.Bit4Title = " ";
            this.vCinc_uc19.Bit5Title = " ";
            this.vCinc_uc19.Bit6Title = "";
            this.vCinc_uc19.Bit7Title = "";
            this.vCinc_uc19.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_uc19.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc19.Location = new System.Drawing.Point(1378, 651);
            this.vCinc_uc19.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc19.Name = "vCinc_uc19";
            this.vCinc_uc19.PGN = "FEFC";
            this.vCinc_uc19.Size = new System.Drawing.Size(188, 260);
            this.vCinc_uc19.SPNName = "Pbuk";
            this.vCinc_uc19.TabIndex = 276;
            this.vCinc_uc19.Value = 128;
            // 
            // vCinc_uc17
            // 
            this.vCinc_uc17.A_FirstByteIndex = 6;
            this.vCinc_uc17.Address = "29";
            this.vCinc_uc17.BackColor = System.Drawing.Color.DarkSalmon;
            this.vCinc_uc17.Bit0Title = "isBlocked";
            this.vCinc_uc17.Bit1Title = " DP ovr helm";
            this.vCinc_uc17.Bit2Title = " DP ovr joy";
            this.vCinc_uc17.Bit3Title = " ";
            this.vCinc_uc17.Bit4Title = " ";
            this.vCinc_uc17.Bit5Title = " ";
            this.vCinc_uc17.Bit6Title = "";
            this.vCinc_uc17.Bit7Title = "";
            this.vCinc_uc17.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_uc17.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc17.Location = new System.Drawing.Point(1978, 391);
            this.vCinc_uc17.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc17.MaxValue = 65535;
            this.vCinc_uc17.Name = "vCinc_uc17";
            this.vCinc_uc17.NumberOfBytes = 2;
            this.vCinc_uc17.PGN = "FF66";
            this.vCinc_uc17.Size = new System.Drawing.Size(200, 260);
            this.vCinc_uc17.SPNName = "IDL RPM";
            this.vCinc_uc17.TabIndex = 275;
            this.vCinc_uc17.Value = 100;
            // 
            // vCinc_uc16
            // 
            this.vCinc_uc16.A_FirstByteIndex = 4;
            this.vCinc_uc16.Address = "29";
            this.vCinc_uc16.BackColor = System.Drawing.Color.DarkSalmon;
            this.vCinc_uc16.Bit0Title = "isBlocked";
            this.vCinc_uc16.Bit1Title = " DP ovr helm";
            this.vCinc_uc16.Bit2Title = " DP ovr joy";
            this.vCinc_uc16.Bit3Title = " ";
            this.vCinc_uc16.Bit4Title = " ";
            this.vCinc_uc16.Bit5Title = " ";
            this.vCinc_uc16.Bit6Title = "";
            this.vCinc_uc16.Bit7Title = "";
            this.vCinc_uc16.ControlMode = VCI_Forms_LIB.ControlModes.Gauge;
            this.vCinc_uc16.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc16.Location = new System.Drawing.Point(1763, 391);
            this.vCinc_uc16.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc16.MaxValue = 360;
            this.vCinc_uc16.Name = "vCinc_uc16";
            this.vCinc_uc16.NumberOfBytes = 2;
            this.vCinc_uc16.PGN = "FF66";
            this.vCinc_uc16.Size = new System.Drawing.Size(200, 260);
            this.vCinc_uc16.SPNName = "Helm";
            this.vCinc_uc16.TabIndex = 274;
            this.vCinc_uc16.Value = 180;
            // 
            // vCinc_uc15
            // 
            this.vCinc_uc15.A_FirstByteIndex = 2;
            this.vCinc_uc15.Address = "29";
            this.vCinc_uc15.BackColor = System.Drawing.Color.MistyRose;
            this.vCinc_uc15.Bit0Title = "isBlocked";
            this.vCinc_uc15.Bit1Title = " DP ovr helm";
            this.vCinc_uc15.Bit2Title = " DP ovr joy";
            this.vCinc_uc15.Bit3Title = " ";
            this.vCinc_uc15.Bit4Title = " ";
            this.vCinc_uc15.Bit5Title = " ";
            this.vCinc_uc15.Bit6Title = "";
            this.vCinc_uc15.Bit7Title = "";
            this.vCinc_uc15.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_uc15.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc15.Location = new System.Drawing.Point(1566, 391);
            this.vCinc_uc15.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc15.MaxValue = 65535;
            this.vCinc_uc15.Name = "vCinc_uc15";
            this.vCinc_uc15.NumberOfBytes = 2;
            this.vCinc_uc15.PGN = "FF65";
            this.vCinc_uc15.Size = new System.Drawing.Size(197, 260);
            this.vCinc_uc15.SPNName = "SOG x10";
            this.vCinc_uc15.TabIndex = 273;
            // 
            // vCinc_uc14
            // 
            this.vCinc_uc14.A_FirstByteIndex = 1;
            this.vCinc_uc14.Address = "29";
            this.vCinc_uc14.BackColor = System.Drawing.Color.MistyRose;
            this.vCinc_uc14.Bit0Title = "isBlocked";
            this.vCinc_uc14.Bit1Title = " DP ovr helm";
            this.vCinc_uc14.Bit2Title = " DP ovr joy";
            this.vCinc_uc14.Bit3Title = " ";
            this.vCinc_uc14.Bit4Title = " ";
            this.vCinc_uc14.Bit5Title = " ";
            this.vCinc_uc14.Bit6Title = "";
            this.vCinc_uc14.Bit7Title = "";
            this.vCinc_uc14.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc14.Location = new System.Drawing.Point(1469, 391);
            this.vCinc_uc14.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc14.Name = "vCinc_uc14";
            this.vCinc_uc14.PGN = "FF65";
            this.vCinc_uc14.Size = new System.Drawing.Size(87, 260);
            this.vCinc_uc14.SPNName = "DPovr";
            this.vCinc_uc14.TabIndex = 272;
            // 
            // vCinc_uc13
            // 
            this.vCinc_uc13.Address = "29";
            this.vCinc_uc13.BackColor = System.Drawing.Color.MistyRose;
            this.vCinc_uc13.Bit0Title = "prob";
            this.vCinc_uc13.Bit1Title = "ovr";
            this.vCinc_uc13.Bit2Title = "on";
            this.vCinc_uc13.Bit3Title = " ";
            this.vCinc_uc13.Bit4Title = " ";
            this.vCinc_uc13.Bit5Title = " ";
            this.vCinc_uc13.Bit6Title = "";
            this.vCinc_uc13.Bit7Title = "";
            this.vCinc_uc13.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc13.Location = new System.Drawing.Point(1378, 391);
            this.vCinc_uc13.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc13.Name = "vCinc_uc13";
            this.vCinc_uc13.PGN = "FF65";
            this.vCinc_uc13.Size = new System.Drawing.Size(81, 260);
            this.vCinc_uc13.SPNName = "DPstat";
            this.vCinc_uc13.TabIndex = 271;
            // 
            // FormDynPosition2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2588, 1303);
            this.Controls.Add(this.vCinc_uc24);
            this.Controls.Add(this.vCinc_uc23);
            this.Controls.Add(this.vCinc_uc22);
            this.Controls.Add(this.vCinc_uc21);
            this.Controls.Add(this.vCinc_uc20);
            this.Controls.Add(this.vCinc_uc19);
            this.Controls.Add(this.vCinc_uc17);
            this.Controls.Add(this.vCinc_uc16);
            this.Controls.Add(this.vCinc_uc15);
            this.Controls.Add(this.vCinc_uc14);
            this.Controls.Add(this.vCinc_uc13);
            this.Controls.Add(this.vCinc_uc12);
            this.Controls.Add(this.vCinc_uc11);
            this.Controls.Add(this.vCinc_uc10);
            this.Controls.Add(this.vCinc_uc9);
            this.Controls.Add(this.vCinc_SFversion2);
            this.Controls.Add(this.vCinc_SFversion1);
            this.Controls.Add(this.vCinc_uc5);
            this.Controls.Add(this.vCinc_uc6);
            this.Controls.Add(this.vCinc_uc7);
            this.Controls.Add(this.vCinc_uc8);
            this.Controls.Add(this.vCinc_uc4);
            this.Controls.Add(this.vCinc_uc3);
            this.Controls.Add(this.vCinc_uc2);
            this.Controls.Add(this.vCinc_uc1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.vCinc_uc18);
            this.Controls.Add(this.vCinc_LatLon_waypoint);
            this.Controls.Add(this.vCinc_LatLon_mapCnter);
            this.Controls.Add(this.vCinc_DynPos1);
            this.Controls.Add(this.btn_restBit1);
            this.Controls.Add(this.btn_restBit0);
            this.Controls.Add(this.cb_uniqueOn);
            this.Controls.Add(this.tb_CAN_Bus_View);
            this.Controls.Add(this.btn_RunStop);
            this.Controls.Add(this.lbl_OnScreenCount);
            this.Controls.Add(this.lbl_onBus);
            this.Controls.Add(this.btn_Validate);
            this.Name = "FormDynPosition2";
            this.Text = "FormDynPosition2";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_restBit1;
        private System.Windows.Forms.Button btn_restBit0;
        private System.Windows.Forms.CheckBox cb_uniqueOn;
        private System.Windows.Forms.TextBox tb_CAN_Bus_View;
        private System.Windows.Forms.Button btn_RunStop;
        private System.Windows.Forms.Label lbl_OnScreenCount;
        private System.Windows.Forms.Label lbl_onBus;
        private System.Windows.Forms.Button btn_Validate;
        private VCI_Forms_LIB.VCinc_DynPos vCinc_DynPos1;
        private VCI_Forms_LIB.VCinc_LatLon vCinc_LatLon_waypoint;
        private VCI_Forms_LIB.VCinc_LatLon vCinc_LatLon_mapCnter;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc18;
        private System.Windows.Forms.TrackBar trackBar1;
        private VCI_Forms_LIB.VCinc_SFversion vCinc_SFversion2;
        private VCI_Forms_LIB.VCinc_SFversion vCinc_SFversion1;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc5;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc6;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc7;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc8;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc4;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc3;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc2;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc1;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc12;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc11;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc10;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc9;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc24;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc23;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc22;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc21;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc20;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc19;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc17;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc16;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc15;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc14;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc13;
    }
}