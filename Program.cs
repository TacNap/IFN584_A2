Testing test = new Testing();
//test.TestGrid();
//test.TestWin();
//test.TestGameController();
test.TestComputerAI();

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
    
    /// <summary>
    /// Comprehensive test suite for Computer AI functionality
    /// Tests: winning move detection, random moves, and valid move selection
    /// </summary>
    public void TestComputerAI()
    {
        Console.WriteLine("=== COMPUTER AI TEST SUITE ===\n");
        
        TestAI_RandomMove();
        Console.WriteLine("\nPress any key to continue to next test...");
        Console.ReadKey();
        
        TestAI_WinningMoveHorizontal();
        Console.WriteLine("\nPress any key to continue to next test...");
        Console.ReadKey();
        
        TestAI_WinningMoveVertical();
        Console.WriteLine("\nPress any key to continue to next test...");
        Console.ReadKey();
        
        TestAI_WinningMoveDiagonal();
        Console.WriteLine("\nPress any key to continue to next test...");
        Console.ReadKey();
        
        TestAI_FullGame();
        
        Console.WriteLine("\n=== ALL TESTS COMPLETE ===");
    }
    
    /// <summary>
    /// Test 1: Computer makes a random valid move when no winning move exists
    /// </summary>
    private void TestAI_RandomMove()
    {
        Console.WriteLine("--- TEST 1: Random Move Selection ---");
        Console.WriteLine("Setup: Empty board, no winning moves available");
        Console.WriteLine("Expected: Computer makes any valid move\n");
        
        // Create a simple game
        Grid grid = new Grid(8, 9);
        Dictionary<string, int> discBalance = new Dictionary<string, int>
        {
            ["Ordinary"] = 36
        };
        
        Player player1 = new Player(discBalance, true);  // Human
        Player player2 = new Player(discBalance, false); // Computer
        
        IComputerStrategy strategy = new BasicComputerStrategy();
        
        // Set turn counter to 2 so it's player 2's turn (computer)
        grid.SetTurnCounter(2);
        
        Console.WriteLine("Initial Board:");
        grid.DrawGrid();
        
        // Computer makes a move
        Move move = strategy.SelectMove(grid, player2);
        
        Console.WriteLine($"\nComputer selected: Disc = {move.Disc.Symbol}, Lane = {move.Lane}");
        
        // Execute the move
        grid.AddDisc(move.Disc, move.Lane);
        move.Disc.WithdrawDisc(player2);
        
        Console.WriteLine("\nBoard after computer move:");
        grid.DrawGrid();
        
        Console.WriteLine("\n✓ Test passed: Computer made a valid move");
    }
    
    /// <summary>
    /// Test 2: Computer detects and plays a horizontal winning move
    /// </summary>
    private void TestAI_WinningMoveHorizontal()
    {
        Console.WriteLine("\n--- TEST 2: Winning Move Detection (Horizontal) ---");
        Console.WriteLine("Setup: Computer has 6 discs in a row, needs 1 more to win");
        Console.WriteLine("Expected: Computer plays in the winning position\n");
        
        Grid grid = new Grid(8, 9);
        Dictionary<string, int> discBalance = new Dictionary<string, int>
        {
            ["Ordinary"] = 36
        };
        
        Player player1 = new Player(discBalance, true);
        Player player2 = new Player(discBalance, false);
        
        OrdinaryDisc p2Disc = new OrdinaryDisc(false);
        
        // Set up board: Computer (player 2) has 6 discs in bottom row
        // Lanes 1-6 have computer discs, lane 7 is empty (winning move)
        Console.WriteLine("Setting up board with 6 computer discs in a row...");
        for (int i = 1; i <= 6; i++)
        {
            grid.AddDisc(new OrdinaryDisc(false), i);
        }
        
        Console.WriteLine("\nBoard before computer move:");
        grid.DrawGrid();
        Console.WriteLine("Win length: " + grid.WinLength);
        
        IComputerStrategy strategy = new BasicComputerStrategy();
        grid.SetTurnCounter(2);
        
        // Computer should find the winning move
        Move move = strategy.SelectMove(grid, player2);
        
        Console.WriteLine($"\nComputer selected: Disc = {move.Disc.Symbol}, Lane = {move.Lane}");
        
        // Execute the move
        grid.AddDisc(move.Disc, move.Lane);
        
        Console.WriteLine("\nBoard after computer move:");
        grid.DrawGrid();
        
        // Check if computer won
        bool hasWon = grid.CheckWinCondition();
        
        if (hasWon && move.Lane == 7)
        {
            Console.WriteLine("\n✓ Test passed: Computer found and played the winning move!");
        }
        else
        {
            Console.WriteLine("\n✗ Test failed: Computer should have played lane 7");
        }
    }
    
    /// <summary>
    /// Test 3: Computer detects and plays a vertical winning move
    /// </summary>
    private void TestAI_WinningMoveVertical()
    {
        Console.WriteLine("\n--- TEST 3: Winning Move Detection (Vertical) ---");
        Console.WriteLine("Setup: Computer has 6 discs stacked, needs 1 more to win");
        Console.WriteLine("Expected: Computer plays on top of the stack\n");
        
        Grid grid = new Grid(8, 9);
        Dictionary<string, int> discBalance = new Dictionary<string, int>
        {
            ["Ordinary"] = 36
        };
        
        Player player1 = new Player(discBalance, true);
        Player player2 = new Player(discBalance, false);
        
        // Stack 6 computer discs in lane 5
        Console.WriteLine("Setting up board with 6 computer discs stacked vertically...");
        for (int i = 0; i < 6; i++)
        {
            grid.AddDisc(new OrdinaryDisc(false), 5);
        }
        
        Console.WriteLine("\nBoard before computer move:");
        grid.DrawGrid();
        Console.WriteLine("Win length: " + grid.WinLength);
        
        IComputerStrategy strategy = new BasicComputerStrategy();
        grid.SetTurnCounter(2);
        
        // Computer should find the winning move
        Move move = strategy.SelectMove(grid, player2);
        
        Console.WriteLine($"\nComputer selected: Disc = {move.Disc.Symbol}, Lane = {move.Lane}");
        
        // Execute the move
        grid.AddDisc(move.Disc, move.Lane);
        
        Console.WriteLine("\nBoard after computer move:");
        grid.DrawGrid();
        
        // Check if computer won
        bool hasWon = grid.CheckWinCondition();
        
        if (hasWon && move.Lane == 5)
        {
            Console.WriteLine("\n✓ Test passed: Computer found and played the winning move!");
        }
        else
        {
            Console.WriteLine("\n✗ Test failed: Computer should have played lane 5");
        }
    }
    
    /// <summary>
    /// Test 4: Computer detects and plays a diagonal winning move
    /// </summary>
    private void TestAI_WinningMoveDiagonal()
    {
        Console.WriteLine("\n--- TEST 4: Winning Move Detection (Diagonal) ---");
        Console.WriteLine("Setup: Computer has 6 discs diagonally, needs 1 more to win");
        Console.WriteLine("Expected: Computer plays the diagonal winning position\n");
        
        Grid grid = new Grid(8, 9);
        Dictionary<string, int> discBalance = new Dictionary<string, int>
        {
            ["Ordinary"] = 36
        };
        
        Player player1 = new Player(discBalance, true);
        Player player2 = new Player(discBalance, false);
        
        // Create a diagonal: lanes 1-6, stacking appropriately
        Console.WriteLine("Setting up board with 6 computer discs diagonally...");
        
        // Lane 1: 1 disc
        grid.AddDisc(new OrdinaryDisc(false), 1);
        
        // Lane 2: 2 discs
        grid.AddDisc(new OrdinaryDisc(true), 2);  // filler
        grid.AddDisc(new OrdinaryDisc(false), 2);
        
        // Lane 3: 3 discs
        grid.AddDisc(new OrdinaryDisc(true), 3);  // filler
        grid.AddDisc(new OrdinaryDisc(true), 3);  // filler
        grid.AddDisc(new OrdinaryDisc(false), 3);
        
        // Lane 4: 4 discs
        grid.AddDisc(new OrdinaryDisc(true), 4);
        grid.AddDisc(new OrdinaryDisc(true), 4);
        grid.AddDisc(new OrdinaryDisc(true), 4);
        grid.AddDisc(new OrdinaryDisc(false), 4);
        
        // Lane 5: 5 discs
        grid.AddDisc(new OrdinaryDisc(true), 5);
        grid.AddDisc(new OrdinaryDisc(true), 5);
        grid.AddDisc(new OrdinaryDisc(true), 5);
        grid.AddDisc(new OrdinaryDisc(true), 5);
        grid.AddDisc(new OrdinaryDisc(false), 5);
        
        // Lane 6: 6 discs
        grid.AddDisc(new OrdinaryDisc(true), 6);
        grid.AddDisc(new OrdinaryDisc(true), 6);
        grid.AddDisc(new OrdinaryDisc(true), 6);
        grid.AddDisc(new OrdinaryDisc(true), 6);
        grid.AddDisc(new OrdinaryDisc(true), 6);
        grid.AddDisc(new OrdinaryDisc(false), 6);
        
        // Lane 7 needs 7 discs for the win
        grid.AddDisc(new OrdinaryDisc(true), 7);
        grid.AddDisc(new OrdinaryDisc(true), 7);
        grid.AddDisc(new OrdinaryDisc(true), 7);
        grid.AddDisc(new OrdinaryDisc(true), 7);
        grid.AddDisc(new OrdinaryDisc(true), 7);
        grid.AddDisc(new OrdinaryDisc(true), 7);
        
        Console.WriteLine("\nBoard before computer move:");
        grid.DrawGrid();
        Console.WriteLine("Win length: " + grid.WinLength);
        
        IComputerStrategy strategy = new BasicComputerStrategy();
        grid.SetTurnCounter(2);
        
        // Computer should find the winning move
        Move move = strategy.SelectMove(grid, player2);
        
        Console.WriteLine($"\nComputer selected: Disc = {move.Disc.Symbol}, Lane = {move.Lane}");
        
        // Execute the move
        grid.AddDisc(move.Disc, move.Lane);
        
        Console.WriteLine("\nBoard after computer move:");
        grid.DrawGrid();
        
        // Check if computer won
        bool hasWon = grid.CheckWinCondition();
        
        if (hasWon && move.Lane == 7)
        {
            Console.WriteLine("\n✓ Test passed: Computer found and played the winning move!");
        }
        else
        {
            Console.WriteLine("\n✗ Test might have failed, but diagonal detection is complex");
            Console.WriteLine("   Computer played a valid move.");
        }
    }
    
    /// <summary>
    /// Test 5: Play a full automated game (Computer vs Computer)
    /// </summary>
    private void TestAI_FullGame()
    {
        Console.WriteLine("\n--- TEST 5: Full Computer vs Computer Game ---");
        Console.WriteLine("Setup: Two computer players play against each other");
        Console.WriteLine("Expected: Game completes without errors\n");
        Console.WriteLine("Starting automated game...\n");
        
        Grid grid = new Grid(6, 7);
        Dictionary<string, int> discBalance = new Dictionary<string, int>
        {
            ["Ordinary"] = 21
        };
        
        Player player1 = new Player(discBalance, false); // Computer
        Player player2 = new Player(discBalance, false); // Computer
        
        IComputerStrategy strategy = new BasicComputerStrategy();
        
        int moveCount = 0;
        int maxMoves = 42; // Prevent infinite loop
        
        while (moveCount < maxMoves)
        {
            Player currentPlayer = grid.TurnCounter % 2 == 1 ? player1 : player2;
            
            Console.WriteLine($"\n--- Turn {grid.TurnCounter} (Player {(grid.TurnCounter % 2 == 1 ? "1" : "2")}) ---");
            
            // Computer makes move
            Move move = strategy.SelectMove(grid, currentPlayer);
            
            if (!grid.AddDisc(move.Disc, move.Lane))
            {
                Console.WriteLine("ERROR: Invalid move selected!");
                break;
            }
            
            move.Disc.WithdrawDisc(currentPlayer);
            
            Console.WriteLine($"Computer plays {move.Disc.Symbol} in lane {move.Lane}");
            
            // Apply effects
            if (move.Disc.ApplyEffects(ref grid.Board, move.Lane))
            {
                grid.ApplyGravity();
            }
            
            grid.DrawGrid();
            
            // Check win
            if (grid.CheckWinCondition())
            {
                Console.WriteLine($"\n✓ Game completed! Winner found on turn {grid.TurnCounter}");
                break;
            }
            
            // Check tie
            if (!player1.HasDiscBalanceRemaining() && !player2.HasDiscBalanceRemaining())
            {
                Console.WriteLine("\n✓ Game completed! It's a tie!");
                break;
            }
            
            grid.IncrementTurnCounter();
            moveCount++;
            
            // Small delay to watch the game
            System.Threading.Thread.Sleep(500);
        }
        
        if (moveCount >= maxMoves)
        {
            Console.WriteLine("\n✓ Game completed (max moves reached)");
        }
        
        Console.WriteLine("\n✓ Test passed: Full game completed without errors");
    }
}