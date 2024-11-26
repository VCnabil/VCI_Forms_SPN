namespace VCI_Forms_SPN
{
    partial class Form1
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
            this.vCinc_B_uc48 = new VCI_Forms_LIB.VCinc_uc();
            this.vCinc_uc1 = new VCI_Forms_LIB.VCinc_uc();
            this.SuspendLayout();
            // 
            // vCinc_B_uc48
            // 
            this.vCinc_B_uc48.A_FirstByteIndex = 5;
            this.vCinc_B_uc48.Address = "01";
            this.vCinc_B_uc48.BackColor = System.Drawing.Color.Moccasin;
            this.vCinc_B_uc48.Bit0Title = "";
            this.vCinc_B_uc48.Bit1Title = "";
            this.vCinc_B_uc48.Bit2Title = "";
            this.vCinc_B_uc48.Bit3Title = "";
            this.vCinc_B_uc48.Bit4Title = "";
            this.vCinc_B_uc48.Bit5Title = "";
            this.vCinc_B_uc48.Bit6Title = "";
            this.vCinc_B_uc48.Bit7Title = "";
            this.vCinc_B_uc48.ControlMode = VCI_Forms_LIB.ControlModes.Gauge;
            this.vCinc_B_uc48.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_B_uc48.G_EndAngle = 30;
            this.vCinc_B_uc48.G_OffsetRotationAngle = 90;
            this.vCinc_B_uc48.G_StartAngle = 150;
            this.vCinc_B_uc48.Location = new System.Drawing.Point(979, 422);
            this.vCinc_B_uc48.m_ticks = 4;
            this.vCinc_B_uc48.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_B_uc48.MaxValue = 250;
            this.vCinc_B_uc48.Name = "vCinc_B_uc48";
            this.vCinc_B_uc48.PGN = "FEFC";
            this.vCinc_B_uc48.Size = new System.Drawing.Size(223, 250);
            this.vCinc_B_uc48.SPNName = "Sbuk";
            this.vCinc_B_uc48.TabIndex = 428;
            this.vCinc_B_uc48.Value = 127;
            // 
            // vCinc_uc1
            // 
            this.vCinc_uc1.A_FirstByteIndex = 5;
            this.vCinc_uc1.Address = "01";
            this.vCinc_uc1.BackColor = System.Drawing.Color.Moccasin;
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
            this.vCinc_uc1.G_OffsetRotationAngle = 300;
            this.vCinc_uc1.Location = new System.Drawing.Point(629, 422);
            this.vCinc_uc1.Margin = new System.Windows.Forms.Padding(0);
            this.vCinc_uc1.MaxValue = 254;
            this.vCinc_uc1.Name = "vCinc_uc1";
            this.vCinc_uc1.PGN = "FEFC";
            this.vCinc_uc1.Size = new System.Drawing.Size(223, 250);
            this.vCinc_uc1.SPNName = "Sbuk";
            this.vCinc_uc1.TabIndex = 429;
            this.vCinc_uc1.Value = 127;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2141, 1095);
            this.Controls.Add(this.vCinc_uc1);
            this.Controls.Add(this.vCinc_B_uc48);
            this.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "nada";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private VCI_Forms_LIB.VCinc_uc vCinc_B_uc48;
        private VCI_Forms_LIB.VCinc_uc vCinc_uc1;
    }
}

