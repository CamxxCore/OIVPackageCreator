using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace OIVPackageCreator
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
    }
}
