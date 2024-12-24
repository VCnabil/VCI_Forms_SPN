// =============================================
// HOW TO POPULATE THE COMBOBOX DYNAMICALLY FROM CHECKED PATHS:
//
// 1) Wire up the checkedListBoxPaths.ItemCheck event so that
//    whenever a path is (un)checked, we rebuild the combobox.
//
// 2) In that event, gather the newly checked paths, use them to
//    create a FirmwareSearcher, collect the project directories,
//    then populate comboBoxDirectories.
//
// 3) Make sure you remove any "hard-coded" usage of rootSVNDirectories
//    in SVeeSearchForm_Load if you only want the combobox to populate
//    based on the user's checked items.
//
// BELOW IS A MINIMAL EXAMPLE of SVeeSearchForm.cs showing exactly
// what lines to add or modify. The relevant code is clearly marked.
// =============================================

using OCV_LIB;
using OCV_LIB.UCFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VCI_Forms_SPN._BackEndDataOBJs.OBJ_defs; // FirmwareSearcher, Firmvci2Searcher, etc.


namespace VCI_Forms_SPN.MyForms.DirSearchForms
{
    public partial class SVeeSearchFormV2 : Form
    {
        // Only needed if you still want to generate an output file.
        // Adjust this as desired:
        private string rootOUTDirectory = @"C:\___Base_Data_READ_Path\ReadParamsDir\";

        public SVeeSearchFormV2()
        {
            InitializeComponent();
            // 1) Wire up the ItemCheck event for the checkedListBoxPaths:
            //    EXACT LINE TO ADD:
            checkedListBoxPaths.ItemCheck += checkedListBoxPaths_ItemCheck;

            // Still handle the button event for file searching (optional).
            buttonSearch.Click += buttonSearch_Click;

            // If you have logic in Load, you can keep it minimal:
            this.Load += SVeeSearchForm_Load;
        }

        // Keep this short if you wish. E.g. load the paths but do NOT
        // create a FirmwareSearcher until the user checks items.
        private void SVeeSearchForm_Load(object sender, EventArgs e)
        {
            // 2) Just load all paths into the checkedListBox, but do NOT
            //    populate the combobox from them yet.
            checkedListBoxPaths.Items.Clear();
            var allPaths = FilePathIdentifyer.GetPaths();
            foreach (var path in allPaths)
            {
                checkedListBoxPaths.Items.Add(path, false);
            }

            // Optionally set the combo to empty at first:
            comboBoxDirectories.DataSource = null;
        }

        // 3) When user checks/unchecks, we want to repopulate the combobox:
        private void checkedListBoxPaths_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // The actual check state changes AFTER this event ends,
            // so do a BeginInvoke to refresh once the control has updated:
            this.BeginInvoke(new Action(RefreshComboBoxFromCheckedPaths));
        }

        // 4) This method does all the "collect directories + populate comboBox" logic:
        private void RefreshComboBoxFromCheckedPaths()
        {
            // A) Gather currently checked paths (ignore "ALL" if you like):
            var selectedPaths = new List<string>();
            bool selectedAll = false;
            foreach (var item in checkedListBoxPaths.CheckedItems)
            {
                if (item.ToString().Equals("ALL", StringComparison.OrdinalIgnoreCase))
                {
                    selectedAll = true;
                    break;
                }
                selectedPaths.Add(item.ToString());
            }
            if (selectedAll)
            {
                selectedPaths.Clear();
                foreach (var item in checkedListBoxPaths.Items)
                {
                    if (!item.ToString().Equals("ALL", StringComparison.OrdinalIgnoreCase))
                        selectedPaths.Add(item.ToString());
                }
            }

            // B) Create a FirmwareSearcher from those selected paths:
            var fwSearcher = new FirmwareSearcher(selectedPaths, rootOUTDirectory);
            fwSearcher.CollectProjectDirectoriesAndParametersFiles();

            // C) Grab the resulting project directories and populate the combobox:
            if (fwSearcher.ProjectDirectories != null && fwSearcher.ProjectDirectories.Count > 0)
            {
                var dirNames = fwSearcher.ProjectDirectories
                    .Select(Path.GetFileName)
                    .Distinct()
                    .ToList();

                comboBoxDirectories.DataSource = dirNames;
                comboBoxDirectories.SelectedIndex = -1; // optional
            }
            else
            {
                comboBoxDirectories.DataSource = null;
            }
        }

        // If you still want the firmware info listing (like before):
        private void comboBoxDirectories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDirectories.SelectedItem == null) return;
            string selectedDirName = comboBoxDirectories.SelectedItem.ToString();
            lbl_FoundCfiles.Text = $"Searching: {selectedDirName}...";
            // ... do your PopulateListBoxWithFirmwareInfo stuff ...
        }

        // Same file search logic from your code:
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            flowLayoutPanelResults.Controls.Clear();
            List<string> matchedFiles = PerformSearch(SearchBox.Text);

            // Display them with vcucFileLink controls:
            foreach (string filePath in matchedFiles)
            {
                double certainty = 0.0; // or any logic you want
                var extension = Path.GetExtension(filePath).TrimStart('.').ToLowerInvariant();
                var fileLinkControl = new vcucFileLink
                {
                    FilePath = filePath,
                    FileName = Path.GetFileName(filePath),
                    FileFormat = extension,
                    Certainty = certainty
                };
                fileLinkControl.FileClicked += FileLinkControl_FileClicked;
                flowLayoutPanelResults.Controls.Add(fileLinkControl);
            }
        }

        private List<string> PerformSearch(string searchText)
        {
            var selectedPaths = new List<string>();
            bool selectedAll = false;
            foreach (var item in checkedListBoxPaths.CheckedItems)
            {
                if (item.ToString().Equals("ALL", StringComparison.OrdinalIgnoreCase))
                {
                    selectedAll = true;
                    break;
                }
                selectedPaths.Add(item.ToString());
            }
            if (selectedAll)
            {
                selectedPaths.Clear();
                foreach (var item in checkedListBoxPaths.Items)
                {
                    if (!item.ToString().Equals("ALL", StringComparison.OrdinalIgnoreCase))
                        selectedPaths.Add(item.ToString());
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
                            matchedFiles.Add(f);
                    }
                }
            }
            return matchedFiles;
        }

        private void FileLinkControl_FileClicked(object sender, string filePath)
        {
            MessageBox.Show("Clicked file: " + filePath);
        }
    }
}