public abstract class Game
{
    // Core Components
    public Grid Grid { get; set; }
    public Player PlayerOne { get; set; }
    public Player PlayerTwo { get; set; }

    public bool IsGameActive { get; set; }

    public List<string> MoveSequence { get; set; }
    public IOController io { get; set; }
    public FileController file { get; set; }

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
            io.PrintError("You have no move to undo yet!");
            return false;
        }

        Grid.Reset();
        PlayMoveSequence();
        return false;
    }

    /// <summary>
    /// Responsible for replaying moves during Undo / Redo 
    /// And testing mode, if implemented.
    /// </summary>
    public virtual void PlayMoveSequence()
    {
        // Iterate through MoveSequence, ParseMove, AddDisc, ApplyEffects
        Console.WriteLine("Not implemented yet!");
    }

    private bool Redo()
    {
        return false;
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
                    io.PrintGreen("Undo!\n");
                    break;
                case "/redo":
                    io.PrintGreen("Redo!\n");
                    break;
                case "/save":
                    io.PrintGreen("Save!\n");
                    break;
                case "/help":
                    io.PrintGreen("Help!\n");
                    break;
                case "/quit":
                    io.PrintGreen("Quit!\n");
                    IsGameActive = false;
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

    // Template Method
    public bool PlayTurn(Human player)
    {
        while (true)
        {
            string input = GetInputGame();
            if (string.IsNullOrEmpty(input))
            {
                io.PrintError("Please enter a valid move or command.");
                continue;
            }

            if (TryHandleCommand(input))
                return false;


            if (!TryParseMove(input, out int lane))
                return false;

            // At this point, its valid input
            Disc disc = CreateDisc(input[0], Grid.TurnCounter % 2 == 1 ? true : false);
            if (!player.HasDiscRemaining(disc))
            {
                io.PrintError("No Disc of that type remaining");
                continue;
            }

            // At this point, we have a disc and know its within balance.
            // Try to add the disc. If it fails, its because the lane is full.
            if (!Grid.AddDisc(disc, lane))
            {
                //Move fails
                io.PrintError("Error: Lane is full");
                continue;
            }
            else
            {
                // Successful move
                // DocumentMove
                player.WithdrawDisc(disc);
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

    // Will become a template method later 
    public abstract bool PlayTurn(Computer player);

    public abstract void GameLoop();
    public virtual Disc CreateDisc(char discType, bool isPlayerOne)
    {
        Disc disc = char.ToLower(discType) switch
        {
            'o' => new OrdinaryDisc(isPlayerOne),
            'b' => new BoringDisc(isPlayerOne),
            'e' => new ExplodingDisc(isPlayerOne),
            'm' => new MagneticDisc(isPlayerOne),
            _ => throw new ArgumentException($"Invalid disc type: {discType}")
        };

        return disc;
    }

    public virtual void ResetGame()
    {
        // Grid.ResetGrid();
        // PlayerOne.ResetPlayer();
        // PlayerTwo.ResetPlayer();
        
    }
}