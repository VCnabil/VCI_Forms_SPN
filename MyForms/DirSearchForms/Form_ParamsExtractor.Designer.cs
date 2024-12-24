namespace VCI_Forms_SPN.MyForms.DirSearchForms
{
    partial class Form_ParamsExtractor
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
            this.lbl_01 = new System.Windows.Forms.Label();
            this.pb_MAIN = new System.Windows.Forms.PictureBox();
            this.lbl_path = new System.Windows.Forms.Label();
            this.lbl_02 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_MAIN)).BeginInit();
            this.SuspendLayout();
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(16, 286);
            this.SearchBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(214, 31);
            this.SearchBox.TabIndex = 20;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(282, 286);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(186, 45);
            this.buttonSearch.TabIndex = 19;
            this.buttonSearch.Text = "button1";
            this.buttonSearch.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelResults
            // 
            this.flowLayoutPanelResults.AutoScroll = true;
            this.flowLayoutPanelResults.BackColor = System.Drawing.Color.PapayaWhip;
            this.flowLayoutPanelResults.Location = new System.Drawing.Point(11, 337);
            this.flowLayoutPanelResults.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.flowLayoutPanelResults.Name = "flowLayoutPanelResults";
            this.flowLayoutPanelResults.Size = new System.Drawing.Size(457, 684);
            this.flowLayoutPanelResults.TabIndex = 18;
            // 
            // checkedListBoxPaths
            // 
            this.checkedListBoxPaths.FormattingEnabled = true;
            this.checkedListBoxPaths.Location = new System.Drawing.Point(11, 52);
            this.checkedListBoxPaths.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkedListBoxPaths.Name = "checkedListBoxPaths";
            this.checkedListBoxPaths.Size = new System.Drawing.Size(457, 228);
            this.checkedListBoxPaths.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Chose a search path root";
            // 
            // lbl_01
            // 
            this.lbl_01.AutoSize = true;
            this.lbl_01.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.875F);
            this.lbl_01.Location = new System.Drawing.Point(27, 1127);
            this.lbl_01.Name = "lbl_01";
            this.lbl_01.Size = new System.Drawing.Size(51, 20);
            this.lbl_01.TabIndex = 21;
            this.lbl_01.Text = "label2";
            // 
            // pb_MAIN
            // 
            this.pb_MAIN.Location = new System.Drawing.Point(470, 12);
            this.pb_MAIN.Name = "pb_MAIN";
            this.pb_MAIN.Size = new System.Drawing.Size(1700, 1100);
            this.pb_MAIN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_MAIN.TabIndex = 313;
            this.pb_MAIN.TabStop = false;
            // 
            // lbl_path
            // 
            this.lbl_path.AutoSize = true;
            this.lbl_path.Location = new System.Drawing.Point(398, 9);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(70, 25);
            this.lbl_path.TabIndex = 314;
            this.lbl_path.Text = "label2";
            // 
            // lbl_02
            // 
            this.lbl_02.AutoSize = true;
            this.lbl_02.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.875F);
            this.lbl_02.Location = new System.Drawing.Point(658, 1127);
            this.lbl_02.Name = "lbl_02";
            this.lbl_02.Size = new System.Drawing.Size(51, 20);
            this.lbl_02.TabIndex = 315;
            this.lbl_02.Text = "label2";
            // 
            // Form_ParamsExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(3867, 2199);
            this.Controls.Add(this.lbl_02);
            this.Controls.Add(this.lbl_path);
            this.Controls.Add(this.lbl_01);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.flowLayoutPanelResults);
            this.Controls.Add(this.checkedListBoxPaths);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pb_MAIN);
            this.Name = "Form_ParamsExtractor";
            this.Text = "Form_ParamsExtractor";
            ((System.ComponentModel.ISupportInitialize)(this.pb_MAIN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelResults;
        private System.Windows.Forms.CheckedListBox checkedListBoxPaths;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_01;
        private System.Windows.Forms.PictureBox pb_MAIN;
        private System.Windows.Forms.Label lbl_path;
        private System.Windows.Forms.Label lbl_02;
    }
}