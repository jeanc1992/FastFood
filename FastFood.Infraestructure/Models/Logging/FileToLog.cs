using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Infrastructure.Models.Logging
{
    public class FileToLog
    {
        public string AssemblyFullName { get; }
        public string LogFileName { get; }
        public bool Filtered { get; }

        public FileToLog(Type type, string filename)
        {
            AssemblyFullName = type.FullName;
            LogFileName = filename;
            Filtered = true;
        }

        public FileToLog(string filename)
        {
            LogFileName = filename;
        }
    }
}
