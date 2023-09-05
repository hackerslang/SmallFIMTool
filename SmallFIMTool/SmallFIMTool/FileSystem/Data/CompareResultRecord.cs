using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallFIMTool.FileSystem
{
    public class CompareResultRecord
    {
        public string FilePath { get; set; }
        public bool DidNotChange { get; set; }
        public bool IsRemoved { get; set; }
        public bool IsNewFile { get; set; } //WasAdded
        public bool HasChanged { get; set; }
        public bool WasMoved { get; set; }
        public string? MovedToPath { get; set; }
        public bool HasChecksumCollision { get; set; }
        public List<string> CollisionPaths { get; set; }
    }
}
