//
//  Cell.cs
//  Hypnos Game of Life C#
//
//  Created by Mark Gerrior on 1/14/21.
//

using System;

public enum State
{
    dead,
    alive
}

public class Cell
{
    // Properties
    public Int64 x;
    public Int64 y;
    public State state;

    public Cell(Int64 _x, Int64 _y, State _state = State.alive)
    {
        x = _x;
        y = _y;
        state = _state;
    }

    public Cell(string coordinatesString, State _state = State.alive)
    {
        string[] coordinateArray = coordinatesString.Split(' ');

        try
        {
            Int64[] coordinates = Array.ConvertAll(coordinateArray, s => Int64.Parse(s));

            x = coordinates[0]!;
            y = coordinates[1]!;
            state = _state;
        }
        catch (InvalidCastException e)
        {
            Environment.FailFast($"The coordinates '{e}' could not be converted to integers.");
        }
    }
}
