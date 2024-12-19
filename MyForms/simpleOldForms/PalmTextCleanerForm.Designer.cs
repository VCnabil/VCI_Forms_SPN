namespace VCI_Forms_SPN.MyForms.simpleOldForms
{
    partial class PalmTextCleanerForm
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
            this.btnProcessFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnProcessFile
            // 
            this.btnProcessFile.Location = new System.Drawing.Point(12, 12);
            this.btnProcessFile.Name = "btnProcessFile";
            this.btnProcessFile.Size = new System.Drawing.Size(611, 134);
            this.btnProcessFile.TabIndex = 0;
            this.btnProcessFile.Text = "Pick a Palm Text file";
            this.btnProcessFile.UseVisualStyleBackColor = true;
            // 
            // PalmTextCleanerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 167);
            this.Controls.Add(this.btnProcessFile);
            this.Name = "PalmTextCleanerForm";
            this.Text = "PalmTextCleanerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProcessFile;
    }
}