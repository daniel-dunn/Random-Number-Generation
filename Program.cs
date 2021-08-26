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

            ArrayList testSet = new ArrayList(); //This will be the benchmark random set of numbers;

            Random r = new Random();

            for (int i = 0; i < 10000; i++)

            {
                testSet.Add(r.Next(1, 10));
            }

            ArrayList modifiedSet = RNGL2(testSet);


           
            WriteToCSV(testSet);
            WriteToCSV(modifiedSet);

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
                int outputInt = i;
                if (s.Next(1, 10) > i)
                {
                  outputInt = DigitizeRoot(i + s.Next(1, 10));
                }
                
                output.Add(outputInt);
                
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
    
        public static void WriteToCSV(ArrayList printArray)
        {

            String desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            String fullFilePath = Path.Combine(desktopPath, "RNGOutput.csv");

            if (!File.Exists(fullFilePath))
            {
                Console.WriteLine("CSV Doesn't exist...creating file");
                using StreamWriter streamWriter = new StreamWriter(fullFilePath);

                Console.WriteLine("File created");

                int printCounter = 0;
                foreach (int i in printArray)
                {
                    Console.WriteLine("Print Counter: " + printCounter++);
                    streamWriter.Write(i + ",");
                }

            }
            else
            {
                Console.WriteLine("CSV exists - appending...");
                try
                {

                    using StreamWriter streamWriter = File.AppendText(fullFilePath);
                    streamWriter.AutoFlush = true;
                    streamWriter.WriteLine();
                    int printCounter = 0;
                    foreach (int i in printArray)
                    {
                        streamWriter.Write(i + ",");
                        Console.WriteLine("Print Counter: " + printCounter++);

                    }

                }

                catch { }

            }
        }  
    }
}