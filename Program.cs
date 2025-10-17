using System.Text.Json.Nodes;

Testing test = new Testing();
//test.TestGrid();
//test.TestWin();
test.TestGameController();

public class Testing()
{

    public void TestGrid()
    {
        Grid grid = new Grid(8, 9);
        OrdinaryDisc od = new OrdinaryDisc(true);
        OrdinaryDisc od2 = new OrdinaryDisc(false);
        ExplodingDisc ed = new ExplodingDisc(true);
        ExplodingDisc ed2 = new ExplodingDisc(false);
        // 0 Degrees
        grid.AddDisc(od, 1);
        grid.AddDisc(ed, 1);
        grid.AddDisc(od2, 1);
        grid.AddDisc(ed2, 1);
        grid.AddDisc(ed2, 2);
        grid.AddDisc(od2, 2);
        grid.AddDisc(ed, 2);
        grid.AddDisc(od, 2);
        grid.DrawGrid();

        // 90 Degrees
        Console.WriteLine("90 Degrees");
        grid.IncrementOrientation();
        grid.ApplyGravity();
        grid.DrawGrid();

        // 180 Degrees
        Console.WriteLine("180 Degrees");
        grid.IncrementOrientation();
        grid.ApplyGravity();
        grid.DrawGrid();

        // 270 Degrees
        Console.WriteLine("270 Degrees");
        grid.IncrementOrientation();
        grid.ApplyGravity();
        grid.DrawGrid();
    }

    public void TestWin()
    {
        Grid grid = new Grid(8, 9);
        OrdinaryDisc od = new OrdinaryDisc(true);
        OrdinaryDisc ed = new OrdinaryDisc(false);
        grid.AddDisc(od, 1);
        grid.AddDisc(od, 2);
        grid.AddDisc(od, 3);
        grid.AddDisc(od, 4);
        grid.AddDisc(od, 5);
        grid.AddDisc(od, 6);
        grid.AddDisc(od, 7);
        grid.DrawGrid();
        grid.CheckWinCondition();

        grid = new Grid(8, 9);
        grid.AddDisc(od, 1);
        grid.AddDisc(od, 1);
        grid.AddDisc(od, 1);
        grid.AddDisc(od, 1);
        grid.AddDisc(od, 1);
        grid.AddDisc(od, 1);
        grid.AddDisc(od, 1);
        grid.DrawGrid();
        grid.CheckWinCondition();

        grid = new Grid(8, 9);
        grid.AddDisc(od, 1);

        grid.AddDisc(od, 2);
        grid.AddDisc(od, 2);


        grid.AddDisc(od, 3);
        grid.AddDisc(od, 3);
        grid.AddDisc(od, 3);
        grid.AddDisc(od, 4);
        grid.AddDisc(od, 4);
        grid.AddDisc(od, 4);
        grid.AddDisc(od, 4);
        grid.AddDisc(od, 5);
        grid.AddDisc(od, 5);
        grid.AddDisc(od, 5);
        grid.AddDisc(od, 5);
        grid.AddDisc(od, 5);
        grid.AddDisc(od, 6);
        grid.AddDisc(od, 6);
        grid.AddDisc(od, 6);
        grid.AddDisc(od, 6);
        grid.AddDisc(od, 6);
        grid.AddDisc(od, 6);

        grid.AddDisc(ed, 7);
        grid.AddDisc(ed, 7);
        grid.AddDisc(ed, 7);
        grid.AddDisc(ed, 7);
        grid.AddDisc(ed, 7);
        grid.AddDisc(ed, 7);
        grid.AddDisc(od, 7);
        grid.DrawGrid();
        grid.CheckWinCondition();

    }

    public void TestGameController()
    {
        GameController gc = new GameController();
        gc.Start();
    }
}


