using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace OIVPackageEditor
{
    public static class Util
    {
        public static GridItemCollection GetAllGridEntries(this PropertyGrid grid)
        {
            if (grid == null)
                throw new ArgumentNullException("grid");

            object view = grid.GetType().GetField("gridView", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(grid);
            return (GridItemCollection)view.GetType().InvokeMember("GetAllGridEntries", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, view, null);
        }

        public static IEnumerable<OIVArchive> GetNodes(this OIVArchive a)
        {
            if (a == null)
            {
                yield break;
            }
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
