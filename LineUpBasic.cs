public class LineUpBasic : Game {

    // Constructor
    public LineUpBasic(bool HvH = true)
    {
        int fixedRows = 8;
        int fixedCols = 9;
        // Create the grid
        Grid = new Grid(fixedRows, fixedCols);
        // Define the number of starting discs
        int discBalance = (fixedRows * fixedCols / 2) + 4;
        // Create the player objects
        PlayerOne = new Human(discBalance);
        if (HvH)
        {
            PlayerTwo = new Human(discBalance);
        }
        else
        {
            PlayerTwo = new Computer(discBalance);
        }

        IsGameActive = true;
        MoveSequence = string.Empty;
        io = new IOController();
        file = new FileController();
    }

    // This can just be a parent method 
    public override string GetInputGame()
    {
        Console.WriteLine("Enter move/command");
        Console.Write("> ");
        string? input = Console.ReadLine();
        return input.ToLower();
    }

    /// <summary>
    /// Check if the input describes a valid move for this game mode:
    /// Check input format
    /// Check disc type
    /// Check lane number
    /// Extract lane number
    /// </summary>
    /// <param name="input"></param>
    /// <param name="lane"></param>
    /// <returns></returns>
    public override bool TryParseMove(string input, out int lane)
    {
        lane = 0; // Must be instantited before continuing
        if (input[0] != 'o')
        {
            io.PrintError("Invalid disc type");
            return false;
        }

        if (input.Length > 2)
        {
            io.PrintError("Invalid lane");
            return false;
        }

        if (!int.TryParse(input.Substring(1), out lane))
        {
            // Parse failed
            io.PrintError("Invalid Lane - Must be a number");
            return false;
        }
        else
        {
            if (lane < 1 || lane > Grid.Board[1].Length)
            {
                io.PrintError("Invalid lane");
                return false;
            }

            // Valid Input
            return true;
        }
    }

    public override bool PlayTurn(Computer player)
    {
        // Disc = FindWinningMove
        throw new NotImplementedException();
    }

    public override void GameLoop()
    {
        while(IsGameActive)
        {
            Grid.DrawGrid();

            // Check if both players have discs remaining
            if (Grid.IsTieGame(PlayerOne, PlayerTwo))
            {
                io.PrintWinner(true, true);
                IsGameActive = false;
                break;
            }

            // Holds a reference to the current player, based on turn number
            Player activePlayer = Grid.TurnCounter % 2 == 1 ? PlayerOne : PlayerTwo;

            // NOT IDEAL
            // For true polymorphism, PlayTurn needs to exist on the Player object. 
            // Which would mean the entire Game object also needs to be passed in...
            bool success = activePlayer switch
            {
                Human h => PlayTurn(h),
                Computer c => PlayTurn(c),
                _ => throw new ArgumentException("Unknown player type")
            };

            // > Check Win Condition Here

            if (success)
                Grid.IncrementTurnCounter();
        }
    }
}