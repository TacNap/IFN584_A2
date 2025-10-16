public class LineUpBasic : Game {

    // Constructor
    // ! This could be made the base, then override for Classic?
    public LineUpBasic(bool HvH = true)
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

    public override bool ComputerTurn(Player player)
    {
        throw new NotImplementedException();
    }

    public override void GameLoop()
    {
        while(IsGameActive)
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
}