using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess
{
    public class CSVParser
    {
        public CSVParser(string filePath) 
        {
            var lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {

            }

        }

    }
}
