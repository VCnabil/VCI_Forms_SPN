using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCI_Forms_SPN._BackEndDataOBJs.DocReaders;

namespace VCI_Forms_SPN.MyForms.simpleOldForms
{
    public partial class PalmTextCleanerForm : Form
    {
        public PalmTextCleanerForm()
        {
            InitializeComponent();
            btnProcessFile.Click += btnProcessFile_Click;
        }
        private void btnProcessFile_Click(object sender, EventArgs e)
        {
            try
            {
                // Remove or comment out the hard-coded file paths:
                // string inputFilePath = @"U:\_Nabil\SSRS_K12_NO_DP.txt";
                // string outputFilePath = @"U:\_Nabil\SSRS_K12_NO_DP_CLEAN.txt";

                // Add an OpenFileDialog to let user select the input file
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Select the Input File";
                    openFileDialog.Filter = "Text Files|*.txt|All Files|*.*";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string inputFilePath = openFileDialog.FileName;

                        // Add a SaveFileDialog to let user select output file location
                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Title = "Select the Output File Location";
                            saveFileDialog.Filter = "Text Files|*.txt|All Files|*.*";
                            saveFileDialog.FileName = "CLEANED_" + System.IO.Path.GetFileName(inputFilePath);

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                string outputFilePath = saveFileDialog.FileName;

                                // Now run the cleaner with the chosen files
                                PalmMenuOutputCleaner.CleanFile(inputFilePath, outputFilePath);

                                MessageBox.Show("File processed and saved successfully!",
                                                "Success",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }






        private void btnProcessFile_Clickold(object sender, EventArgs e)
        {
            try
            {
                string inputFilePath = @"U:\_Nabil\SSRS_K12_NO_DP.txt";
                string outputFilePath = @"U:\_Nabil\SSRS_K12_NO_DP_CLEAN.txt";

                PalmMenuOutputCleaner.CleanFile(inputFilePath, outputFilePath);

                MessageBox.Show("File processed and saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
