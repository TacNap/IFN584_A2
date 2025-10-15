using System.ComponentModel;
using System.Data;

public class Grid
{
    public Disc[][] Board; // need to change privacy. May need to add 'get' and 'set' methods for disc.ApplyEffects

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

    public bool AddDisc(Disc disc, int lane)
    {
        Console.WriteLine("This method isn't implemented yet");
        return false;
    }

    // requires Orientation parameter
    public void ApplyGravity()
    {
        int rows = Board.Length;
        int cols = Board[0].Length;

        if (orientation == Orientation.North)
        {
            // gravity down
            for (int col = 0; col < cols; col++)
            {
                List<Disc> discs = new List<Disc>();
                
                for (int row = 0; row < rows; row++)
                {
                    if (Board[row][col] != null)
                    {
                        discs.Add(Board[row][col]);
                        Board[row][col] = null;
                    }
                }
                
                int startRow = rows - discs.Count;
                for (int i = 0; i < discs.Count; i++)
                {
                    Board[startRow + i][col] = discs[i];
                }
            }
        }
        else if (orientation == Orientation.East)
        {
            // gravity left
            for (int row = 0; row < rows; row++)
            {
                List<Disc> discs = new List<Disc>();
                
                for (int col = cols - 1; col >= 0; col--)
                {
                    if (Board[row][col] != null)
                    {
                        discs.Add(Board[row][col]);
                        Board[row][col] = null;
                    }
                }

                for (int i = 0; i < discs.Count; i++)
                {
                    Board[row][i] = discs[i];
                }
            }
        }
        else if (orientation == Orientation.South)
        {
            // gravity up
            for (int col = 0; col < cols; col++)
            {
                List<Disc> discs = new List<Disc>();
                
                for (int row = rows - 1; row >= 0; row--)
                {
                    if (Board[row][col] != null)
                    {
                        discs.Add(Board[row][col]);
                        Board[row][col] = null;
                    }
                }
                
                for (int i = 0; i < discs.Count; i++)
                {
                    Board[i][col] = discs[i];
                }
            }
        }
        else if (orientation == Orientation.West)
        {
            // gravity left
            for (int row = 0; row < rows; row++)
            {
                List<Disc> discs = new List<Disc>();
                
                for (int col = 0; col < cols; col++)
                {
                    if (Board[row][col] != null)
                    {
                        discs.Add(Board[row][col]);
                        Board[row][col] = null;
                    }
                }
                
                int startCol = cols - discs.Count;
                for (int i = 0; i < discs.Count; i++)
                {
                    Board[row][startCol + i] = discs[i];
                }
            }
        }
    }

    public void Spin()
    {
        int pre_rows = Board.Length;
        int pre_cols = Board[0].Length;
        Disc[][] newBoard = new Disc[pre_cols][];

        for (int i = 0; i < pre_cols; i++)
            newBoard[i] = new Disc[pre_rows];
            
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
        int pre_rows = Board.Length;
        int pre_cols = Board[0].Length;
        switch(orientation)

        {
            case Orientation.North:
                for (int row = 0; row < pre_rows; row++) 
                {
                    for (int col = 0; col < pre_cols; col++) 
                    
                    if (Board[row][col] != null)
                    {
                        Console.Write(Board[row][col].Symbol);
                    }
                        Console.WriteLine();
                }
                break;

            case Orientation.East:
                
                for (int col = 0; col < pre_cols; col++)
                {
                    for (int row = pre_rows - 1; row >= 0; row--)

                    if (Board[row][col] != null)
                    {
                        Console.Write(Board[row][col].Symbol);
                    }
                        Console.WriteLine();
                }
                break;
        
            case Orientation.South:
                for (int row = pre_rows - 1; row >= 0; row--)
                {
                    for (int col = pre_cols - 1; col >= 0; col--)

                    if (Board[row][col] != null)
                    {
                        Console.Write(Board[row][col].Symbol);
                    }
                        Console.WriteLine();
                }
                break;
            
            case Orientation.West:
                for (int col = pre_cols - 1; col >= 0; col--)
                {
                    for (int row = 0; row < pre_rows; row++)

                    if (Board[row][col] != null)
                    {
                        Console.Write(Board[row][col].Symbol);
                    }
                        Console.WriteLine();
                }
                break;
	    }
    }

    public bool CheckWinCondition()
    {
        return false;
    }
}
