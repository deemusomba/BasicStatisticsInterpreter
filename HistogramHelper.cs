using System;
using System.Collections.Generic;

namespace StatisticalInterpreter
{
    public static class HistogramHelper
    {
        public static void GenerateVerticalHistogram(List<double> values, string columnName)
        {
            Console.WriteLine($"Histogram for column '{columnName}':");

            int maxFrequency = (int)Math.Ceiling(values.Count / 10.0);

            for (int i = maxFrequency; i > 0; i--)
            {
                Console.Write($"{i * 10,3} | ");

                foreach (double value in values)
                {
                    int frequency = (int)Math.Floor(value / 10.0);

                    if (frequency >= i)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("    +-----------------------------------------------------");
            Console.WriteLine("      0   10  20  30  40  50  60  70  80  90  100");
        }

        public static void GenerateHorizontalHistogram(List<double> values, string columnName)
        {
            Console.WriteLine($"Histogram for column '{columnName}':");

            int maxFrequency = (int)Math.Ceiling(values.Count / 10.0);

            for (int i = maxFrequency; i > 0; i--)
            {
                Console.Write($"{i * 10,3} | ");

                foreach (double value in values)
                {
                    int frequency = (int)Math.Floor(value / 10.0);

                    if (frequency >= i)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("    +-----------------------------------------------------");
            Console.WriteLine("      0   10  20  30  40  50  60  70  80  90  100");
        }
    }
}
