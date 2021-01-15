//
//  GameGrid.cs
//  Hypnos Game of Life Swift
//
//  Created by Mark Gerrior on 1/14/21.
//

using System;
using System.Collections.Generic;

class GameOfLife
{
    Dictionary<string, Cell> cells = new Dictionary<string, Cell>();
    Dictionary<string, Cell> potentialCells = new Dictionary<string, Cell>();

    List<string> cellsToKill;
    List<string> cellsToBirth;

    public int generation = 0;
    int population = 0;
    // {
    //BUGBUG        cells.filter{ $0.value.state == .alive }.count
    // }

    public GameOfLife(List<string> lifeFile)
    {
        // Create sparse grid
        foreach (string row in lifeFile)
        {
            if (row.StartsWith('#')) { continue; }

            string trimmedRow = row.Trim();

            if (trimmedRow.Length < 3) { continue; }

            Cell newCell = new Cell(trimmedRow);
            cells.Add(trimmedRow, newCell);
        }
    }

    void clearGrid()
    {
        cells.Clear();

        generation = 0;
    }

    public List<string> exportGrid()
    {
        List<string> results = new List<string>();;
        results.Add("#Life 1.06");

        // BUGBUG var sortedKeys = cells.Keys.();

        // foreach (var key in sortedKeys)
        // {
        //     results.Add(cells[key]);
        // }

        return results;
    }

    Cell cellAt(int x, int y, bool createIfNotPresent = true)
    {
        string key = $"{x} {y}";

        // Is key in the current list of live cells?
        if (cells[key] != null)
        {
            return cells[key];
        }

        // Is key in the current list of potential cells?
        if (potentialCells[key] != null)
        {
            return potentialCells[key];
        }

        // Not found it. Create a cell so caller logic can process it
        Cell newCell = new Cell(x, y, State.dead);

        if (createIfNotPresent)
        {
            // This is a cell adjacent to a live cell. It could spring to life so we need to check it later
            potentialCells[key] = newCell;
        } // else we're checking a dead adjacent cell; we don't want to check further; so don't add to potentialCells
          // we'll just return an object to satisfy the caller logic

        return newCell;
    }

    void lookAround(bool aliveCells = true)
    {
        var cellsToSearch = potentialCells;

        if (aliveCells)
        {
            cellsToSearch = cells;
        }

        // Dictionary<string, Cell> 
        foreach (KeyValuePair<string, Cell> pair in cellsToSearch)
        {
            var key = pair.Key;
            var cellToCheck = pair.Value;

            int count = 0;
            var coordinates = (x: cellToCheck.x, y: cellToCheck.y);

            // West
            if (coordinates.x != int.MinValue + 1)
            {
                var cell = cellAt(x: coordinates.x - 1, y: coordinates.y, createIfNotPresent: aliveCells);
                if (cell.state == State.alive)
                {
                    count = count + 1;
                }
            }

            // North West
            if (coordinates.x != int.MinValue + 1 && coordinates.y != int.MinValue + 1)
            {
                var cell = cellAt(x: coordinates.x - 1, y: coordinates.y + 1, createIfNotPresent: aliveCells);
                if (cell.state == State.alive)
                {
                    count = count + 1;
                }
            }

            // North
            if (coordinates.y != int.MinValue + 1)
            {
                var cell = cellAt(x: coordinates.x, y: coordinates.y + 1, createIfNotPresent: aliveCells);
                if (cell.state == State.alive)
                {
                    count = count + 1;
                }
            }

            // North East
            if (coordinates.x < int.MaxValue - 1 && coordinates.y != int.MinValue + 1)
            {
                var cell = cellAt(x: coordinates.x + 1, y: coordinates.y + 1, createIfNotPresent: aliveCells);
                if (cell.state == State.alive)
                {
                    count = count + 1;
                }
            }

            // East
            if (coordinates.x < int.MaxValue - 1)
            {
                var cell = cellAt(x: coordinates.x + 1, y: coordinates.y, createIfNotPresent: aliveCells);
                if (cell.state == State.alive)
                {
                    count = count + 1;
                }
            }

            // South East
            if (coordinates.x < int.MaxValue - 1 && coordinates.y < int.MaxValue - 1)
            {
                var cell = cellAt(x: coordinates.x + 1, y: coordinates.y - 1, createIfNotPresent: aliveCells);
                if (cell.state == State.alive)
                {
                    count = count + 1;
                }
            }

            // South
            if (coordinates.y < int.MaxValue - 1)
            {
                var cell = cellAt(x: coordinates.x, y: coordinates.y - 1, createIfNotPresent: aliveCells);
                if (cell.state == State.alive)
                {
                    count = count + 1;
                }
            }

            // South West
            if (coordinates.x != int.MinValue + 1 && coordinates.y < int.MaxValue - 1)
            {
                var cell = cellAt(x: coordinates.x - 1, y: coordinates.y - 1, createIfNotPresent: aliveCells);
                if (cell.state == State.alive)
                {
                    count = count + 1;
                }
            }

            // One pass looks at cells (alive). The other pass looks as potentialCells (dead)
            if ((count < 2 || count > 3) && cellToCheck.state == State.alive)
            {
                cellsToKill.Add(key);
            }
            else
            {
                if (count == 3 && cellToCheck.state == State.dead)
                {
                    cellsToBirth.Add(key);
                }
            }
        }
    }

    public void performGameTurn()
    {
        cellsToKill.Clear(); // Array of keys
        cellsToBirth.Clear(); // Array of keys
        potentialCells.Clear(); // Dictionary of key, cell

        // Look at all live cells, record if they need to die
        lookAround(true);
        // The pior call will add cells to potentialCells
        // These are the adjacent cells to live cells
        // We now need to check if any of these cells will spring to life
        lookAround(false);

        // Remove dead cells
        foreach (string dead in cellsToKill)
        {
            // Use the dead key to set the dictionary value to nil
            cells[dead] = null;
        }

        // Add cells that were born this turn
        foreach (string born in cellsToBirth)
        {
            // Use the born key to access the cell in the dictionary
            Cell cell = potentialCells[born];
            // Set its new state to alive
            cell.state = State.alive;
            // Add the new cell to the list of alive cells
            cells[born] = cell;
        }

        generation += 1;
    }
}
