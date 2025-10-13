public class LineUpBasic : Game {
    
    // Constructor
    public LineUpBasic(int GridHeight, int GridWidth, bool HvH = true)
    {
        Grid = new Grid(GridHeight, GridWidth);
        int discBalance = (GridHeight * GridWidth / 2) + 4; // Defines the starting number of discs
        PlayerOne = new Human(discBalance);
        if (HvH)
        {
            PlayerTwo = new Human(discBalance);
        }
        else
        {
            PlayerTwo = new Computer(discBalance);
        }
        IsGameActive = true;
        MoveSequence = string.Empty;        
        io = new IOController();
        FileController = new FileController();
    }
    public override void GameLoop()
    {
        while(IsGameActive)
        {
            if(Grid.IsTieGame(PlayerOne, PlayerTwo))
            {
                io.PrintWinner(true, true);
                IsGameActive = false;
                break;
            }
        }
    }
}