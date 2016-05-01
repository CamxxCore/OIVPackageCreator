using System.Collections.Generic;

namespace OIVPackageEditor
{
    public static class Util
    {
        public static IEnumerable<OIVArchive> GetNodes(this OIVArchive a)
        {
            if (a == null)
                yield break;

            yield return a;
            foreach (var n in a.NestedArchives)
            {
                foreach (var innerN in GetNodes(n))
                {
                    yield return innerN;
                }
            }
        }

        public static IEnumerable<OIVArchiveFile> GetNestedFiles(this OIVArchive a)
        {
            foreach (var file in a.SourceFiles)
                yield return file;

            foreach (var n in a.NestedArchives)
            {
                foreach (var innerN in GetNodes(a))
                {
                    foreach (var file in innerN.SourceFiles)
                        yield return file;
                }
            }
        }
    }
}
