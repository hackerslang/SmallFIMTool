using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallFIMTool.FileSystem
{
    public class ResultsComparison
    {
        public string NewPath { get; set; }
        public string OldPath { get; set; }

        public ResultsComparison(string newPath, string oldPath)
        {
            NewPath = newPath;
            OldPath = oldPath;
        }

        public List<CompareResultRecord> Compare()
        {
            List<FileResultRecord> newResults = Read(NewPath);
            List<FileResultRecord> oldResults = Read(OldPath);
            List<CompareResultRecord> compareResults = new List<CompareResultRecord>();

            foreach (var newResult in newResults)
            {
                bool fileFound = false;
                bool encounteredEqualPath = false;
                var checksumMatches = new List<string>();

                CompareResultRecord record = new CompareResultRecord();

                foreach (var oldResult in oldResults)
                {
                    if (newResult.Checksum.Equals(oldResult.Checksum))
                    {
                        checksumMatches.Add(oldResult.FilePath);

                        if (newResult.FilePath.ToLower().Equals(oldResult.FilePath.ToLower()))
                        {
                            fileFound = true;
                        }
                    }
                    else if (newResult.FilePath.ToLower().Equals(oldResult.FilePath.ToLower()))
                    {
                        encounteredEqualPath = true;
                    }
                }

                //Do not handle collisions for right now, they are extremely RARE!!
                if (!fileFound && checksumMatches.Count > 0)
                {
                    record.FilePath = newResult.FilePath;
                    record.DidNotChange = true;
                    record.WasMoved = true;
                    record.MovedToPath = checksumMatches[0];

                    compareResults.Add(record);
                }
                else if (!fileFound && checksumMatches.Count == 0)
                {
                    record.FilePath = newResult.FilePath;
                    record.IsNewFile = true;

                    compareResults.Add(record);
                }
                else if (encounteredEqualPath)
                {
                    record.FilePath = newResult.FilePath;
                    record.HasChanged = true;

                    compareResults.Add(record);
                }
            }

            var removedFiles = oldResults.Where(o => 
                !newResults.Any(n => n.FilePath.ToLower().Equals(o.FilePath.ToLower()) || n.Checksum.Equals(o.Checksum))).ToList();

            var removedFileResultRecords = removedFiles.Select(rf => new CompareResultRecord() { FilePath = rf.FilePath, IsRemoved = true }).ToList();

            compareResults.AddRange(removedFileResultRecords);

            return compareResults;
        }

        private List<FileResultRecord> Read(string path)
        {
            var resultRecords = new List<FileResultRecord>();
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                resultRecords.Add(FileResultRecord.Read(line));
            }

            return resultRecords;
        }
    }
}
