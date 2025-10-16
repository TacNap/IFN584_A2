public static class IOController
{
    /// <summary>
    /// Changes text colour to green
    /// </summary>
    /// Needs to be added to class diagram
    public static void PrintGreen(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(text);
        Console.ResetColor();
    }

    /// <summary>
    /// Prints text with a red background
    /// </summary>
    public static void PrintError(string text)
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
    public static void PrintSuccess(string text)
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(text);
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void PrintMenu()
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

    public static void PrintBannerNewGame()
    {
        PrintGreen("╔═══════════════════════════════════════╗\n");
        PrintGreen("║                New Game               ║\n");
        PrintGreen("╚═══════════════════════════════════════╝\n");
    }
    
    public static void PrintWinner(bool P1, bool P2)
    {
        if (P1 && P2)
        {
            PrintGreen("It's a tie!");
        }
        else if (P1)
        {
            PrintGreen("Player One Wins");
        }
        else
        {
            PrintGreen("Player Two Wins");
        }

        Console.Read();
    }

    public static int GetInputMenu()
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
    
    // This should be broken into 3 methods later.
    // too beefy atm
    public static GameConfig GetInputNewGame()
    {
        PrintBannerNewGame();
        GameConfig config = new GameConfig();

        // ## Get Game Mode 
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
            }
            else
            {
                PrintError("Error: Must be a number");
            }
        }

        // ## Get Player Mode  
        while (true)
        {
            // Get Input
            PrintGreen("Which Player Mode?\n");
            PrintGreen("[1] HvH\n");
            PrintGreen("[2] HvC\n");
            Console.Write("> ");
            string? input = Console.ReadLine();
            int num;

            // Validate it
            bool success = int.TryParse(input, out num);
            if (success)
            {
                if (num < 1 || num > 2)
                {
                    PrintError("Error: Invalid Choice");
                    continue;
                }

                // Convert into PlayerMode value
                GameConfig.PlayerMode mode = GameConfig.PlayerMode.HvH;
                switch (num)
                {
                    case 1:
                        mode = GameConfig.PlayerMode.HvH;
                        break;
                    case 2:
                        mode = GameConfig.PlayerMode.HvC;
                        break;
                }

                // GameConfig value gets set here
                if (!config.SetPlayerMode(mode))
                {
                    PrintError("Error: Unsupported Player Mode");
                    continue;
                }
                break;
            }
            else
            {
                PrintError("Error: Must be a number");
            }
        }
        
        // Get Grid Size, if game mode is Classic
        if(config.SelectedGameMode == GameConfig.GameMode.Classic)
        {
            // Check if they want to deviate from default
            bool ChangeGrid = false;
            while (true)
            {
                // Get Input
                PrintGreen("Change Grid Size? [ Default : 6 x 7 ]\n");
                PrintGreen("[1] Yes\n");
                PrintGreen("[2] No\n");
                Console.Write("> ");
                string? input = Console.ReadLine();
                int num;

                // Validate it
                bool success = int.TryParse(input, out num);
                if (success)
                {
                    if (num < 1 || num > 2)
                    {
                        PrintError("Error: Invalid Choice");
                        continue;
                    }
                    if (num == 1)
                    {
                        ChangeGrid = true;
                        break;
                    }
                    break;
                }
                else
                {
                    PrintError("Error: Must be a number");
                }
            }

            // If yes, get dimensions
            if(ChangeGrid)
            {
                while (true)
                {
                    // Get Input
                    PrintGreen("Enter Rows [Min 1, Max 10]:\n");
                    Console.Write("> ");
                    string? input = Console.ReadLine();
                    int num;

                    // Validate it
                    bool success = int.TryParse(input, out num);
                    if (success)
                    {
                        if (!config.SetGridHeight(num))
                        {
                            PrintError("Error: Invalid value");
                            continue;
                        }
                        break;
                    }
                    else
                    {
                        PrintError("Error: Must be a number");
                    }
                }
                
                while (true)
                {
                    // Get Input
                    PrintGreen($"Enter Columns [Min {config.GridHeight}, Max 10]:\n");
                    Console.Write("> ");
                    string? input = Console.ReadLine();
                    int num;

                    // Validate it
                    bool success = int.TryParse(input, out num);
                    if (success)
                    {
                        if (!config.SetGridWidth(num))
                        {
                            PrintError("Error: Invalid value");
                            continue;
                        }
                        break;
                    }
                    else
                    {
                        PrintError("Error: Must be a number");
                    }
                }
            }
        }
        return config;
    }
}
