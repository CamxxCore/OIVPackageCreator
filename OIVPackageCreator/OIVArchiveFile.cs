using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIVPackageCreator
{
    public class OIVArchiveFile
    {
        public string Source { get; set; }
        public string Name { get; set; }

        public OIVArchive Parent { get; set; }

        public OIVArchiveFile(OIVArchive parent)
        {
            Parent = parent;
        }

        public OIVArchiveFile(OIVArchive parent, string source, string name)
        {
            Parent = parent;
            Source = source;
            Name = name;
        }
    }
}
