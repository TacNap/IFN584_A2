using System.Dynamic;

public abstract class Game
{
    // Core Components
    public Grid Grid { get; set; }
    public Player PlayerOne { get; set; }
    public Player PlayerTwo { get; set; }

    public bool IsGameActive { get; set; }

    public string MoveSequence { get; set; }
    public IOController io { get; set; }
    public FileController file { get; set; }

    public abstract void GameLoop();
    public virtual Disc CreateDisc(string discType, bool isPlayerOne)
    {
        Disc disc = discType.ToLower() switch
        {
            "ordinary" => new OrdinaryDisc(),
            "boring" => new BoringDisc(),
            "exploding" or "explosive" => new ExplodingDisc(),
            "magnetic" => new MagneticDisc(),
            _ => throw new ArgumentException($"Invalid disc type: {discType}")
        };
        // Get symbol based on player

        return disc;
    }

    public virtual void ResetGame()
    {
        // Grid.ResetGrid(); //need to create a resetgrid method in grid class
        // PlayerOne.ResetPlayer();
        // PlayerTwo.ResetPlayer();
        MoveSequence = string.Empty;
    }

// Determines whether the game has ended (win or tie).
public virtual bool IsGameOver()
{
    return Grid.CheckWinCondition() || Grid.IsTieGame();
}
}