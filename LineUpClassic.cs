using Newtonsoft.Json;

public class LineUpClassic : Game
{
    private void ConfigureAllowedDiscs()
    {
        AllowedDiscChars = new[] { 'o', 'b', 'e', 'm' };
    }

    public LineUpClassic(int GridHeight, int GridWidth, bool HvH = true)
    {
        ConfigureAllowedDiscs();

        Grid = new Grid(GridHeight, GridWidth);
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
    
        computerStrategy = new BasicComputerStrategy();
    }

    [JsonConstructor]
    public LineUpClassic(Grid grid, Player playerOne, Player playerTwo, bool isGameActive, List<string> moveSequence, FileController file)
        : base(grid, playerOne, playerTwo, isGameActive, moveSequence, file)
    {
        ConfigureAllowedDiscs();

        // Strategy is initialized in base constructor
    }

    public override void GameLoop()
    {
        while(IsGameActive)
        {
            PrintPlayerData();
            Grid.DrawGrid();

            if (Grid.IsTieGame(PlayerOne, PlayerTwo))
            {
                IOController.PrintWinner(true, true);
                IsGameActive = false;
                break;
            }

            Player activePlayer = Grid.TurnCounter % 2 == 1 ? PlayerOne : PlayerTwo;

            bool successfulMove = activePlayer.IsHuman ? PlayerTurn(activePlayer) : ComputerTurn(activePlayer);

            if (successfulMove)
            {
                if(Grid.CheckWinCondition())
                {
                    IsGameActive = false;
                    break;
                }
                Grid.IncrementTurnCounter();
            }
        }
    }

    public override void PrintPlayerData()
    {
        Console.WriteLine("--------------");
        Player player = Grid.TurnCounter % 2 == 1 ? PlayerOne : PlayerTwo;
        Console.WriteLine($"Player {(Grid.TurnCounter % 2 == 1 ? "One" : "Two")} Discs:");
        Console.WriteLine($"  Ordinary: {player.DiscBalance["Ordinary"]}");
        Console.WriteLine($"  Boring: {player.DiscBalance["Boring"]}");
        Console.WriteLine($"  Exploding: {player.DiscBalance["Exploding"]}");
        Console.WriteLine($"  Magnetic: {player.DiscBalance["Magnetic"]}");
    }
}
