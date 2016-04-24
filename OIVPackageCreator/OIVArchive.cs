using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OIVPackageCreator
{
    public class OIVArchive
    {
        public RageArchiveType Version { get; set; }

        public List<OIVArchiveFile> SourceFiles { get; set; } = new List<OIVArchiveFile>();

        public List<OIVArchive> NestedArchives { get; set; } = new List<OIVArchive>();

        public string Path { get; set; }

        public bool CreateIfNotExist { get; set; }

        public OIVArchive()
        {
        }

        public OIVArchive(RageArchiveType version, List<OIVArchiveFile> sourceFiles, string path, bool create)
        {
            Version = version;
            SourceFiles = sourceFiles;
            Path = path;
            CreateIfNotExist = create;
        }

        public void Add(string sourcePath, string name)
        {
            SourceFiles.Add(new OIVArchiveFile(this, sourcePath, name));
        }


    }

    public enum RageArchiveType
    {
        IMG3, // GTAIV
        RPF2, // GTAIV
        RPF3, // GTAIV
        RPF4, // Payne
        RPF7 // GTAV
    }
}
