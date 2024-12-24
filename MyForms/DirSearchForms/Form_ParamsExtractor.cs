using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OCV_LIB;
using OCV_LIB.UCFiles;
using VCI_Forms_SPN._BackEndDataOBJs.OBJ_defs;
namespace VCI_Forms_SPN.MyForms.DirSearchForms
{
    public partial class Form_ParamsExtractor : Form
    {
        public Form_ParamsExtractor()
        {
            InitializeComponent();
            this.Load += Form_ParamsExtractor_Load;
            buttonSearch.Click += buttonSearch_Click;
        }
        private void Form_ParamsExtractor_Load(object sender, EventArgs e)
        {
            checkedListBoxPaths.Items.Clear();
            var allPaths = FilePathIdentifyer.GetPaths();
            foreach (var path in allPaths)
            {
                checkedListBoxPaths.Items.Add(path, false);
            }
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            flowLayoutPanelResults.Controls.Clear();
            var matchedFiles = PerformSearch(SearchBox.Text);
            int linkCount = 0;
            foreach (var filePath in matchedFiles)
            {
                if (linkCount >= 128)
                {
                    //MessageBox.Show("Reached the maximum limit of 128 results.");
                    break;
                }
                var extension = Path.GetExtension(filePath).TrimStart('.').ToLowerInvariant();
                var fileName = Path.GetFileName(filePath);
                var fileLinkControl = new vcucFileLink
                {
                    FilePath = filePath,
                    FileName = fileName,
                    FileFormat = extension,
                    Certainty = 0.0
                };
                fileLinkControl.FileClicked += FileLinkControl_FileClicked;
                flowLayoutPanelResults.Controls.Add(fileLinkControl);
                linkCount++;
            }
        }

        private List<string> PerformSearch(string searchText)
        {
            var selectedPaths = new List<string>();
            bool selectAll = false;
            foreach (var item in checkedListBoxPaths.CheckedItems)
            {
                if (item.ToString().Equals("ALL", StringComparison.OrdinalIgnoreCase))
                {
                    selectAll = true;
                    break;
                }
                selectedPaths.Add(item.ToString());
            }
            if (selectAll)
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
                            if (ShouldInclude(f))
                            {
                                matchedFiles.Add(f);
                            }
                        }
                    }
                }
            }
            return matchedFiles;
        }
        private bool ShouldInclude(string filePath)
        {
            string fileName = Path.GetFileName(filePath).ToLowerInvariant();
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (fileName == "main.c")
            {
                string dir = Path.GetDirectoryName(filePath);
                string possibleHPath = Path.Combine(dir, "vci2.h");
                if (File.Exists(possibleHPath))
                {
                    return true;
                }
                return false;
            }
            if (fileName == "parameters.cpp")
            {
                return true;
            }
            if (extension == ".pdf")
            {
                return true;
            }
            return false;
        }
        private void FileLinkControl_FileClicked(object sender, string filePath)
        {
            var parsedParams = FirmwareParamExtractor.ExtractFirmwareParameters(filePath);
            if (parsedParams != null)
            {
                lbl_01.Text = BuildParamsDisplayString(parsedParams);
            }
            else
            {
                lbl_01.Text = "No parameters extracted from: " + filePath;
            }
        }
        private string BuildParamsDisplayString(FirmwareParameters fp)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"FileName: {fp.SourceFileName}");  // <-- Add this line
            sb.AppendLine($"Project: {fp.ProjectName}");
            sb.AppendLine($"DirName: {fp.FirmwareDirName}");
            sb.AppendLine($"Version: {fp.Version}");
            sb.AppendLine($"EepromStatus: {fp.EepromStatus}");
            sb.AppendLine($"SvnRevision: {fp.SvnRevision}");
            sb.AppendLine();
            sb.AppendLine($"NumberOfWaterJets: {fp.NumberOfWaterJets}");
            sb.AppendLine($"NumberOfBowThrusters: {fp.NumberOfBowThrusters}");
            sb.AppendLine($"NumberOfNozzles: {fp.NumberOfNozzles}");
            sb.AppendLine($"NumberOfBuckets: {fp.NumberOfBuckets}");
            sb.AppendLine($"NumberOfIntTabs: {fp.NumberOfIntTabs}");
            sb.AppendLine($"NumberOfStations: {fp.NumberOfStations}");
            sb.AppendLine($"NumberOfJoysticks: {fp.NumberOfJoysticks}");
            sb.AppendLine($"NumberOfLevers: {fp.NumberOfLevers}");
            sb.AppendLine($"NumberOfHelms: {fp.NumberOfHelms}");
            sb.AppendLine($"NumberOfTillers: {fp.NumberOfTillers}");
            sb.AppendLine($"NumberOfLCDs: {fp.NumberOfLCDs}");
            sb.AppendLine();
            sb.AppendLine($"HasIdleKnob: {fp.HasIdleKnob}");
            sb.AppendLine($"HasCAN: {fp.HasCAN}");
            sb.AppendLine($"HasRS232: {fp.HasRS232}");
            sb.AppendLine($"CalMode: {fp.CalMode}");
            return sb.ToString();
        }
    }
}