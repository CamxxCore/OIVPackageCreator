namespace OIVPackageEditor
{
    public class OIVGenericFile
    {
        public string Source { get; set; }
        public string Destination { get; set; }

        /// <summary>
        /// File name 
        /// </summary>
        public string Name { get { return Source.Substring(Source.LastIndexOf('\\') + 1); } }

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
