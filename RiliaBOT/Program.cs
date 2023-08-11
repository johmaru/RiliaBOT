using System;
using System.IO;

namespace RiliaBOT
{
    internal class Program
    {
        public static void Main(string[] args)
        {
          AskExists_Toml();
        }

        private static void BinFile()
        {
            switch (Directory.Exists("./bin"))
            {
                case true :
                    Console.WriteLine("Log : Dir True");
                    break;
                case false:
                    Directory.CreateDirectory("./bin");
                    Console.WriteLine("Log : Dir False");
                    break;
            }
        }
        
        private static void AskExists_Toml()
        {
            switch (File.Exists("./Config.toml"))
            {
                case false:
                    using (FileStream fs = File.Create("./Config.toml"))
                    {
                    }
                    Console.WriteLine("Log : ConfigToml not Exists");
                    break;
                case true:
                    Console.WriteLine("Log : ConfigToml Exists");
                    break;
            }
        }
    }
}