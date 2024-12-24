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
            this.listBoxFirmwareInfo = new System.Windows.Forms.ListBox();
            this.checkedListBoxPaths = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanelResults = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBoxDirectories
            // 
            this.comboBoxDirectories.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.comboBoxDirectories.FontWeight = MetroFramework.MetroComboBoxWeight.Bold;
            this.comboBoxDirectories.FormattingEnabled = true;
            this.comboBoxDirectories.ItemHeight = 29;
            this.comboBoxDirectories.Location = new System.Drawing.Point(469, 69);
            this.comboBoxDirectories.Name = "comboBoxDirectories";
            this.comboBoxDirectories.Size = new System.Drawing.Size(505, 35);
            this.comboBoxDirectories.TabIndex = 0;
            this.comboBoxDirectories.UseSelectable = true;
            // 
            // lbl_FoundCfiles
            // 
            this.lbl_FoundCfiles.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lbl_FoundCfiles.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_FoundCfiles.Location = new System.Drawing.Point(987, 72);
            this.lbl_FoundCfiles.Name = "lbl_FoundCfiles";
            this.lbl_FoundCfiles.Size = new System.Drawing.Size(622, 35);
            this.lbl_FoundCfiles.TabIndex = 0;
            this.lbl_FoundCfiles.Text = "lbl_FoundCfiles";
            // 
            // listBoxFirmwareInfo
            // 
            this.listBoxFirmwareInfo.FormattingEnabled = true;
            this.listBoxFirmwareInfo.ItemHeight = 25;
            this.listBoxFirmwareInfo.Location = new System.Drawing.Point(987, 110);
            this.listBoxFirmwareInfo.Name = "listBoxFirmwareInfo";
            this.listBoxFirmwareInfo.Size = new System.Drawing.Size(1168, 279);
            this.listBoxFirmwareInfo.TabIndex = 1;
            // 
            // checkedListBoxPaths
            // 
            this.checkedListBoxPaths.FormattingEnabled = true;
            this.checkedListBoxPaths.Location = new System.Drawing.Point(23, 140);
            this.checkedListBoxPaths.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkedListBoxPaths.Name = "checkedListBoxPaths";
            this.checkedListBoxPaths.Size = new System.Drawing.Size(422, 228);
            this.checkedListBoxPaths.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 97);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Chose a search path root";
            // 
            // flowLayoutPanelResults
            // 
            this.flowLayoutPanelResults.AutoScroll = true;
            this.flowLayoutPanelResults.BackColor = System.Drawing.Color.PapayaWhip;
            this.flowLayoutPanelResults.Location = new System.Drawing.Point(23, 545);
            this.flowLayoutPanelResults.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.flowLayoutPanelResults.Name = "flowLayoutPanelResults";
            this.flowLayoutPanelResults.Size = new System.Drawing.Size(937, 826);
            this.flowLayoutPanelResults.TabIndex = 13;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(259, 374);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(186, 31);
            this.buttonSearch.TabIndex = 14;
            this.buttonSearch.Text = "button1";
            this.buttonSearch.UseVisualStyleBackColor = true;
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(28, 374);
            this.SearchBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(188, 31);
            this.SearchBox.TabIndex = 15;
            // 
            // SVeeSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2216, 1400);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.flowLayoutPanelResults);
            this.Controls.Add(this.checkedListBoxPaths);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxFirmwareInfo);
            this.Controls.Add(this.lbl_FoundCfiles);
            this.Controls.Add(this.comboBoxDirectories);
            this.Name = "SVeeSearchForm";
            this.Text = "SVeeSearchForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox comboBoxDirectories;
        private MetroFramework.Controls.MetroLabel lbl_FoundCfiles;
        private System.Windows.Forms.ListBox listBoxFirmwareInfo;
        private System.Windows.Forms.CheckedListBox checkedListBoxPaths;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelResults;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox SearchBox;
    }
}