using SmallFIMTool.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallFIMTool
{
    public class FIMWorker
    {
        public ConsoleOptions ConsoleOptions { get; set; }

        public FIMWorker(ConsoleOptions options)
        {
            ConsoleOptions = options;
        }

        public void Work()
        {

        }
    }
}
