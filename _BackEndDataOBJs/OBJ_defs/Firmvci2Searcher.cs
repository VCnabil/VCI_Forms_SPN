using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCI_Forms_SPN._BackEndDataOBJs.OBJ_defs
{
    public class Firmvci2Searcher
    {
        private List<string> rootDirectories;
        private string rootOUTDirectory;

        public List<string> ProjectDirectories { get; private set; }
        public Dictionary<string, List<string>> ProjectFiles { get; private set; }

        public Firmvci2Searcher(List<string> rootDirs, string outputDirectory)
        {
            rootDirectories = rootDirs;
            rootOUTDirectory = outputDirectory;

            ProjectDirectories = new List<string>();
            ProjectFiles = new Dictionary<string, List<string>>();
        }
        public void CollectProjectDirectoriesAndParametersFiles()
        {
            try
            {
                foreach (string rootDir in rootDirectories)
                {
                    if (Directory.Exists(rootDir))
                    {
                        // Get directories up to 10 levels deep
                        var directories = GetDirectoriesWithMaxDepth(rootDir, 4).ToList();

                        foreach (string projectPath in directories)
                        {
                            List<string> foundFiles = new List<string>();

                            try
                            {
                                // Search for main.c and vci2.h
                                string[] mainCFiles = Directory.GetFiles(projectPath, "main.c", SearchOption.TopDirectoryOnly);
                                string[] vci2Headers = Directory.GetFiles(projectPath, "vci2.h", SearchOption.TopDirectoryOnly);

                                if (mainCFiles.Length > 0 && vci2Headers.Length > 0)
                                {
                                    foundFiles.AddRange(mainCFiles);
                                    foundFiles.AddRange(vci2Headers);

                                    // Determine project name
                                    string mainCPath = mainCFiles.First();
                                    string projectName = DetermineProjectName(mainCPath);

                                    string uniqueProjectKey = Path.Combine(rootDir, projectName);

                                    if (ProjectFiles.ContainsKey(uniqueProjectKey))
                                    {
                                        ProjectFiles[uniqueProjectKey].AddRange(foundFiles);
                                    }
                                    else
                                    {
                                        ProjectFiles[uniqueProjectKey] = foundFiles;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error processing files in {projectPath}: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"The directory '{rootDir}' does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while collecting project directories and files: {ex.Message}");
            }
        }
        private string DetermineProjectName(string mainCPath)
        {
            string parentDir = Path.GetFileName(Path.GetDirectoryName(mainCPath)); // Parent directory of main.c
            string grandParentDir = Path.GetFileName(Directory.GetParent(Path.GetDirectoryName(mainCPath))?.FullName); // Grandparent directory

            // If the parent directory name matches "KA2600", "KA2700", "KA3600", etc., use grandparent directory as project name
            if (Regex.IsMatch(parentDir, @"KA\d{4}", RegexOptions.IgnoreCase))
            {
                return grandParentDir ?? parentDir; // Fallback to parentDir if grandparentDir is null
            }

            // Otherwise, use parent directory name
            return parentDir;
        }
        private IEnumerable<string> GetDirectoriesWithMaxDepth(string rootDir, int maxDepth)
        {
            List<string> directories = new List<string>();
            TraverseDirectories(rootDir, 0, maxDepth, directories);
            return directories;
        }

        private void TraverseDirectories(string currentDir, int currentDepth, int maxDepth, List<string> directories)
        {
            if (currentDepth > maxDepth) return; // Stop recursion beyond maxDepth

            try
            {
                directories.Add(currentDir);

                foreach (var subDir in Directory.GetDirectories(currentDir))
                {
                    TraverseDirectories(subDir, currentDepth + 1, maxDepth, directories);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Access denied to directory: {currentDir}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing directory {currentDir}: {ex.Message}");
            }
        }



        public List<FirmwareParameters> GetFirmwareParametersList(string uniqueProjectKey)
        {
            List<FirmwareParameters> firmwareParametersList = new List<FirmwareParameters>();

            try
            {
                if (ProjectFiles.TryGetValue(uniqueProjectKey, out List<string> projectFiles))
                {
                    // We know vci2.h and main.c are present in projectFiles
                    // Identify the main.c file to parse
                    string mainCFile = projectFiles.FirstOrDefault(f => Path.GetFileName(f).Equals("main.c", StringComparison.OrdinalIgnoreCase));
                    if (!string.IsNullOrEmpty(mainCFile))
                    {
                        // Project name from uniqueProjectKey
                        string projectName = Path.GetFileName(uniqueProjectKey);

                        FirmwareParameters firmwareParams = new FirmwareParameters
                        {
                            ProjectName = projectName,
                            FirmwareDirName = Path.GetFileName(Path.GetDirectoryName(mainCFile)),
                            ParametersFilePath = Path.GetDirectoryName(mainCFile)
                        };

                        // Parse main.c to find #define SW_VERSION line
                        ParseMainCForVersion(mainCFile, firmwareParams);

                        // If firmwareParams.Version or EepromStatus is not set (missing data?), you could skip adding this
                        if (!string.IsNullOrWhiteSpace(firmwareParams.Version) && !string.IsNullOrWhiteSpace(firmwareParams.EepromStatus))
                        {
                            // Optionally call SetAdditionalProperties if needed
                            // firmwareParams.SetAdditionalProperties();

                            firmwareParametersList.Add(firmwareParams);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while gathering firmware parameters: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return firmwareParametersList;
        }

        private void ParseMainCForVersion(string mainCPath, FirmwareParameters firmwareParams)
        {
            try
            {
                string[] lines = File.ReadAllLines(mainCPath);

                // Regex to match SW_VERSION with various formats like "bla12.155-ka" or "10.252"
                Regex versionRegex = new Regex(@"#define\s+SW_VERSION\s+""([\w\d-]+)\.([\w\d-]+)""");

                foreach (string line in lines)
                {
                    Match match = versionRegex.Match(line);
                    if (match.Success)
                    {
                        // Extract the major and minor parts
                        string major = match.Groups[1].Value; // e.g., "bla12"
                        string minor = match.Groups[2].Value; // e.g., "155-ka"
                        minor = "m" + minor;

                        firmwareParams.Version = major;        // Assign major part
                        firmwareParams.EepromStatus = minor;   // Assign minor part

                        break; // Stop after first match
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error parsing main.c for SW_VERSION: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public void GenerateOutputFile_AllParams(string argFileOutName)
        {
            string outputFilePath = Path.Combine(rootOUTDirectory, argFileOutName + ".txt");
            List<FirmwareParameters> allFirmwareParameters = new List<FirmwareParameters>();

            foreach (var projectEntry in ProjectFiles)
            {
                string projectDirectoryName = projectEntry.Key;
                List<FirmwareParameters> firmwareParametersList = GetFirmwareParametersList(projectDirectoryName);
                allFirmwareParameters.AddRange(firmwareParametersList);
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(outputFilePath, false))
                {
                    writer.WriteLine("ProjectName\tFirmwareDirName\tParametersFilePath\tVersion\tEepromStatus");

                    foreach (var fp in allFirmwareParameters)
                    {
                        string line = $"{fp.ProjectName}\t{fp.FirmwareDirName}\t{fp.ParametersFilePath}\t{fp.Version}\t{fp.EepromStatus}";
                        writer.WriteLine(line);

                        // Debug: Log each line written
                        Console.WriteLine($"Written: {line}");
                    }
                }

                Console.WriteLine("Output file generated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating output file: {ex.Message}");
            }
        }

        public void GenerateOutputFile_AllParamseek(string argFileOutName)
        {
            string outputFilePath = Path.Combine(rootOUTDirectory, argFileOutName + ".txt");

            List<FirmwareParameters> allFirmwareParameters = new List<FirmwareParameters>();

            // Gather all firmware parameters from all projects
            foreach (var projectEntry in ProjectFiles)
            {
                string projectDirectoryName = projectEntry.Key;
                List<FirmwareParameters> firmwareParametersList = GetFirmwareParametersList(projectDirectoryName);
                allFirmwareParameters.AddRange(firmwareParametersList);
            }

            using (StreamWriter writer = new StreamWriter(outputFilePath, false))
            {
                // Write header line for the five properties
                writer.WriteLine("ProjectName\tFirmwareDirName\tParametersFilePath\tVersion\tEepromStatus");

                foreach (var fp in allFirmwareParameters)
                {
                    // If Version or EepromStatus is empty, we can still print the line 
                    // because we didn't specify filtering here. If you want to omit rows 
                    // without Version/EepromStatus, add a check like in V4.

                    string line = $"{fp.ProjectName}\t{fp.FirmwareDirName}\t{fp.ParametersFilePath}\t{fp.Version}\t{fp.EepromStatus}";
                    writer.WriteLine(line);
                }
            }

            System.Windows.Forms.MessageBox.Show("AllParams output file generated successfully.", "Success", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        public void GenerateOutputFile_V4(string argOutFilename)
        {
            string outputFilePath = Path.Combine(rootOUTDirectory, argOutFilename + ".txt");
            List<FirmwareParameters> allFirmwareParameters = new List<FirmwareParameters>();

            foreach (var projectEntry in ProjectFiles)
            {
                string projectDirectoryName = projectEntry.Key;
                List<FirmwareParameters> firmwareParametersList = GetFirmwareParametersList(projectDirectoryName);
                allFirmwareParameters.AddRange(firmwareParametersList);
            }

            using (StreamWriter writer = new StreamWriter(outputFilePath, false))
            {
                // Header line: sysdiag (FirmwareDirName), Boat Name (ProjectName), Software (Version.EepromStatus), ParametersFilePath
                // The user previously asked for columns: sysdiag, Boat Name, Software, # LCD, # Nozzles, # Buckets, # Int/Tabs, Joystick, Levers, Helm, Tiller, RS232, CAN, Cal, Idle Knob, Dim, ParametersFilePath.
                // But now we are only concerned with a smaller set of properties. The user specifically said we are only concerned with these 5 properties: 
                // ProjectName, FirmwareDirName, ParametersFilePath, Version, EepromStatus
                // For V4, we previously had more columns, but the user states "we are only concerned" now, implying we should simplify.
                //
                // Let's simplify V4 to match the user's new concern:
                // sysdiag (FirmwareDirName), Boat Name (ProjectName), Software (Version.EepromStatus), ParametersFilePath

                writer.WriteLine("sysdiag\tBoat Name\tSoftware\tParametersFilePath");

                foreach (var firmwareParams in allFirmwareParameters)
                {
                    // Construct Software from Version and EepromStatus
                    string software = $"{firmwareParams.Version}.{firmwareParams.EepromStatus}";
                    if (string.IsNullOrWhiteSpace(software) || software == ".")
                    {
                        continue; // Omit rows without valid Software
                    }

                    string sysdiag = firmwareParams.FirmwareDirName;   // sysdiag maps to FirmwareDirName
                    string boatName = firmwareParams.ProjectName;       // Boat Name maps to ProjectName
                    string parametersFilePath = firmwareParams.ParametersFilePath;

                    // Build the line with the now simplified columns
                    string line = $"{sysdiag}\t{boatName}\t{software}\t{parametersFilePath}";

                    writer.WriteLine(line);
                }
            }

            System.Windows.Forms.MessageBox.Show("Output file (V4) generated successfully.", "Success", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }


    }
}
