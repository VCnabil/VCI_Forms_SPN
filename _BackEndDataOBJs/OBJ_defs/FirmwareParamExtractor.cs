using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VCI_Forms_SPN._BackEndDataOBJs.OBJ_defs; // For FirmwareParameters, vcEparam, etc.

namespace VCI_Forms_SPN.MyForms.DirSearchForms
{
    public static class FirmwareParamExtractor
    {
        public static FirmwareParameters ExtractFirmwareParameters(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return null; // Can't parse
            }

            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            string filename = Path.GetFileName(filePath).ToLowerInvariant();

            // 1) If it's "parameters.cpp"
            if (filename.Equals("parameters.cpp", StringComparison.OrdinalIgnoreCase))
            {
                return ParseParametersCpp(filePath);
            }
            // 2) If it's "main.c"
            else if (filename.Equals("main.c", StringComparison.OrdinalIgnoreCase))
            {
                return ParseMainC(filePath);
            }
            // 3) If it's ".pdf"
            else if (extension == ".pdf")
            {
                return ParsePdfFile(filePath);
            }

            // Otherwise, we do not parse it here
            return null;
        }

        // ------------------------------
        //  A) "parameters.cpp" parsing
        // ------------------------------
        private static FirmwareParameters ParseParametersCpp(string filePath)
        {
            try
            {
                var firmwareParams = new FirmwareParameters
                {
                    ProjectName = Path.GetFileName(Path.GetDirectoryName(filePath)),
                    FirmwareDirName = Path.GetFileName(Path.GetDirectoryName(filePath)),
                    ParametersFilePath = Path.GetDirectoryName(filePath),
                    SourceFileName = Path.GetFileName(filePath)
                };

                string[] lines = File.ReadAllLines(filePath);

                // Regex patterns to detect lines, e.g.:
                //   EParam SOME_PARAM(123, 999);
                //   EParam SOME_PARAM = 123;
                //   int    SOME_PARAM = 123;
                //   int16  SOME_PARAM = 123;
                string patternEParamTwoArgs = @"^\s*EParam\s+(\w+)\s*\(\s*(\d+)\s*,\s*\d+\s*\)\s*;";
                string patternEParamOneArg = @"^\s*EParam\s+(\w+)\s*=\s*(\d+)\s*;";
                string patternVariable = @"^\s*(int|int16)\s+(\w+)\s*=\s*(\d+)\s*;";

                Regex regexEParamTwoArgs = new Regex(patternEParamTwoArgs, RegexOptions.IgnoreCase);
                Regex regexEParamOneArg = new Regex(patternEParamOneArg, RegexOptions.IgnoreCase);
                Regex regexVariable = new Regex(patternVariable, RegexOptions.IgnoreCase);

                foreach (var line in lines)
                {
                    string trimmedLine = line.Trim();

                    // A) EParam SOME_PARAM(123, 999);
                    Match match2A = regexEParamTwoArgs.Match(trimmedLine);
                    if (match2A.Success)
                    {
                        string paramName = match2A.Groups[1].Value;
                        int valueA = int.Parse(match2A.Groups[2].Value);

                        firmwareParams.EParams[paramName] = new vcEparam(paramName, valueA);
                        continue;
                    }

                    // B) EParam SOME_PARAM = 123;
                    Match match1A = regexEParamOneArg.Match(trimmedLine);
                    if (match1A.Success)
                    {
                        string paramName = match1A.Groups[1].Value.ToUpper();
                        int value = int.Parse(match1A.Groups[2].Value);

                        if (paramName == "VERSION") firmwareParams.Version = value.ToString();
                        else if (paramName == "EEPROM_STATUS") firmwareParams.EepromStatus = value.ToString();
                        else if (paramName == "SVN_REVISION") firmwareParams.SvnRevision = value.ToString();
                        else
                            firmwareParams.EParams[paramName] = new vcEparam(paramName, value);

                        continue;
                    }

                    // C) int / int16  SOME_PARAM = 123;
                    Match matchVar = regexVariable.Match(trimmedLine);
                    if (matchVar.Success)
                    {
                        string paramName = matchVar.Groups[2].Value.ToUpper();
                        int value = int.Parse(matchVar.Groups[3].Value);

                        if (paramName == "VERSION") firmwareParams.Version = value.ToString();
                        else if (paramName == "EEPROM_STATUS") firmwareParams.EepromStatus = value.ToString();
                        else if (paramName == "SVN_REVISION") firmwareParams.SvnRevision = value.ToString();
                        else
                            firmwareParams.EParams[paramName] = new vcEparam(paramName, value);
                    }
                }

                //  *IMPORTANT* => Fill out the "additional properties" from EParams.
                //  This will parse out e.g. NUMBER_WATERJET_UNITS => NumberOfWaterJets, etc.
                firmwareParams.SetAdditionalProperties();

                return firmwareParams;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error parsing parameters.cpp: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // ------------------------------
        //  B) "main.c" parsing
        // ------------------------------
        private static FirmwareParameters ParseMainC(string filePath)
        {
            try
            {
                var projectDir = Path.GetFileName(Path.GetDirectoryName(filePath));
                var firmwareParams = new FirmwareParameters
                {
                    ProjectName = projectDir,
                    FirmwareDirName = projectDir,
                    ParametersFilePath = Path.GetDirectoryName(filePath),
                    SourceFileName = Path.GetFileName(filePath)
                };

                // Example: #define SW_VERSION "123.456"
                // We'll interpret "123" -> Version, "m456" -> EepromStatus
                string[] lines = File.ReadAllLines(filePath);
                Regex versionRegex = new Regex(@"#define\s+SW_VERSION\s+""([\w\d-]+)\.([\w\d-]+)""");

                foreach (string line in lines)
                {
                    Match match = versionRegex.Match(line);
                    if (match.Success)
                    {
                        string major = match.Groups[1].Value;
                        string minor = match.Groups[2].Value;

                        firmwareParams.Version = major;
                        firmwareParams.EepromStatus = "m" + minor;

                        break;
                    }
                }

                // We typically won't see EParam definitions in main.c for all waterjet properties,
                // so "SetAdditionalProperties()" will just initialize them to zero or false.
                firmwareParams.SetAdditionalProperties();
                return firmwareParams;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error parsing main.c: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // ------------------------------
        //  C) Placeholder: parse ".pdf"
        // ------------------------------
        private static FirmwareParameters ParsePdfFile(string filePath)
        {
            // If you have advanced logic to parse PDF files, do it here.
            // For now, we just set some placeholders.
            var firmwareParams = new FirmwareParameters
            {
                ProjectName = Path.GetFileNameWithoutExtension(filePath),
                FirmwareDirName = Path.GetFileNameWithoutExtension(filePath),
                ParametersFilePath = Path.GetDirectoryName(filePath),
                Version = "N/A",
                EepromStatus = "N/A",
                SourceFileName = Path.GetFileName(filePath)
            };

            // We can do "SetAdditionalProperties()" too, so it zeroes out waterjets, bow thrusters, etc.
            firmwareParams.SetAdditionalProperties();

            return firmwareParams;
        }
    }
}
