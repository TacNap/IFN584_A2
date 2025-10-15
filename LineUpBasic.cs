public class LineUpBasic : Game {

    // Constructor
    public LineUpBasic(bool HvH = true)
    {
        int fixedRows = 8;
        int fixedCols = 9;
        // Create the grid
        Grid = new Grid(fixedRows, fixedCols);
        // Define the number of starting discs
        int discBalance = (fixedRows * fixedCols / 2) + 4;
        // Create the player objects
        PlayerOne = new Human(discBalance);
        if (HvH)
        {
            PlayerTwo = new Human(discBalance);
        }
        else
        {
            PlayerTwo = new Computer(discBalance);
        }

        IsGameActive = true;
        MoveSequence = string.Empty;
        io = new IOController();
        file = new FileController();
    }

    public override string GetInputGame()
    {
        Console.WriteLine("Enter move/command");
        Console.Write("> ");
        string input = Console.ReadLine();
        return input;
    }

    public override bool TryParseMove(string input, out int lane)
    {
        lane = 0;
        // check if the input is valid for a move
        // check if the type is allowed for this game mode
        // check if lane numbers are within reason. use orientation
        // extract and IntParse lane.
        // print errors as necessary
        return false;
    }

    // Ideally this could become a template method that sits in the abstract Game class
    public override bool PlayTurn(Human player)
    {
        while(true)
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
            Disc disc = CreateDisc(input[0], Grid.TurnCounter % 2 == 0 ? false : true);
            if(!player.HasDiscRemaining(disc))
            {
                io.PrintError("No Disc of that type remaining");
            }

            // // At this point, we have a disc and know its within balance.
            // // Try to add the disc. If it fails, its because the lane is full.
            if(!Grid.AddDisc(disc, lane))
            {
                //Move fails
                io.PrintError("Error: Lane is full");
                return false;
            } else
            {
                // Successful move
                player.WithdrawDisc(disc);
                Grid.ApplyGravity();
                if (disc.ApplyEffects(Grid.Board, lane))
                {
                    Grid.ApplyGravity();
                    Grid.DrawGrid();
                }
            }
        }
    }
    public override bool PlayTurn(Computer player)
    {
        // Disc = FindWinningMove
        throw new NotImplementedException();
    }

    public override void GameLoop()
    {
        while(IsGameActive)
        {
            // DrawGrid
            Console.WriteLine("< Grid should be drawn here >");
            if (Grid.IsTieGame(PlayerOne, PlayerTwo))
            {
                io.PrintWinner(true, true);
                IsGameActive = false;
                break;
            }

            if (Grid.TurnCounter % 2 == 0)
            {
                PlayerOne.PlayTurn();
            }
            else
            {
                PlayerTwo.PlayTurn();
            }
        }
    }
}