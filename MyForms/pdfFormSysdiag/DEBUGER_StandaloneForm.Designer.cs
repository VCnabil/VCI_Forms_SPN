namespace VCI_Forms_SPN.MyForms.pdfFormSysdiag
{
    partial class DEBUGER_StandaloneForm
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
            this.btn_clear = new System.Windows.Forms.Button();
            this.textBox_Display = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(1830, 3);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(132, 44);
            this.btn_clear.TabIndex = 274;
            this.btn_clear.Text = "clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            // 
            // textBox_Display
            // 
            this.textBox_Display.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBox_Display.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Display.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Display.ForeColor = System.Drawing.Color.Lime;
            this.textBox_Display.Location = new System.Drawing.Point(0, 50);
            this.textBox_Display.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_Display.Multiline = true;
            this.textBox_Display.Name = "textBox_Display";
            this.textBox_Display.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Display.Size = new System.Drawing.Size(1974, 876);
            this.textBox_Display.TabIndex = 273;
            this.textBox_Display.Text = "CON";
            // 
            // DEBUGER_StandaloneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1974, 929);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.textBox_Display);
            this.Name = "DEBUGER_StandaloneForm";
            this.Text = "DEBUGER_StandaloneForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.TextBox textBox_Display;
    }
}