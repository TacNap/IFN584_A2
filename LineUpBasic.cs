using Newtonsoft.Json;

namespace LineUp2
{
    public class LineUpBasic : Game
    {
        public LineUpBasic(bool HvH = true)
        {
            int fixedRows = 8;
            int fixedCols = 9;
            Grid = new Grid(fixedRows, fixedCols);

            int ordinaryBalance = fixedRows * fixedCols / 2;
            Dictionary<string, int> p1DiscBalance = new Dictionary<string, int>
            {
                ["Ordinary"] = ordinaryBalance,
            };
            Dictionary<string, int> p2DiscBalance = new Dictionary<string, int>
            {
                ["Ordinary"] = ordinaryBalance,
            };

            PlayerOne = new Player(p1DiscBalance);
            PlayerTwo = new Player(p2DiscBalance, HvH);
            IsGameActive = true;
            MoveSequence = [];
            file = new FileController();

            computerStrategy = new BasicComputerStrategy();
        }

        [JsonConstructor]
        public LineUpBasic(Grid grid, Player playerOne, Player playerTwo, bool isGameActive, List<Move> moveSequence, FileController file)
            : base(grid, playerOne, playerTwo, isGameActive, moveSequence, file)
        {
            // Strategy is initialized in base constructor
        }

        public override void CheckBoard(bool suppress = false)
        {
            if (Grid.CheckWinCondition())
            {
                PrintFrame();
                IsGameActive = false;
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
                return;
            }
            Grid.IncrementTurnCounter();
            Console.Clear();
            PrintFrame();
        }
    }
}