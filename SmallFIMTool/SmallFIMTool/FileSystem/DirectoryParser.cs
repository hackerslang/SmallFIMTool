using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallFIMTool.FileSystem
{
    public static class DirectoryParser
    {
        public static List<String> GetAllFiles(string directoryPath, bool isRecursive = true)
        {
            List<String> filePaths = new List<string>();

            filePaths = GetAllFilesFromDirectory(directoryPath, isRecursive);

            return filePaths;
        }

        public static List<String> GetAllFilesFromDirectory(string directoryPath, bool isRecursive = true)
        {
            List<String> files = new List<String>();

            try
            {
                if (isRecursive)
                {
                    foreach (string folderPath in Directory.GetDirectories(directoryPath))
                    {
                        if (Directory.Exists(folderPath))
                        {
                            List<String> filesInDirectory = GetAllFilesFromDirectory(folderPath, true);

                            foreach (string file in filesInDirectory)
                            {
                                files.Add(file);
                            }
                        }
                    }
                }

                foreach (string filePath in Directory.GetFiles(directoryPath))
                {
                    files.Add(filePath);
                }
            }
            catch (Exception ex)
            {

            }

            return files;
        }
    }
}
