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
        file = new FileController();
        MoveSequence = [];
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