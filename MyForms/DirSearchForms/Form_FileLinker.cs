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
using OCV_LIB;

namespace VCI_Forms_SPN.MyForms.DirSearchForms
{
    public partial class Form_FileLinker : Form
    {
        public Form_FileLinker()
        {
            InitializeComponent();
            buttonSearch.Click += buttonSearch_Click;
            this.Load += new System.EventHandler(this.Form1_Load);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var paths = FilePathIdentifyer.GetPaths();
            foreach (var p in paths)
            {
                checkedListBoxPaths.Items.Add(p, false); //load the paths into the checkedListBox
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

                var fileLinkControl = new OCV_LIB.UCFiles.vcucFileLink
                {
                    FilePath = filePath,
                    FileName = fileName,
                    FileFormat = extension,
                    Certainty = 0.0 // For now
                };
                fileLinkControl.FileClicked += FileLinkControl_FileClicked;
                flowLayoutPanelResults.Controls.Add(fileLinkControl);
            }
        }


        private List<string> PerformSearch(string searchText)
        {
            var selectedPaths = new List<string>();
            // Gather selected paths from the checkedListBox
            bool allSelected = false;
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
                // If ALL is selected, gather all paths from the list (except "ALL")
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
                if (System.IO.Directory.Exists(dir))
                {
                    // Get all files and check if filename contains the search text
                    var files = System.IO.Directory.GetFiles(dir, "*", System.IO.SearchOption.AllDirectories);
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

        private void FileLinkControl_FileClicked(object sender, string filePath)
        {
            MessageBox.Show("Clicked file: " + filePath);
        }
    }
}
