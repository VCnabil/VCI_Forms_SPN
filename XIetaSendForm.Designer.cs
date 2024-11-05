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
            // XIetaSendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2375, 840);
            this.Controls.Add(this.cb_uniqueOn);
            this.Controls.Add(this.tb_CAN_Bus_View);
            this.Controls.Add(this.btn_RunStop);
            this.Controls.Add(this.lbl_OnScreenCount);
            this.Controls.Add(this.lbl_onBus);
            this.Controls.Add(this.btn_Validate);
            this.Name = "XIetaSendForm";
            this.Text = "XIetaSendForm";
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
    }
}