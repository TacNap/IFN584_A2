public class LineUpSpin : Game {

    // Constructor
    public LineUpSpin(bool HvH = true)
    {
        int fixedRows = 8;
        int fixedCols = 9;
        int discBalance = (fixedRows * fixedCols / 2) + 4;
        PlayerOne = new Human(discBalance);
        if (HvH)
        {
            PlayerTwo = new Human(discBalance);
        }
        else
        {
            PlayerTwo = new Computer(discBalance);
        }
        Grid = new Grid(fixedRows, fixedCols);
        io = new IOController();
        file = new FileController();
        MoveSequence = string.Empty;
    }
    public override string GetInputGame()
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


    public override bool PlayTurn(Computer player)
    {
        throw new NotImplementedException();
    }

    private void Spin()
    {
        if (Grid.TurnCounter % 5 == 0)
            Grid.Spin();
    }
    public override void GameLoop()
    {
        while(IsGameActive)
        {
            Grid.DrawGrid();

            // Check if both players have discs remaining
            if (Grid.IsTieGame(PlayerOne, PlayerTwo))
            {
                io.PrintWinner(true, true);
                IsGameActive = false;
                break;
            }

            // Holds a reference to the current player, based on turn number
            Player activePlayer = Grid.TurnCounter % 2 == 1 ? PlayerOne : PlayerTwo;

            // NOT IDEAL
            // For true polymorphism, PlayTurn needs to exist on the Player object. 
            // Which would mean the entire Game object also needs to be passed in...
            bool success = activePlayer switch
            {
                Human h => PlayTurn(h),
                Computer c => PlayTurn(c),
                _ => throw new ArgumentException("Unknown player type")
            };

            // > Check Win Condition Here
            Spin();
            if (success)
                Grid.IncrementTurnCounter();
        }
    }
}