using Newtonsoft.Json;

public class LineUpClassic : Game
{
    private void ConfigureAllowedDiscs()
    {
        AllowedDiscChars = new[] { 'o', 'b', 'e', 'm' };
    }

    public LineUpClassic(int GridHeight, int GridWidth, bool HvH = true)
    {
        ConfigureAllowedDiscs();

        Grid = new Grid(GridHeight, GridWidth);
        int ordinaryBalance = GridHeight * GridWidth / 2;
        Dictionary<string, int> discBalance = new Dictionary<string, int>
        {
            ["Ordinary"] = ordinaryBalance,
            ["Boring"] = 2,
            ["Exploding"] = 2,
            ["Magnetic"] = 2
        };

        PlayerOne = new Player(discBalance);
        PlayerTwo = new Player(discBalance, HvH);
        IsGameActive = true;
        MoveSequence = [];
        file = new FileController();
    
        computerStrategy = new BasicComputerStrategy();
    }

    [JsonConstructor]
    public LineUpClassic(Grid grid, Player playerOne, Player playerTwo, bool isGameActive, List<Move> moveSequence, FileController file)
        : base(grid, playerOne, playerTwo, isGameActive, moveSequence, file)
    {
        ConfigureAllowedDiscs();

        // Strategy is initialized in base constructor
    }

    // * Revisit meee. Make me a template method or something 
    // Do I even account for grid length = 10?
    // do i even account for spin???
    public override bool TryParseMove(string input, out int lane)
    {
        lane = 0; // Must be instantited before continuing
        string validChar = "obem";
        if (!validChar.Contains(input[0])) // This should reference some dictionary of moves on the game subclass
        {
            IOController.PrintError("Invalid disc type");
            return false;
        }

        if (input.Length > 2)
        {
            IOController.PrintError("Invalid lane");
            return false;
        }

        if (!int.TryParse(input.Substring(1), out lane))
        {
            // Parse failed
            IOController.PrintError("Invalid Lane - Must be a number");
            return false;
        }
        else
        {
            if (lane < 1 || lane > Grid.Board[1].Length)
            {
                IOController.PrintError("Invalid lane");
                return false;
            }

            // Valid Input
            return true;
        }
    }
    public override void CheckBoard()
    {
        if (Grid.CheckWinCondition())
        {
            IsGameActive = false;
            return;
        }
        Grid.IncrementTurnCounter();
    }

    
}
