namespace VCI_Forms_SPN.MyForms
{
    partial class PongForm
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
            this.tb_P1 = new System.Windows.Forms.TrackBar();
            this.tb_P2 = new System.Windows.Forms.TrackBar();
            this.btn_Start = new System.Windows.Forms.Button();
            this.lbl_P1pos = new System.Windows.Forms.Label();
            this.lbl_P2pos = new System.Windows.Forms.Label();
            this.lbl_ballXpos = new System.Windows.Forms.Label();
            this.lbl_ballYpos = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.vCinc_BallX = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_BallY = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_P2y = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_P1y = new VCI_Forms_LIB.VCinc_uc();
            this.cb_uniqueOn = new System.Windows.Forms.CheckBox();
            this.tb_CAN_Bus_View = new System.Windows.Forms.TextBox();
            this.btn_RunStop = new System.Windows.Forms.Button();
            this.lbl_OnScreenCount = new System.Windows.Forms.Label();
            this.lbl_onBus = new System.Windows.Forms.Label();
            this.btn_Validate = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.vCinc_SFversion3 = new VCI_Forms_LIB.VCinc_SFversion();
            this.vCinc_SFversion2 = new VCI_Forms_LIB.VCinc_SFversion();
            this.vCinc_SFversion1 = new VCI_Forms_LIB.VCinc_SFversion();
            ((System.ComponentModel.ISupportInitialize)(this.tb_P1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_P2)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_P1
            // 
            this.tb_P1.Location = new System.Drawing.Point(9, 12);
            this.tb_P1.Maximum = 480;
            this.tb_P1.Name = "tb_P1";
            this.tb_P1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tb_P1.Size = new System.Drawing.Size(90, 480);
            this.tb_P1.TabIndex = 1;
            this.tb_P1.Value = 240;
            // 
            // tb_P2
            // 
            this.tb_P2.Location = new System.Drawing.Point(911, 12);
            this.tb_P2.Maximum = 480;
            this.tb_P2.Name = "tb_P2";
            this.tb_P2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tb_P2.Size = new System.Drawing.Size(90, 480);
            this.tb_P2.TabIndex = 2;
            this.tb_P2.Value = 240;
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(96, 498);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(106, 52);
            this.btn_Start.TabIndex = 3;
            this.btn_Start.Text = "start";
            this.btn_Start.UseVisualStyleBackColor = true;
            // 
            // lbl_P1pos
            // 
            this.lbl_P1pos.AutoSize = true;
            this.lbl_P1pos.Location = new System.Drawing.Point(285, 512);
            this.lbl_P1pos.Name = "lbl_P1pos";
            this.lbl_P1pos.Size = new System.Drawing.Size(70, 25);
            this.lbl_P1pos.TabIndex = 4;
            this.lbl_P1pos.Text = "label1";
            // 
            // lbl_P2pos
            // 
            this.lbl_P2pos.AutoSize = true;
            this.lbl_P2pos.Location = new System.Drawing.Point(290, 557);
            this.lbl_P2pos.Name = "lbl_P2pos";
            this.lbl_P2pos.Size = new System.Drawing.Size(70, 25);
            this.lbl_P2pos.TabIndex = 5;
            this.lbl_P2pos.Text = "label1";
            // 
            // lbl_ballXpos
            // 
            this.lbl_ballXpos.AutoSize = true;
            this.lbl_ballXpos.Location = new System.Drawing.Point(431, 511);
            this.lbl_ballXpos.Name = "lbl_ballXpos";
            this.lbl_ballXpos.Size = new System.Drawing.Size(70, 25);
            this.lbl_ballXpos.TabIndex = 6;
            this.lbl_ballXpos.Text = "label1";
            // 
            // lbl_ballYpos
            // 
            this.lbl_ballYpos.AutoSize = true;
            this.lbl_ballYpos.Location = new System.Drawing.Point(436, 557);
            this.lbl_ballYpos.Name = "lbl_ballYpos";
            this.lbl_ballYpos.Size = new System.Drawing.Size(70, 25);
            this.lbl_ballYpos.TabIndex = 7;
            this.lbl_ballYpos.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(105, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 480);
            this.panel1.TabIndex = 0;
            // 
            // vCinc_BallX
            // 
            this.vCinc_BallX.A_FirstByteIndex = 6;
            this.vCinc_BallX.Address = "00";
            this.vCinc_BallX.BackColor = System.Drawing.Color.Moccasin;
            this.vCinc_BallX.Bit0Title = "";
            this.vCinc_BallX.Bit1Title = "";
            this.vCinc_BallX.Bit2Title = "";
            this.vCinc_BallX.Bit3Title = "";
            this.vCinc_BallX.Bit4Title = "";
            this.vCinc_BallX.Bit5Title = "";
            this.vCinc_BallX.Bit6Title = "";
            this.vCinc_BallX.Bit7Title = "";
            this.vCinc_BallX.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_BallX.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_BallX.Location = new System.Drawing.Point(934, 602);
            this.vCinc_BallX.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_BallX.MaxValue = 800;
            this.vCinc_BallX.Name = "vCinc_BallX";
            this.vCinc_BallX.NumberOfBytes = 2;
            this.vCinc_BallX.PGN = "FFF0";
            this.vCinc_BallX.Size = new System.Drawing.Size(200, 250);
            this.vCinc_BallX.SPNName = "bX";
            this.vCinc_BallX.TabIndex = 487;
            this.vCinc_BallX.Value = 60;
            // 
            // vCinc_BallY
            // 
            this.vCinc_BallY.A_FirstByteIndex = 4;
            this.vCinc_BallY.Address = "00";
            this.vCinc_BallY.BackColor = System.Drawing.Color.Moccasin;
            this.vCinc_BallY.Bit0Title = "";
            this.vCinc_BallY.Bit1Title = "";
            this.vCinc_BallY.Bit2Title = "";
            this.vCinc_BallY.Bit3Title = "";
            this.vCinc_BallY.Bit4Title = "";
            this.vCinc_BallY.Bit5Title = "";
            this.vCinc_BallY.Bit6Title = "";
            this.vCinc_BallY.Bit7Title = "";
            this.vCinc_BallY.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_BallY.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_BallY.Location = new System.Drawing.Point(734, 852);
            this.vCinc_BallY.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_BallY.MaxValue = 480;
            this.vCinc_BallY.Name = "vCinc_BallY";
            this.vCinc_BallY.NumberOfBytes = 2;
            this.vCinc_BallY.PGN = "FFF0";
            this.vCinc_BallY.Size = new System.Drawing.Size(200, 250);
            this.vCinc_BallY.SPNName = "bY";
            this.vCinc_BallY.TabIndex = 486;
            this.vCinc_BallY.Value = 60;
            // 
            // vCinc_P2y
            // 
            this.vCinc_P2y.A_FirstByteIndex = 2;
            this.vCinc_P2y.Address = "00";
            this.vCinc_P2y.BackColor = System.Drawing.Color.Moccasin;
            this.vCinc_P2y.Bit0Title = "";
            this.vCinc_P2y.Bit1Title = "";
            this.vCinc_P2y.Bit2Title = "";
            this.vCinc_P2y.Bit3Title = "";
            this.vCinc_P2y.Bit4Title = "";
            this.vCinc_P2y.Bit5Title = "";
            this.vCinc_P2y.Bit6Title = "";
            this.vCinc_P2y.Bit7Title = "";
            this.vCinc_P2y.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_P2y.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_P2y.Location = new System.Drawing.Point(231, 602);
            this.vCinc_P2y.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_P2y.MaxValue = 480;
            this.vCinc_P2y.Name = "vCinc_P2y";
            this.vCinc_P2y.NumberOfBytes = 2;
            this.vCinc_P2y.PGN = "FFF0";
            this.vCinc_P2y.Size = new System.Drawing.Size(200, 250);
            this.vCinc_P2y.SPNName = "P2y";
            this.vCinc_P2y.TabIndex = 485;
            this.vCinc_P2y.Value = 60;
            // 
            // vCinc_P1y
            // 
            this.vCinc_P1y.Address = "00";
            this.vCinc_P1y.BackColor = System.Drawing.Color.Moccasin;
            this.vCinc_P1y.Bit0Title = "";
            this.vCinc_P1y.Bit1Title = "";
            this.vCinc_P1y.Bit2Title = "";
            this.vCinc_P1y.Bit3Title = "";
            this.vCinc_P1y.Bit4Title = "";
            this.vCinc_P1y.Bit5Title = "";
            this.vCinc_P1y.Bit6Title = "";
            this.vCinc_P1y.Bit7Title = "";
            this.vCinc_P1y.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_P1y.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_P1y.Location = new System.Drawing.Point(31, 602);
            this.vCinc_P1y.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_P1y.MaxValue = 480;
            this.vCinc_P1y.Name = "vCinc_P1y";
            this.vCinc_P1y.NumberOfBytes = 2;
            this.vCinc_P1y.PGN = "FFF0";
            this.vCinc_P1y.Size = new System.Drawing.Size(200, 250);
            this.vCinc_P1y.SPNName = "P1y";
            this.vCinc_P1y.TabIndex = 484;
            this.vCinc_P1y.Value = 60;
            // 
            // cb_uniqueOn
            // 
            this.cb_uniqueOn.AutoSize = true;
            this.cb_uniqueOn.Location = new System.Drawing.Point(1292, 70);
            this.cb_uniqueOn.Margin = new System.Windows.Forms.Padding(6);
            this.cb_uniqueOn.Name = "cb_uniqueOn";
            this.cb_uniqueOn.Size = new System.Drawing.Size(292, 29);
            this.cb_uniqueOn.TabIndex = 483;
            this.cb_uniqueOn.Text = "group messages by pgn ?";
            this.cb_uniqueOn.UseVisualStyleBackColor = true;
            // 
            // tb_CAN_Bus_View
            // 
            this.tb_CAN_Bus_View.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tb_CAN_Bus_View.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_CAN_Bus_View.ForeColor = System.Drawing.Color.Lime;
            this.tb_CAN_Bus_View.Location = new System.Drawing.Point(1282, 109);
            this.tb_CAN_Bus_View.Margin = new System.Windows.Forms.Padding(6);
            this.tb_CAN_Bus_View.Multiline = true;
            this.tb_CAN_Bus_View.Name = "tb_CAN_Bus_View";
            this.tb_CAN_Bus_View.ReadOnly = true;
            this.tb_CAN_Bus_View.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_CAN_Bus_View.Size = new System.Drawing.Size(604, 462);
            this.tb_CAN_Bus_View.TabIndex = 482;
            this.tb_CAN_Bus_View.Text = ": Console Bkg.green  -c -0 ";
            // 
            // btn_RunStop
            // 
            this.btn_RunStop.Location = new System.Drawing.Point(1705, 30);
            this.btn_RunStop.Margin = new System.Windows.Forms.Padding(6);
            this.btn_RunStop.Name = "btn_RunStop";
            this.btn_RunStop.Size = new System.Drawing.Size(138, 42);
            this.btn_RunStop.TabIndex = 481;
            this.btn_RunStop.Text = "Send Can";
            this.btn_RunStop.UseVisualStyleBackColor = true;
            // 
            // lbl_OnScreenCount
            // 
            this.lbl_OnScreenCount.Location = new System.Drawing.Point(1421, 30);
            this.lbl_OnScreenCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_OnScreenCount.Name = "lbl_OnScreenCount";
            this.lbl_OnScreenCount.Size = new System.Drawing.Size(128, 26);
            this.lbl_OnScreenCount.TabIndex = 480;
            this.lbl_OnScreenCount.Text = "on screen ";
            this.lbl_OnScreenCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_onBus
            // 
            this.lbl_onBus.Location = new System.Drawing.Point(1287, 30);
            this.lbl_onBus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_onBus.Name = "lbl_onBus";
            this.lbl_onBus.Size = new System.Drawing.Size(128, 26);
            this.lbl_onBus.TabIndex = 479;
            this.lbl_onBus.Text = "on bus";
            this.lbl_onBus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Validate
            // 
            this.btn_Validate.Location = new System.Drawing.Point(1557, 30);
            this.btn_Validate.Margin = new System.Windows.Forms.Padding(6);
            this.btn_Validate.Name = "btn_Validate";
            this.btn_Validate.Size = new System.Drawing.Size(138, 42);
            this.btn_Validate.TabIndex = 478;
            this.btn_Validate.Text = "validator";
            this.btn_Validate.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(583, 512);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(150, 29);
            this.checkBox1.TabIndex = 488;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // vCinc_SFversion3
            // 
            this.vCinc_SFversion3.BackColor = System.Drawing.Color.DarkTurquoise;
            this.vCinc_SFversion3.Location = new System.Drawing.Point(1377, 790);
            this.vCinc_SFversion3.Major = 87;
            this.vCinc_SFversion3.Major_firstbyteindex = 0;
            this.vCinc_SFversion3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_SFversion3.Minor = 1003;
            this.vCinc_SFversion3.Minor_firstbyteindex = 1;
            this.vCinc_SFversion3.Minor_numofbytes = 2;
            this.vCinc_SFversion3.Name = "vCinc_SFversion3";
            this.vCinc_SFversion3.PGN = "ff54";
            this.vCinc_SFversion3.Priority = "0c";
            this.vCinc_SFversion3.PrnName = "SSRS CU";
            this.vCinc_SFversion3.Rev = 9166;
            this.vCinc_SFversion3.Rev_firstbyteindex = 3;
            this.vCinc_SFversion3.Rev_numofbytes = 2;
            this.vCinc_SFversion3.Size = new System.Drawing.Size(268, 134);
            this.vCinc_SFversion3.Source = "34";
            this.vCinc_SFversion3.TabIndex = 491;
            // 
            // vCinc_SFversion2
            // 
            this.vCinc_SFversion2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.vCinc_SFversion2.Location = new System.Drawing.Point(1285, 957);
            this.vCinc_SFversion2.Major = 86;
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
            this.vCinc_SFversion2.Size = new System.Drawing.Size(271, 132);
            this.vCinc_SFversion2.Source = "29";
            this.vCinc_SFversion2.TabIndex = 490;
            // 
            // vCinc_SFversion1
            // 
            this.vCinc_SFversion1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.vCinc_SFversion1.Location = new System.Drawing.Point(1486, 638);
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
            this.vCinc_SFversion1.Size = new System.Drawing.Size(246, 123);
            this.vCinc_SFversion1.Source = "29";
            this.vCinc_SFversion1.TabIndex = 489;
            // 
            // PongForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1922, 1126);
            this.Controls.Add(this.vCinc_SFversion3);
            this.Controls.Add(this.vCinc_SFversion2);
            this.Controls.Add(this.vCinc_SFversion1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.vCinc_BallX);
            this.Controls.Add(this.vCinc_BallY);
            this.Controls.Add(this.vCinc_P2y);
            this.Controls.Add(this.vCinc_P1y);
            this.Controls.Add(this.cb_uniqueOn);
            this.Controls.Add(this.tb_CAN_Bus_View);
            this.Controls.Add(this.btn_RunStop);
            this.Controls.Add(this.lbl_OnScreenCount);
            this.Controls.Add(this.lbl_onBus);
            this.Controls.Add(this.btn_Validate);
            this.Controls.Add(this.lbl_ballYpos);
            this.Controls.Add(this.lbl_ballXpos);
            this.Controls.Add(this.lbl_P2pos);
            this.Controls.Add(this.lbl_P1pos);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.tb_P2);
            this.Controls.Add(this.tb_P1);
            this.Controls.Add(this.panel1);
            this.Name = "PongForm";
            this.Text = "PngForm";
            ((System.ComponentModel.ISupportInitialize)(this.tb_P1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_P2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar tb_P1;
        private System.Windows.Forms.TrackBar tb_P2;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Label lbl_P1pos;
        private System.Windows.Forms.Label lbl_P2pos;
        private System.Windows.Forms.Label lbl_ballXpos;
        private System.Windows.Forms.Label lbl_ballYpos;
        private System.Windows.Forms.Panel panel1;
        private VCI_Forms_LIB.VCinc_uc vCinc_BallX;
        private VCI_Forms_LIB.VCinc_uc vCinc_BallY;
        private VCI_Forms_LIB.VCinc_uc vCinc_P2y;
        private VCI_Forms_LIB.VCinc_uc vCinc_P1y;
        private System.Windows.Forms.CheckBox cb_uniqueOn;
        private System.Windows.Forms.TextBox tb_CAN_Bus_View;
        private System.Windows.Forms.Button btn_RunStop;
        private System.Windows.Forms.Label lbl_OnScreenCount;
        private System.Windows.Forms.Label lbl_onBus;
        private System.Windows.Forms.Button btn_Validate;
        private System.Windows.Forms.CheckBox checkBox1;
        private VCI_Forms_LIB.VCinc_SFversion vCinc_SFversion3;
        private VCI_Forms_LIB.VCinc_SFversion vCinc_SFversion2;
        private VCI_Forms_LIB.VCinc_SFversion vCinc_SFversion1;
    }
}