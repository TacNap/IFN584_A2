using Newtonsoft.Json;

public class LineUpClassic : Game
{
    public LineUpClassic(int GridHeight, int GridWidth, bool HvH = true)
    {
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
        // Strategy is initialized in base constructor
    }

    public override bool TryParseMove(string input, out int lane)
    {
        lane = 0;
        string validChar = "obem";
        if (!validChar.Contains(input[0]))
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

            return true;
        }
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