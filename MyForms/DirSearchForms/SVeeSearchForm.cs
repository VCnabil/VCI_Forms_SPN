using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; 
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                string selectedDirectoryName = comboBoxDirectories.SelectedItem.ToString();
                lbl_FoundCfiles.Text = $"Searching for C files in '{selectedDirectoryName}'...";
                PopulateListBoxWithFirmwareInfo(selectedDirectoryName);
            }
        }

        private void PopulateListBoxWithFirmwareInfo(string projectDirectoryName)
        {
            string rootSVNDirectory = @"C:\_____Ufake\SVN_Projects\";
            string projectDirectoryPath = Path.Combine(rootSVNDirectory, projectDirectoryName);

            List<string> firmwareInfoList = new List<string>();

            try
            {
                if (Directory.Exists(projectDirectoryPath))
                {
                    string[] parametersFiles = Directory.GetFiles(projectDirectoryPath, "parameters.cpp", SearchOption.AllDirectories);

                    foreach (string parametersFilePath in parametersFiles)
                    {
                        string firmwareDirPath = Path.GetDirectoryName(parametersFilePath);
                        string firmwareDirName = Path.GetFileName(firmwareDirPath);

                        string[] fileLines = File.ReadAllLines(parametersFilePath);

                        string version = "";
                        string eepromStatus = "";
                        string svnRevision = "";

                        foreach (string line in fileLines)
                        {
                            if (line.Contains("EParam VERSION ="))
                            {
                                version = ExtractValue(line);
                            }
                            else if (line.Contains("EParam EEPROM_STATUS ="))
                            {
                                eepromStatus = ExtractValue(line);
                            }
                            else if (line.Contains("EParam SVN_REVISION ="))
                            {
                                svnRevision = ExtractValue(line);
                            }
                        }
                        string displayString = $"{projectDirectoryName}: {firmwareDirName} {version}_{eepromStatus}_Rev{svnRevision}";

                        firmwareInfoList.Add(displayString);
                    }
                    listBoxFirmwareInfo.DataSource = firmwareInfoList;
                }
                else
                {
                    MessageBox.Show($"The project directory '{projectDirectoryPath}' does not exist.", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching for firmware directories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ExtractValue(string line)
        {
            // Use regular expression to match numbers after '=' sign
            var match = Regex.Match(line, @"=\s*(\d+)\s*;");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return "";
            }
        }
        private string ExtractValue_usingEqualAndSemicolon(string line)
        {
            // Extracts the value after '=' and before ';'
            int equalsIndex = line.IndexOf('=');
            int semicolonIndex = line.IndexOf(';');

            if (equalsIndex != -1 && semicolonIndex != -1 && semicolonIndex > equalsIndex)
            {
                string value = line.Substring(equalsIndex + 1, semicolonIndex - equalsIndex - 1).Trim();
                return value;
            }
            else
            {
                return "";
            }
        }
    }
}
