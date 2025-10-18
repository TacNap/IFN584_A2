
namespace LineUp2
{
    public abstract class Game
    {
        // Core Components
        public Grid Grid { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        public bool IsGameActive { get; set; }

        protected char[] AllowedDiscChars = new[] { 'o' };

        public List<Move> MoveSequence { get; set; }

        private Stack<Move> redoStack = new();

        public FileController file { get; set; }
        protected IComputerStrategy computerStrategy;

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
            computerStrategy = new BasicComputerStrategy();
        }

        public string GetInputGame(bool testMode = false)
        {
            string instruction = !testMode ? "Enter move/command" : "Enter string of moves for testing, seperated by a comma \",\"";
            Console.WriteLine(instruction);
            Console.Write("> ");
            string? input = Console.ReadLine();
            return input.Trim().ToLower();
        }

        /// <summary>
        /// Adds move to the MoveSequence list, and clears the redoStack.
        /// Used after a successful move AND during testing mode.
        /// </summary>
        /// <param name="move"></param>
        private void DocumentMove(Move move)
        {
            int index = Grid.TurnCounter - 1;

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

        /// <summary>
        /// Orchestration of move undo.
        /// Moves the last two moves in MoveSequence to redoStack,
        /// then calls PlayMoveSequence
        /// </summary>
        /// <returns></returns>
        private void Undo()
        {
            if (MoveSequence.Count < 2)
            {
                IOController.PrintError("You need at least two moves recorded to undo.");
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                int lastIndex = MoveSequence.Count - 1;
                Move removedMove = MoveSequence[lastIndex];
                MoveSequence.RemoveAt(lastIndex);
                redoStack.Push(removedMove);
            }

            PlayMoveSequence(MoveSequence.Count);
            return;
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

        /// <summary>
        /// Verifies if the player's input for disc type is allowed in this game mode.
        /// </summary>
        /// <param name="discChar"></param>
        /// <returns>true if allowed</returns>
        protected virtual bool VerifyDiscChar(char discChar)
        {
            char normalized = char.ToLowerInvariant(discChar);

            foreach (char allowed in AllowedDiscChars)
            {
                if (normalized == allowed)
                {
                    return true;
                }
            }
            return false;
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
        public bool TryParseMove(string input, out int lane)
        {
            lane = 0; // Must be instantited before continuing

            // Check if empty
            if (input == "" || input == null)
            {
                IOController.PrintError("Input is empty");
                lane = -1;
                return false;
            }

            // Check if disc type is allowed
            if (!VerifyDiscChar(input[0]))
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
                    IOController.PrintError("Invalid lane - Out of bounds");
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
                PrintPlayerData();
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
                    move.Disc.WithdrawDisc(player);
                    Grid.DrawGrid();
                    if (move.Disc.ApplyEffects(ref Grid.Board, move.Lane))
                    {
                        // Tyler: return disc to hand for special (boring only)
                        if (disc.DiscReturn != null)
                        {
                            PlayerOne.ReturnDisc(disc.DiscReturn[0]);
                            PlayerTwo.ReturnDisc(disc.DiscReturn[1]);
                        }

                        Grid.ApplyGravity();
                        Grid.DrawGrid();
                    }
                    DocumentMove(move);
                    return true;
                }
            }
        }

        /// <summary>
        /// Using in Testing Mode.
        /// Takes input from terminal and converts into a list of Move objects, which then populates MoveSequence. 
        /// Then calls PlayMoveSequence
        /// </summary>
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
        // Template Method for computer player
        public bool ComputerTurn(Player player)
        {
            // Use the strategy to select a move
            Move move = computerStrategy.SelectMove(Grid, player);

            // Execute the move
            if (!Grid.AddDisc(move))
            {
                IOController.PrintError("Error: Computer selected invalid move");
                return false;
            }
            DocumentMove(move);

            // Withdraw the disc from player's balance
            move.Disc.WithdrawDisc(player);
            Grid.DrawGrid();

            // Apply effects
            if (move.Disc.ApplyEffects(ref Grid.Board, move.Lane))
            {
                Grid.ApplyGravity();
                Grid.DrawGrid();
            }
            return true;
        }

        public abstract void CheckBoard();

        public void GameLoop()
        {
            while (IsGameActive)
            {
                //PrintPlayerData();
                //Grid.DrawGrid();

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

                if (activePlayer.IsHuman)
                {
                    IOController.PrintGameBanner();
                    Grid.DrawGrid();
                }

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

        // Need a way to only reset the correct discs based on game mode. 
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
            Console.WriteLine();
            Player player = Grid.TurnCounter % 2 == 1 ? PlayerOne : PlayerTwo;
            IOController.PrintDiscInventory(player.DiscBalance);
        }
    }
}

