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