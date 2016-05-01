namespace OIVPackageEditor
{
    public class OIVArchiveFile
    {
        public string Source { get; set; }
        public string Name { get; set; }

        public string FullPath
        {
            get
            {
                return string.Format("{0}\\{1}", Parent.Path, Name);
            }
        }

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
