public class IOController
{
    /// <summary>
    /// Changes text colour to green
    /// </summary>
    /// Needs to be added to class diagram
    public void PrintGreen(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(text);
        Console.ResetColor();
    }

    /// <summary>
    /// Prints text with a red background
    /// </summary>
    public void PrintError(string text)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(text);
        Console.ResetColor();
        Console.WriteLine();
    }

    /// <summary>
    /// Prints text with a green background
    /// </summary>
    public void PrintSuccess(string text)
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(text);
        Console.ResetColor();
        Console.WriteLine();
    }

    public void PrintMenu()
    {
        PrintGreen("╔═══════════════════════════════════════╗\n");
        PrintGreen("║           Welcome to LineUp           ║\n");
        PrintGreen("╚═══════════════════════════════════════╝\n");
        PrintGreen("Please enter one of the following commands:\n");
        PrintGreen("[1] New Game\n");
        PrintGreen("[2] Load Game\n");
        PrintGreen("[3] Help\n");
        PrintGreen("[4] Quit\n");
    }

    public void PrintBannerNewGame()
    {
        PrintGreen("╔═══════════════════════════════════════╗\n");
        PrintGreen("║                New Game               ║\n");
        PrintGreen("╚═══════════════════════════════════════╝\n");
    }
    
    public void PrintWinner(bool P1, bool P2)
    {
        Console.WriteLine("[Run]\t IOController | PrintWinner");
    }

    public int GetInputMenu()
    {
        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            int num;
            bool success = int.TryParse(input, out num);
            if (success)
            {
                if (num > 0 && num < 5)
                {
                    return num;
                }
                else
                {
                    PrintError("Invalid!");
                    continue;
                }
            }
            else
            {
                PrintError("Must be a number!");
            }

        }
    }
    
    // ** this function is pretty bloated. Could be separated
    // Also hard-coded for enum values. Coupled problem. 
    // Is there a more dynamic way of converting int's into enum values?
    public GameConfig GetInputNewGame()
    {
        PrintBannerNewGame();
        GameConfig config = new GameConfig();
        
        // Get selected game mode 
        while (true)
        {
            // Get Input
            PrintGreen("Which Game Mode?\n");
            PrintGreen("[1] Classic\n");
            PrintGreen("[2] Basic\n");
            PrintGreen("[3] Spin\n");
            Console.Write("> ");
            string? input = Console.ReadLine();
            int num;

            // Validate it
            bool success = int.TryParse(input, out num);
            if (success)
            {
                if (num < 1 || num > 3)
                {
                    PrintError("Error: Invalid Choice");
                    continue;
                }

                // Convert into GameMode value
                GameConfig.GameMode mode = GameConfig.GameMode.Classic;
                switch (num)
                {
                    case 1:
                        mode = GameConfig.GameMode.Classic;
                        break;
                    case 2:
                        mode = GameConfig.GameMode.Basic;
                        break;
                    case 3:
                        mode = GameConfig.GameMode.Spin;
                        break;
                }
                
                // GameConfig value gets set here
                if (!config.SetGameMode(mode))
                {
                    PrintError("Error: Unsupported Game Mode");
                    continue;
                }
                break;
            } else
            {
                PrintError("Error: Must be a number");
            }
        }
        return config;
    }
}
