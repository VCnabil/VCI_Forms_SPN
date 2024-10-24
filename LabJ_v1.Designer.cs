namespace VCI_Forms_SPN
{
    partial class LabJ_v1
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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.labelVoltage = new System.Windows.Forms.Label();
            this.btn_MUX1 = new System.Windows.Forms.Button();
            this.btn_MUX2 = new System.Windows.Forms.Button();
            this.btn_MUX4 = new System.Windows.Forms.Button();
            this.btn_MUX5 = new System.Windows.Forms.Button();
            this.textBoxMin = new System.Windows.Forms.TextBox();
            this.textBoxMax = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(29, 203);
            this.trackBar1.Maximum = 500;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(361, 90);
            this.trackBar1.TabIndex = 1;
            // 
            // labelVoltage
            // 
            this.labelVoltage.AutoSize = true;
            this.labelVoltage.Location = new System.Drawing.Point(168, 155);
            this.labelVoltage.Name = "labelVoltage";
            this.labelVoltage.Size = new System.Drawing.Size(53, 25);
            this.labelVoltage.TabIndex = 2;
            this.labelVoltage.Text = "0.0v";
            // 
            // btn_MUX1
            // 
            this.btn_MUX1.Location = new System.Drawing.Point(33, 37);
            this.btn_MUX1.Name = "btn_MUX1";
            this.btn_MUX1.Size = new System.Drawing.Size(170, 42);
            this.btn_MUX1.TabIndex = 3;
            this.btn_MUX1.Text = "Port Buk AIN1";
            this.btn_MUX1.UseVisualStyleBackColor = true;
            // 
            // btn_MUX2
            // 
            this.btn_MUX2.Location = new System.Drawing.Point(33, 85);
            this.btn_MUX2.Name = "btn_MUX2";
            this.btn_MUX2.Size = new System.Drawing.Size(170, 42);
            this.btn_MUX2.TabIndex = 4;
            this.btn_MUX2.Text = "Port Noz AIN2";
            this.btn_MUX2.UseVisualStyleBackColor = true;
            // 
            // btn_MUX4
            // 
            this.btn_MUX4.Location = new System.Drawing.Point(209, 37);
            this.btn_MUX4.Name = "btn_MUX4";
            this.btn_MUX4.Size = new System.Drawing.Size(170, 42);
            this.btn_MUX4.TabIndex = 5;
            this.btn_MUX4.Text = "Stbd Buk AIN4";
            this.btn_MUX4.UseVisualStyleBackColor = true;
            // 
            // btn_MUX5
            // 
            this.btn_MUX5.Location = new System.Drawing.Point(209, 85);
            this.btn_MUX5.Name = "btn_MUX5";
            this.btn_MUX5.Size = new System.Drawing.Size(170, 42);
            this.btn_MUX5.TabIndex = 6;
            this.btn_MUX5.Text = "Stbd Noz AIN5";
            this.btn_MUX5.UseVisualStyleBackColor = true;
            // 
            // textBoxMin
            // 
            this.textBoxMin.Location = new System.Drawing.Point(41, 155);
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.Size = new System.Drawing.Size(100, 31);
            this.textBoxMin.TabIndex = 7;
            // 
            // textBoxMax
            // 
            this.textBoxMax.Location = new System.Drawing.Point(279, 155);
            this.textBoxMax.Name = "textBoxMax";
            this.textBoxMax.Size = new System.Drawing.Size(100, 31);
            this.textBoxMax.TabIndex = 8;
            // 
            // LabJ_v1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 311);
            this.Controls.Add(this.textBoxMax);
            this.Controls.Add(this.textBoxMin);
            this.Controls.Add(this.btn_MUX5);
            this.Controls.Add(this.btn_MUX4);
            this.Controls.Add(this.btn_MUX2);
            this.Controls.Add(this.btn_MUX1);
            this.Controls.Add(this.labelVoltage);
            this.Controls.Add(this.trackBar1);
            this.Name = "LabJ_v1";
            this.Text = "LabJ_v1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label labelVoltage;
        private System.Windows.Forms.Button btn_MUX1;
        private System.Windows.Forms.Button btn_MUX2;
        private System.Windows.Forms.Button btn_MUX4;
        private System.Windows.Forms.Button btn_MUX5;
        private System.Windows.Forms.TextBox textBoxMin;
        private System.Windows.Forms.TextBox textBoxMax;
    }
}