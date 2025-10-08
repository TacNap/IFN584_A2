// Testing for Grid
Console.WriteLine("## Grid Testing ##");
Grid grid = new Grid(6, 7);
Console.WriteLine($"Turn Counter     [0]: {grid.TurnCounter}");
grid.IncrementTurnCounter();
Console.WriteLine($"Turn Counter ++  [1]: {grid.TurnCounter}");
grid.DecrementTurnCounter();
Console.WriteLine($"Turn Counter --  [0]: {grid.TurnCounter}");

Console.WriteLine($"Turn Counter Set [True]: {grid.SetTurnCounter(5)}");
Console.WriteLine($"Turn Counter     [5]: {grid.TurnCounter}");

Console.WriteLine($"Turn Counter Set [False]: {grid.SetTurnCounter(-1)}");
Console.WriteLine($"Turn Counter     [5]: {grid.TurnCounter}");

Console.WriteLine($"Read Orientation      [North]: {grid.orientation}");
grid.IncrementOrientation();
Console.WriteLine($"Increment Orientation [East]: {grid.orientation}");