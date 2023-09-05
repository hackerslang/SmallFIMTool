using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallFIMTool.Options
{
    public class ConsoleOptionKey
    {
        public string LongAttribute { get; set; }
        public char ShortAttribute { get; set; }
        public string RequiredType { get; set; }    
        public string MappedConsoleOption { get; set; }
        public bool StacksTogether { get; set; }
        public string? PossibleValues { get; set; }
    }

    public class ConsoleOption
    {
        public ConsoleOptionKey Key { get; set; }
        public object Value { get; set; }
    }
}
