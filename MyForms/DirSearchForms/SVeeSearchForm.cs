using OCV_LIB;                       // For FilePathIdentifyer
using OCV_LIB.UCFiles;              // For vcucFileLink
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
using VCI_Forms_SPN._BackEndDataOBJs.OBJ_defs;  // For FirmwareSearcher, Firmvci2Searcher, etc.


namespace VCI_Forms_SPN.MyForms.DirSearchForms
{
    public partial class SVeeSearchForm : Form
    {
        // Keep these private fields but do NOT assign them here:
        private FirmwareSearcher firmwareSearcher_Motherboard;
        private Firmvci2Searcher firmwareSearcher_Cantrak;

        // (Optional) adjust your output directory as needed:
        private string rootOUTDirectory = @"C:\___Base_Data_READ_Path\ReadParamsDir\";

        public SVeeSearchForm()
        {
            InitializeComponent();

            // Hook form load and button events:
            this.Load += SVeeSearchForm_Load;
            buttonSearch.Click += buttonSearch_Click;
        }

        private void SVeeSearchForm_Load(object sender, EventArgs e)
        {
            // 1) Use FilePathIdentifyer to get all possible paths:
            var allPaths = FilePathIdentifyer.GetPaths();

            // 2) Our actual rootSVNDirectories = everything except "ALL":
            var rootSVNDirectories = allPaths
                .Where(path => !path.Equals("ALL", StringComparison.OrdinalIgnoreCase))
                .ToList();

            // 3) Instantiate the firmware searchers with those root paths:
            firmwareSearcher_Motherboard = new FirmwareSearcher(rootSVNDirectories, rootOUTDirectory);
            firmwareSearcher_Cantrak = new Firmvci2Searcher(rootSVNDirectories, rootOUTDirectory);

            // 4) Collect (or generate files) if desired:
            firmwareSearcher_Motherboard.CollectProjectDirectoriesAndParametersFiles();
            firmwareSearcher_Cantrak.CollectProjectDirectoriesAndParametersFiles();
            firmwareSearcher_Motherboard.GenerateOutputFile_AllParams("mini_svn_Udrive_Motherboard");
            firmwareSearcher_Cantrak.GenerateOutputFile_AllParams("mini_svn_Udrive_Cantrack");

            // 5) Populate the firmware comboBox:
            PopulateComboBoxWithDirectories();

            // 6) Also populate the checkedListBoxPaths with the same paths:
            checkedListBoxPaths.Items.Clear();
            foreach (var p in allPaths)
            {
                checkedListBoxPaths.Items.Add(p, false);
            }
        }

        private void PopulateComboBoxWithDirectories()
        {
            try
            {
                // Pull from our Motherboard searcher
                var allProjDirs = firmwareSearcher_Motherboard.ProjectDirectories;
                if (allProjDirs != null && allProjDirs.Count > 0)
                {
                    // Distinct folder names:
                    var directoryNames = allProjDirs
                        .Select(Path.GetFileName)
                        .Distinct()
                        .ToList();

                    comboBoxDirectories.DataSource = directoryNames;
                    comboBoxDirectories.SelectedIndex = -1;
                    comboBoxDirectories.SelectedIndexChanged += ComboBoxDirectories_SelectedIndexChanged;
                }
                else
                {
                    MessageBox.Show("No project directories found.", "No Projects",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating directories: " + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void ComboBoxDirectories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDirectories.SelectedItem != null)
            {
                string selectedDirectoryName = comboBoxDirectories.SelectedItem.ToString();
                lbl_FoundCfiles.Text = $"Searching '{selectedDirectoryName}'...";
                PopulateListBoxWithFirmwareInfo(selectedDirectoryName);
            }
        }

        private void PopulateListBoxWithFirmwareInfo(string projectDirectoryName)
        {
            var firmwareParametersList = firmwareSearcher_Motherboard.GetFirmwareParametersList(projectDirectoryName);

            if (firmwareParametersList != null && firmwareParametersList.Count > 0)
            {
                var info = new List<string>();
                foreach (var fp in firmwareParametersList)
                {
                    string displayString = $"{fp.ProjectName}: [{fp.FirmwareDirName}] " +
                                           $"{fp.Version}_{fp.EepromStatus}";
                    if (!string.IsNullOrEmpty(fp.SvnRevision))
                    {
                        displayString += $"_Rev{fp.SvnRevision}";
                    }
                    info.Add(displayString);
                }
                listBoxFirmwareInfo.DataSource = info;
            }
            else
            {
                listBoxFirmwareInfo.DataSource = null;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            flowLayoutPanelResults.Controls.Clear();
            List<string> matchedFiles = PerformSearch(SearchBox.Text);

            foreach (string filePath in matchedFiles)
            {
                var extension = Path.GetExtension(filePath).TrimStart('.').ToLowerInvariant();
                var fileName = Path.GetFileName(filePath);

                // Very naive "certainty" example:
                double matchCount = fileName
                                    .ToLower()
                                    .Count(c => SearchBox.Text.ToLower().Contains(c));
                double certainty = (matchCount / fileName.Length) * 100.0;

                var fileLinkControl = new vcucFileLink
                {
                    FilePath = filePath,
                    FileName = fileName,
                    FileFormat = extension,
                    Certainty = Math.Round(certainty, 2)
                };
                fileLinkControl.FileClicked += FileLinkControl_FileClicked;
                flowLayoutPanelResults.Controls.Add(fileLinkControl);
            }
        }

        private List<string> PerformSearch(string searchText)
        {
            var selectedPaths = new List<string>();
            bool allSelected = false;

            // Gather which paths are checked:
            foreach (var item in checkedListBoxPaths.CheckedItems)
            {
                if (item.ToString().Equals("ALL", StringComparison.OrdinalIgnoreCase))
                {
                    allSelected = true;
                    break;
                }
                else
                {
                    selectedPaths.Add(item.ToString());
                }
            }

            if (allSelected)
            {
                selectedPaths.Clear();
                foreach (var item in checkedListBoxPaths.Items)
                {
                    var path = item.ToString();
                    if (!path.Equals("ALL", StringComparison.OrdinalIgnoreCase))
                        selectedPaths.Add(path);
                }
            }

            var matchedFiles = new List<string>();
            foreach (var dir in selectedPaths)
            {
                if (Directory.Exists(dir))
                {
                    var files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
                    foreach (var f in files)
                    {
                        if (f.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            matchedFiles.Add(f);
                        }
                    }
                }
            }
            return matchedFiles;
        }

        // --- FROM Form_FileLinker ---
        private void FileLinkControl_FileClicked(object sender, string filePath)
        {
            MessageBox.Show("Clicked file: " + filePath);
        }
    }



}

