namespace VCI_Forms_SPN
{
    partial class BirdonRawPgn
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
            this.vCinc_uc12 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_SFversion3 = new VCI_Forms_LIB.VCinc_SFversion();
            this.vCinc_SFversion1 = new VCI_Forms_LIB.VCinc_SFversion();
            this.SuspendLayout();
            // 
            // cb_uniqueOn
            // 
            this.cb_uniqueOn.AutoSize = true;
            this.cb_uniqueOn.Location = new System.Drawing.Point(48, 61);
            this.cb_uniqueOn.Margin = new System.Windows.Forms.Padding(6);
            this.cb_uniqueOn.Name = "cb_uniqueOn";
            this.cb_uniqueOn.Size = new System.Drawing.Size(153, 29);
            this.cb_uniqueOn.TabIndex = 416;
            this.cb_uniqueOn.Text = "group pgn?";
            this.cb_uniqueOn.UseVisualStyleBackColor = true;
            // 
            // tb_CAN_Bus_View
            // 
            this.tb_CAN_Bus_View.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tb_CAN_Bus_View.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_CAN_Bus_View.ForeColor = System.Drawing.Color.Lime;
            this.tb_CAN_Bus_View.Location = new System.Drawing.Point(48, 131);
            this.tb_CAN_Bus_View.Margin = new System.Windows.Forms.Padding(6);
            this.tb_CAN_Bus_View.Multiline = true;
            this.tb_CAN_Bus_View.Name = "tb_CAN_Bus_View";
            this.tb_CAN_Bus_View.ReadOnly = true;
            this.tb_CAN_Bus_View.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_CAN_Bus_View.Size = new System.Drawing.Size(606, 589);
            this.tb_CAN_Bus_View.TabIndex = 415;
            this.tb_CAN_Bus_View.Text = ": Console Bkg.green  -c -0 ";
            // 
            // btn_RunStop
            // 
            this.btn_RunStop.Location = new System.Drawing.Point(372, 16);
            this.btn_RunStop.Margin = new System.Windows.Forms.Padding(6);
            this.btn_RunStop.Name = "btn_RunStop";
            this.btn_RunStop.Size = new System.Drawing.Size(167, 45);
            this.btn_RunStop.TabIndex = 414;
            this.btn_RunStop.Text = "Send Can";
            this.btn_RunStop.UseVisualStyleBackColor = true;
            // 
            // lbl_OnScreenCount
            // 
            this.lbl_OnScreenCount.Location = new System.Drawing.Point(135, 19);
            this.lbl_OnScreenCount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_OnScreenCount.Name = "lbl_OnScreenCount";
            this.lbl_OnScreenCount.Size = new System.Drawing.Size(121, 36);
            this.lbl_OnScreenCount.TabIndex = 413;
            this.lbl_OnScreenCount.Text = "on screen ";
            this.lbl_OnScreenCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_onBus
            // 
            this.lbl_onBus.Location = new System.Drawing.Point(25, 19);
            this.lbl_onBus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_onBus.Name = "lbl_onBus";
            this.lbl_onBus.Size = new System.Drawing.Size(98, 36);
            this.lbl_onBus.TabIndex = 412;
            this.lbl_onBus.Text = "on bus";
            this.lbl_onBus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Validate
            // 
            this.btn_Validate.Location = new System.Drawing.Point(257, 15);
            this.btn_Validate.Margin = new System.Windows.Forms.Padding(6);
            this.btn_Validate.Name = "btn_Validate";
            this.btn_Validate.Size = new System.Drawing.Size(110, 45);
            this.btn_Validate.TabIndex = 411;
            this.btn_Validate.Text = "validator";
            this.btn_Validate.UseVisualStyleBackColor = true;
            // 
            // vCinc_uc12
            // 
            this.vCinc_uc12.A_FirstByteIndex = 7;
            this.vCinc_uc12.Address = "29";
            this.vCinc_uc12.BackColor = System.Drawing.Color.PaleTurquoise;
            this.vCinc_uc12.Bit0Title = "";
            this.vCinc_uc12.Bit1Title = " ";
            this.vCinc_uc12.Bit2Title = " ";
            this.vCinc_uc12.Bit3Title = "  Sta1 Tiller Selec";
            this.vCinc_uc12.Bit4Title = " ";
            this.vCinc_uc12.Bit5Title = " ";
            this.vCinc_uc12.Bit6Title = "Flip_Dutycycle";
            this.vCinc_uc12.Bit7Title = "";
            this.vCinc_uc12.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_uc12.Location = new System.Drawing.Point(679, 214);
            this.vCinc_uc12.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc12.Name = "vCinc_uc12";
            this.vCinc_uc12.PGN = "FF59";
            this.vCinc_uc12.Size = new System.Drawing.Size(307, 235);
            this.vCinc_uc12.SPNName = "SWITCHES raw";
            this.vCinc_uc12.TabIndex = 419;
            // 
            // vCinc_SFversion3
            // 
            this.vCinc_SFversion3.BackColor = System.Drawing.Color.DarkTurquoise;
            this.vCinc_SFversion3.Location = new System.Drawing.Point(1129, 720);
            this.vCinc_SFversion3.Major = 99;
            this.vCinc_SFversion3.Major_firstbyteindex = 4;
            this.vCinc_SFversion3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_SFversion3.Minor = 1;
            this.vCinc_SFversion3.Minor_firstbyteindex = 5;
            this.vCinc_SFversion3.Name = "vCinc_SFversion3";
            this.vCinc_SFversion3.PGN = "ff54";
            this.vCinc_SFversion3.Priority = "0c";
            this.vCinc_SFversion3.PrnName = "AM Birdon ";
            this.vCinc_SFversion3.Rev = 5818;
            this.vCinc_SFversion3.Rev_firstbyteindex = 6;
            this.vCinc_SFversion3.Rev_numofbytes = 2;
            this.vCinc_SFversion3.Size = new System.Drawing.Size(246, 134);
            this.vCinc_SFversion3.Source = "34";
            this.vCinc_SFversion3.TabIndex = 494;
            // 
            // vCinc_SFversion1
            // 
            this.vCinc_SFversion1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.vCinc_SFversion1.Location = new System.Drawing.Point(1129, 569);
            this.vCinc_SFversion1.Major = 99;
            this.vCinc_SFversion1.Major_firstbyteindex = 2;
            this.vCinc_SFversion1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_SFversion1.Minor = 1001;
            this.vCinc_SFversion1.Minor_firstbyteindex = 3;
            this.vCinc_SFversion1.Minor_numofbytes = 2;
            this.vCinc_SFversion1.Name = "vCinc_SFversion1";
            this.vCinc_SFversion1.PGN = "ff30";
            this.vCinc_SFversion1.PrnName = "CU Birdon";
            this.vCinc_SFversion1.Rev = 6187;
            this.vCinc_SFversion1.Rev_firstbyteindex = 5;
            this.vCinc_SFversion1.Rev_numofbytes = 2;
            this.vCinc_SFversion1.Size = new System.Drawing.Size(246, 134);
            this.vCinc_SFversion1.Source = "29";
            this.vCinc_SFversion1.TabIndex = 493;
            // 
            // BirdonRawPgn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2619, 1467);
            this.Controls.Add(this.vCinc_SFversion3);
            this.Controls.Add(this.vCinc_SFversion1);
            this.Controls.Add(this.vCinc_uc12);
            this.Controls.Add(this.cb_uniqueOn);
            this.Controls.Add(this.tb_CAN_Bus_View);
            this.Controls.Add(this.btn_RunStop);
            this.Controls.Add(this.lbl_OnScreenCount);
            this.Controls.Add(this.lbl_onBus);
            this.Controls.Add(this.btn_Validate);
            this.Name = "BirdonRawPgn";
            this.Text = "BirdonRawPgn";
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
        private VCI_Forms_LIB.VCinc_uc vCinc_uc12;
        private VCI_Forms_LIB.VCinc_SFversion vCinc_SFversion3;
        private VCI_Forms_LIB.VCinc_SFversion vCinc_SFversion1;
    }
}