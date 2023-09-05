using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallFIMTool.FileSystem
{
    public class FileResultRecord
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Checksum { get; set; }

        public string ConcatLine()
        {
            return string.Format("{0},{1},{2}", FilePath, FileName, Checksum);
        }

        public static FileResultRecord Read(string line)
        {
            var pieces = line.Split(',');

            return new FileResultRecord()
            {
                FilePath = pieces[0],
                FileName = pieces[1],
                Checksum = pieces[2]
            };
        }
    }
}
