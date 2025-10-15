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

    public abstract string GetInputGame();

    public bool TryHandleCommand(string input)
    {
        //check if starts with "/"
        // if not, return false
        // otherwise try to run command against a switch statement
        // return true regardless of outcome - attempted to run command
        // Errors printed here
        return false;
    }

    public abstract bool TryParseMove(string input, out int lane);

    public abstract bool PlayTurn(Human player);

    public abstract bool PlayTurn(Computer player);

    public abstract void GameLoop();
    public virtual Disc CreateDisc(char discType, bool isPlayerOne)
    {
        Disc disc = Char.ToLower(discType) switch
        {
            'o' => new OrdinaryDisc(isPlayerOne),
            'b' => new BoringDisc(isPlayerOne),
            'e' => new ExplodingDisc(isPlayerOne),
            'm' => new MagneticDisc(isPlayerOne),
            _ => throw new ArgumentException($"Invalid disc type: {discType}")
        };

        return disc;
    }

    public virtual void ResetGame()
    {
        // Grid.ResetGrid();
        // PlayerOne.ResetPlayer();
        // PlayerTwo.ResetPlayer();
        MoveSequence = string.Empty;
    }
}