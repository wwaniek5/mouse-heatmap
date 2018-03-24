using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseHeatmap.Collector
{
    public class ScreenUnit
    {
        public int ScreenUnitId { get; set; }
        public int Y { get; set; }
        public int X { get; set; }
        public long MousePassedCount { get; set; }
        public long MouseFinishedCount { get;  set; }
        public long SpeedCount { get;  set; }
    }
}
