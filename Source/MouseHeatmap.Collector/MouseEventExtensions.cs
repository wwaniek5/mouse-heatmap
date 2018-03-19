using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseHeatmap.Collector
{
    public static class MouseEventExtensions
    {
        public static int NumberOfPixelsInScreenBlock = 10;

        public static Point ToScreenBlock(this MouseEventArgs mouseEvent)
        {
            return new Point(mouseEvent.X / NumberOfPixelsInScreenBlock, mouseEvent.Y / NumberOfPixelsInScreenBlock);
         }
    }
}
