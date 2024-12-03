namespace VCI_Forms_SPN
{
    partial class Form2
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
            this.btn_PipeToggle = new System.Windows.Forms.Button();
            this.vCinc_LatLon_waypoint = new VCI_Forms_LIB.VCinc_LatLon();
            this.vCinc_LatLon_mapCnter = new VCI_Forms_LIB.VCinc_LatLon();
            this.tb_CAN_Bus_View = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.vCinc_LatLon1 = new VCI_Forms_LIB.VCinc_LatLon();
            this.vCinc_LatLon2 = new VCI_Forms_LIB.VCinc_LatLon();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_PipeToggle
            // 
            this.btn_PipeToggle.Location = new System.Drawing.Point(1208, 300);
            this.btn_PipeToggle.Margin = new System.Windows.Forms.Padding(6);
            this.btn_PipeToggle.Name = "btn_PipeToggle";
            this.btn_PipeToggle.Size = new System.Drawing.Size(150, 35);
            this.btn_PipeToggle.TabIndex = 328;
            this.btn_PipeToggle.Text = "pipe";
            this.btn_PipeToggle.UseVisualStyleBackColor = true;
            this.btn_PipeToggle.Click += new System.EventHandler(this.btn_SetCoordinates);
            // 
            // vCinc_LatLon_waypoint
            // 
            this.vCinc_LatLon_waypoint.BackColor = System.Drawing.Color.RosyBrown;
            this.vCinc_LatLon_waypoint.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_LatLon_waypoint.Location = new System.Drawing.Point(682, 12);
            this.vCinc_LatLon_waypoint.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_LatLon_waypoint.Name = "vCinc_LatLon_waypoint";
            this.vCinc_LatLon_waypoint.Size = new System.Drawing.Size(314, 82);
            this.vCinc_LatLon_waypoint.TabIndex = 331;
            // 
            // vCinc_LatLon_mapCnter
            // 
            this.vCinc_LatLon_mapCnter.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.vCinc_LatLon_mapCnter.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_LatLon_mapCnter.Location = new System.Drawing.Point(682, 100);
            this.vCinc_LatLon_mapCnter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_LatLon_mapCnter.Name = "vCinc_LatLon_mapCnter";
            this.vCinc_LatLon_mapCnter.Size = new System.Drawing.Size(318, 82);
            this.vCinc_LatLon_mapCnter.TabIndex = 330;
            // 
            // tb_CAN_Bus_View
            // 
            this.tb_CAN_Bus_View.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tb_CAN_Bus_View.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_CAN_Bus_View.ForeColor = System.Drawing.Color.Lime;
            this.tb_CAN_Bus_View.Location = new System.Drawing.Point(15, 15);
            this.tb_CAN_Bus_View.Margin = new System.Windows.Forms.Padding(6);
            this.tb_CAN_Bus_View.Multiline = true;
            this.tb_CAN_Bus_View.Name = "tb_CAN_Bus_View";
            this.tb_CAN_Bus_View.ReadOnly = true;
            this.tb_CAN_Bus_View.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_CAN_Bus_View.Size = new System.Drawing.Size(659, 635);
            this.tb_CAN_Bus_View.TabIndex = 329;
            this.tb_CAN_Bus_View.Text = ": Console Bkg.green  -c -0 ";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(1014, 59);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 31);
            this.numericUpDown1.TabIndex = 332;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(1014, 105);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 31);
            this.numericUpDown2.TabIndex = 333;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(1014, 142);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(120, 31);
            this.numericUpDown3.TabIndex = 334;
            // 
            // vCinc_LatLon1
            // 
            this.vCinc_LatLon1.BackColor = System.Drawing.Color.RosyBrown;
            this.vCinc_LatLon1.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_LatLon1.Location = new System.Drawing.Point(682, 300);
            this.vCinc_LatLon1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_LatLon1.Name = "vCinc_LatLon1";
            this.vCinc_LatLon1.Size = new System.Drawing.Size(472, 82);
            this.vCinc_LatLon1.TabIndex = 336;
            // 
            // vCinc_LatLon2
            // 
            this.vCinc_LatLon2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.vCinc_LatLon2.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.vCinc_LatLon2.Location = new System.Drawing.Point(678, 423);
            this.vCinc_LatLon2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.vCinc_LatLon2.Name = "vCinc_LatLon2";
            this.vCinc_LatLon2.Size = new System.Drawing.Size(476, 82);
            this.vCinc_LatLon2.TabIndex = 335;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2076, 759);
            this.Controls.Add(this.vCinc_LatLon1);
            this.Controls.Add(this.vCinc_LatLon2);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.vCinc_LatLon_waypoint);
            this.Controls.Add(this.vCinc_LatLon_mapCnter);
            this.Controls.Add(this.tb_CAN_Bus_View);
            this.Controls.Add(this.btn_PipeToggle);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_PipeToggle;
        private VCI_Forms_LIB.VCinc_LatLon vCinc_LatLon_waypoint;
        private VCI_Forms_LIB.VCinc_LatLon vCinc_LatLon_mapCnter;
        private System.Windows.Forms.TextBox tb_CAN_Bus_View;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private VCI_Forms_LIB.VCinc_LatLon vCinc_LatLon1;
        private VCI_Forms_LIB.VCinc_LatLon vCinc_LatLon2;
    }
}