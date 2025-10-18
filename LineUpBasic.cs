using Newtonsoft.Json;

public class LineUpBasic : Game
{
    public LineUpBasic(bool HvH = true)
    {
        int fixedRows = 8;
        int fixedCols = 9;
        Grid = new Grid(fixedRows, fixedCols);

        int ordinaryBalance = fixedRows * fixedCols / 2;
        Dictionary<string, int> discBalance = new Dictionary<string, int>
        {
            ["Ordinary"] = ordinaryBalance,
        };

        PlayerOne = new Player(discBalance);
        PlayerTwo = new Player(discBalance, HvH);
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