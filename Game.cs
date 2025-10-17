public abstract class Game
{
    // Core Components
    public Grid Grid { get; set; }
    public Player PlayerOne { get; set; }
    public Player PlayerTwo { get; set; }

    public bool IsGameActive { get; set; }

    public List<string> MoveSequence { get; set; }

    public FileController file { get; set; }

    // Strategy for computer AI
    protected IComputerStrategy computerStrategy;

    public string GetInputGame()
    {
        Console.WriteLine("Enter move/command");
        Console.Write("> ");
        string? input = Console.ReadLine();
        return input.Trim().ToLower();
    }

    private void DocumentMove(string move)
    {
        MoveSequence.Add(move);
    }

    private bool Undo()
    {
        if (Grid.TurnCounter <= 2)
        {
            IOController.PrintError("You have no move to undo yet!");
            return false;
        }

        Grid.Reset();
        PlayMoveSequence();
        return false;
    }

    private bool Redo()
    {
        return false;
    }

    public virtual void PlayMoveSequence()
    {
        Console.WriteLine("Not implemented yet!");
    }

    public bool TryHandleCommand(string input)
    {
        if(!input.StartsWith("/"))
        {
            return false;
        }
        else
        {
            switch (input)
            {
                case "/undo":
                    IOController.PrintGreen("Undo!\n");
                    break;
                case "/redo":
                    IOController.PrintGreen("Redo!\n");
                    break;
                case "/save":
                    IOController.PrintGreen("Save!\n");
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

    public virtual bool TryParseMove(string input, out int lane)
    {
        lane = 0;
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
            IOController.PrintError("Invalid Lane - Must be a number");
            return false;
        }
        else
        {
            if (lane < 1 || lane > Grid.Board[0].Length)
            {
                IOController.PrintError("Invalid lane");
                return false;
            }

            return true;
        }
    }

    // Template Method for human player
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
                continue;

            Disc disc = Disc.CreateDisc(input[0], Grid.TurnCounter % 2 == 1);
            if (!disc.HasDiscRemaining(player))
            {
                IOController.PrintError("No Disc of that type remaining");
                continue;
            }

            if (!Grid.AddDisc(disc, lane))
            {
                IOController.PrintError("Error: Lane is full");
                continue;
            }
            else
            {
                // Successful move
                DocumentMove(input);
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

    // Template Method for computer player - uses Strategy Pattern
    public bool ComputerTurn(Player player)
    {
        Console.WriteLine("Computer is thinking...");
        
        // Use the strategy to select a move
        Move move = computerStrategy.SelectMove(Grid, player);

        // Execute the move
        if (!Grid.AddDisc(move.Disc, move.Lane))
        {
            IOController.PrintError("Error: Computer selected invalid move");
            return false;
        }

        // Document the move
        char discChar = GetDiscCharFromDisc(move.Disc);
        DocumentMove($"{discChar}{move.Lane}");

        // Withdraw the disc from player's balance
        move.Disc.WithdrawDisc(player);

        Console.WriteLine($"Computer plays {move.Disc.Symbol} in lane {move.Lane}");
        Grid.DrawGrid();

        // Apply effects
        if (move.Disc.ApplyEffects(ref Grid.Board, move.Lane))
        {
            Grid.ApplyGravity();
            Grid.DrawGrid();
        }

        return true;
    }

    /// <summary>
    /// Helper method to get the disc character for move documentation
    /// </summary>
    private char GetDiscCharFromDisc(Disc disc)
    {
        string symbol = disc.Symbol.ToLower();
        return symbol switch
        {
            "@" or "#" => 'o',
            "b" => 'b',
            "e" => 'e',
            "m" => 'm',
            _ => 'o'
        };
    }

    public abstract void GameLoop();

    public virtual void PrintPlayerData()
    {
        Console.WriteLine("--------------");
        Player player = Grid.TurnCounter % 2 == 1 ? PlayerOne : PlayerTwo;
        Console.WriteLine($"Discs: {player.DiscBalance["Ordinary"]}");
    }
}