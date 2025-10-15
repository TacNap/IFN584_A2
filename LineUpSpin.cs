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
    public override void GameLoop()
    {
        Console.WriteLine("< Core Loop starts here! >");
        Console.ReadLine();
    }
}