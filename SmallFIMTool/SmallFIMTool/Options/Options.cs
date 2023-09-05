using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallFIMTool.Options
{
    public class ConsoleOptions
    {
        public string SourceDirectoryPath { get; set; }
        public string DestinationDirectoryPath { get; set; }    
        public bool Compare { get; set; }
        public bool Hash { get; set; }
        public bool TraverseRecursively { get; set; }
        public int SkipLargeFilesMB { get; set; }
    }
}
