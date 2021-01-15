//
//  Program.cs
//  Hypnos Game of Life C#
//
//  Created by Mark Gerrior on 1/14/21.
//

using System;
using System.Collections.Generic;

namespace Hypnos_Game_of_Life_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ConsoleIO consoleIO = new ConsoleIO();

            consoleIO.ingestCommandLine(args);

            if (consoleIO.path == "")
            {
                consoleIO.writeMessage("File not specifed", OutputType.error);
                consoleIO.printUsage();
            }
            else
            {
                List<string> file = consoleIO.openFile();
                //print("Number of lines: \(file.count)")

                GameOfLife game = new GameOfLife(file);

                while (game.generation < consoleIO.iterations)
                {
                    game.performGameTurn();
                }

                List<string> result = game.exportGrid();
                consoleIO.writeFile(result);
            }
        }
    }
}
