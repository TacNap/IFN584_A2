public class LineUpClassic : Game {

    public LineUpClassic(int GridHeight, int GridWidth, bool HvH = true)
    {
        Grid = new Grid(GridHeight, GridWidth);
        int discBalance = (GridHeight * GridWidth / 2) + 4;
        PlayerOne = new Human(discBalance);
        if (HvH)
        {
            PlayerTwo = new Human(discBalance);
        }
        else
        {
            PlayerTwo = new Computer(discBalance);
        }
        io = new IOController();
        file = new FileController();
        MoveSequence = string.Empty;
    }
    public override string GetInputGame()
    {
        throw new NotImplementedException();
    }
    public override bool PlayTurn(Human player)
    {
        throw new NotImplementedException();
    }
    public override bool PlayTurn(Computer player)
    {
        throw new NotImplementedException();
    }

    public override void GameLoop()
    {
        Console.WriteLine("< Core Loop starts here! >");
        Console.ReadLine();
    }
}