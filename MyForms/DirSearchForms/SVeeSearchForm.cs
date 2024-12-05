using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;  // Needed for file operations
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCI_Forms_SPN.MyForms.DirSearchForms
{
    public partial class SVeeSearchForm : Form
    {
        // Class-level variables
        private string rootSVNDirectory = @"C:\_____Ufake\Code_Source\"; private string outputDebugFilneName = "test_fake_U.txt";

        private List<string> projectDirectories = new List<string>();
        private Dictionary<string, List<string>> projectParametersFiles = new Dictionary<string, List<string>>();

        public SVeeSearchForm()
        {
            InitializeComponent();
            Load += SVeeSearchForm_Load;
            comboBoxDirectories.SelectedIndexChanged += ComboBoxDirectories_SelectedIndexChanged;

            // Collect project directories and parameters.cpp file paths
            CollectProjectDirectoriesAndParametersFiles();

            // Collect firmware info for all projects using the pre-collected data
            CollectFirmwareInfoForAllProjects();
        }

        private void CollectProjectDirectoriesAndParametersFiles()
        {
            try
            {
                if (Directory.Exists(rootSVNDirectory))
                {
                    // Get all project directories
                    projectDirectories = Directory.GetDirectories(rootSVNDirectory).ToList();

                    // For each project, collect parameters.cpp file paths
                    foreach (string projectDirectoryPath in projectDirectories)
                    {
                        string projectDirectoryName = Path.GetFileName(projectDirectoryPath);

                        // Find all parameters.cpp files in the project directory
                        string[] parametersFiles = Directory.GetFiles(projectDirectoryPath, "parameters.cpp", SearchOption.AllDirectories);

                        // Store the file paths in the dictionary
                        projectParametersFiles[projectDirectoryName] = parametersFiles.ToList();
                    }
                }
                else
                {
                    MessageBox.Show($"The root SVN directory '{rootSVNDirectory}' does not exist.", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while collecting project directories and parameters files: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CollectFirmwareInfoForAllProjects()
        {
            string outputFilePath = @"C:\___Base_Data_READ_Path\ReadParamsDir\"+ outputDebugFilneName;

            try
            {
                using (StreamWriter writer = new StreamWriter(outputFilePath, false))
                {
                    foreach (var projectEntry in projectParametersFiles)
                    {
                        string projectDirectoryName = projectEntry.Key;
                        List<string> parametersFiles = projectEntry.Value;

                        List<string> firmwareInfoList = GetFirmwareInfoList(projectDirectoryName, parametersFiles);

                        if (firmwareInfoList.Count > 0)
                        {
                            writer.WriteLine($"Project: {projectDirectoryName}");
                            writer.WriteLine("-----------------------------------");

                            foreach (string displayString in firmwareInfoList)
                            {
                                writer.WriteLine(displayString);
                            }

                            writer.WriteLine();
                        }
                    }
                }

                MessageBox.Show("Firmware information collected for all projects.", "Collection Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while collecting firmware information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<string> GetFirmwareInfoList(string projectDirectoryName, List<string> parametersFiles)
        {
            List<string> firmwareInfoList = new List<string>();

            try
            {
                foreach (string parametersFilePath in parametersFiles)
                {
                    string firmwareDirPath = Path.GetDirectoryName(parametersFilePath);
                    string firmwareDirName = Path.GetFileName(firmwareDirPath);

                    string[] fileLines = File.ReadAllLines(parametersFilePath);

                    string version = "";
                    string eepromStatus = "";
                    string svnRevision = "";

                    // Regular expression to match variable declarations
                    string pattern = @"^\s*(EParam|int|int16)\s+(\w+)\s*=\s*(\d+)\s*;";
                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                    foreach (string line in fileLines)
                    {
                        Match match = regex.Match(line.Trim());
                        if (match.Success)
                        {
                            string variableName = match.Groups[2].Value.ToUpper();
                            string value = match.Groups[3].Value;

                            if (variableName == "VERSION")
                            {
                                version = value;
                            }
                            else if (variableName == "EEPROM_STATUS")
                            {
                                eepromStatus = value;
                            }
                            else if (variableName == "SVN_REVISION" || variableName == "SVN_REVISION")
                            {
                                svnRevision = value;
                            }
                            else if (variableName == "SVN_REVISION")
                            {
                                svnRevision = value;
                            }
                        }
                    }

                    string displayString = $"{projectDirectoryName}: [{firmwareDirName}]  {version}_{eepromStatus}";

                    if (!string.IsNullOrEmpty(svnRevision))
                    {
                        displayString += $"_Rev{svnRevision}";
                    }

                    firmwareInfoList.Add(displayString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while gathering firmware info: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return firmwareInfoList;
        }

        private void SVeeSearchForm_Load(object sender, EventArgs e)
        {
            PopulateComboBoxWithDirectories();
        }

        private void PopulateComboBoxWithDirectories()
        {
            try
            {
                if (projectDirectories != null && projectDirectories.Count > 0)
                {
                    List<string> directoryNames = projectDirectories.Select(Path.GetFileName).ToList();
                    comboBoxDirectories.DataSource = directoryNames;
                    comboBoxDirectories.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("No project directories found.", "No Projects", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while populating directories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (projectParametersFiles.TryGetValue(projectDirectoryName, out List<string> parametersFiles))
            {
                List<string> firmwareInfoList = GetFirmwareInfoList(projectDirectoryName, parametersFiles);
                listBoxFirmwareInfo.DataSource = firmwareInfoList;
            }
            else
            {
                listBoxFirmwareInfo.DataSource = null;
                MessageBox.Show($"No parameters.cpp files found for project '{projectDirectoryName}'.", "No Parameters Files", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
