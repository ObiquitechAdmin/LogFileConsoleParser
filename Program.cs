using System;
using System.Collections.Generic;

namespace RegexFind
{
    internal class Program
    {
        private string logFilePath = @"..\..\Release51_Log1.txt";
        //ReadAndPrintLogFile();

        static void Main(string[] args)
        {
            ConsoleColor mainForegroundColour = ConsoleColor.Green;
            Console.ForegroundColor = mainForegroundColour;

            LogFileProcessor processor = new LogFileProcessor();
            string userInputLogFilePath = processor.GetLogFilePathFromUser();
            List<string> logLines = processor.ReadAndStoreLogLines(userInputLogFilePath);
            processor.ProcessLogFile(logLines);

            Console.ForegroundColor = ConsoleColor.Yellow;
            WaitForEscapeKey();
        }

        static void WaitForEscapeKey()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPress the 'Escape' key to exit the program...");

            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
            } while (keyInfo.Key != ConsoleKey.Escape);
        }

    }
}