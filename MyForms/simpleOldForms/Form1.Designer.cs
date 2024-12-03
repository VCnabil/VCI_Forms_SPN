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
            this.webView2Obj = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.webView2Obj)).BeginInit();
            this.SuspendLayout();
            // 
            // webView2Obj
            // 
            this.webView2Obj.AllowExternalDrop = true;
            this.webView2Obj.CreationProperties = null;
            this.webView2Obj.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView2Obj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView2Obj.Location = new System.Drawing.Point(0, 0);
            this.webView2Obj.Name = "webView2Obj";
            this.webView2Obj.Size = new System.Drawing.Size(2141, 1756);
            this.webView2Obj.TabIndex = 0;
            this.webView2Obj.ZoomFactor = 1D;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2141, 1756);
            this.Controls.Add(this.webView2Obj);
            this.Font = new System.Drawing.Font("Arial Narrow", 7F);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "nada";
            ((System.ComponentModel.ISupportInitialize)(this.webView2Obj)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView2Obj;
    }
}

