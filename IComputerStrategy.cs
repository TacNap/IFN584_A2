// Strategy Interface - defines contract for computer AI
public interface IComputerStrategy
{
    /// <summary>
    /// Selects a move for the computer player
    /// </summary>
    /// <param name="grid">Current game grid</param>
    /// <param name="player">Computer player</param>
    /// <returns>A valid move (disc and lane)</returns>
    Move SelectMove(Grid grid, Player player);
}