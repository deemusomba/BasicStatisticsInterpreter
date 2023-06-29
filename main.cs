using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StatisticalInterpreter
{
    class Program
    {
        static List<string> columnNames;
        static List<List<string>> data;

        static void Main(string[] args)
        {
            columnNames = new List<string>();
            data = new List<List<string>>();

            bool exit = false;

            while (!exit)
            {
                Console.Write("$ ");
                string input = Console.ReadLine();
                string[] parts = input.Split(' ');
                string command = parts[0].ToLower();

                switch (command)
                {
                    case "load":
                        if (parts.Length == 2)
                        {
                            LoadFile(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'load' command requires a file name.");
                        }
                        break;

                    case "store":
                        if (parts.Length == 2)
                        {
                            StoreFile(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'store' command requires a file name.");
                        }
                        break;

                    case "clone":
                        if (parts.Length == 3)
                        {
                            CloneFile(parts[1], parts[2]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'clone' command requires two file names.");
                        }
                        break;

                    case "html":
                        if (parts.Length == 2)
                        {
                            ConvertToHTML(parts[1]);
                        }
                        else if (parts.Length == 3)
                        {
                            ConvertToHTML(parts[1], parts[2]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'html' command requires one or two file names.");
                        }
                        break;

                    case "min":
                        if (parts.Length == 1)
                        {
                            CalculateMinimum();
                        }
                        else if (parts.Length == 2)
                        {
                            CalculateMinimum(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'min' command requires zero or one column name.");
                        }
                        break;

                    case "max":
                        if (parts.Length == 1)
                        {
                            CalculateMaximum();
                        }
                        else if (parts.Length == 2)
                        {
                            CalculateMaximum(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'max' command requires zero or one column name.");
                        }
                        break;

                    case "median":
                        if (parts.Length == 1)
                        {
                            CalculateMedian();
                        }
                        else if (parts.Length == 2)
                        {
                            CalculateMedian(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'median' command requires zero or one column name.");
                        }
                        break;

                    case "mean":
                        if (parts.Length == 1)
                        {
                            CalculateMean();
                        }
                        else if (parts.Length == 2)
                        {
                            CalculateMean(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'mean' command requires zero or one column name.");
                        }
                        break;

                    case "variance":
                        if (parts.Length == 1)
                        {
                            CalculateVariance();
                        }
                        else if (parts.Length == 2)
                        {
                            CalculateVariance(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'variance' command requires zero or one column name.");
                        }
                        break;

                    case "stdv":
                        if (parts.Length == 1)
                        {
                            CalculateStandardDeviation();
                        }
                        else if (parts.Length == 2)
                        {
                            CalculateStandardDeviation(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'stdv' command requires zero or one column name.");
                        }
                        break;

                    case "add":
                        if (parts.Length == 3)
                        {
                            AddColumns(parts[1], parts[2]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'add' command requires two column names.");
                        }
                        break;

                    case "sub":
                        if (parts.Length == 3)
                        {
                            SubtractColumns(parts[1], parts[2]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'sub' command requires two column names.");
                        }
                        break;

                    case "corr":
                        if (parts.Length == 3)
                        {
                            CalculateCorrelation(parts[1], parts[2]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'corr' command requires two column names.");
                        }
                        break;

                    case "regression":
                        if (parts.Length == 2)
                        {
                            CalculateRegression(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'regression' command requires one column name.");
                        }
                        break;

                    case "show":
                        DisplayData();
                        break;

                    case "titles":
                        DisplayColumnNames();
                        break;

                    case "report":
                        GenerateReport();
                        break;

                    case "rows":
                        DisplayRowCount();
                        break;

                    case "columns":
                        DisplayColumnCount();
                        break;

                    case "vhisto":
                        if (parts.Length == 2)
                        {
                            DisplayVerticalHistogram(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'vhisto' command requires a column name.");
                        }
                        break;

                    case "hhisto":
                        if (parts.Length == 2)
                        {
                            DisplayHorizontalHistogram(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'hhisto' command requires a column name.");
                        }
                        break;

                    case "sort":
                        if (parts.Length == 2)
                        {
                            SortData(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'sort' command requires a column name.");
                        }
                        break;

                    case "help":
                        DisplayHelp();
                        break;

                    case "man":
                        if (parts.Length == 2)
                        {
                            DisplayManual(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'man' command requires a command name.");
                        }
                        break;

                    case "oddrows":
                        DisplayOddRows();
                        break;

                    case "evenrows":
                        DisplayEvenRows();
                        break;

                    case "primes":
                        if (parts.Length == 2)
                        {
                            DisplayPrimeNumbers(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'primes' command requires a column name.");
                        }
                        break;

                    case "delete":
                        if (parts.Length >= 2)
                        {
                            string deleteType = parts[1].ToLower();

                            if (deleteType == "occurrence" && parts.Length == 4)
                            {
                                DeleteOccurrence(parts[2], parts[3]);
                            }
                            else if (deleteType == "row" && parts.Length == 3)
                            {
                                DeleteRowByIndex(parts[2]);
                            }
                            else if (deleteType == "column" && parts.Length == 3)
                            {
                                DeleteColumn(parts[2]);
                            }
                            else
                            {
                                Console.WriteLine("Syntax error. 'delete' command is not properly formatted.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'delete' command requires additional parameters.");
                        }
                        break;

                    case "insert":
                        if (parts.Length >= 3)
                        {
                            InsertRow(parts.Skip(1).ToArray());
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'insert' command requires additional parameters.");
                        }
                        break;

                    case "replace":
                        if (parts.Length >= 3)
                        {
                            string replaceType = parts[1].ToLower();

                            if (replaceType == "occurrence" && parts.Length == 4)
                            {
                                ReplaceOccurrences(parts[2], parts[3]);
                            }
                            else if (replaceType == "column" && parts.Length == 4)
                            {
                                ReplaceColumnValues(parts[2], parts[3]);
                            }
                            else
                            {
                                Console.WriteLine("Syntax error. 'replace' command is not properly formatted.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Syntax error. 'replace' command requires additional parameters.");
                        }
                        break;

                    case "exit":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Unknown command. Type 'help' for a list of commands.");
                        break;
                }
            }
        }

        static void LoadFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);

                if (lines.Length >= 3)
                {
                    columnNames = lines[2].Split(',').ToList();
                    data = lines.Skip(3).Select(line => line.Split(',').ToList()).ToList();

                    Console.WriteLine($"{fileName} is loaded.");
                }
                else
                {
                    Console.WriteLine($"Error loading {fileName}. File format is invalid.");
                }
            }
            else
            {
                Console.WriteLine($"Error loading {fileName}. File does not exist.");
            }
        }

        static void StoreFile(string fileName)
        {
            if (columnNames.Count > 0 && data.Count > 0)
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine($"{fileName} {columnNames.Count} {data.Count}");

                    writer.WriteLine(string.Join(",", columnNames));

                    foreach (List<string> rowData in data)
                    {
                        writer.WriteLine(string.Join(",", rowData));
                    }
                }

                Console.WriteLine($"{fileName} is stored.");
            }
            else
            {
                Console.WriteLine($"Error storing {fileName}. No data loaded.");
            }
        }

        static void CloneFile(string sourceFileName, string targetFileName)
        {
            if (File.Exists(sourceFileName))
            {
                File.Copy(sourceFileName, targetFileName);

                Console.WriteLine($"{sourceFileName} is cloned to {targetFileName}.");
            }
            else
            {
                Console.WriteLine($"Error cloning {sourceFileName}. File does not exist.");
            }
        }

        static void ConvertToHTML(string fileName)
        {
            if (columnNames.Count > 0 && data.Count > 0)
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine("<html>");
                    writer.WriteLine("<head>");
                    writer.WriteLine("<style>");
                    writer.WriteLine("table { border-collapse: collapse; }");
                    writer.WriteLine("th, td { border: 1px solid black; padding: 5px; }");
                    writer.WriteLine("</style>");
                    writer.WriteLine("</head>");
                    writer.WriteLine("<body>");
                    writer.WriteLine("<table>");

                    writer.WriteLine("<tr>");
                    foreach (string columnName in columnNames)
                    {
                        writer.WriteLine($"<th>{columnName}</th>");
                    }
                    writer.WriteLine("</tr>");

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

                Console.WriteLine($"{fileName} is converted to HTML.");
            }
            else
            {
                Console.WriteLine($"Error converting {fileName} to HTML. No data loaded.");
            }
        }

        static void ConvertToHTML(string fileName, string columnName)
        {
            if (columnNames.Contains(columnName))
            {
                int columnIndex = columnNames.IndexOf(columnName);
                string htmlFileName = Path.GetFileNameWithoutExtension(fileName) + ".html";

                using (StreamWriter writer = new StreamWriter(htmlFileName))
                {
                    writer.WriteLine("<html>");
                    writer.WriteLine("<head>");
                    writer.WriteLine("<style>");
                    writer.WriteLine("table { border-collapse: collapse; }");
                    writer.WriteLine("th, td { border: 1px solid black; padding: 5px; }");
                    writer.WriteLine("</style>");
                    writer.WriteLine("</head>");
                    writer.WriteLine("<body>");
                    writer.WriteLine("<table>");

                    writer.WriteLine("<tr>");
                    writer.WriteLine($"<th>{columnName}</th>");
                    writer.WriteLine("</tr>");

                    foreach (List<string> rowData in data)
                    {
                        writer.WriteLine("<tr>");
                        writer.WriteLine($"<td>{rowData[columnIndex]}</td>");
                        writer.WriteLine("</tr>");
                    }

                    writer.WriteLine("</table>");
                    writer.WriteLine("</body>");
                    writer.WriteLine("</html>");
                }

                Console.WriteLine($"{columnName} from {fileName} is converted to HTML in {htmlFileName}.");
            }
            else
            {
                Console.WriteLine($"Error converting {columnName} to HTML. Column does not exist.");
            }
        }

        static void CalculateMinimum(string columnName = "")
        {
            if (columnNames.Count > 0 && data.Count > 0)
            {
                if (string.IsNullOrEmpty(columnName))
                {
                    List<double> minValues = new List<double>();

                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        double minValue = double.MaxValue;

                        foreach (List<string> rowData in data)
                        {
                            if (double.TryParse(rowData[i], out double value))
                            {
                                minValue = Math.Min(minValue, value);
                            }
                        }

                        minValues.Add(minValue);
                    }

                    Console.WriteLine("Minimum Values:");

                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        Console.WriteLine($"{columnNames[i]}: {minValues[i]}");
                    }
                }
                else
                {
                    if (columnNames.Contains(columnName))
                    {
                        int columnIndex = columnNames.IndexOf(columnName);
                        double minValue = double.MaxValue;

                        foreach (List<string> rowData in data)
                        {
                            if (double.TryParse(rowData[columnIndex], out double value))
                            {
                                minValue = Math.Min(minValue, value);
                            }
                        }

                        Console.WriteLine($"Minimum Value for {columnName}: {minValue}");
                    }
                    else
                    {
                        Console.WriteLine($"Error calculating minimum value. Column '{columnName}' does not exist.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error calculating minimum value. No data loaded.");
            }
        }

        static void CalculateMaximum(string columnName = "")
        {
            if (columnNames.Count > 0 && data.Count > 0)
            {
                if (string.IsNullOrEmpty(columnName))
                {
                    List<double> maxValues = new List<double>();

                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        double maxValue = double.MinValue;

                        foreach (List<string> rowData in data)
                        {
                            if (double.TryParse(rowData[i], out double value))
                            {
                                maxValue = Math.Max(maxValue, value);
                            }
                        }

                        maxValues.Add(maxValue);
                    }

                    Console.WriteLine("Maximum Values:");

                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        Console.WriteLine($"{columnNames[i]}: {maxValues[i]}");
                    }
                }
                else
                {
                    if (columnNames.Contains(columnName))
                    {
                        int columnIndex = columnNames.IndexOf(columnName);
                        double maxValue = double.MinValue;

                        foreach (List<string> rowData in data)
                        {
                            if (double.TryParse(rowData[columnIndex], out double value))
                            {
                                maxValue = Math.Max(maxValue, value);
                            }
                        }

                        Console.WriteLine($"Maximum Value for {columnName}: {maxValue}");
                    }
                    else
                    {
                        Console.WriteLine($"Error calculating maximum value. Column '{columnName}' does not exist.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error calculating maximum value. No data loaded.");
            }
        }

        static void CalculateMedian(string columnName = "")
        {
            if (columnNames.Count > 0 && data.Count > 0)
            {
                if (string.IsNullOrEmpty(columnName))
                {
                    List<double> medians = new List<double>();

                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        List<double> values = new List<double>();

                        foreach (List<string> rowData in data)
                        {
                            if (double.TryParse(rowData[i], out double value))
                            {
                                values.Add(value);
                            }
                        }

                        values.Sort();
                        double median = 0.0;

                        if (values.Count > 0)
                        {
                            int mid = values.Count / 2;

                            if (values.Count % 2 == 0)
                            {
                                median = (values[mid - 1] + values[mid]) / 2;
                            }
                            else
                            {
                                median = values[mid];
                            }
                        }

                        medians.Add(median);
                    }

                    Console.WriteLine("Median Values:");

                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        Console.WriteLine($"{columnNames[i]}: {medians[i]}");
                    }
                }
                else
                {
                    if (columnNames.Contains(columnName))
                    {
                        int columnIndex = columnNames.IndexOf(columnName);
                        List<double> values = new List<double>();

                        foreach (List<string> rowData in data)
                        {
                            if (double.TryParse(rowData[columnIndex], out double value))
                            {
                                values.Add(value);
                            }
                        }

                        values.Sort();
                        double median = 0.0;

                        if (values.Count > 0)
                        {
                            int mid = values.Count / 2;

                            if (values.Count % 2 == 0)
                            {
                                median = (values[mid - 1] + values[mid]) / 2;
                            }
                            else
                            {
                                median = values[mid];
                            }
                        }

                        Console.WriteLine($"Median Value for {columnName}: {median}");
                    }
                    else
                    {
                        Console.WriteLine($"Error calculating median value. Column '{columnName}' does not exist.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error calculating median value. No data loaded.");
            }
        }

        static void CalculateMean(string columnName = "")
        {
            if (columnNames.Count > 0 && data.Count > 0)
            {
                if (string.IsNullOrEmpty(columnName))
                {
                    List<double> means = new List<double>();

                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        List<double> values = new List<double>();

                        foreach (List<string> rowData in data)
                        {
                            if (double.TryParse(rowData[i], out double value))
                            {
                                values.Add(value);
                            }
                        }

                        double mean = 0.0;

                        if (values.Count > 0)
                        {
                            mean = values.Sum() / values.Count;
                        }

                        means.Add(mean);
                    }

                    Console.WriteLine("Mean Values:");

                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        Console.WriteLine($"{columnNames[i]}: {means[i]}");
                    }
                }
                else
                {
                    if (columnNames.Contains(columnName))
                    {
                        int columnIndex = columnNames.IndexOf(columnName);
                        List<double> values = new List<double>();

                        foreach (List<string> rowData in data)
                        {
                            if (double.TryParse(rowData[columnIndex], out double value))
                            {
                                values.Add(value);
                            }
                        }

                        double mean = 0.0;

                        if (values.Count > 0)
                        {
                            mean = values.Sum() / values.Count;
                        }

                        Console.WriteLine($"Mean Value for {columnName}: {mean}");
                    }
                    else
                    {
                        Console.WriteLine($"Error calculating mean value. Column '{columnName}' does not exist.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error calculating mean value. No data loaded.");
            }
        }

        static void CalculateStandardDeviation(string columnName = "")
        {
            if (columnNames.Count > 0 && data.Count > 0)
            {
                if (string.IsNullOrEmpty(columnName))
                {
                    List<double> standardDeviations = new List<double>();

                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        List<double> values = new List<double>();

                        foreach (List<string> rowData in data)
                        {
                            if (double.TryParse(rowData[i], out double value))
                            {
                                values.Add(value);
                            }
                        }

                        double mean = 0.0;

                        if (values.Count > 0)
                        {
                            mean = values.Sum() / values.Count;
                        }

                        double variance = 0.0;

                        foreach (double value in values)
                        {
                            variance += Math.Pow(value - mean, 2);
                        }

                        variance /= values.Count;

                        double standardDeviation = Math.Sqrt(variance);

                        standardDeviations.Add(standardDeviation);
                    }

                    Console.WriteLine("Standard Deviation Values:");

                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        Console.WriteLine($"{columnNames[i]}: {standardDeviations[i]}");
                    }
                }
                else
                {
                    if (columnNames.Contains(columnName))
                    {
                        int columnIndex = columnNames.IndexOf(columnName);
                        List<double> values = new List<double>();

                        foreach (List<string> rowData in data)
                        {
                            if (double.TryParse(rowData[columnIndex], out double value))
                            {
                                values.Add(value);
                            }
                        }

                        double mean = 0.0;

                        if (values.Count > 0)
                        {
                            mean = values.Sum() / values.Count;
                        }

                        double variance = 0.0;

                        foreach (double value in values)
                        {
                            variance += Math.Pow(value - mean, 2);
                        }

                        variance /= values.Count;

                        double standardDeviation = Math.Sqrt(variance);

                        Console.WriteLine($"Standard Deviation Value for {columnName}: {standardDeviation}");
                    }
                    else
                    {
                        Console.WriteLine($"Error calculating standard deviation value. Column '{columnName}' does not exist.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error calculating standard deviation value. No data loaded.");
            }
        }

        static void DeleteRow(string[] conditions)
        {
            List<List<string>> newData = new List<List<string>>();

            foreach (List<string> rowData in data)
            {
                bool match = true;

                foreach (string condition in conditions)
                {
                    string[] parts = condition.Split('=');

                    if (parts.Length == 2)
                    {
                        int columnIndex = columnNames.IndexOf(parts[0]);

                        if (columnIndex >= 0)
                        {
                            if (rowData[columnIndex] != parts[1])
                            {
                                match = false;
                                break;
                            }
                        }
                        else
                        {
                            match = false;
                            break;
                        }
                    }
                    else
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                {
                    newData.Add(rowData);
                }
            }

            int deletedRows = data.Count - newData.Count;
            data = newData;

            Console.WriteLine($"{deletedRows} row(s) deleted.");
        }

        static void InsertRow(string[] values)
        {
            if (values.Length == columnNames.Count)
            {
                data.Add(values.ToList());
                Console.WriteLine("Row inserted.");
            }
            else
            {
                Console.WriteLine("Error inserting row. Number of values does not match the number of columns.");
            }
        }

        static void ReplaceOccurrences(string oldValue, string newValue)
        {
            int replacements = 0;

            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Count; j++)
                {
                    if (data[i][j] == oldValue)
                    {
                        data[i][j] = newValue;
                        replacements++;
                    }
                }
            }

            Console.WriteLine($"{replacements} occurrence(s) replaced.");
        }

        static void ReplaceColumnValues(string columnName, string newValue)
        {
            if (columnNames.Contains(columnName))
            {
                int columnIndex = columnNames.IndexOf(columnName);

                for (int i = 0; i < data.Count; i++)
                {
                    data[i][columnIndex] = newValue;
                }

                Console.WriteLine($"All values in column '{columnName}' replaced with '{newValue}'.");
            }
            else
            {
                Console.WriteLine($"Error replacing column values. Column '{columnName}' does not exist.");
            }
        }
    }
}
