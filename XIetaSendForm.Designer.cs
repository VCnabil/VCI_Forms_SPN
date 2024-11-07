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
            this.vCinc_LatLon_waypoint = new VCI_Forms_LIB.VCinc_LatLon();
            this.vCinc_LatLon_mapCnter = new VCI_Forms_LIB.VCinc_LatLon();
            this.mapPanel2 = new System.Windows.Forms.Panel();
            this.gridSquareSizeBox = new System.Windows.Forms.TextBox();
            this.unitsCheckBox = new System.Windows.Forms.CheckBox();
            this.cb_useTbLon = new System.Windows.Forms.CheckBox();
            this.cb_useTbLat = new System.Windows.Forms.CheckBox();
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
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(81, 815);
            this.trackBar1.Maximum = 72000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(633, 62);
            this.trackBar1.TabIndex = 224;
            this.trackBar1.TickFrequency = 1000;
            this.trackBar1.Value = 36000;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(546, 785);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 27);
            this.label1.TabIndex = 225;
            this.label1.Text = "heading";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tb_XI
            // 
            this.tb_XI.Location = new System.Drawing.Point(1700, 863);
            this.tb_XI.Name = "tb_XI";
            this.tb_XI.Size = new System.Drawing.Size(172, 31);
            this.tb_XI.TabIndex = 226;
            this.tb_XI.Text = "120000";
            // 
            // tb_ETA
            // 
            this.tb_ETA.Location = new System.Drawing.Point(1878, 863);
            this.tb_ETA.Name = "tb_ETA";
            this.tb_ETA.Size = new System.Drawing.Size(172, 31);
            this.tb_ETA.TabIndex = 227;
            this.tb_ETA.Text = "543210";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1704, 835);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 25);
            this.label2.TabIndex = 228;
            this.label2.Text = "09 FF67 29  Xi  eta";
            // 
            // trackBarLon
            // 
            this.trackBarLon.AutoSize = false;
            this.trackBarLon.LargeChange = 1;
            this.trackBarLon.Location = new System.Drawing.Point(81, 8);
            this.trackBarLon.Maximum = 100;
            this.trackBarLon.Minimum = -100;
            this.trackBarLon.Name = "trackBarLon";
            this.trackBarLon.Size = new System.Drawing.Size(680, 56);
            this.trackBarLon.TabIndex = 232;
            // 
            // trackBarLat
            // 
            this.trackBarLat.AutoSize = false;
            this.trackBarLat.LargeChange = 1;
            this.trackBarLat.Location = new System.Drawing.Point(12, 72);
            this.trackBarLat.Maximum = 100;
            this.trackBarLat.Minimum = -100;
            this.trackBarLat.Name = "trackBarLat";
            this.trackBarLat.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarLat.Size = new System.Drawing.Size(63, 680);
            this.trackBarLat.TabIndex = 233;
            // 
            // vCinc_LatLon_waypoint
            // 
            this.vCinc_LatLon_waypoint.BackColor = System.Drawing.Color.RosyBrown;
            this.vCinc_LatLon_waypoint.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_LatLon_waypoint.LatitudeDecimal = 0D;
            this.vCinc_LatLon_waypoint.Location = new System.Drawing.Point(1107, 70);
            this.vCinc_LatLon_waypoint.LongitudeDecimal = 0D;
            this.vCinc_LatLon_waypoint.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_LatLon_waypoint.Name = "vCinc_LatLon_waypoint";
            this.vCinc_LatLon_waypoint.Size = new System.Drawing.Size(314, 82);
            this.vCinc_LatLon_waypoint.TabIndex = 231;
            // 
            // vCinc_LatLon_mapCnter
            // 
            this.vCinc_LatLon_mapCnter.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.vCinc_LatLon_mapCnter.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_LatLon_mapCnter.LatitudeDecimal = 0D;
            this.vCinc_LatLon_mapCnter.Location = new System.Drawing.Point(785, 72);
            this.vCinc_LatLon_mapCnter.LongitudeDecimal = 0D;
            this.vCinc_LatLon_mapCnter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_LatLon_mapCnter.Name = "vCinc_LatLon_mapCnter";
            this.vCinc_LatLon_mapCnter.Size = new System.Drawing.Size(318, 82);
            this.vCinc_LatLon_mapCnter.TabIndex = 230;
            // 
            // mapPanel2
            // 
            this.mapPanel2.BackColor = System.Drawing.Color.White;
            this.mapPanel2.Location = new System.Drawing.Point(81, 72);
            this.mapPanel2.Name = "mapPanel2";
            this.mapPanel2.Size = new System.Drawing.Size(680, 680);
            this.mapPanel2.TabIndex = 234;
            // 
            // gridSquareSizeBox
            // 
            this.gridSquareSizeBox.Location = new System.Drawing.Point(81, 769);
            this.gridSquareSizeBox.Name = "gridSquareSizeBox";
            this.gridSquareSizeBox.Size = new System.Drawing.Size(83, 31);
            this.gridSquareSizeBox.TabIndex = 236;
            this.gridSquareSizeBox.Text = "100";
            // 
            // unitsCheckBox
            // 
            this.unitsCheckBox.AutoSize = true;
            this.unitsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unitsCheckBox.Location = new System.Drawing.Point(192, 773);
            this.unitsCheckBox.Margin = new System.Windows.Forms.Padding(1);
            this.unitsCheckBox.Name = "unitsCheckBox";
            this.unitsCheckBox.Size = new System.Drawing.Size(90, 27);
            this.unitsCheckBox.TabIndex = 235;
            this.unitsCheckBox.Text = "Meters";
            this.unitsCheckBox.UseVisualStyleBackColor = true;
            // 
            // cb_useTbLon
            // 
            this.cb_useTbLon.AutoSize = true;
            this.cb_useTbLon.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_useTbLon.Location = new System.Drawing.Point(8, 8);
            this.cb_useTbLon.Margin = new System.Windows.Forms.Padding(1);
            this.cb_useTbLon.Name = "cb_useTbLon";
            this.cb_useTbLon.Size = new System.Drawing.Size(67, 27);
            this.cb_useTbLon.TabIndex = 237;
            this.cb_useTbLon.Text = "use";
            this.cb_useTbLon.UseVisualStyleBackColor = true;
            // 
            // cb_useTbLat
            // 
            this.cb_useTbLat.AutoSize = true;
            this.cb_useTbLat.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.7F);
            this.cb_useTbLat.Location = new System.Drawing.Point(8, 39);
            this.cb_useTbLat.Name = "cb_useTbLat";
            this.cb_useTbLat.Size = new System.Drawing.Size(67, 27);
            this.cb_useTbLat.TabIndex = 238;
            this.cb_useTbLat.Text = "use";
            this.cb_useTbLat.UseVisualStyleBackColor = true;
            // 
            // XIetaSendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.ClientSize = new System.Drawing.Size(2375, 915);
            this.Controls.Add(this.cb_useTbLat);
            this.Controls.Add(this.cb_useTbLon);
            this.Controls.Add(this.gridSquareSizeBox);
            this.Controls.Add(this.unitsCheckBox);
            this.Controls.Add(this.mapPanel2);
            this.Controls.Add(this.trackBarLat);
            this.Controls.Add(this.trackBarLon);
            this.Controls.Add(this.vCinc_LatLon_waypoint);
            this.Controls.Add(this.vCinc_LatLon_mapCnter);
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
        private VCI_Forms_LIB.VCinc_LatLon vCinc_LatLon_mapCnter;
        private VCI_Forms_LIB.VCinc_LatLon vCinc_LatLon_waypoint;
        private System.Windows.Forms.TrackBar trackBarLon;
        private System.Windows.Forms.TrackBar trackBarLat;
        private System.Windows.Forms.Panel mapPanel2;
        private System.Windows.Forms.TextBox gridSquareSizeBox;
        private System.Windows.Forms.CheckBox unitsCheckBox;
        private System.Windows.Forms.CheckBox cb_useTbLon;
        private System.Windows.Forms.CheckBox cb_useTbLat;
    }
}