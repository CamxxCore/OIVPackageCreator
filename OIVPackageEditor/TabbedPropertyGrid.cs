using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OIVPackageEditor
{
    public class TabbedPropertyGrid : PropertyGrid
    {
        public TabbedPropertyGrid() : base() { }
        public void SetParent(Form form)
        {
            // Catch null arguments
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            // Set this property to intercept all events
            form.KeyPreview = true;

            // Listen for keydown event
            form.KeyDown += new KeyEventHandler(this.Form_KeyDown);
        }
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            // Exit if cursor not in control
            if (!this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
            {
                return;
            }

            // Handle tab key
            if (e.KeyCode != Keys.Tab) { return; }
            e.Handled = true;
            e.SuppressKeyPress = true;

            // Get selected griditem
            GridItem gridItem = this.SelectedGridItem;
            if (gridItem == null) { return; }

            // Create a collection all visible child griditems in propertygrid
            GridItem root = gridItem;
            while (root.GridItemType != GridItemType.Root)
            {
                root = root.Parent;
            }
            List<GridItem> gridItems = new List<GridItem>();
            this.FindItems(root, gridItems);

            // Get position of selected griditem in collection
            int index = gridItems.IndexOf(gridItem);

            ++index;
            index %= gridItems.Count;
            this.SelectedGridItem = gridItems[index];
        }
        private void FindItems(GridItem item, List<GridItem> gridItems)
        {
            switch (item.GridItemType)
            {
                case GridItemType.Root:
                case GridItemType.Category:
                    foreach (GridItem i in item.GridItems)
                    {
                        this.FindItems(i, gridItems);
                    }
                    break;
                case GridItemType.Property:
                    gridItems.Add(item);
                    if (item.Expanded)
                    {
                        foreach (GridItem i in item.GridItems)
                        {
                            this.FindItems(i, gridItems);
                        }
                    }
                    break;
                case GridItemType.ArrayValue:
                    break;
            }
        }
    }
}
