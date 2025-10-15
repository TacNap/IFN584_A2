using System.Reflection.Metadata;

Testing test = new Testing();
test.TestGrid();
//test.TestGameController();

public class Testing()
{
    public void TestGrid()
    {
        Grid grid = new Grid(7, 8);
        OrdinaryDisc od = new OrdinaryDisc(true);
        OrdinaryDisc od2 = new OrdinaryDisc(false);
        ExplodingDisc ed = new ExplodingDisc(true);
        ExplodingDisc ed2 = new ExplodingDisc(false);
        grid.Board[0][0] = od;
        grid.Board[0][1] = od2;
        grid.Board[0][2] = ed;
        grid.Board[0][3] = ed2;
        Console.WriteLine("0 Degrees");
        grid.DrawGrid();

        Console.WriteLine("90 Degrees");
        grid.IncrementOrientation();
        grid.DrawGrid();

        Console.WriteLine("180 Degrees");
        grid.IncrementOrientation();
        grid.DrawGrid();

        Console.WriteLine("270 Degrees");
        grid.IncrementOrientation();
        grid.DrawGrid();
    }

    public void TestGameController()
    {
        GameController gc = new GameController();
        gc.Start();
    }
}


