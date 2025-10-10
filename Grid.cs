using System.ComponentModel;

public class Grid
{
    // private Disc[][] Board;
    public enum Orientation // Defines the clockwise rotation of the Board
    {
        North, // 0 Degrees
        East, // 90 Degrees
        South, // 180 Degrees
        West // 270 Degrees
    }

    public Orientation orientation { get; private set; }
    public int WinLength { get; }

    public int TurnCounter { get; private set; }

    // Constructor
    public Grid(int GridHeight, int GridWidth)
    {
        // this.Board = new Disc[GridHeight];
        // for (int col = 0; col < GridHeight; i++) {
        //      Board[i] = new Disc[GridWidth]
        // }

        this.orientation = Orientation.North;
        this.WinLength = (int)Math.Floor(GridHeight * GridWidth * 0.1);
    }
    // Methods
    public void IncrementTurnCounter()
    {
        TurnCounter++;
    }

    public void DecrementTurnCounter()
    {
        TurnCounter--;
    }

    // Needs to be updated in class diagram
    public bool SetTurnCounter(int num)
    {
        if (num < 0) return false;
        TurnCounter = num;
        return true;
    }

    /// <summary>
    /// Increments orientation to the next rotation value, or returns to initial value.
    /// This is more involved than I thought it would be. Might be worth switching to some other method / data structure. 
    /// Taken from: https://stackoverflow.com/questions/642542/how-to-get-next-or-previous-enum-value-in-c-sharp
    /// </summary>
    public void IncrementOrientation()
    {
        // Get all values in the enum
        Orientation[] values = (Orientation[])Enum.GetValues(typeof(Orientation));
        int index = (Array.IndexOf(values, orientation) + 1) % values.Length;
        orientation = values[index];
    }

    // needs Player object parameters
    // Not implemented yet
    public bool IsTieGame()
    {
        Console.WriteLine("This method isn't implemented yet");
        return false;
    }

    public bool AddDisc(int lane)
    {
        Console.WriteLine("This method isn't implemented yet");
        return false;
    }

    // requires Orientation parameter
    public void ApplyGravity()
    {
        Console.WriteLine("This method isn't implemented yet");
        return;
    }

    public void Spin()
    {
        return;
    }

    // requires Orientation parameter
    public void DrawGrid()
    {
        return;
    }

    public bool CheckWinCondition()
    {
        return false;
    }
}
