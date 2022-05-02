using System;
using LINQtoCSV;
using System.IO;
using System.Collections.Generic;

namespace Derivco02
{
    public static class Program
    {
        static void Main(string[] args)
        {
            
            ReadCsvFile();

            Console.Write("\n-----------------------------------------------------------------------\n");

            string[] csvLine = File.ReadAllLines("Contacts.csv");

            var Boys = new List<string>();
            var Girls = new List<string>();

            for (int i = 1; i < csvLine.Length; i++)
            {
                string[] rowData = csvLine[i].Split(',');
                Boys.Add(rowData[0]);
                Girls.Add(rowData[1]);
            }

            Console.WriteLine("Boys:");
            for (int i = 0; i < Boys.Count; i++)
            {
                Console.WriteLine(Boys[i]);
            }

            Console.Write("\n-----------------------------------------------------------------------\n");

            Console.WriteLine("Girls:");
            for (int i = 0; i < Girls.Count; i++)
            {
                Console.WriteLine(Girls[i]);
            }
            Console.ReadKey();


           //alculateSimilarity();


        }


       

        private static void ReadCsvFile()
        {
            var csvFileDescription = new CsvFileDescription
            {
                FirstLineHasColumnNames = true,
                IgnoreUnknownColumns = true,
                SeparatorChar = ',',
                UseFieldIndexForReadingData = false
            };
            var csvContext = new CsvContext();
            var reads = csvContext.Read<ReadFrom>("Contacts.csv", csvFileDescription);

            foreach (var ReadFrom in reads)
            {
                Console.WriteLine($"{ReadFrom.Boys} | {ReadFrom.Girls} ");
            }           
        }




        public static int ComputeLevenshteinDistance( this string Boys, string Girls)
        {
            if (string.IsNullOrEmpty(Boys))
                return string.IsNullOrEmpty(Girls) ? 0 : Girls.Length;

            if (string.IsNullOrEmpty(Girls))
                return string.IsNullOrEmpty(Boys) ? 0 : Boys.Length;

            int BoysLength = Boys.Length;
            int GirlsLength = Girls.Length;

            int[,] distance = new int[BoysLength + 1, GirlsLength + 1];

            // Step 1
            for (int i = 0; i <= BoysLength; distance[i, 0] = i++) ;
            for (int j = 0; j <= GirlsLength; distance[0, j] = j++) ;

            for (int i = 1; i <= BoysLength; i++)
            {
                for (int j = 1; j <= GirlsLength; j++)
                {
                    // Step 2
                    int cost = (Girls[j - 1] == Boys[i - 1]) ? 0 : 1;

                    // Step 3
                    distance[i, j] = Math.Min(
                                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                                        distance[i - 1, j - 1] + cost);
                }
            }

            return distance[BoysLength, GirlsLength];
        }


        private static double CalculateSimilarity(this string Boys, string Girls)
        {
            if (string.IsNullOrEmpty(Boys))
                return string.IsNullOrEmpty(Girls) ? 1 : 0;

            if (string.IsNullOrEmpty(Girls))
                return string.IsNullOrEmpty(Boys) ? 1 : 0;

            double stepsToSame = ComputeLevenshteinDistance(Boys, Girls);
            return (1.0 - (stepsToSame / (double)Math.Max(Boys.Length, Girls.Length)));



           
        }

    }
    }

