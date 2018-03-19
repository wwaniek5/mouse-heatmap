
namespace MouseHeatmap.Collector
{
    public class SQLiteConnectionString
    {
        private string connectionString;

        public SQLiteConnectionString(string path)
        {
            //todo validation
            connectionString = $@"Data Source={path};";
        }

        public override string ToString()
        {
            return connectionString;
        }
    }
}