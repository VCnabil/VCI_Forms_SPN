namespace VCI_Forms_SPN.MyForms.DirSearchForms
{
    partial class SVeeSearchForm
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
            this.comboBoxDirectories = new MetroFramework.Controls.MetroComboBox();
            this.lbl_FoundCfiles = new MetroFramework.Controls.MetroLabel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // comboBoxDirectories
            // 
            this.comboBoxDirectories.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxDirectories.FontWeight = MetroFramework.MetroComboBoxWeight.Bold;
            this.comboBoxDirectories.FormattingEnabled = true;
            this.comboBoxDirectories.ItemHeight = 29;
            this.comboBoxDirectories.Location = new System.Drawing.Point(73, 31);
            this.comboBoxDirectories.Name = "comboBoxDirectories";
            this.comboBoxDirectories.Size = new System.Drawing.Size(505, 35);
            this.comboBoxDirectories.TabIndex = 0;
            this.comboBoxDirectories.UseSelectable = true;
            // 
            // lbl_FoundCfiles
            // 
            this.lbl_FoundCfiles.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_FoundCfiles.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_FoundCfiles.Location = new System.Drawing.Point(594, 31);
            this.lbl_FoundCfiles.Name = "lbl_FoundCfiles";
            this.lbl_FoundCfiles.Size = new System.Drawing.Size(622, 35);
            this.lbl_FoundCfiles.TabIndex = 0;
            this.lbl_FoundCfiles.Text = "lbl_FoundCfiles";
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(594, 126);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(729, 97);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // SVeeSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1940, 1227);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lbl_FoundCfiles);
            this.Controls.Add(this.comboBoxDirectories);
            this.Name = "SVeeSearchForm";
            this.Text = "SVeeSearchForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox comboBoxDirectories;
        private MetroFramework.Controls.MetroLabel lbl_FoundCfiles;
        private System.Windows.Forms.ListView listView1;
    }
}