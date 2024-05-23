using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexFind
{
    public class LogFileProcessor
    {
        public string GetLogFilePathFromUser()
        {
            StringBuilder userInputFilePath = new StringBuilder();
            ConsoleKeyInfo keyInfo;
            string absoluteFilePath = string.Empty;

            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Please enter the log file path (or press 'Escape' to exit): ");
                    Console.ForegroundColor = ConsoleColor.Gray; // Set the color for user input

                    do
                    {
                        keyInfo = Console.ReadKey(false);

                        // If the user presses the 'Escape' key, exit the program
                        if (keyInfo.Key == ConsoleKey.Escape)
                        {
                            Environment.Exit(0);
                        }

                        // If the user presses the 'Enter' key, check if the file path is valid
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            absoluteFilePath = Path.GetFullPath(userInputFilePath.ToString());
                            if (!File.Exists(absoluteFilePath))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nThe file path is not valid. Please try again.");
                                userInputFilePath.Clear();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                            }
                            else
                            {
                                Console.WriteLine();
                                return absoluteFilePath;
                            }
                        }
                        else
                        {
                            userInputFilePath.Append(keyInfo.KeyChar);
                        }
                    }
                    while (true);
                }
                catch (PathTooLongException ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("\nA path too long error occurred: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                catch (IOException ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("\nAn I/O error occurred: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                catch (SecurityException ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("\nA security error occurred: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("\nAn argument error occurred: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("\nAn unauthorized access error occurred: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("\nAn unexpected error occurred: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    return string.Empty;
                }
                finally
                {
                    userInputFilePath.Clear(); // Clear the StringBuilder
                    Console.ResetColor(); // Reset the color to the default
                }
            }
        }

        public void ReadAndPrintLogFile(string logFilePath_p)
        {
            string line;

            try
            {
                using (StreamReader sr = new StreamReader(logFilePath_p))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("\nAn error occurred: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        public List<string> ReadAndStoreLogLines(string logFilePath_p)
        {
            string line;

            List<string> logLines = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(logFilePath_p))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        logLines.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("\nAn error occurred: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }

            return logLines;
        }

        public void ProcessLogFile(List<string> logFileLines)
        {
            try
            {
                List<string> truncatedWords = new List<string>();

                foreach (string logLine in logFileLines)
                {
                    // Find all alphabet letters
                    MatchCollection matches = Regex.Matches(logLine, @"[a-zA-Z]");

                    if (matches.Count >= 2)
                    {
                        // Find the index of the second alphabet letter
                        int index = matches[1].Index;

                        // Truncate the characters up until the second alphabet letter
                        string truncatedLine = logLine.Substring(index);

                        // Find the next colon character and truncate the rest of the string
                        int colonIndex = truncatedLine.IndexOf(':');
                        if (colonIndex != -1)
                        {
                            string furtherTruncatedLine = truncatedLine.Substring(0, colonIndex);

                            // Store the further truncated line in the list
                            truncatedWords.Add(furtherTruncatedLine);
                        }
                    }
                }

                // Print each item stored in the list
                foreach (string item in truncatedWords)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(item);
                }

                // Print the count of items in the list of truncated words
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nNumber of lines in truncatedWords: ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(truncatedWords.Count);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("\nAn error occurred: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        public List<string> ProcessLogFile(List<string> logFileLines, Int16 anyNum)
        {
            try
            {
                List<string> truncatedWords = new List<string>();

                foreach (string logLine in logFileLines)
                {
                    // Find all alphabet letters
                    MatchCollection matches = Regex.Matches(logLine, @"[a-zA-Z]");

                    if (matches.Count >= 2)
                    {
                        // Find the index of the second alphabet letter
                        int index = matches[1].Index;

                        // Truncate the characters up until the second alphabet letter
                        string truncatedLine = logLine.Substring(index);

                        // Find the next colon character and truncate the rest of the string
                        int colonIndex = truncatedLine.IndexOf(':');
                        if (colonIndex != -1)
                        {
                            string furtherTruncatedLine = truncatedLine.Substring(0, colonIndex);

                            // Store the further truncated line in the list
                            truncatedWords.Add(furtherTruncatedLine);
                        }
                    }
                }

                // Print the count of items in the list of truncated words
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nNumber of lines in truncatedWords: ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(truncatedWords.Count);

                return truncatedWords;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("\nAn error occurred: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                return new List<string>(); // Return an empty list in case of an error
            }
            finally
            {
                Console.ResetColor();
            }
        }

        public void PrintWithColourBaseOnWarning(List<string> itemsToColour_p)
        {
            try
            {
                List<string> uniqueItems = GetUniqueItems(itemsToColour_p);
                // create a List<string> of random hex numbers using CountUniqueItems(itemsToColour_p) as the number of items
                List<string> randomHexNumbers = new List<string>(uniqueItems.Count);

                // for loop up to the count of randomHexNumbers
                for (int i = 0; i < randomHexNumbers.Capacity; i++)
                {
                    // add each 'hex' string (the result of GetRandomHexNumber(6)) to the list of random hex numbers
                    randomHexNumbers.Add(GetRandomHexNumber(6).ToString());
                    Console.WriteLine($"{uniqueItems[i].Pastel(randomHexNumbers[i])}");
                }

                //Console.WriteLine("CpuPowerCalculator".Pastel(Color.FromArgb(206, 74, 152)));
                //Console.WriteLine($"Warning: {itemsToColour_p[0].Pastel(Color.FromArgb(206, 74, 152))}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("\nAn error occurred: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        public int CountUniqueItems(List<string> originalList_p)
        {
            HashSet<string> uniqueItems = new HashSet<string>(originalList_p);
            return uniqueItems.Count;
        }

        public List<string> GetUniqueItems(List<string> originalList_p)
        {
            HashSet<string> uniqueItems = new HashSet<string>(originalList_p);
            return new List<string>(uniqueItems);
        }

        static Random random = new Random();
        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }

    }

}