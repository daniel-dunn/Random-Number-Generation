using System;
using CsvHelper;
using System.Collections;
using System.IO;
using System.Data;

namespace RNG
{
    class Program
    {
        static void Main(string[] args)
        {

            /*for (int i = 0; i < 9; i++)
            {
                Cenario();
            }
            */
            ArrayList testSet = new ArrayList();

            Random r = new Random();

            for (int i = 0; i < 10; i++)
            {
                testSet.Add(r.Next(1, 10));
            }
            WriteToCSV(testSet);

        }  
        public static int DigitizeRoot(int input)
        {
            if (input >= 10)
            {
                input = 1 +(input % 10);
                DigitizeRoot(input);
            }
            return input;
        }
        public static ArrayList RNGL2(ArrayList input)
        {
            ArrayList output = new ArrayList();

            foreach (int i in input)
            {
                Random s = new Random();
                if (s.Next(1, 10) > i)
                {
                  output.Add(DigitizeRoot(i + s.Next(1, 10)));
                }
                else
                {
                  output.Add(i);
                }
            }
            return output;
        }
        public static void PrintArrayList(ArrayList input)
        {
            foreach(int i in input)
            {
                Console.Write(i +" ");
            }
            Console.WriteLine();
        }
        public static void Scenario()
        {
            Random randNum = new Random();
            ArrayList dataset = new ArrayList();
            for (int i = 0; i < 9; i++)
            {
                dataset.Add(randNum.Next(1, 10));
            }
            Console.WriteLine("dataset");
            PrintArrayList(dataset);
            Console.WriteLine("dataset_modified");
            PrintArrayList(RNGL2(dataset));
        }
        public static void AddCSV(ArrayList Number, string filepath)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
                {
                    file.Write(Number);
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Oopsie", ex);
            }
        }
    
        public async static void WriteToCSV(ArrayList printArray)
        {

            String filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            using StreamWriter streamWriter = new StreamWriter(Path.Combine(filePath,"RNGOutput.csv"));
            foreach (int i in printArray)
            {
               await streamWriter.WriteAsync(i + ",");
            }
        }  
    }
}