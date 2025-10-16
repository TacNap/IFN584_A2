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
        // 0 Degrees
        grid.AddDisc(od, 0);
        grid.AddDisc(ed, 0);
        grid.AddDisc(od2, 0);
        grid.AddDisc(ed2, 0);
        grid.DrawGrid();

        // 90 Degrees
        Console.WriteLine("90 Degrees");
        grid.IncrementOrientation();
        grid.ApplyGravity();
        grid.DrawGridBaseline();

        // 180 Degrees
        Console.WriteLine("180 Degrees");
        grid.IncrementOrientation();
        grid.ApplyGravity();
        grid.DrawGridBaseline();

        // 270 Degrees
        Console.WriteLine("270 Degrees");
        grid.IncrementOrientation();
        grid.ApplyGravity();
        grid.DrawGridBaseline();
        
        
    }

    public void TestGameController()
    {
        GameController gc = new GameController();
        gc.Start();
    }
}


