public class LineUpBasic : Game {
    
    // Constructor
    public LineUpBasic(int GridHeight, int GridWidth, bool HvH = true)
    {
        // Create the grid
        Grid = new Grid(GridHeight, GridWidth);
        // Define the number of starting discs
        int discBalance = (GridHeight * GridWidth / 2) + 4; 
        // Create the player objects
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
        file = new FileController();
    }
    public override void GameLoop()
    {
        while(IsGameActive)
        {
            // DrawGrid
            Console.WriteLine("< Grid should be drawn here >");
            if (Grid.IsTieGame(PlayerOne, PlayerTwo))
            {
                io.PrintWinner(true, true);
                IsGameActive = false;
                break;
            }

            if (Grid.TurnCounter % 2 == 0)
            {
                PlayerOne.PlayTurn();
            }
            else
            {
                PlayerTwo.PlayTurn();
            }
        }
    }
}