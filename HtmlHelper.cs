using System.Collections.Generic;
using System.IO;

namespace StatisticalInterpreter
{
    public static class HtmlHelper
    {
        public static void GenerateHtmlTable(List<List<string>> data, List<string> columnNames, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("<html>");
                writer.WriteLine("<body>");
                writer.WriteLine("<table>");

                // Write column names
                writer.WriteLine("<tr>");
                foreach (string columnName in columnNames)
                {
                    writer.WriteLine($"<th>{columnName}</th>");
                }
                writer.WriteLine("</tr>");

                // Write data rows
                foreach (List<string> rowData in data)
                {
                    writer.WriteLine("<tr>");
                    foreach (string value in rowData)
                    {
                        writer.WriteLine($"<td>{value}</td>");
                    }
                    writer.WriteLine("</tr>");
                }

                writer.WriteLine("</table>");
                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
            }
        }
    }
}
