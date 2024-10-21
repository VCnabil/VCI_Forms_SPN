namespace VCI_Forms_SPN
{
    partial class C3iForm
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
            this.vCinc_uc1 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc2 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc3 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc4 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc5 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc6 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc7 = new VCI_Forms_LIB.VCinc_uc();
            this.SuspendLayout();
            // 
            // cb_uniqueOn
            // 
            this.cb_uniqueOn.AutoSize = true;
            this.cb_uniqueOn.Location = new System.Drawing.Point(1692, 56);
            this.cb_uniqueOn.Margin = new System.Windows.Forms.Padding(6);
            this.cb_uniqueOn.Name = "cb_uniqueOn";
            this.cb_uniqueOn.Size = new System.Drawing.Size(292, 29);
            this.cb_uniqueOn.TabIndex = 217;
            this.cb_uniqueOn.Text = "group messages by pgn ?";
            this.cb_uniqueOn.UseVisualStyleBackColor = true;
            // 
            // tb_CAN_Bus_View
            // 
            this.tb_CAN_Bus_View.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tb_CAN_Bus_View.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_CAN_Bus_View.ForeColor = System.Drawing.Color.Lime;
            this.tb_CAN_Bus_View.Location = new System.Drawing.Point(1681, 97);
            this.tb_CAN_Bus_View.Margin = new System.Windows.Forms.Padding(6);
            this.tb_CAN_Bus_View.Multiline = true;
            this.tb_CAN_Bus_View.Name = "tb_CAN_Bus_View";
            this.tb_CAN_Bus_View.ReadOnly = true;
            this.tb_CAN_Bus_View.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_CAN_Bus_View.Size = new System.Drawing.Size(659, 1394);
            this.tb_CAN_Bus_View.TabIndex = 216;
            this.tb_CAN_Bus_View.Text = ": Console Bkg.green  -c -0 ";
            // 
            // btn_RunStop
            // 
            this.btn_RunStop.Location = new System.Drawing.Point(2147, 5);
            this.btn_RunStop.Margin = new System.Windows.Forms.Padding(6);
            this.btn_RunStop.Name = "btn_RunStop";
            this.btn_RunStop.Size = new System.Drawing.Size(150, 44);
            this.btn_RunStop.TabIndex = 215;
            this.btn_RunStop.Text = "Send Can";
            this.btn_RunStop.UseVisualStyleBackColor = true;
            // 
            // lbl_OnScreenCount
            // 
            this.lbl_OnScreenCount.Location = new System.Drawing.Point(1833, 14);
            this.lbl_OnScreenCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_OnScreenCount.Name = "lbl_OnScreenCount";
            this.lbl_OnScreenCount.Size = new System.Drawing.Size(140, 27);
            this.lbl_OnScreenCount.TabIndex = 214;
            this.lbl_OnScreenCount.Text = "on screen ";
            this.lbl_OnScreenCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_onBus
            // 
            this.lbl_onBus.Location = new System.Drawing.Point(1673, 14);
            this.lbl_onBus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_onBus.Name = "lbl_onBus";
            this.lbl_onBus.Size = new System.Drawing.Size(140, 27);
            this.lbl_onBus.TabIndex = 213;
            this.lbl_onBus.Text = "on bus";
            this.lbl_onBus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Validate
            // 
            this.btn_Validate.Location = new System.Drawing.Point(1985, 5);
            this.btn_Validate.Margin = new System.Windows.Forms.Padding(6);
            this.btn_Validate.Name = "btn_Validate";
            this.btn_Validate.Size = new System.Drawing.Size(150, 44);
            this.btn_Validate.TabIndex = 212;
            this.btn_Validate.Text = "validator";
            this.btn_Validate.UseVisualStyleBackColor = true;
            // 
            // vCinc_uc1
            // 
            this.vCinc_uc1.A_FirstByteIndex = 3;
            this.vCinc_uc1.Address = "00";
            this.vCinc_uc1.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc1.Bit0Title = "";
            this.vCinc_uc1.Bit1Title = "";
            this.vCinc_uc1.Bit2Title = "";
            this.vCinc_uc1.Bit3Title = "";
            this.vCinc_uc1.Bit4Title = "";
            this.vCinc_uc1.Bit5Title = "";
            this.vCinc_uc1.Bit6Title = "";
            this.vCinc_uc1.Bit7Title = "";
            this.vCinc_uc1.ControlMode = VCI_Forms_LIB.ControlModes.Gauge;
            this.vCinc_uc1.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc1.Location = new System.Drawing.Point(9, 5);
            this.vCinc_uc1.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc1.MaxValue = 41000;
            this.vCinc_uc1.Name = "vCinc_uc1";
            this.vCinc_uc1.NumberOfBytes = 2;
            this.vCinc_uc1.PGN = "F004";
            this.vCinc_uc1.Size = new System.Drawing.Size(200, 260);
            this.vCinc_uc1.SPNName = "Electronic Engine Controller";
            this.vCinc_uc1.TabIndex = 218;
            this.vCinc_uc1.Value = 1000;
            // 
            // vCinc_uc2
            // 
            this.vCinc_uc2.A_FirstByteIndex = 3;
            this.vCinc_uc2.Address = "00";
            this.vCinc_uc2.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc2.Bit0Title = "";
            this.vCinc_uc2.Bit1Title = "";
            this.vCinc_uc2.Bit2Title = "";
            this.vCinc_uc2.Bit3Title = "";
            this.vCinc_uc2.Bit4Title = "";
            this.vCinc_uc2.Bit5Title = "";
            this.vCinc_uc2.Bit6Title = "";
            this.vCinc_uc2.Bit7Title = "";
            this.vCinc_uc2.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_uc2.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc2.Location = new System.Drawing.Point(209, 5);
            this.vCinc_uc2.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc2.Name = "vCinc_uc2";
            this.vCinc_uc2.PGN = "FEEF";
            this.vCinc_uc2.Size = new System.Drawing.Size(200, 260);
            this.vCinc_uc2.SPNName = " Engine pressure";
            this.vCinc_uc2.TabIndex = 219;
            // 
            // vCinc_uc3
            // 
            this.vCinc_uc3.Address = "00";
            this.vCinc_uc3.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc3.Bit0Title = "";
            this.vCinc_uc3.Bit1Title = "";
            this.vCinc_uc3.Bit2Title = "";
            this.vCinc_uc3.Bit3Title = "";
            this.vCinc_uc3.Bit4Title = "";
            this.vCinc_uc3.Bit5Title = "";
            this.vCinc_uc3.Bit6Title = "";
            this.vCinc_uc3.Bit7Title = "";
            this.vCinc_uc3.ControlMode = VCI_Forms_LIB.ControlModes.Gauge;
            this.vCinc_uc3.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc3.Location = new System.Drawing.Point(409, 5);
            this.vCinc_uc3.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc3.Name = "vCinc_uc3";
            this.vCinc_uc3.PGN = "FEEE";
            this.vCinc_uc3.Size = new System.Drawing.Size(200, 260);
            this.vCinc_uc3.SPNName = "Engine Temperture";
            this.vCinc_uc3.TabIndex = 220;
            // 
            // vCinc_uc4
            // 
            this.vCinc_uc4.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc4.Bit0Title = "";
            this.vCinc_uc4.Bit1Title = "";
            this.vCinc_uc4.Bit2Title = "";
            this.vCinc_uc4.Bit3Title = "";
            this.vCinc_uc4.Bit4Title = "";
            this.vCinc_uc4.Bit5Title = "";
            this.vCinc_uc4.Bit6Title = "";
            this.vCinc_uc4.Bit7Title = "";
            this.vCinc_uc4.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_uc4.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc4.Location = new System.Drawing.Point(609, 5);
            this.vCinc_uc4.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc4.Name = "vCinc_uc4";
            this.vCinc_uc4.PGN = "0000";
            this.vCinc_uc4.Size = new System.Drawing.Size(200, 260);
            this.vCinc_uc4.SPNName = "ECO";
            this.vCinc_uc4.TabIndex = 221;
            this.vCinc_uc4.Value = 18;
            // 
            // vCinc_uc5
            // 
            this.vCinc_uc5.Address = "00";
            this.vCinc_uc5.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc5.Bit0Title = "";
            this.vCinc_uc5.Bit1Title = "";
            this.vCinc_uc5.Bit2Title = "";
            this.vCinc_uc5.Bit3Title = "";
            this.vCinc_uc5.Bit4Title = "";
            this.vCinc_uc5.Bit5Title = "";
            this.vCinc_uc5.Bit6Title = "";
            this.vCinc_uc5.Bit7Title = "";
            this.vCinc_uc5.ControlMode = VCI_Forms_LIB.ControlModes.Gauge;
            this.vCinc_uc5.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc5.Location = new System.Drawing.Point(809, 5);
            this.vCinc_uc5.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc5.MaxValue = 320;
            this.vCinc_uc5.Name = "vCinc_uc5";
            this.vCinc_uc5.NumberOfBytes = 2;
            this.vCinc_uc5.PGN = "FEF7";
            this.vCinc_uc5.Size = new System.Drawing.Size(200, 260);
            this.vCinc_uc5.SPNName = "Volts";
            this.vCinc_uc5.TabIndex = 222;
            this.vCinc_uc5.Value = 10;
            // 
            // vCinc_uc6
            // 
            this.vCinc_uc6.Address = "00";
            this.vCinc_uc6.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc6.Bit0Title = "";
            this.vCinc_uc6.Bit1Title = "";
            this.vCinc_uc6.Bit2Title = "";
            this.vCinc_uc6.Bit3Title = "";
            this.vCinc_uc6.Bit4Title = "";
            this.vCinc_uc6.Bit5Title = "";
            this.vCinc_uc6.Bit6Title = "";
            this.vCinc_uc6.Bit7Title = "";
            this.vCinc_uc6.ControlMode = VCI_Forms_LIB.ControlModes.Gauge;
            this.vCinc_uc6.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc6.Location = new System.Drawing.Point(1209, 5);
            this.vCinc_uc6.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc6.Name = "vCinc_uc6";
            this.vCinc_uc6.PGN = "FE56";
            this.vCinc_uc6.Size = new System.Drawing.Size(200, 260);
            this.vCinc_uc6.SPNName = "Engine status";
            this.vCinc_uc6.TabIndex = 224;
            this.vCinc_uc6.Value = 10;
            // 
            // vCinc_uc7
            // 
            this.vCinc_uc7.Address = "FF";
            this.vCinc_uc7.BackColor = System.Drawing.Color.Linen;
            this.vCinc_uc7.Bit0Title = "";
            this.vCinc_uc7.Bit1Title = "";
            this.vCinc_uc7.Bit2Title = "";
            this.vCinc_uc7.Bit3Title = "";
            this.vCinc_uc7.Bit4Title = "";
            this.vCinc_uc7.Bit5Title = "";
            this.vCinc_uc7.Bit6Title = "";
            this.vCinc_uc7.Bit7Title = "";
            this.vCinc_uc7.ControlMode = VCI_Forms_LIB.ControlModes.Trackbar;
            this.vCinc_uc7.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc7.Location = new System.Drawing.Point(1009, 5);
            this.vCinc_uc7.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc7.MaxValue = 10000;
            this.vCinc_uc7.Name = "vCinc_uc7";
            this.vCinc_uc7.NumberOfBytes = 4;
            this.vCinc_uc7.PGN = "FEE5";
            this.vCinc_uc7.Size = new System.Drawing.Size(200, 260);
            this.vCinc_uc7.SPNName = "ECO";
            this.vCinc_uc7.TabIndex = 223;
            this.vCinc_uc7.Value = 265;
            // 
            // C3iForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2344, 1521);
            this.Controls.Add(this.vCinc_uc6);
            this.Controls.Add(this.vCinc_uc7);
            this.Controls.Add(this.vCinc_uc5);
            this.Controls.Add(this.vCinc_uc4);
            this.Controls.Add(this.vCinc_uc3);
            this.Controls.Add(this.vCinc_uc2);
            this.Controls.Add(this.vCinc_uc1);
            this.Controls.Add(this.cb_uniqueOn);
            this.Controls.Add(this.tb_CAN_Bus_View);
            this.Controls.Add(this.btn_RunStop);
            this.Controls.Add(this.lbl_OnScreenCount);
            this.Controls.Add(this.lbl_onBus);
            this.Controls.Add(this.btn_Validate);
            this.Name = "C3iForm";
            this.Text = "C3iForm";
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
        private VCI_Forms_LIB.VCinc_uc vCinc_uc1;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc2;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc3;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc4;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc5;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc6;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc7;
    }
}