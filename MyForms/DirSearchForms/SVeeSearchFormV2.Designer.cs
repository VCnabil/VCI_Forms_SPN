namespace VCI_Forms_SPN.MyForms.DirSearchForms
{
    partial class SVeeSearchFormV2
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
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.flowLayoutPanelResults = new System.Windows.Forms.FlowLayoutPanel();
            this.checkedListBoxPaths = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxFirmwareInfo = new System.Windows.Forms.ListBox();
            this.lbl_FoundCfiles = new MetroFramework.Controls.MetroLabel();
            this.comboBoxDirectories = new MetroFramework.Controls.MetroComboBox();
            this.SuspendLayout();
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(25, 321);
            this.SearchBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(188, 31);
            this.SearchBox.TabIndex = 23;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(256, 321);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(186, 31);
            this.buttonSearch.TabIndex = 22;
            this.buttonSearch.Text = "button1";
            this.buttonSearch.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelResults
            // 
            this.flowLayoutPanelResults.AutoScroll = true;
            this.flowLayoutPanelResults.BackColor = System.Drawing.Color.PapayaWhip;
            this.flowLayoutPanelResults.Location = new System.Drawing.Point(20, 492);
            this.flowLayoutPanelResults.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.flowLayoutPanelResults.Name = "flowLayoutPanelResults";
            this.flowLayoutPanelResults.Size = new System.Drawing.Size(937, 826);
            this.flowLayoutPanelResults.TabIndex = 21;
            // 
            // checkedListBoxPaths
            // 
            this.checkedListBoxPaths.FormattingEnabled = true;
            this.checkedListBoxPaths.Location = new System.Drawing.Point(20, 87);
            this.checkedListBoxPaths.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkedListBoxPaths.Name = "checkedListBoxPaths";
            this.checkedListBoxPaths.Size = new System.Drawing.Size(422, 228);
            this.checkedListBoxPaths.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 25);
            this.label1.TabIndex = 19;
            this.label1.Text = "Chose a search path root";
            // 
            // listBoxFirmwareInfo
            // 
            this.listBoxFirmwareInfo.FormattingEnabled = true;
            this.listBoxFirmwareInfo.ItemHeight = 25;
            this.listBoxFirmwareInfo.Location = new System.Drawing.Point(984, 57);
            this.listBoxFirmwareInfo.Name = "listBoxFirmwareInfo";
            this.listBoxFirmwareInfo.Size = new System.Drawing.Size(1168, 279);
            this.listBoxFirmwareInfo.TabIndex = 18;
            // 
            // lbl_FoundCfiles
            // 
            this.lbl_FoundCfiles.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_FoundCfiles.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_FoundCfiles.Location = new System.Drawing.Point(984, 19);
            this.lbl_FoundCfiles.Name = "lbl_FoundCfiles";
            this.lbl_FoundCfiles.Size = new System.Drawing.Size(622, 35);
            this.lbl_FoundCfiles.TabIndex = 16;
            this.lbl_FoundCfiles.Text = "lbl_FoundCfiles";
            // 
            // comboBoxDirectories
            // 
            this.comboBoxDirectories.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxDirectories.FontWeight = MetroFramework.MetroComboBoxWeight.Bold;
            this.comboBoxDirectories.FormattingEnabled = true;
            this.comboBoxDirectories.ItemHeight = 29;
            this.comboBoxDirectories.Location = new System.Drawing.Point(466, 16);
            this.comboBoxDirectories.Name = "comboBoxDirectories";
            this.comboBoxDirectories.Size = new System.Drawing.Size(505, 35);
            this.comboBoxDirectories.TabIndex = 17;
            this.comboBoxDirectories.UseSelectable = true;
            // 
            // SVeeSearchFormV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2276, 1344);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.flowLayoutPanelResults);
            this.Controls.Add(this.checkedListBoxPaths);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxFirmwareInfo);
            this.Controls.Add(this.lbl_FoundCfiles);
            this.Controls.Add(this.comboBoxDirectories);
            this.Name = "SVeeSearchFormV2";
            this.Text = "SVeeSearchFormV2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelResults;
        private System.Windows.Forms.CheckedListBox checkedListBoxPaths;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxFirmwareInfo;
        private MetroFramework.Controls.MetroLabel lbl_FoundCfiles;
        private MetroFramework.Controls.MetroComboBox comboBoxDirectories;
    }
}