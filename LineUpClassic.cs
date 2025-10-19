using Newtonsoft.Json;

namespace LineUp2
{
    public class LineUpClassic : Game
    {

        public LineUpClassic(int GridHeight, int GridWidth, bool HvH = true)
        {
            AllowedDiscChars = new[] { 'o', 'b', 'e', 'm' };

            Grid = new Grid(GridHeight, GridWidth);
            int ordinaryBalance = GridHeight * GridWidth / 2;
            Dictionary<string, int> p1DiscBalance = new Dictionary<string, int>
            {
                ["Ordinary"] = ordinaryBalance,
                ["Boring"] = 2,
                ["Exploding"] = 2,
                ["Magnetic"] = 2
            };
            Dictionary<string, int> p2DiscBalance = new Dictionary<string, int>
            {
                ["Ordinary"] = ordinaryBalance,
                ["Boring"] = 2,
                ["Exploding"] = 2,
                ["Magnetic"] = 2
            };

            PlayerOne = new Player(p1DiscBalance);
            PlayerTwo = new Player(p2DiscBalance, HvH);
            IsGameActive = true;
            MoveSequence = [];
            file = new FileController();

            computerStrategy = new BasicComputerStrategy();
        }

        [JsonConstructor]
        public LineUpClassic(Grid grid, Player playerOne, Player playerTwo, bool isGameActive, List<Move> moveSequence, FileController file)
            : base(grid, playerOne, playerTwo, isGameActive, moveSequence, file)
        {
            AllowedDiscChars = new[] { 'o', 'b', 'e', 'm' };
        }


        public override void CheckBoard(bool suppress = false)
        {
            Grid.IncrementTurnCounter();
            Move move = MoveSequence[^1];
            Console.Clear();
            PrintFrame();
            if (move.Disc.ApplyEffects(ref Grid.Board, move.Lane))
            {
                if (move.Disc.DiscReturn != null)
                {
                    PlayerOne.ReturnDisc(move.Disc.DiscReturn[0]);
                    PlayerTwo.ReturnDisc(move.Disc.DiscReturn[1]);
                }

                Grid.ApplyGravity();
                if (!suppress) Thread.Sleep(500);
                Console.Clear();
                PrintFrame();
                if (Grid.CheckWinCondition())
                {
                    Grid.DecrementTurnCounter();
                    PrintFrame();
                    IsGameActive = false;
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                    return;
                }
            }
        }
        
        public override void Reset()
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


    }
}
