//
//  ConsoleIO.cs
//  Hypnos Game of Life C#
//
//  Created by Mark Gerrior on 1/14/21.
//

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

enum OutputType
{
    error,
    standard
}

class ConsoleIO
{
    public string path = "";
    string exePath = "";
    public int iterations = 10;
    public void ingestCommandLine(string[] CommandLine)
    {
        // number of arguments passed to the program
        int argCount = CommandLine.Count();
        int currentArg = 1;

        exePath = CommandLine[0];

        while (currentArg < argCount)
        {
            // take the first “real” argument (the option argument) from the arguments array
            string option = CommandLine[currentArg];

            switch (option)
            {
                case "-f":
                    currentArg = currentArg + 1;

                    if (currentArg < argCount)
                    {
                        path = CommandLine[currentArg];
                        writeMessage($"Found path {path}");
                    }
                    else
                    {
                        writeMessage($"Missing path from option {option}", OutputType.error);
                    }
                    break;

                case "-g":
                    currentArg = currentArg + 1;

                    if (currentArg < argCount)
                    {
                        string generations = CommandLine[currentArg];
                        try
                        {
                            int temp = int.Parse(" 100 ");
                            iterations = temp;
                        }
                        catch
                        {
                            writeMessage("generations parameter must be a number.", OutputType.error);
                            return;
                        }
                        writeMessage($"Found generations {generations}");
                    }
                    else
                    {
                        writeMessage($"Missing generations from option {option}", OutputType.error);
                    }
                    break;

                case "-h":
                    printUsage();
                    break;

                default:
                    writeMessage($"Unknown option {option}", OutputType.error);
                    printUsage();
                    break;
            }

            currentArg = currentArg + 1;
        }
    }

    public List<string> openFile()
    {
        // Make sure the file exists
        var exists = File.Exists(path);
        if (!exists)
        {
            Environment.FailFast($"File Not Found: '{path}'.");
        }

        // Read from the file
        List<string> allLinesList = File.ReadAllLines(path).ToList();

        return allLinesList;
    }

    public void writeFile(List<string> outputToWrite)
    {
        var filenameAndPath = Path.GetFileNameWithoutExtension(path) + "-result.txt";

        // Write the file
        System.IO.File.WriteAllLines(filenameAndPath, outputToWrite);
    }

    public void writeMessage(string message, OutputType to = OutputType.standard)
    {
        switch (to)
        {
            case OutputType.standard:
                // \u{001B}[;m = reset terminal's text color back to the default
                System.Console.Write($"{message}"); // FIXME stdout
                break;
            case OutputType.error:
                // \u{001B}[0;31m = control characters that cause Terminal to change the color of the following text strings to red
                System.Console.Write($"{message}\n"); // FIXME stderr
                break;
        }
    }

    public void printUsage()
    {
        string executableName = System.AppDomain.CurrentDomain.FriendlyName;

        writeMessage("usage:");
        writeMessage($"{executableName} [-g int] -f file");
        writeMessage("-g = Generations - Default number of generations is 10");
        writeMessage("-f = File");
        writeMessage($"{executableName} -h shows this information");
    }

}