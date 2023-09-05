using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallFIMTool.FileSystem
{
    public class OutputFileCreator
    {
        public string Path { get; set; }
        public FileResult Result { get; set; }

        public OutputFileCreator(string path, FileResult result)
        {
            Path = path;
            Result = result;
        }

        public void ParseResults()
        { 
            StringBuilder sb = new StringBuilder();

            foreach (FileResultRecord record in Result.ResultRecords)
            {
                sb.AppendLine(record.ConcatLine());
            }

            File.WriteAllText(Path, sb.ToString());
        }

        
    }
}
