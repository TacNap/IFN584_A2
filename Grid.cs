using System.ComponentModel;

public class Grid
{
    private Disc[][] Board;
    
    // Should this be defined in the class, or elsewhere?
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
        this.Board = new Disc[GridHeight][];
        for (int col = 0; col < GridHeight; col++)
        {
            Board[col] = new Disc[GridWidth];
        }

        this.orientation = Orientation.North;
        this.WinLength = (int)Math.Floor(GridHeight * GridWidth * 0.1);
        this.TurnCounter = 0;
    }
    // Methods

    /// <summary>
    /// Increment turn counter by 1. 
    /// Used when a successful move is made by a player.
    /// </summary>
    public void IncrementTurnCounter()
    {
        TurnCounter++;
    }

    /// <summary>
    /// Decrement turn counter by 1.
    /// Might not actually need this but will leave for now.
    /// </summary>
    public void DecrementTurnCounter()
    {
        TurnCounter--;
    }

    /// <summary>
    /// Set turn counter to a specified value.
    /// Must be positive.
    /// Used for undo / redo / load game.
    /// </summary>
    /// <param name="num"></param>
    /// <returns>true if TurnCounter value is changed.</returns>
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

    // Not implemented yet
    public bool IsTieGame(Player p1, Player p2)
    {
        return !p1.HasDiscRemaining() && !p2.HasDiscRemaining();
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
        int pre_rows = Board.Length;
        int pre_cols = Board[0].Length;
        Disc[][] newBoard = new Disc[pre_cols][];

        for (int new_col = 0; new_col < pre_cols; new_col++)
            newBoard[new_col] = new Disc[pre_rows];
            
        for (int new_row = 0; new_row < pre_rows; new_row++)
        {
            for (int col = 0; col < pre_cols; col++)
            {
                newBoard[col][pre_rows - 1 - new_row] = Board[new_row][col];
            }
        }
        Board = newBoard;
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
