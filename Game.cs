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

    public abstract bool PlayTurn(Human player);

    public abstract bool PlayTurn(Computer player);

    public abstract void GameLoop();
    public virtual Disc CreateDisc(string discType, bool isPlayerOne)
    {
        // Disc disc = discType.ToLower() switch
        // {
        //     "ordinary" => new OrdinaryDisc(),
        //     "boring" => new BoringDisc(),
        //     "exploding" or "explosive" => new ExplodingDisc(),
        //     "magnetic" => new MagneticDisc(),
        //     _ => throw new ArgumentException($"Invalid disc type: {discType}")
        // };
        // // Get symbol based on player

        // return disc;
        return null;
    }

    public virtual void ResetGame()
    {
        // Grid.ResetGrid();
        // PlayerOne.ResetPlayer();
        // PlayerTwo.ResetPlayer();
        MoveSequence = string.Empty;
    }
}