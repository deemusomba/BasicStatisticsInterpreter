using System.Collections.Generic;
using System.IO;

namespace StatisticalInterpreter
{
    public static class FileHelper
    {
        public static List<List<string>> ReadCSV(string filePath)
        {
            List<List<string>> data = new List<List<string>>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    data.Add(new List<string>(values));
                }
            }

            return data;
        }

        public static void WriteCSV(List<List<string>> data, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (List<string> rowData in data)
                {
                    writer.WriteLine(string.Join(",", rowData));
                }
            }
        }
    }
}
