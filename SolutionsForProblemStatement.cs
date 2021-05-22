using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace DempProgram
{
    class Program
    {

        
        static void Main(string[] args)
        {
            //Palindrome function
            Console.WriteLine("Enter string to check for palindrome:");
            string inputString = Console.ReadLine();
            bool checkPalindrome = palindrome(inputString);
            if (checkPalindrome)
            {
                Console.WriteLine("Given string is a Palindrom!");
            }
            else
            {
                Console.WriteLine("Given string is not a Palindrom!!");
            }

            //GroupByOwners function
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("Input.txt", "Randy");
            dictionary.Add("Code.py", "Stan");
            dictionary.Add("Output.txt", "Randy");
            Dictionary<string, List<string>> result = groupByOwners(dictionary);
            foreach (KeyValuePair<string, List<string>> dict in result)
            {
                string separator = ", ";
                Console.WriteLine("{0}:{1}", dict.Key, String.Join(separator, dict.Value));
            }

            //ParseLogfile function
            parseLogFile();

            //ChangeDirectory
            Console.WriteLine("Enter new path:");
            string newPath = Console.ReadLine();
            Console.WriteLine("Enter current path:");
            string currentPath = Console.ReadLine();
            changeDirectory(newPath, currentPath);


        }


        /*
         * Write a function that checks if a given word is a palindrome. Character case should be ignored.
        */
        static bool palindrome(string inputString)
        {
            try
            {
                bool isPalindrome = false;
                string reverseString = new string(inputString.ToCharArray().Reverse().ToArray());
                if (String.Equals(inputString, reverseString))
                {
                    isPalindrome = true;
                }
                return isPalindrome;
            }
            catch
            {
                Console.WriteLine("Something went wrong in palindrome !!");
                return false;
            }
        }


        /*Implement a group_by_owners function that:
            •         Accepts a dictionary containing the file owner name for each file name.
            •         Returns a dictionary containing a list of file names for each owner name, in any order.
            For example, for dictionary {'Input.txt': 'Randy', 'Code.py': 'Stan', 'Output.txt': 'Randy'} the group_by_owners function should return {'Randy': ['Input.txt', 'Output.txt'], 'Stan': ['Code.py']}.
        */

        static Dictionary<string, List<string>> groupByOwners(Dictionary<string, string> fileOwners)
        {
            Dictionary<string, List<string>> ownersDictionary = new Dictionary<string, List<string>>();
            try
            {
                foreach (var item in fileOwners)
                {
                    string fileOwnerName = item.Value;
                    string fileName = item.Key;
                    if (!ownersDictionary.ContainsKey(fileOwnerName))
                    {
                        List<string> fileNamesOfOwner = new List<string>();
                        fileNamesOfOwner.Add(fileName);
                        ownersDictionary.Add(fileOwnerName, fileNamesOfOwner);
                    }
                    else
                    {
                        ownersDictionary[fileOwnerName].Add(fileName);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong in groupByOwners !!");
            }
            return ownersDictionary;


        }

        /*Write a function to parse a log file & extract details of Errors & Warnings recorded into a separate file.*/
         
        static void parseLogFile()
        {
            
            StringBuilder text = new StringBuilder();
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = path.Remove(0, 6);
            path = path.ToLowerInvariant();
            if (path.Contains("bin"))
            {
                path = path.Substring(0, path.LastIndexOf("bin"));
            }
            
            string inputLogFilePath = path + @"Files\LogFile.log";
            string outPutLogFilePath = path + @"Files\TextFile1.log";
            try
            {
                string[] lines = File.ReadAllLines(inputLogFilePath);
                foreach (string line in lines)
                {
                    if (line.ToUpper().Contains("WARNING") || line.ToUpper().Contains("ERROR"))
                    {
                        text.AppendLine(line);
                    }
                }



                using (StreamWriter writer = new StreamWriter(outPutLogFilePath))
                {
                    writer.Write(text.ToString());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong parseLogFile!!");
            }
        }


        /* Write a function that provides a change directory (cd) function for an abstract file system.

               Notes:
               Root path is '/'.
               Path separator is '/'.
               Parent directory is addressable as '..'.
               Directory names consist only of English alphabet letters (A-Z and a-z).
               The function should support both relative and absolute paths.
               The function will not be passed any invalid paths.
               Do not use built-in path-related functions.

               For example:
               path = Path('/a/b/c/d')
               path.cd('../x')
               print(path.current_path)

               Output:
               Should display '/a/b/c/x'.

        */
        static void changeDirectory(string newPath, string currentPath)
        {
            try {
                if (newPath == "/")
                {
                    currentPath = "/";
                    return;
                }

                while (newPath.Length > 0)
                {
                    if (newPath.Length > 1)
                    {
                        if (newPath.Substring(0, 2) == "..")
                        {
                            if (!String.IsNullOrEmpty(currentPath))
                            {
                                currentPath = currentPath.Remove(currentPath.LastIndexOf("/", StringComparison.Ordinal));
                                if (String.IsNullOrEmpty(currentPath))
                                {
                                    currentPath = "/";
                                }
                            }

                            newPath = newPath.Remove(0, 2);
                            continue;
                        }
                    }

                    if (newPath[0] == '/')
                    {
                        newPath = newPath.Remove(0, 1);
                        if (newPath[0] == '.')
                        {
                            continue;
                        }
                    }

                    if (currentPath.Last() != '/')
                    {
                        currentPath += "/";
                    }

                    var nextPath = newPath.IndexOf("/", StringComparison.Ordinal);
                    if (nextPath == -1)
                    {
                        currentPath += newPath;
                        newPath = "";
                    }
                    else
                    {
                        currentPath += newPath.Substring(0, nextPath);
                        newPath = newPath.Remove(0, nextPath);
                    }
                }
                Console.WriteLine(currentPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong changeDirectory!!");
            }
            }
        

    }
}
