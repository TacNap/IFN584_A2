using System.Reflection.Metadata;

Testing test = new Testing();
//test.TestGrid();
test.TestGameController();

public class Testing()
{
    public void TestGrid()
    {
    }

    public void TestGameController()
    {
        GameController gc = new GameController();
        gc.Start();
    }
}


