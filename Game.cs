
namespace LineUp2
{
    public abstract class Game
    {
        /// <summary>
        /// Responsible for holding Disc objects and related methods
        /// </summary>
        public Grid Grid { get; set; } 
        
        /// <summary>
        /// First player. Always goes first. 
        /// </summary>
        public Player PlayerOne { get; set; }
        
        /// <summary>
        /// Second player
        /// </summary>
        public Player PlayerTwo { get; set; }

        /// <summary>
        /// False when game is quit or finished
        /// </summary>
        public bool IsGameActive { get; set; }

        /// <summary>
        /// Determines the disc types that are allowed in this game mode.
        /// </summary>
        protected char[] AllowedDiscChars = new[] { 'o' };

        /// <summary>
        /// History of played moves for this game. Used for undo/redo and testing
        /// </summary>
        public List<Move> MoveSequence { get; set; }

        /// <summary>
        /// Stack of possible /redo moves
        /// Does not get serialized
        /// </summary>
        private Stack<Move> redoStack = new();

        /// <summary>
        /// Responsible for file io
        /// </summary>
        public FileController file { get; set; }

        /// <summary>
        /// Responsible for AI behaviour
        /// </summary>
        protected IComputerStrategy computerStrategy;

        // Empty constructor is required to differentiate from JSON constructor when loading
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

        /// <summary>
        /// Get input from terminal during a game
        /// </summary>
        /// <param name="testMode"></param>
        /// <returns></returns>
        public string GetInputGame(bool testMode = false)
        {
            string instruction = !testMode ? "Enter move/command. Type /help for a list of commands." : "Enter string of moves for testing, seperated by a comma \",\"";
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
        /// then calls PlayMoveSequence to 'replay' the game from turn 1
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

        /// Returns moves in RedoStack to MoveSequence before playing again
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

        /// <summary>
        /// Resets the game and plays all moves in MoveSequence. 
        /// </summary>
        /// <param name="moveCount"></param>
        /// <returns></returns>
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
                CheckBoard(true);

                if (!IsGameActive)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if the user's input is a command, and run accordingly
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
                        Undo();
                        Console.Clear();
                        break;
                    case "/redo":
                        Redo();
                        Console.Clear();
                        break;
                    case "/save":
                        Console.Clear();
                        file.GameSerialization(this);
                        break;
                    case "/help":
                        IOController.PrintInGameHelp();
                        break;
                    case "/quit":
                        IsGameActive = false;
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
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
            foreach (char allowed in AllowedDiscChars)
            {
                if (discChar == allowed)
                {
                    return true;
                }
            }
            return false;
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
        public bool TryParseMove(string input, out int lane)
        {
            lane = 0; // Must be instantited before continuing

            // Check if empty
            if (input == "" || input == null)
            {
                Console.Clear();
                IOController.PrintError("Error: Input is empty");
                lane = -1;
                return false;
            }

            // Check if disc type is allowed
            if (!VerifyDiscChar(input[0]))
            {
                Console.Clear();
                IOController.PrintError("Error: Invalid disc type");
                return false;
            }

            if (input.Length > 3)
            {
                Console.Clear();
                IOController.PrintError("Error: Invalid lane");
                return false;
            }

            if (!int.TryParse(input.Substring(1), out lane))
            {
                // Parse failed
                Console.Clear();
                IOController.PrintError("Error: Invalid lane - Must be a number");
                return false;
            }
            else
            {
                if (lane < 1 || lane > Grid.Board[1].Length)
                {
                    Console.Clear();
                    IOController.PrintError("Error: Invalid lane - Out of bounds");
                    return false;
                }

                // Valid Input
                return true;
            }
        }

        /// <summary>
        /// Main orchestration for (Human) player's turn:
        /// Get Input
        /// Validate the input
        /// Check if its a valid move
        /// Execute it
        /// Document it 
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool PlayerTurn(Player player)
        {
            while (true)
            {
                string input = GetInputGame();
                if (string.IsNullOrEmpty(input))
                {
                    Console.Clear();
                    IOController.PrintError("Please enter a valid move or command.");
                    return false;
                }

                if (TryHandleCommand(input))
                    return false;


                if (!TryParseMove(input, out int lane))
                    return false;

                // At this point, its valid input
                Disc disc = Disc.CreateDisc(input[0], Grid.TurnCounter % 2 == 1 ? true : false);
                if (!disc.HasDiscRemaining(player))
                {
                    Console.Clear();
                    IOController.PrintError("Error: No Disc of that type remaining");
                    return false;
                }

                // Place into move struct for portability
                Move move = new Move(disc, lane);

                // At this point, we have a disc and know its within balance.
                // Try to add the disc. If it fails, its because the lane is full.
                if (!Grid.AddDisc(move))
                {
                    //Move fails
                    Console.Clear();
                    IOController.PrintError("Error: Lane is full");
                    return false;
                }
                else
                {
                    // Successful move
                    move.Disc.WithdrawDisc(player);
                    DocumentMove(move);
                    return true;
                }
            }
        }

        /// <summary>
        /// Calls out to computerStrategy to find a winning move or random move.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
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

            string error = null;
            for (int turn = 0; turn < moveList.Length; turn++)
            {
                string move = moveList[turn].Trim().ToLower();
                if (string.IsNullOrWhiteSpace(move))
                {
                    error = $"Move number {turn + 1} is empty. Please enter a new test sequence!";
                    break;
                }
                if (!TryParseMove(move, out int lane))
                {
                    error = $"Move number {turn + 1} ({move}) is invalid. Please enter a new test sequence!";
                    break;
                }
                bool isPlayerOne = turn % 2 == 0;
                Disc disc = Disc.CreateDisc(move[0], isPlayerOne);
                MoveSequence.Add(new Move(disc, lane));
            }

            // Play through moves
            PlayMoveSequence(MoveSequence.Count);
            IOController.PrintError(error);
        }

        /// <summary>
        /// Unique behaviour that is implemented by subclasses.
        /// eg. Spin
        /// or Apply Effects
        /// </summary>
        /// <param name="sypress"></param>
        public abstract void CheckBoard(bool sypress = false);

        public void GameLoop()
        {
            Console.Clear();
            PrintFrame(PlayerOne);

            while (IsGameActive)
            {
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

                bool successfulMove = activePlayer.IsHuman ? PlayerTurn(activePlayer) : ComputerTurn(activePlayer);

                if (successfulMove)
                {
                    CheckBoard();
                } else
                {
                    PrintFrame();
                }
            }
        }

        public virtual void Reset()
        {
            Grid.Reset();
            int OrdinaryDiscCount = Grid.Board.Length * Grid.Board[0].Length / 2;
            Dictionary<string, int> P1Discs = new Dictionary<string, int>
            {
                ["Ordinary"] = OrdinaryDiscCount,
            };
            Dictionary<string, int> P2Discs = new Dictionary<string, int>(P1Discs);

            PlayerOne.ResetDiscBalance(P1Discs);
            PlayerTwo.ResetDiscBalance(P2Discs);

        }

        /// <summary>
        /// Prints pretty colours to the screen 
        /// </summary>
        /// <param name="player"></param>
        public void PrintFrame(Player? player = null)
        {
            if (player == null)
            {
                player = Grid.TurnCounter % 2 == 1 ? PlayerOne : PlayerTwo;
            }
            IOController.PrintGameBanner(this, PlayerOne, PlayerTwo);
            Grid.DrawGrid();
            Console.WriteLine();
            IOController.PrintDiscInventory(player.DiscBalance);
        }
    }
}

