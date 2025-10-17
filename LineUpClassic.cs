using Newtonsoft.Json;

public class LineUpClassic : Game {

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
    public override bool TryParseMove(string input, out int lane)
    {
        lane = 0;
        // check if the input is valid for a move
        // check if the type is allowed
        // check if lane numbers are within reason. use orientation
        // extract and IntParse lane.
        // print errors as necessary
        return false;
    }
    public override void GameLoop()
    {
        Console.WriteLine("< Core Loop starts here! >");
        Console.ReadLine();
    }

    

    
}
