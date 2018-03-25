using System;
using System.Collections.Generic;
using System.IO;

namespace MouseHeatmap.Collector
{
    public class DirectoryFinder
    {
        public DirectoryInfo Find(string name)
        {
            var currentDirectory = new DirectoryInfo(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            while (currentDirectory.Name != name)
            {
                currentDirectory = currentDirectory.Parent;
            }

            return currentDirectory;
        }
    }
}
