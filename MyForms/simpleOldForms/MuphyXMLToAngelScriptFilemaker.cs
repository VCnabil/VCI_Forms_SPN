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
using System.Xml.Linq;

namespace VCI_Forms_SPN.MyForms.simpleOldForms
{
    public partial class MuphyXMLToAngelScriptFilemaker : Form
    {
        //  C:\____SVN_vcinc_2023\projects\ms_san_francisco\MS_SF_Murphy_PV780\MS_SF_DEV\PV780_MSSF_67_11_Rev6373Copy.configData
        private static MuphyXMLToAngelScriptFilemaker _instance;
        private const string BASEPATH_svnProjects = @"C:\____SVN_vcinc_2023\projects";
        private const string BASEPATH_svnMS_SF_DEV = @"C:\____SVN_vcinc_2023\projects\ms_san_francisco\MS_SF_Murphy_PV780\MS_SF_DEV";
        public MuphyXMLToAngelScriptFilemaker()
        {
            InitializeComponent();
            btnBrowse.Click += btnBrowse_Click;
            btnProcess.Click += btnProcess_Click;
            // Handle FormClosed to reset the instance
            this.FormClosed += MainForm_FormClosed;
        }
        // Ensures only one instance of MainForm can be open
        public static MuphyXMLToAngelScriptFilemaker GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MuphyXMLToAngelScriptFilemaker();
            }
            return _instance;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null; // Reset the instance when the form is closed
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = BASEPATH_svnMS_SF_DEV;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtDirectoryPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = BASEPATH_svnMS_SF_DEV;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string xmlFilePath = openFileDialog.FileName;

                if (string.IsNullOrWhiteSpace(xmlFilePath) || !File.Exists(xmlFilePath))
                {
                    MessageBox.Show("Please select a valid XML file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    string targetDirectory = CreateTargetDirectory(xmlFilePath);
                    ExtractScriptsFromXml(xmlFilePath, targetDirectory);
                    MessageBox.Show("Scripts extracted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string CreateTargetDirectory(string xmlFilePath)
        {
            string directoryName = Path.GetFileNameWithoutExtension(xmlFilePath);
            string baseDirectory = Path.GetDirectoryName(xmlFilePath);
            string targetBaseName = $"ANGEL_{directoryName}";

            string targetDirectory;

            if (rdoOverwrite.Checked)
            {
                // Overwrite mode: remove existing directory if it exists
                targetDirectory = Path.Combine(baseDirectory, targetBaseName);
                if (Directory.Exists(targetDirectory))
                {
                    Directory.Delete(targetDirectory, true);
                }
            }
            else
            {
                // Make new mode: find a new directory name with incremented suffix
                int count = Directory.GetDirectories(baseDirectory, $"{targetBaseName}*").Length;
                targetDirectory = Path.Combine(baseDirectory, $"{targetBaseName}_{count + 1}");
            }

            // Create the target directory
            Directory.CreateDirectory(targetDirectory);

            return targetDirectory;
        }

        private void ExtractScriptsFromXml(string xmlFilePath, string targetDirectory)
        {
            XDocument xmlDoc = XDocument.Load(xmlFilePath);
            var scripts = xmlDoc.Descendants("Script");

            foreach (var script in scripts)
            {
                string scriptName = script.Element("Name")?.Value;
                string scriptText = script.Element("ScriptText")?.Value;

                if (string.IsNullOrEmpty(scriptName) || scriptText == null)
                {
                    continue; // Skip if required data is missing
                }

                string scriptFilePath = Path.Combine(targetDirectory, $"{scriptName}.as");

                // Create new file or overwrite existing one
                File.WriteAllText(scriptFilePath, scriptText);
            }
        }
    }
}
