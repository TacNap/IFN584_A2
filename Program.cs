using System.Reflection.Metadata;

Testing test = new Testing();
test.TestGrid();
//test.TestGameController();

public class Testing()
{
    public void TestGrid()
    {
        Grid grid = new Grid(8, 9);
        OrdinaryDisc od = new OrdinaryDisc(true);
        OrdinaryDisc od2 = new OrdinaryDisc(false);
        ExplodingDisc ed = new ExplodingDisc(true);
        ExplodingDisc ed2 = new ExplodingDisc(false);
        grid.Board[7][0] = od;
        grid.Board[7][1] = od2;
        grid.Board[7][2] = ed;
        grid.Board[7][3] = ed2;
        Console.WriteLine("0 Degrees");
        grid.DrawGrid();
        // Console.WriteLine("Add Disc 1st lane");
        // grid.AddDisc(od, 0);
        // grid.DrawGrid();

        Console.WriteLine("90 Degrees");
        grid.IncrementOrientation();
        grid.DrawGrid();
        Console.WriteLine("Add Disc 3rd lane");
        grid.AddDisc(od, 2);
        grid.DrawGrid();

        // Console.WriteLine("180 Degrees");
        // grid.IncrementOrientation();
        // grid.DrawGrid();

        // Console.WriteLine("270 Degrees");
        // grid.IncrementOrientation();
        // grid.DrawGrid();
    }

    public void TestGameController()
    {
        GameController gc = new GameController();
        gc.Start();
    }
}


