using Newtonsoft.Json;

public class LineUpSpin : Game
{
    public LineUpSpin(bool HvH = true)
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
        
        // ADD THIS: Initialize computer strategy
        computerStrategy = new BasicComputerStrategy();
    }
    
    [JsonConstructor]
    public LineUpSpin(Grid grid, Player playerOne, Player playerTwo, bool isGameActive, List<string> moveSequence, FileController file)
        : base(grid, playerOne, playerTwo, isGameActive, moveSequence, file)
    {
        // Strategy is initialized in base constructor
    }

    private void CheckSpin()
    {
        if (Grid.TurnCounter % 5 == 0)
        {
            Console.WriteLine("\n*** SPIN! The grid rotates 90° clockwise! ***");
            Grid.Spin();
        }
    }
    
    // REMOVE THIS - now implemented in Game.cs
    // public override bool ComputerTurn(Player player)
    // {
    //     throw new NotImplementedException();
    // }

    public override void GameLoop()
    {
        while (IsGameActive)
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
                if (Grid.CheckWinCondition())
                {
                    IsGameActive = false;
                    break;
                }
                Grid.IncrementTurnCounter();
                CheckSpin();  // Check spin AFTER incrementing turn
            }
        }
    }
}