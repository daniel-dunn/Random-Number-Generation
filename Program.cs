using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace RNG
{
    class Program
    {
        public static int dataAmount;
        static void Main(string[] args)
        {
            dataAmount = 0;
            ArrayList testSet = new ArrayList(); //This will be the benchmark random set of numbers;

            Random r = new Random();

            Console.WriteLine("Please enter amount of data you'd like to generate: ");

            dataAmount = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < dataAmount; i++)
            {
                int randomProduct = r.Next(1, 10) * r.Next(1, 10);
                int digitizedProduct = DigitizeRoot(randomProduct);
                if(digitizedProduct > 9)
                {
                    Console.WriteLine("Digitized Prodct = " + digitizedProduct);
                    Console.WriteLine("Random Product = " + randomProduct);
                }
                int data = digitizedProduct;
                testSet.Add(data);
            }

            ArrayList modifiedSet = RNGL2(testSet);



            /*
             * WriteToCSV(testSet);
             * WriteToCSV(modifiedSet);
            */

            Console.WriteLine("Analysis of Dataset:");
            PrintDictionary(AnalyseFrequency(testSet));
            Console.WriteLine();
            Console.WriteLine("Analysis of Modified Dataset");
            PrintDictionary(AnalyseFrequency(modifiedSet));

        }  
        public static int DigitizeRoot(int input)
        {

            
            while (input > 9)
            {
                input = 1 + (input % 10);
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
                //Console.WriteLine("CSV Doesn't exist...creating file");
                using StreamWriter streamWriter = new StreamWriter(fullFilePath);

                Console.WriteLine("File created");

                int printCounter = 0;
                foreach (int i in printArray)
                {
                    //Console.WriteLine("Print Counter: " + printCounter++);
                    streamWriter.Write(i + ",");
                }

            }
            else
            {
                //Console.WriteLine("CSV exists - appending...");
                try
                {

                    using StreamWriter streamWriter = File.AppendText(fullFilePath);
                    streamWriter.AutoFlush = true;
                    streamWriter.WriteLine();
                    int printCounter = 0;
                    foreach (int i in printArray)
                    {
                        streamWriter.Write(i + ",");
                        //Console.WriteLine("Print Counter: " + printCounter++);

                    }

                }

                catch { }

            }
        }  

        public static SortedDictionary<int,int> AnalyseFrequency(ArrayList input)
        {
            //instanciation and enumeration of digit-frequncy map <key,value> -> <digit,count>
            SortedDictionary<int, int> digitFrequencyMap = new SortedDictionary<int, int>();
            for(int i = 1; i<10; i++)
            {
                digitFrequencyMap.Add(i, 0);
            }

            foreach(int i in input)
            {
                digitFrequencyMap[i] = ++digitFrequencyMap[i];
            }

            return digitFrequencyMap;
        }

        public static void PrintDictionary(SortedDictionary<int,int> dictionary)
        {
            foreach(KeyValuePair<int,int> kvp in dictionary)
            {
                double percentage = (kvp.Value * 100) / dataAmount;
                
                Console.WriteLine("<" + kvp.Key + "," + kvp.Value + "," + Math.Round(percentage,2).ToString() + "%>");
            }
        }
    }
}