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

    public string GetInputGame(bool testMode = false)
    {
        string instruction = !testMode ? "Enter move/command" : "Enter string of moves for testing, seperated by a comma \",\"; or a command";
        Console.WriteLine(instruction);
        Console.Write("> ");
        string? input = Console.ReadLine();
        return input.Trim().ToLower();
    }

    // Not convinced yet that these methods should exist on Game
    private void DocumentMove(string move)
    {
        if (Grid.TurnCounter - 1 < MoveSequence.Count) MoveSequence[Grid.TurnCounter] = move;
        else MoveSequence.Add(move);
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
    public virtual bool PlayMoveSequence()
    {
        // Reset disc balance
        int OrdinaryDiscCount = Grid.Board.Length * Grid.Board[0].Length / 2;
        PlayerOne.ResetDiscBalance(new Dictionary<string, int>
        {
            ["Ordinary"] = OrdinaryDiscCount,
            ["Boring"] = 2,
            ["Exploding"] = 2,
            ["Magnetic"] = 2
        });
        PlayerTwo.ResetDiscBalance(new Dictionary<string, int>
        {
            ["Ordinary"] = OrdinaryDiscCount,
            ["Boring"] = 2,
            ["Exploding"] = 2,
            ["Magnetic"] = 2
        });

        // Iterate through MoveSequence based on TurnCounter
        Console.WriteLine($"\t\t Grid.TurnCounter: {Grid.TurnCounter}");
        Console.WriteLine($"\t\t MoveSquence length: {MoveSequence.Count}");
        for (int i = 0; i < Grid.TurnCounter - 1; i++)
        {
            // Check if both players have discs remaining
            if (Grid.IsTieGame(PlayerOne, PlayerTwo))
            {
                IOController.PrintWinner(true, true);
                return true;
            }

            Console.WriteLine($"\t\t MoveSequence index: {i}");
            string input = MoveSequence[i];
            Console.WriteLine($"\t\t MoveSequence[{i}]: {input}");
            // Parse Move
            if (!TryParseMove(input, out int lane))
            {
                IOController.PrintError("Stop playing sequence!");
                return true;
            }

            // Check if there are disc left
            bool IsPlayerOne = (i + 1) % 2 == 1;
            Player player = IsPlayerOne ? PlayerOne : PlayerTwo;
            Disc disc = Disc.CreateDisc(input[0], IsPlayerOne);

            if (!disc.HasDiscRemaining(player))
            {
                IOController.PrintError($"No Disc {disc.Symbol} remaining");
                continue;
            }

            // Add disc
            Grid.AddDisc(disc, lane);
            Grid.ApplyGravity();
            // ApplyEffects
            if (disc.ApplyEffects(ref Grid.Board, lane)) Grid.ApplyGravity();

            // TODO: This is supposed to replace the "ApplyEffect" or whatever for different game mode
            // CheckBoard();

            // Update disc balance
            disc.WithdrawDisc(player);
            Grid.DrawGrid();
            if (Grid.CheckWinCondition())
            {
                return true;
            }
        }
        return false;
    }
    
    public bool TryHandleCommand(string input)
    {
        if (!input.StartsWith("/"))
        {
            return false;
        }
        else
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
                    IOController.PrintInGameHelp();
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

    // **** Revisit meee. Make me a template method or something 
    // Do I even account for grid length = 10?
    // do i even account for spin???
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
        if (input == "" | input == null)
        {
            IOController.PrintError("Input is empty");
            lane = -1;
            return false;
        }
        if (input[0] != 'o') // This should reference some dictionary of moves on the game subclass
        {
            IOController.PrintError("Invalid disc type");
            return false;
        }

        if (input.Length > 3)
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
                // TODO: (Remove) from this
                // Note: Temp code to test Undo/Redo
                string move = "";
                switch(disc.Symbol)
                {
                    case "@":
                    case "#":
                        move = "o";
                        break;
                    case "B":
                    case "b":
                        move = "b";
                        break;
                    case "M":
                    case "m":
                        move = "m";
                        break;
                    case "E":
                    case "e":
                        move = "e";
                        break;
                }
                move += $"{lane}";
                DocumentMove(move);
                // TODO: (Remove) to this
                return true;
            }
        }
    }
    
    public void TestLoop()
    {
        while (IsGameActive)
        {
            Grid.DrawGrid();

            bool invalidMove = false;
            // Get input
            string input = GetInputGame(true);
            // Check if input is command
            if (TryHandleCommand(input)) break;

            // Split input into moves
            string[] moveList =input.Split(",");
            if (moveList.Length == 0) break;
            for (int i = 0; i < moveList.Length; i++)
            {
                string move = moveList[i].Trim().ToLower();
                if (!TryParseMove(move, out int lane))
                {
                    IOController.PrintError($"Move number {i + 1} ({move}) is invalid. Please enter a new test sequence!");
                    invalidMove = true;
                    break;
                }
                DocumentMove(move);
                Grid.IncrementTurnCounter();
            }
            if (invalidMove) continue;

            Grid.Reset(true);
            IsGameActive = !PlayMoveSequence();
        }
        
    }

    // Might become a template method later 
    public abstract bool ComputerTurn(Player player);

    public abstract void CheckBoard();

    public void GameLoop()
    {
        while(IsGameActive)
        {
            PrintPlayerData();
            Grid.DrawGrid();

            // Check if both players have discs remaining
            if (Grid.IsTieGame(PlayerOne, PlayerTwo))
            {
                IOController.PrintWinner(true, true);
                IsGameActive = false;
                break;
            }

            // Holds a reference to the current player, based on turn number
            // Just for less repeated code :)
            Player activePlayer = Grid.TurnCounter % 2 == 1 ? PlayerOne : PlayerTwo;

            // NOT IDEAL
            // For true polymorphism, PlayTurn needs to exist on the Player object. 
            // Which would mean the entire Game object also needs to be passed in...
            bool successfulMove = activePlayer.IsHuman ? PlayerTurn(activePlayer) : ComputerTurn(activePlayer);
            // ! Board currently renders twice by accident after a move is played.. Will fix later. 

            if (successfulMove)
            {
                CheckBoard();
            }
        }
    }

    public virtual void PrintPlayerData()
    {
        Console.WriteLine("--------------");
        Player player = Grid.TurnCounter % 2 == 1 ? PlayerOne : PlayerTwo;
        Console.WriteLine($"Discs: {player.DiscBalance["Ordinary"]}");
    }
}
