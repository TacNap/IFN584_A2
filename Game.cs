public abstract class Game
{
    // Core Components
    public Grid Grid { get; set; }
    public Player PlayerOne { get; set; }
    public Player PlayerTwo { get; set; }

    public bool IsGameActive { get; set; }

    public List<Move> MoveSequence { get; set; }

    private Stack<Move> redoStack = new();

    public FileController file { get; set; }

    // Empty constructor is required to differentiate from JSON constructor
    protected Game()
    {
    }

    // Inherited by JSON Constructors - Required for Deserialization
    protected Game(Grid grid, Player playerOne, Player playerTwo, bool isGameActive, List<Move> moveSequence, FileController fileController)
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
        string instruction = !testMode ? "Enter move/command" : "Enter string of moves for testing, seperated by a comma \",\"";
        Console.WriteLine(instruction);
        Console.Write("> ");
        string? input = Console.ReadLine();
        return input.Trim().ToLower();
    }

    // Not convinced yet that these methods should exist on Game
    private void DocumentMove(Move move)
    {
        int index = Grid.TurnCounter - 1;
        if (index < 0)
        {
            index = 0;
        }

        if (index < MoveSequence.Count)
        {
            MoveSequence[index] = move;
        }
        else
        {
            MoveSequence.Add(move);
        }

        redoStack.Clear();
    }

    private bool Undo()
    {
        if (MoveSequence.Count < 2)
        {
            IOController.PrintError("You need at least two moves recorded to undo.");
            return false;
        }

        for (int i = 0; i < 2; i++)
        {
            int lastIndex = MoveSequence.Count - 1;
            Move removedMove = MoveSequence[lastIndex];
            MoveSequence.RemoveAt(lastIndex);
            redoStack.Push(removedMove);
        }

        bool sequenceEnded = PlayMoveSequence(MoveSequence.Count);
        IsGameActive = !sequenceEnded;

        return !sequenceEnded;
    }

    private bool Redo()
    {
        if (redoStack.Count < 2)
        {
            IOController.PrintError("You have no move to redo yet!");
            return false;
        }

        List<Move> movesToRestore = new List<Move>(2);
        for (int i = 0; i < 2; i++)
        {
            movesToRestore.Add(redoStack.Pop());
        }
        foreach (Move move in movesToRestore)
        {
            MoveSequence.Add(move);
        }

        bool sequenceEnded = PlayMoveSequence(MoveSequence.Count);
        IsGameActive = !sequenceEnded;

        return !sequenceEnded;
    }

    private bool PlayMoveSequence(int moveCount)
    {
        Reset();

        for (int turn = 1; turn <= moveCount; turn++)
        {
            if (Grid.IsTieGame(PlayerOne, PlayerTwo))
            {
                IOController.PrintWinner(true, true);

                IsGameActive = false;
                return true;
            }

            Move move = MoveSequence[turn - 1];
            Player player = turn % 2 == 1 ? PlayerOne : PlayerTwo;

            if (!move.Disc.HasDiscRemaining(player))
            {
                IOController.PrintError("Error: Corrupted move sequence");

                IsGameActive = false;
                return true;
            }

            if (!Grid.AddDisc(move))
            {
                IOController.PrintError("Error: Invalid move sequence");

                IsGameActive = false;
                return true;
            }

            if (move.Disc.ApplyEffects(ref Grid.Board, move.Lane))
            {
                Grid.ApplyGravity();
            }

            move.Disc.WithdrawDisc(player);
            CheckBoard();

            if (!IsGameActive)
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

            // Place into move struct for portability
            Move move = new Move(disc, lane);

            // At this point, we have a disc and know its within balance.
            // Try to add the disc. If it fails, its because the lane is full.
            if (!Grid.AddDisc(move))
            {
                //Move fails
                IOController.PrintError("Error: Lane is full");
                continue;
            }
            else
            {
                // Successful move
                // DocumentMove
                move.Disc.WithdrawDisc(player);
                Grid.DrawGrid();
                if (move.Disc.ApplyEffects(ref Grid.Board, move.Lane))
                {
                    Grid.ApplyGravity();
                    Grid.DrawGrid();
                }
                DocumentMove(move);
                return true;
            }
        }
    }
    
    public void TestLoop()
    {
        Grid.DrawGrid();
        // Get test input sequence
        string input = GetInputGame(true);

        // Split input into moves
        string[] moveList = input.Split(",");
        if (moveList.Length == 0) return;
        for (int turn = 0; turn < moveList.Length; turn++)
        {
            string move = moveList[turn].Trim().ToLower();
            if (string.IsNullOrWhiteSpace(move))
            {
                IOController.PrintError($"Move number {turn + 1} is empty. Please enter a new test sequence!");
                break;
            }
            if (!TryParseMove(move, out int lane))
            {
                IOController.PrintError($"Move number {turn + 1} ({move}) is invalid. Please enter a new test sequence!");
                break;
            }
            bool isPlayerOne = turn % 2 == 0;
            Disc disc = Disc.CreateDisc(move[0], isPlayerOne);
            MoveSequence.Add(new Move(disc, lane));
        }

        // Play through moves
        PlayMoveSequence(MoveSequence.Count);
    }

    // Might become a template method later 
    public abstract bool ComputerTurn(Player player);

    public abstract void CheckBoard();

    public void GameLoop()
    {
        while (IsGameActive)
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
    
    public void Reset()
    {
        Grid.Reset();
        int OrdinaryDiscCount = Grid.Board.Length * Grid.Board[0].Length / 2;
        Dictionary<string, int> P1Discs = new Dictionary<string, int>
        {
            ["Ordinary"] = OrdinaryDiscCount,
            ["Boring"] = 2,
            ["Exploding"] = 2,
            ["Magnetic"] = 2
        };
        Dictionary<string, int> P2Discs = new Dictionary<string, int>(P1Discs);

        PlayerOne.ResetDiscBalance(P1Discs);
        PlayerTwo.ResetDiscBalance(P2Discs);

    }

    public virtual void PrintPlayerData()
    {
        Console.WriteLine("--------------");
        Player player = Grid.TurnCounter % 2 == 1 ? PlayerOne : PlayerTwo;
        Console.WriteLine($"Discs: {player.DiscBalance["Ordinary"]}");
    }
}

