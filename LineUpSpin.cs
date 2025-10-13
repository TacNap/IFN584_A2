public class LineUpSpin : Game {

    // Constructor
    public LineUpSpin(int GridHeight, int GridWidth, bool HvH = true)
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
        IOController = new IOController();
        FileController = new FileController();
        MoveSequence = string.Empty;        
    }
    public override void GameLoop()
    {
        // very cool
    }
}