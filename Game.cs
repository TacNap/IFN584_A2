public abstract class Game
{
    // Core Components
    public Grid Grid { get; set; }
    public Player PlayerOne { get; set; }
    public Player PlayerTwo { get; set; }

    public bool IsGameActive { get; set; }

    public List<string> MoveSequence { get; set; }

    public FileController file { get; set; }

    // Empty constructor is required to differentiate from JSON constructor
    protected Game()
    {
    }

    // Inherited by JSON Constructors
    protected Game(Grid grid, Player playerOne, Player playerTwo, bool isGameActive, List<string> moveSequence, FileController fileController)
    {
        Grid = grid;
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
        IsGameActive = isGameActive;
        MoveSequence = moveSequence ?? [];
        file = fileController ?? new FileController();
    }

    public string GetInputGame()
    {
        Console.WriteLine("Enter move/command");
        Console.Write("> ");
        string? input = Console.ReadLine();
        return input.Trim().ToLower();
    }

    // Not convinced yet that these methods should exist on Game
    private void DocumentMove(string move)
    {
        MoveSequence.Add(move);
    }

    private bool Undo()
    {
        // attempt to undo 2 moves. If turn counter < 3 : fail?
        if (Grid.TurnCounter <= 2)
        {
            IOController.PrintError("You have no move to undo yet!");
            return false;
        }

        // Decrement TurnCounter
        for (int _ = 0; _ < 2; _++)
        {
            Grid.DecrementTurnCounter();
        }
    
        // Run Moves with new TurnCounter
        Console.WriteLine($"\t\t Undo | TurnCounter: {Grid.TurnCounter}");
        Grid.Reset(true);
        PlayMoveSequence();
        Grid.DrawGrid();

        // TODO: Change Disc count
        
        return true;
    }

    private bool Redo()
    {
        // Decrement TurnCounter
        for (int _ = 0; _ < 2; _++)
        {
            Grid.IncrementTurnCounter();
        }

        // Run Moves with new TurnCounter
        Console.WriteLine($"\t\t Redo | TurnCounter: {Grid.TurnCounter}");
        Grid.Reset(true);
        PlayMoveSequence();
        Grid.DrawGrid();

        // TODO: Change Disc count

        return true;
    }


    /// <summary>
    /// Responsible for replaying moves during Undo / Redo 
    /// And testing mode, if implemented.
    /// </summary>
    public virtual void PlayMoveSequence()
    {
        // TODO: Test the logic

        // Iterate through MoveSequence based on TurnCounter
        Console.WriteLine($"\t\t Grid.TurnCounter: {Grid.TurnCounter}");
        Console.WriteLine($"\t\t MoveSquence length: {MoveSequence.Count}");
        for (int i = 0; i < Grid.TurnCounter-1; i++)
        {
            Console.WriteLine($"\t\t MoveSequence index: {i}");
            string input = MoveSequence[i];
            Console.WriteLine($"\t\t MoveSequence[{i}]: {input}");
            // ParseMove
            if (!TryParseMove(input, out int lane))
            {
                // ?
                break;
            }

            // AddDisc
            bool IsPlayerOne = (i+1) % 2 == 1;
            Disc disc = Disc.CreateDisc(input[0], IsPlayerOne);
            Grid.AddDisc(disc, lane);
            Grid.ApplyGravity();
            // ApplyEffects
            if (disc.ApplyEffects(ref Grid.Board, lane)) Grid.ApplyGravity();

            // CheckBoard();
        }
    }
    
    public bool TryHandleCommand(string input)
    {
        if(!input.StartsWith("/"))
        {
            return false;
        } else
        {
            switch (input)
            {
                case "/undo":
                    IOController.PrintGreen("Undo!\n");
                    Undo();
                    break;
                case "/redo":
                    IOController.PrintGreen("Redo!\n");
                    Redo();
                    break;
                case "/save":
                    file.GameSerialization(this);
                    break;
                case "/help":
                    IOController.PrintGreen("Help!\n");
                    break;
                case "/quit":
                    IOController.PrintGreen("Quit!\n");
                    IsGameActive = false;
                    break;
                default:
                    IOController.PrintError("Error: Unrecognised command");
                    break;
            }
            return true;
        }
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
    public virtual bool TryParseMove(string input, out int lane)
    {
        lane = 0; // Must be instantited before continuing
        if (input[0] != 'o')
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

    // Template Method
    public bool PlayerTurn(Player player)
    {
        while (true)
        {
            string input = GetInputGame();
            if (string.IsNullOrEmpty(input))
            {
                IOController.PrintError("Please enter a valid move or command.");
                continue;
            }

            if (TryHandleCommand(input))
                return false;


            if (!TryParseMove(input, out int lane))
                return false;

            // At this point, its valid input
            Disc disc = Disc.CreateDisc(input[0], Grid.TurnCounter % 2 == 1 ? true : false);
            if (!disc.HasDiscRemaining(player))
            {
                IOController.PrintError("No Disc of that type remaining");
                continue;
            }

            // At this point, we have a disc and know its within balance.
            // Try to add the disc. If it fails, its because the lane is full.
            if (!Grid.AddDisc(disc, lane))
            {
                //Move fails
                IOController.PrintError("Error: Lane is full");
                continue;
            }
            else
            {
                // Successful move
                // DocumentMove
                disc.WithdrawDisc(player);
                Grid.DrawGrid();
                if (disc.ApplyEffects(ref Grid.Board, lane))
                {
                    Grid.ApplyGravity();
                    Grid.DrawGrid();
                }
                return true;
            }
        }
    }

    // Might become a template method later 
    public abstract bool ComputerTurn(Player player);

    public abstract void GameLoop();

    public virtual void PrintPlayerData()
    {
        Console.WriteLine("--------------");
        Player player = Grid.TurnCounter % 2 == 1 ? PlayerOne : PlayerTwo;
        Console.WriteLine($"Discs: {player.DiscBalance["Ordinary"]}");
    }
}
