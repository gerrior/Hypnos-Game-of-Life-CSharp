using System;

public enum State
{
    dead,
    alive
}

public class Cell
{
    // Properties
    public int x;
    public int y;
    public State state;

    public Cell(int _x, int _y, State _state = State.alive)
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
            int[] coordinates = Array.ConvertAll(coordinateArray, s => int.Parse(s));

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
