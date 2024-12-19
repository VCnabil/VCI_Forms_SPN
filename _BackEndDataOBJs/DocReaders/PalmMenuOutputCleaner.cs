using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCI_Forms_SPN._BackEndDataOBJs.DocReaders
{
    public static class PalmMenuOutputCleaner
    {
        public static string[] Clean(string[] lines)
        {
            const int LINES_AFTER_MAIN_MENU = 15;
            var linesToRemove = new HashSet<int>();
            bool firstVectorBlockKept = false;

            // 1. Remove MAIN MENU blocks
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().Contains("MAIN MENU"))
                {
                    // Remove the line above MAIN MENU if it's the separator
                    if (i > 0 && lines[i - 1].Trim() == "----------------------------")
                    {
                        linesToRemove.Add(i - 1);
                    }

                    // Remove MAIN MENU line and the next 15 lines
                    for (int j = i; j <= i + LINES_AFTER_MAIN_MENU && j < lines.Length; j++)
                    {
                        linesToRemove.Add(j);
                    }
                }
            }

            // 2. Remove info blocks around "Vector Controls Inc." except the first encountered block
            const string marker = "Vector Controls Inc.";
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(marker))
                {
                    if (!firstVectorBlockKept)
                    {
                        // Keep the first block, do not remove lines around it.
                        firstVectorBlockKept = true;
                    }
                    else
                    {
                        // Remove subsequent occurrences
                        for (int offset = -3; offset <= 1; offset++)
                        {
                            int indexToRemove = i + offset;
                            if (indexToRemove >= 0 && indexToRemove < lines.Length)
                            {
                                linesToRemove.Add(indexToRemove);
                            }
                        }
                    }
                }
            }

            // 3. Remove the "2. Change Parameters:" block until "G) Dynamic Positioning"
            bool removingChangeParamsBlock = false;
            for (int i = 0; i < lines.Length; i++)
            {
                string trimmedLine = lines[i].Trim();
                if (trimmedLine.StartsWith("2. Change Parameters:", StringComparison.OrdinalIgnoreCase))
                {
                    removingChangeParamsBlock = true;
                    linesToRemove.Add(i); // remove the start line
                }
                else if (removingChangeParamsBlock)
                {
                    linesToRemove.Add(i);
                    if (trimmedLine.StartsWith("G) Dynamic Positioning", StringComparison.OrdinalIgnoreCase))
                    {
                        removingChangeParamsBlock = false; // end of block
                    }
                }
            }

            // Filter out removed lines
            var filteredLines = lines
                .Where((line, index) => !linesToRemove.Contains(index))
                .ToList();

            // Finally, ensure no more than 3 consecutive empty lines remain
            int emptyCount = 0;
            for (int i = filteredLines.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrWhiteSpace(filteredLines[i]))
                {
                    emptyCount++;
                    if (emptyCount > 3)
                    {
                        filteredLines.RemoveAt(i);
                    }
                }
                else
                {
                    emptyCount = 0;
                }
            }

            return filteredLines.ToArray();
        }

        // Optional helper method if you want to clean using file paths directly
        public static void CleanFile(string inputFilePath, string outputFilePath)
        {
            if (File.Exists(inputFilePath))
            {
                string[] lines = File.ReadAllLines(inputFilePath);
                var cleanedLines = Clean(lines);
                File.WriteAllLines(outputFilePath, cleanedLines);
            }
            else
            {
                throw new FileNotFoundException("Input file not found.", inputFilePath);
            }
        }
    }
}
