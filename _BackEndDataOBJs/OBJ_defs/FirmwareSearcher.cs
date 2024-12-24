using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;// Only if you plan to use MessageBox for error handling


namespace VCI_Forms_SPN._BackEndDataOBJs.OBJ_defs
{
    public class FirmwareSearcher
    {
        // Class-level variables
        private string rootSVNDirectory;
        private List<string> rootSVNDirectories;
        private string rootOUTDirectory;
        public List<string> ProjectDirectories { get; private set; }
        public Dictionary<string, List<string>> ProjectParametersFiles { get; private set; }
        public FirmwareSearcher(List<string> rootDirectories, string outputDirectory)
        {
            rootSVNDirectories = rootDirectories;
            rootOUTDirectory = outputDirectory;
            ProjectDirectories = new List<string>();
            ProjectParametersFiles = new Dictionary<string, List<string>>();
        }
        public void CollectProjectDirectoriesAndParametersFiles()
        {
            try
            {
                foreach (string rootDirectory in rootSVNDirectories)
                {
                    if (Directory.Exists(rootDirectory))
                    {
                        var directories = Directory.GetDirectories(rootDirectory).ToList();
                        ProjectDirectories.AddRange(directories);
                        foreach (string projectDirectoryPath in directories)
                        {
                            string projectDirectoryName = Path.GetFileName(projectDirectoryPath);
                            string[] parametersFiles = Directory.GetFiles(projectDirectoryPath, "parameters.cpp", SearchOption.AllDirectories);
                            if (ProjectParametersFiles.ContainsKey(projectDirectoryName))
                            {
                                ProjectParametersFiles[projectDirectoryName].AddRange(parametersFiles);
                            }
                            else
                            {
                                ProjectParametersFiles[projectDirectoryName] = parametersFiles.ToList();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"The root SVN directory '{rootDirectory}' does not exist.", "Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while collecting project directories and parameters files: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public List<FirmwareParameters> GetFirmwareParametersList(string projectDirectoryName)
        {
            List<FirmwareParameters> firmwareParametersList = new List<FirmwareParameters>();
            try
            {
                if (ProjectParametersFiles.TryGetValue(projectDirectoryName, out List<string> parametersFiles))
                {
                    foreach (string parametersFilePath in parametersFiles)
                    {
                        FirmwareParameters firmwareParams = new FirmwareParameters
                        {
                            ProjectName = projectDirectoryName,
                            FirmwareDirName = Path.GetFileName(Path.GetDirectoryName(parametersFilePath)),
                            ParametersFilePath = Path.GetDirectoryName(parametersFilePath)
                        };
                        string[] fileLines = File.ReadAllLines(parametersFilePath);
                        string patternEParamTwoArgs = @"^\s*EParam\s+(\w+)\s*\(\s*(\d+)\s*,\s*\d+\s*\)\s*;";
                        string patternEParamOneArg = @"^\s*EParam\s+(\w+)\s*=\s*(\d+)\s*;";
                        string patternVariable = @"^\s*(int|int16)\s+(\w+)\s*=\s*(\d+)\s*;";
                        Regex regexEParamTwoArgs = new Regex(patternEParamTwoArgs, RegexOptions.IgnoreCase);
                        Regex regexEParamOneArg = new Regex(patternEParamOneArg, RegexOptions.IgnoreCase);
                        Regex regexVariable = new Regex(patternVariable, RegexOptions.IgnoreCase);
                        foreach (string line in fileLines)
                        {
                            string trimmedLine = line.Trim();
                            Match matchEParamTwoArgs = regexEParamTwoArgs.Match(trimmedLine);
                            if (matchEParamTwoArgs.Success)
                            {
                                string paramName = matchEParamTwoArgs.Groups[1].Value;
                                int valueA = int.Parse(matchEParamTwoArgs.Groups[2].Value);
                                vcEparam eparam = new vcEparam(paramName, valueA);
                                firmwareParams.EParams[paramName] = eparam;
                                continue;
                            }
                            Match matchEParamOneArg = regexEParamOneArg.Match(trimmedLine);
                            if (matchEParamOneArg.Success)
                            {
                                string paramName = matchEParamOneArg.Groups[1].Value.ToUpper();
                                int value = int.Parse(matchEParamOneArg.Groups[2].Value);
                                if (paramName == "VERSION")
                                {
                                    firmwareParams.Version = value.ToString();
                                }
                                else if (paramName == "EEPROM_STATUS")
                                {
                                    firmwareParams.EepromStatus = value.ToString();
                                }
                                else if (paramName == "SVN_REVISION")
                                {
                                    firmwareParams.SvnRevision = value.ToString();
                                }
                                else
                                {
                                    vcEparam eparam = new vcEparam(paramName, value);
                                    firmwareParams.EParams[paramName] = eparam;
                                }
                                continue;
                            }
                            Match matchVariable = regexVariable.Match(trimmedLine);
                            if (matchVariable.Success)
                            {
                                string paramName = matchVariable.Groups[2].Value.ToUpper();
                                int value = int.Parse(matchVariable.Groups[3].Value);
                                if (paramName == "VERSION")
                                {
                                    firmwareParams.Version = value.ToString();
                                }
                                else if (paramName == "EEPROM_STATUS")
                                {
                                    firmwareParams.EepromStatus = value.ToString();
                                }
                                else if (paramName == "SVN_REVISION")
                                {
                                    firmwareParams.SvnRevision = value.ToString();
                                }
                                else
                                {
                                    vcEparam eparam = new vcEparam(paramName, value);
                                    firmwareParams.EParams[paramName] = eparam;
                                }
                                continue;
                            }
                        }
                        firmwareParams.SetAdditionalProperties();
                        firmwareParametersList.Add(firmwareParams);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("An error occurred while gathering firmware parameters: " + ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return firmwareParametersList;
        }
        public void GenerateOutputFile_AllParams(string argOutfileName)
        {
            string outputFilePath = Path.Combine(rootOUTDirectory, argOutfileName + "_MB.txt");
            List<FirmwareParameters> allFirmwareParameters = new List<FirmwareParameters>();
            foreach (var projectEntry in ProjectParametersFiles)
            {
                string projectDirectoryName = projectEntry.Key;
                List<FirmwareParameters> firmwareParametersList = GetFirmwareParametersList(projectDirectoryName);
                allFirmwareParameters.AddRange(firmwareParametersList);
            }
            using (StreamWriter writer = new StreamWriter(outputFilePath, false))
            {
                writer.WriteLine("ProjectName\tVersion\tEepromStatus\tSoftware\tRevision\t#waterjets\t#stations\t# LCD\t# Nozzles\t# Buckets\t# Int/Tabs\t#bowthrust\tJoystick\tLevers\tHelm\tTiller\tRS232\tCAN\tCal\tIdle Knob\tDim\tFirmwareDirName\tParametersFilePath");
                foreach (var firmwareParams in allFirmwareParameters)
                {
                    string software = $"{firmwareParams.Version}.{firmwareParams.EepromStatus}";
                    if (string.IsNullOrWhiteSpace(software) || software == ".")
                    {
                        continue;
                    }
                    string projectName = firmwareParams.ProjectName;
                    string version = firmwareParams.Version;
                    string eepromStatus = firmwareParams.EepromStatus;
                    string revision = firmwareParams.SvnRevision;
                    string numberOfWaterJets = firmwareParams.NumberOfWaterJets.ToString();
                    string numberOfStations = firmwareParams.NumberOfStations.ToString();
                    string numberOfLCDs = firmwareParams.NumberOfLCDs.ToString();
                    string numberOfNozzles = firmwareParams.NumberOfNozzles.ToString();
                    string numberOfBuckets = firmwareParams.NumberOfBuckets.ToString();
                    string numberOfIntTabs = firmwareParams.NumberOfIntTabs.ToString();
                    string numberOfBowThrusters = firmwareParams.NumberOfBowThrusters.ToString();
                    string numberOfJoysticks = firmwareParams.NumberOfJoysticks.ToString();
                    string numberOfLevers = firmwareParams.NumberOfLevers.ToString();
                    string numberOfHelms = firmwareParams.NumberOfHelms.ToString();
                    string numberOfTillers = firmwareParams.NumberOfTillers.ToString();
                    string hasRS232 = firmwareParams.HasRS232 ? "Y" : "N";
                    string hasCAN = firmwareParams.HasCAN ? "Y" : "N";
                    string calMode = firmwareParams.CalMode;
                    string hasIdleKnob = firmwareParams.HasIdleKnob ? "Y" : "N";
                    string hasDim = firmwareParams.HasDim ? "Y" : "N";
                    string firmwareDirName = firmwareParams.FirmwareDirName;
                    string parametersFilePath = firmwareParams.ParametersFilePath;
                    string line = $"{projectName}\t{version}\t{eepromStatus}\t{software}\t{revision}\t{numberOfWaterJets}\t{numberOfStations}\t{numberOfLCDs}\t{numberOfNozzles}\t{numberOfBuckets}\t{numberOfIntTabs}\t{numberOfBowThrusters}\t{numberOfJoysticks}\t{numberOfLevers}\t{numberOfHelms}\t{numberOfTillers}\t{hasRS232}\t{hasCAN}\t{calMode}\t{hasIdleKnob}\t{hasDim}\t{firmwareDirName}\t{parametersFilePath}";
                    writer.WriteLine(line);
                }
            }
        }

    }
}
