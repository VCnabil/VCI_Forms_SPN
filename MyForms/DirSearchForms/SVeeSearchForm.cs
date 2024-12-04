using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCI_Forms_SPN.MyForms.DirSearchForms
{
    public partial class SVeeSearchForm : Form
    {
        public SVeeSearchForm()
        {
            InitializeComponent();
            Load += SVeeSearchForm_Load; 
            comboBoxDirectories.SelectedIndexChanged += ComboBoxDirectories_SelectedIndexChanged;

        }

        private void SVeeSearchForm_Load(object sender, EventArgs e)
        {
            PopulateComboBoxWithDirectories();
        }

        private void PopulateComboBoxWithDirectories()
        {
            string rootSVNDirectory = @"C:\_____Ufake\SVN_Projects\";
            List<string> directoryNames = new List<string>();
            try
            {
                if (Directory.Exists(rootSVNDirectory))
                {
                    IEnumerable<string> directories = Directory.EnumerateDirectories(rootSVNDirectory);

                    foreach (string directory in directories)
                    {
                        string directoryName = Path.GetFileName(directory);
                        directoryNames.Add(directoryName);
                    }
                    comboBoxDirectories.DataSource = directoryNames;
                    comboBoxDirectories.SelectedIndex = -1;  
                }
                else
                {
                    MessageBox.Show($"The directory '{rootSVNDirectory}' does not exist.", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching directories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboBoxDirectories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDirectories.SelectedItem != null)
            {
                string selectedDirectory = comboBoxDirectories.SelectedItem.ToString();
                lbl_FoundCfiles.Text = $"Searching for C files in '{selectedDirectory}'...";
            }
        }
    }
}
