namespace VCI_Forms_SPN
{
    partial class XIetaSendForm
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
            this.cb_uniqueOn = new System.Windows.Forms.CheckBox();
            this.tb_CAN_Bus_View = new System.Windows.Forms.TextBox();
            this.btn_RunStop = new System.Windows.Forms.Button();
            this.lbl_OnScreenCount = new System.Windows.Forms.Label();
            this.lbl_onBus = new System.Windows.Forms.Label();
            this.btn_Validate = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_XI = new System.Windows.Forms.TextBox();
            this.tb_ETA = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarLon = new System.Windows.Forms.TrackBar();
            this.trackBarLat = new System.Windows.Forms.TrackBar();
            this.vCinc_LatLon2 = new VCI_Forms_LIB.VCinc_LatLon();
            this.vCinc_LatLon1 = new VCI_Forms_LIB.VCinc_LatLon();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLat)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_uniqueOn
            // 
            this.cb_uniqueOn.AutoSize = true;
            this.cb_uniqueOn.Location = new System.Drawing.Point(1712, 59);
            this.cb_uniqueOn.Margin = new System.Windows.Forms.Padding(6);
            this.cb_uniqueOn.Name = "cb_uniqueOn";
            this.cb_uniqueOn.Size = new System.Drawing.Size(292, 29);
            this.cb_uniqueOn.TabIndex = 223;
            this.cb_uniqueOn.Text = "group messages by pgn ?";
            this.cb_uniqueOn.UseVisualStyleBackColor = true;
            // 
            // tb_CAN_Bus_View
            // 
            this.tb_CAN_Bus_View.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tb_CAN_Bus_View.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_CAN_Bus_View.ForeColor = System.Drawing.Color.Lime;
            this.tb_CAN_Bus_View.Location = new System.Drawing.Point(1701, 100);
            this.tb_CAN_Bus_View.Margin = new System.Windows.Forms.Padding(6);
            this.tb_CAN_Bus_View.Multiline = true;
            this.tb_CAN_Bus_View.Name = "tb_CAN_Bus_View";
            this.tb_CAN_Bus_View.ReadOnly = true;
            this.tb_CAN_Bus_View.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_CAN_Bus_View.Size = new System.Drawing.Size(659, 727);
            this.tb_CAN_Bus_View.TabIndex = 222;
            this.tb_CAN_Bus_View.Text = ": Console Bkg.green  -c -0 ";
            // 
            // btn_RunStop
            // 
            this.btn_RunStop.Location = new System.Drawing.Point(2167, 8);
            this.btn_RunStop.Margin = new System.Windows.Forms.Padding(6);
            this.btn_RunStop.Name = "btn_RunStop";
            this.btn_RunStop.Size = new System.Drawing.Size(150, 44);
            this.btn_RunStop.TabIndex = 221;
            this.btn_RunStop.Text = "Send Can";
            this.btn_RunStop.UseVisualStyleBackColor = true;
            // 
            // lbl_OnScreenCount
            // 
            this.lbl_OnScreenCount.Location = new System.Drawing.Point(1853, 17);
            this.lbl_OnScreenCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_OnScreenCount.Name = "lbl_OnScreenCount";
            this.lbl_OnScreenCount.Size = new System.Drawing.Size(140, 27);
            this.lbl_OnScreenCount.TabIndex = 220;
            this.lbl_OnScreenCount.Text = "on screen ";
            this.lbl_OnScreenCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_onBus
            // 
            this.lbl_onBus.Location = new System.Drawing.Point(1693, 17);
            this.lbl_onBus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_onBus.Name = "lbl_onBus";
            this.lbl_onBus.Size = new System.Drawing.Size(140, 27);
            this.lbl_onBus.TabIndex = 219;
            this.lbl_onBus.Text = "on bus";
            this.lbl_onBus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Validate
            // 
            this.btn_Validate.Location = new System.Drawing.Point(2005, 8);
            this.btn_Validate.Margin = new System.Windows.Forms.Padding(6);
            this.btn_Validate.Name = "btn_Validate";
            this.btn_Validate.Size = new System.Drawing.Size(150, 44);
            this.btn_Validate.TabIndex = 218;
            this.btn_Validate.Text = "validator";
            this.btn_Validate.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(110, 8);
            this.trackBar1.Maximum = 72000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(480, 90);
            this.trackBar1.TabIndex = 224;
            this.trackBar1.Value = 36000;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(364, 143);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 27);
            this.label1.TabIndex = 225;
            this.label1.Text = "heading";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_XI
            // 
            this.tb_XI.Location = new System.Drawing.Point(149, 173);
            this.tb_XI.Name = "tb_XI";
            this.tb_XI.Size = new System.Drawing.Size(172, 31);
            this.tb_XI.TabIndex = 226;
            this.tb_XI.Text = "120000";
            // 
            // tb_ETA
            // 
            this.tb_ETA.Location = new System.Drawing.Point(327, 173);
            this.tb_ETA.Name = "tb_ETA";
            this.tb_ETA.Size = new System.Drawing.Size(172, 31);
            this.tb_ETA.TabIndex = 227;
            this.tb_ETA.Text = "543210";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 25);
            this.label2.TabIndex = 228;
            this.label2.Text = "09 FF67 29  Xi  eta";
            // 
            // trackBarLon
            // 
            this.trackBarLon.Location = new System.Drawing.Point(110, 413);
            this.trackBarLon.Maximum = 180000;
            this.trackBarLon.Minimum = -180000;
            this.trackBarLon.Name = "trackBarLon";
            this.trackBarLon.Size = new System.Drawing.Size(480, 90);
            this.trackBarLon.TabIndex = 232;
            // 
            // trackBarLat
            // 
            this.trackBarLat.Location = new System.Drawing.Point(14, 8);
            this.trackBarLat.Maximum = 90000;
            this.trackBarLat.Minimum = -90000;
            this.trackBarLat.Name = "trackBarLat";
            this.trackBarLat.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarLat.Size = new System.Drawing.Size(90, 482);
            this.trackBarLat.TabIndex = 233;
            // 
            // vCinc_LatLon2
            // 
            this.vCinc_LatLon2.BackColor = System.Drawing.Color.RosyBrown;
            this.vCinc_LatLon2.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_LatLon2.LatitudeDecimal = 0D;
            this.vCinc_LatLon2.Location = new System.Drawing.Point(147, 298);
            this.vCinc_LatLon2.LongitudeDecimal = 0D;
            this.vCinc_LatLon2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_LatLon2.Name = "vCinc_LatLon2";
            this.vCinc_LatLon2.Size = new System.Drawing.Size(322, 82);
            this.vCinc_LatLon2.TabIndex = 231;
            // 
            // vCinc_LatLon1
            // 
            this.vCinc_LatLon1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.vCinc_LatLon1.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_LatLon1.LatitudeDecimal = 0D;
            this.vCinc_LatLon1.Location = new System.Drawing.Point(147, 210);
            this.vCinc_LatLon1.LongitudeDecimal = 0D;
            this.vCinc_LatLon1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_LatLon1.Name = "vCinc_LatLon1";
            this.vCinc_LatLon1.Size = new System.Drawing.Size(318, 82);
            this.vCinc_LatLon1.TabIndex = 230;
            // 
            // XIetaSendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2375, 840);
            this.Controls.Add(this.trackBarLat);
            this.Controls.Add(this.trackBarLon);
            this.Controls.Add(this.vCinc_LatLon2);
            this.Controls.Add(this.vCinc_LatLon1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_ETA);
            this.Controls.Add(this.tb_XI);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.cb_uniqueOn);
            this.Controls.Add(this.tb_CAN_Bus_View);
            this.Controls.Add(this.btn_RunStop);
            this.Controls.Add(this.lbl_OnScreenCount);
            this.Controls.Add(this.lbl_onBus);
            this.Controls.Add(this.btn_Validate);
            this.Name = "XIetaSendForm";
            this.Text = "XIetaSendForm";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cb_uniqueOn;
        private System.Windows.Forms.TextBox tb_CAN_Bus_View;
        private System.Windows.Forms.Button btn_RunStop;
        private System.Windows.Forms.Label lbl_OnScreenCount;
        private System.Windows.Forms.Label lbl_onBus;
        private System.Windows.Forms.Button btn_Validate;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_XI;
        private System.Windows.Forms.TextBox tb_ETA;
        private System.Windows.Forms.Label label2;
        private VCI_Forms_LIB.VCinc_LatLon vCinc_LatLon1;
        private VCI_Forms_LIB.VCinc_LatLon vCinc_LatLon2;
        private System.Windows.Forms.TrackBar trackBarLon;
        private System.Windows.Forms.TrackBar trackBarLat;
    }
}