
﻿using System.Text.Json.Nodes;

Testing test = new Testing();
//test.TestGrid();
//test.TestWin();
test.TestGameController();

//test.TestBoring();
//test.TestMagnetic();
//test.TestExploding();

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

    public void TestBoring()
    {
        Grid grid = new Grid(8, 9);
        Disc OD = new OrdinaryDisc(true);
        Disc od = new OrdinaryDisc(false);
        Disc BD = new BoringDisc(true);
        Disc bd = new BoringDisc(false);

        Console.WriteLine("\n\t Boring Test");
        
        grid.AddDisc(OD, 1);
        grid.AddDisc(od, 1);
        grid.AddDisc(OD, 1);
        grid.AddDisc(od, 1);
        
        grid.AddDisc(OD, 2);
        grid.AddDisc(od, 2);
        grid.AddDisc(OD, 2);
        grid.AddDisc(od, 2);
        
        grid.AddDisc(OD, 3);
        grid.AddDisc(od, 3);
        grid.AddDisc(OD, 3);
        grid.AddDisc(od, 3);
        grid.AddDisc(OD, 3);
        grid.AddDisc(od, 3);
        grid.AddDisc(OD, 3);
        grid.AddDisc(od, 3);
        
        grid.AddDisc(OD, 4);
        grid.AddDisc(od, 4);
        grid.AddDisc(OD, 4);
        grid.AddDisc(od, 4);
        
        grid.AddDisc(OD, 5);
        grid.AddDisc(od, 5);
        grid.AddDisc(OD, 5);
        grid.AddDisc(od, 5);
        grid.AddDisc(OD, 5);
        grid.AddDisc(od, 5);
        grid.AddDisc(OD, 5);
        grid.AddDisc(od, 5);
        grid.DrawGrid();

        // Boring
        grid.AddDisc(BD, 1);
        grid.DrawGrid();
        BD.ApplyEffects(ref grid.Board, 1);
        grid.DrawGrid();
    }

    public void TestMagnetic()
    {
        Grid grid = new Grid(8, 9);
        Disc OD = new OrdinaryDisc(true);
        Disc od = new OrdinaryDisc(false);
        Disc MD = new MagneticDisc(true);
        Disc md = new MagneticDisc(false);

        Console.WriteLine("\n\t Magnetic Test");
        
        grid.AddDisc(OD, 1);
        grid.AddDisc(od, 1);
        grid.AddDisc(OD, 1);
        grid.AddDisc(od, 1);
        
        grid.AddDisc(OD, 2);
        grid.AddDisc(od, 2);
        grid.AddDisc(OD, 2);
        grid.AddDisc(od, 2);
        
        grid.AddDisc(OD, 3);
        grid.AddDisc(od, 3);
        grid.AddDisc(OD, 3);
        grid.AddDisc(od, 3);
        grid.AddDisc(OD, 3);
        grid.AddDisc(od, 3);
        grid.AddDisc(OD, 3);
        grid.AddDisc(od, 3);
        
        grid.AddDisc(OD, 4);
        grid.AddDisc(od, 4);
        grid.AddDisc(OD, 4);
        grid.AddDisc(od, 4);
        
        grid.AddDisc(OD, 5);
        grid.AddDisc(od, 5);
        grid.AddDisc(OD, 5);
        grid.AddDisc(od, 5);
        grid.AddDisc(OD, 5);
        grid.AddDisc(od, 5);
        grid.AddDisc(OD, 5);
        grid.AddDisc(od, 5);
        grid.DrawGrid();

        // Magnetic
        grid.AddDisc(MD, 2);
        grid.DrawGrid();
        MD.ApplyEffects(ref grid.Board, 2);
        grid.DrawGrid();
    }

    public void TestExploding()
    {
        Grid grid = new Grid(8, 9);
        Disc OD = new OrdinaryDisc(true);
        Disc od = new OrdinaryDisc(false);
        Disc ED = new ExplodingDisc(true);
        Disc ed = new ExplodingDisc(false);

        Console.WriteLine("\n\t Exploding Test");
        
        grid.AddDisc(OD, 1);
        grid.AddDisc(od, 1);
        grid.AddDisc(OD, 1);
        grid.AddDisc(od, 1);
        
        grid.AddDisc(OD, 2);
        grid.AddDisc(od, 2);
        grid.AddDisc(OD, 2);
        grid.AddDisc(od, 2);
        
        grid.AddDisc(OD, 3);
        grid.AddDisc(od, 3);
        grid.AddDisc(OD, 3);
        grid.AddDisc(od, 3);
        grid.AddDisc(OD, 3);
        grid.AddDisc(od, 3);
        grid.AddDisc(OD, 3);
        grid.AddDisc(od, 3);
        
        grid.AddDisc(OD, 4);
        grid.AddDisc(od, 4);
        grid.AddDisc(OD, 4);
        grid.AddDisc(od, 4);
        
        grid.AddDisc(OD, 5);
        grid.AddDisc(od, 5);
        grid.AddDisc(OD, 5);
        grid.AddDisc(od, 5);
        grid.AddDisc(OD, 5);
        grid.AddDisc(od, 5);
        grid.AddDisc(OD, 5);
        grid.AddDisc(od, 5);
        grid.DrawGrid();


        // Exploding
        grid.AddDisc(ED, 4);
        grid.DrawGrid();
        ED.ApplyEffects(ref grid.Board, 4);
        grid.DrawGrid();
        grid.ApplyGravity();
        grid.DrawGrid();
    }
}


