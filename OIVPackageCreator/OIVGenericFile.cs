using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIVPackageCreator
{
    public class OIVGenericFile
    {
        public string Source { get; set; }
        public string Destination { get; set; }

        public OIVGenericFile()
        {
        }

        public OIVGenericFile(string source, string destination)
        {
            Source = source;
            Destination = destination;
        }
    }
}
