using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseHeatmap.Collector
{
    public class SQLiteConnectionString
    {
        private string connectionString;

        public SQLiteConnectionString(string path)
        {
            connectionString = $@"Data Source={path};";
        }

        public override string ToString()
        {
            return connectionString;
        }
    }
}