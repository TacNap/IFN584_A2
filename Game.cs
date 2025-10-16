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

    // Template Method
    public bool PlayTurn(Human player)
    {
        while (true)
        {
            string input = GetInputGame();
            if (string.IsNullOrEmpty(input))
            {
                io.PrintError("Please enter a valid move or command.");
                continue;
            }

            if (TryHandleCommand(input))
                return false;


            if (!TryParseMove(input, out int lane))
                return false;

            // // At this point, its valid input
            Disc disc = CreateDisc(input[0], Grid.TurnCounter % 2 == 1 ? true : false);
            if (!player.HasDiscRemaining(disc))
            {
                io.PrintError("No Disc of that type remaining");
                continue;
            }

            // // At this point, we have a disc and know its within balance.
            // // Try to add the disc. If it fails, its because the lane is full.
            if (!Grid.AddDisc(disc, lane))
            {
                //Move fails
                io.PrintError("Error: Lane is full");
                continue;
            }
            else
            {
                // Successful move
                player.WithdrawDisc(disc);
                Grid.DrawGrid();
                if (disc.ApplyEffects(ref Grid.Board, lane))
                {
                    Grid.ApplyGravity();
                    Grid.DrawGrid();
                }
                return true;
            }
        }
    }

    // Will become a template method later 
    public abstract bool PlayTurn(Computer player);

    public abstract void GameLoop();
    public virtual Disc CreateDisc(char discType, bool isPlayerOne)
    {
        Disc disc = char.ToLower(discType) switch
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