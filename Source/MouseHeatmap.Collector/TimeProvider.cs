using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseHeatmap.Collector
{
    public interface ITimeProvider
    {
        long Now();
    }

    public class TimeProvider : ITimeProvider
    {
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.Winapi)]
        private static extern void GetSystemTimePreciseAsFileTime(out long filetime);

        public long Now()
        {
            GetSystemTimePreciseAsFileTime(out long preciseNow);
            return preciseNow;
        }
    }


}
