public class LineUpClassic : Game {
    
    public LineUpClassic(int GridHeight, int GridWidth, bool HvH = true)
    {
        PlayerOne = new Human();
        if (HvH)
        {
            PlayerTwo = new Human();
        } else
        {
            PlayerTwo = new Computer();
        }
        Grid = new Grid(GridHeight, GridWidth);
        io = new IOController();
        file = new FileController();
        MoveSequence = string.Empty;        
    }

    public override void GameLoop()
    {
        // very cool
    }
}