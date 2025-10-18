using Newtonsoft.Json;

namespace LineUP2
{
    public class LineUpClassic : Game
    {

        public LineUpClassic(int GridHeight, int GridWidth, bool HvH = true)
        {
            Grid = new Grid(GridHeight, GridWidth);

            // Define disc amounts
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
        }

        // Constructor used when loading from file
        [JsonConstructor]
        public LineUpClassic(Grid grid, Player playerOne, Player playerTwo, bool isGameActive, List<string> moveSequence, FileController file)
            : base(grid, playerOne, playerTwo, isGameActive, moveSequence, file)
        {
        }

        public override bool ComputerTurn(Player player)
        {
            throw new NotImplementedException();
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
        public override void GameLoop()
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
                    if (Grid.CheckWinCondition())
                    {
                        IsGameActive = false;
                        break;
                    }
                    Grid.IncrementTurnCounter();
                }
            }
        }




    }
}
