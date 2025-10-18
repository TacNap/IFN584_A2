using Newtonsoft.Json;

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

        // Strategy is initialized in base constructor
    }


    public override void CheckBoard()
    {
        if (Grid.CheckWinCondition())
        {
            IsGameActive = false;
            return;
        }
        Grid.IncrementTurnCounter();
    }

    
}
