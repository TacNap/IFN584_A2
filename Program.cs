
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
    public void TestGameController()
    {
        GameController gc = new GameController();
        gc.Start();
    }
}


