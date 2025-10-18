using Newtonsoft.Json;

public class LineUpSpin : Game {

    // Constructor
    public LineUpSpin(bool HvH = true)
    {
        // ! may want to move this to the factory method in gamecontroller??
        int fixedRows = 8;
        int fixedCols = 9;
        // Create the grid
        Grid = new Grid(fixedRows, fixedCols);

        // Define the number of starting discs
        int ordinaryBalance = fixedRows * fixedCols / 2;
        Dictionary<string, int> discBalance = new Dictionary<string, int>
        {
            ["Ordinary"] = ordinaryBalance,
        };

        // Create the player objects
        PlayerOne = new Player(discBalance);
        PlayerTwo = new Player(discBalance, HvH);
        IsGameActive = true;
        MoveSequence = [];
        file = new FileController();
    }
    
    // Constructor used when loading from file 
    [JsonConstructor]
    public LineUpSpin(Grid grid, Player playerOne, Player playerTwo, bool isGameActive, List<string> moveSequence, FileController file)
        : base(grid, playerOne, playerTwo, isGameActive, moveSequence, file)
    {
    }

    public override bool ComputerTurn(Player player)
    {
        throw new NotImplementedException();
    }
    
    public override void CheckBoard()
    {
        if (Grid.CheckWinCondition())
        {
            IsGameActive = false;
            return;
        }
        // Spin behaviour
        if (Grid.TurnCounter % 5 == 0)
        {
            Grid.Spin(); 
            if (Grid.CheckWinCondition())
            {
                IsGameActive = false;
                return;
            }
        }
        Grid.IncrementTurnCounter();
    }




}
