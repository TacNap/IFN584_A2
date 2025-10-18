
// Concrete Strategy - Basic AI implementation
public class BasicComputerStrategy : IComputerStrategy
{
    private Random random;

    public BasicComputerStrategy()
    {
        random = new Random();
    }

    public Move SelectMove(Grid grid, Player player)
    {
        // First, try to find a winning move
        Move? winningMove = FindWinningMove(grid, player);
        if (winningMove.HasValue)
        {
            return winningMove.Value;
        }

        // Otherwise, select a random valid move
        return FindRandomMove(grid, player);
    }

    /// <summary>
    /// Attempts to find a move that wins the game immediately
    /// </summary>
    private Move? FindWinningMove(Grid grid, Player player)
    {
        int rows = grid.Board.Length; // unused - can be removed?
        int cols = grid.Board[0].Length;
        bool isPlayerOne = grid.TurnCounter % 2 == 1;

        // Try each disc type the player has
        foreach (var discType in player.DiscBalance)
        {
            // Retrieve the disc type
            char discChar = char.ToLower(discType.Key[0]);

            // Create a test disc
            Disc testDisc = Disc.CreateDisc(discChar, isPlayerOne);

            // Make sure the player has a disc of this type remaining
            if (testDisc.HasDiscRemaining(player)) continue;

            // Try each lane
            for (int lane = 1; lane <= cols; lane++)
            {
                // Check if lane is not full
                if (grid.Board[0][lane - 1] != null) continue;

                // Simulate adding the disc
                if (TrySimulateMove(grid, testDisc, lane, out Grid simulatedGrid))
                {
                    // Check if this move wins
                    if (simulatedGrid.CheckWinCondition())
                    {
                        // Found a winning move!
                        return new Move(Disc.CreateDisc(discChar, isPlayerOne), lane);
                    }
                }
            }
        }

        return null; // No winning move found
    }

    /// <summary>
    /// Selects a random valid move
    /// </summary>
    private Move FindRandomMove(Grid grid, Player player)
    {
        int cols = grid.Board[0].Length;
        bool isPlayerOne = grid.TurnCounter % 2 == 1;

        // Get available disc types
        List<string> availableDiscs = new List<string>();
        foreach (var discType in player.DiscBalance)
        {
            if (discType.Value > 0)
            {
                availableDiscs.Add(discType.Key);
            }
        }

        // Get available lanes (not full)
        List<int> availableLanes = new List<int>();
        for (int lane = 1; lane <= cols; lane++)
        {
            if (grid.Board[0][lane - 1] == null) // Lane is not full
            {
                availableLanes.Add(lane);
            }
        }

        // Randomly select from available options
        string selectedDiscType = availableDiscs[random.Next(availableDiscs.Count)];
        int selectedLane = availableLanes[random.Next(availableLanes.Count)];

        char discChar = char.ToLower(selectedDiscType[0]);
        Disc selectedDisc = Disc.CreateDisc(discChar, isPlayerOne);

        return new Move(selectedDisc, selectedLane);
    }

    /// <summary>
    /// Simulates a move without modifying the actual grid
    /// Returns a copy of the grid with the move applied
    /// </summary>
    private bool TrySimulateMove(Grid original, Disc disc, int lane, out Grid simulated)
    {
        // Create a deep copy of the grid
        simulated = CopyGrid(original);
        Move move = new Move(disc, lane);

        // Try to add the disc
        if (!simulated.AddDisc(move))
        {
            return false;
        }

        // Apply effects
        disc.ApplyEffects(ref simulated.Board, lane);
        simulated.ApplyGravity();

        return true;
    }

    /// <summary>
    /// Creates a deep copy of a grid for simulation purposes
    /// We have to make a deep copy because when calling `Dics.ApplyEffects`, some discs change their own properties.
    /// </summary>
    private Grid CopyGrid(Grid original)
    {
        int rows = original.Board.Length;
        int cols = original.Board[0].Length;
        
        Grid copy = new Grid(rows, cols);
        
        // Copy board state
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (original.Board[i][j] != null)
                {
                    Disc originalDisc = original.Board[i][j];
                    copy.Board[i][j] = originalDisc.Clone();
                }
            }
        }

        // Copy other grid properties
        copy.SetTurnCounter(original.TurnCounter);

        return copy;
    }
}
