# Hypnos Game of Life C#

This is a port of [Hypnos Game of Life Swift](https://github.com/gerrior/Hypnos-Game-of-Life-Swift): a 64-bit signed integer space version. It was built in Visual Studio Code (Version: 1.52.1) using C# v1.23.8 and .NET 5.0.2. The project is based on John Conway's [Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life) and utilizes [Life 1.06](https://www.conwaylife.com/wiki/Life_1.06) file format for input. 

## Command Line Usage

```
usage:
golcsharp [-g int] -f file [-s]
-g = Generations - Default number of generations is 10
-f = File
-s = Save - When present, will save every generation to individual files
golcsharp -h shows this information
```
While I started with the basic game logic from iOS from my summer project; it had to be heavily adapted since I switched from a fixed grid to a sparse grid stored in a dictionary. This results in a much smaller memory footprint; especially with smaller game files.

### My Approach
1. Load the file.
1. The game board only contains live cells.
1. Process each live cell in the game board and note if the cell is dying this iteration. 
1. In the process of checking the game board, note adjoining cells.
1. Check each of the adjoining cells to determine whether the cell should be alive (added to the game board) after this iteration. 
1. Remove the dead cells from the game board.
1. From the list of adjoining cells, add any new cells to the game board.
1. Got to step 3 and repeat number of generations specified by the user.
1. Save final game board to file with ```-result.txt``` appended to input filename.

### How to Review
Start with ```main()``` in ```Program.cs```. The bulk of the program logic is in ```GameOfLife.cs``` with a supporting object in ```Cell.cs```. ```ConsoleIO.cs``` supports the command line program (file input/output and parsing the command line) and should be reviewed last.

### How to Run
1. Launch Visual Studio Code
2. In Visual Studio Code, select File > Open... and after navigating to the ```Hypnos Game of Life CSharp``` directory, press the Open button
3. Within Visual Studio Code, expand the ```.vscode``` directory and open ```launch.json```
4. Change the default line 14 of ```"args": "",``` to ```"args": "-g 10 -f life.txt",```
5. From the Run menu, select "Start Debugging"
