//
//  ConsoleIO.cs
//  Hypnos Game of Life C#
//
//  Created by Mark Gerrior on 1/14/21.
//

using System.Linq;

enum OutputType
{
    error,
    standard
}

class ConsoleIO
{
    string path = "";
    int iterations = 10;
    public void ingestCommandLine(string[] CommandLine)
    {
        // number of arguments passed to the program
        int argCount = CommandLine.Count();
        int currentArg = 1;

        while (currentArg < argCount)
        {

            // take the first “real” argument (the option argument) from the arguments array
            string argument = CommandLine[currentArg];

            // skip the first character in the argument's string (the hyphen character)
            // let (option, value) = getOption(String(argument.suffix(1)))
            // BUGBUG
            string option = "";
            string value = "";

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
                        writeMessage($"Missing path from option {value}", OutputType.error);
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
                        writeMessage($"Missing generations from option {value}", OutputType.error);
                    }
                    break;
                case "-h":
                    printUsage();
                    break;
                default:
                    writeMessage($"Unknown option {value}", OutputType.error);
                    printUsage();
                    break;
            }

            currentArg = currentArg + 1;
        }
    }

    // func openFile() -> [String] {

    //     // Includes trailing slash
    //     let appDirectory = URL(string: CommandLine.arguments[0] as String)!.deletingLastPathComponent()

    //     // appendingPathComponent fails at 120+ characters.
    //     let filenameAndPath = appDirectory.absoluteString + path
    //     //print(filenameAndPath)

    //     // Make sure the file exists
    //     guard FileManager.default.fileExists(atPath: filenameAndPath) else {
    //         preconditionFailure("File Not Found: \(filenameAndPath)")
    //     }

    //     // Read from the file
    //     do {
    //         let data = try String(contentsOfFile: filenameAndPath, encoding: String.Encoding.utf8)
    //         let fileAsArray = data.components(separatedBy: .newlines)
    //         return fileAsArray
    //     } catch let error as NSError {
    //         print("Failed reading from URL: \(filenameAndPath), Error: " + error.localizedDescription)
    //     }
    //     return []
    // }

    // func writeFile(outputToWrite: [String]) {
    //     // Includes trailing slash
    //     let appDirectory = URL(string: CommandLine.arguments[0] as String)!.deletingLastPathComponent()

    //     // appendingPathComponent fails at 120+ characters.
    //     let filenameWithoutExtension = URL(fileURLWithPath: path).deletingPathExtension().lastPathComponent
    //     let filenameAndPath = appDirectory.absoluteString + filenameWithoutExtension + "-result.txt"
    //     //print(filenameAndPath)

    //     let url = URL(string: filenameAndPath)!.path
    //     let joinedStrings = outputToWrite.joined(separator: "\n")

    //     // Write the file
    //     do {
    //         try joinedStrings.write(toFile: url, atomically: true, encoding: String.Encoding.utf8)
    //     } catch let error as NSError {
    //         print("Failed writing to URL: \(filenameAndPath), Error: " + error.localizedDescription)
    //     }
    // }

    private void writeMessage(string message, OutputType to = OutputType.standard)
    {
        switch (to)
        {
            case OutputType.standard:
                // \u{001B}[;m = reset terminal's text color back to the default
                System.Console.Write($"{message}"); // BUGBUG stdout
                break;
            case OutputType.error:
                // \u{001B}[0;31m = control characters that cause Terminal to change the color of the following text strings to red
                System.Console.Write($"{message}\n"); // BUGBUG stderr
                break;
        }
    }

    private void printUsage()
    {
        // BUGBUG let executableName = URL(string: CommandLine.arguments[0] as String)!.lastPathComponent
        string executableName = "gol";

        writeMessage("usage:");
        writeMessage($"{executableName} [-g int] -f file");
        writeMessage("-g = Generations - Default number of generations is 10");
        writeMessage("-f = File");
        writeMessage($"{executableName} -h shows this information");
    }

}