namespace LineUp2
{
    public static class IOController
    {

        public static void PrintGameBanner()
        {
            PrintGreen("╔═══════════════════════════════════════════════════════════════╗\n");
            PrintGreen("║                          LineUp                               ║\n");
            PrintGreen("╚═══════════════════════════════════════════════════════════════╝\n");
        }

        public static void PrintDiscInventory(Dictionary<string, int> discBalance)
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");

            foreach (var disc in discBalance)
            {
                Console.Write("║ ");
                PrintYellow($"{disc.Key,-12}");
                Console.Write(": ");
                PrintCyan($"{disc.Value,2}");
                Console.WriteLine(new string(' ', 46) + "║");
            }

            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        }
        
        /// <summary>
        /// Displays current game status information including turn, game mode, player mode, and win condition
        /// </summary>
        public static void PrintGameStatus(Grid grid, int winLength)//Grid grid, GameConfig.PlayerMode playerMode, GameConfig.GameMode gameMode, int winLength
        {
            int boxWidth = 64; // Total width of the box
            
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            
            // Line 1: Turn and win info
            string currentPlayer = grid.TurnCounter % 2 == 1 ? "Player One" : "Player Two";
            string line1 = $"Turn: {grid.TurnCounter} | {currentPlayer} | Win: {winLength} in a row";
            int line1Padding = boxWidth - line1.Length - 3; // -3 for "║ " and " ║"
            
            Console.Write("║ ");
            PrintYellow("Turn: ");
            Console.Write($"{grid.TurnCounter} | {currentPlayer} | Win: {winLength} in a row");
            Console.Write(new string(' ', Math.Max(0, line1Padding)));
            Console.WriteLine(" ║");
            
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
        }

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
        /// Changes text colour to cyan
        /// </summary>
        public static void PrintCyan(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Changes text colour to yellow
        /// </summary>
        public static void PrintYellow(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
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
            PrintGreen("Developed by: Matthew, Trieu, Yee Wei, Linh, Sanjika\n\n");
            PrintGreen("Please enter one of the following commands:\n");
            PrintGreen("[1] New Game\n");
            PrintGreen("[2] Load Game\n");
            PrintGreen("[3] Test mode\n");
            PrintGreen("[4] Help\n");
            PrintGreen("[5] Quit\n");
        }

        public static void PrintBannerNewGame()
        {
            PrintGreen("╔═══════════════════════════════════════╗\n");
            PrintGreen("║                New Game               ║\n");
            PrintGreen("╚═══════════════════════════════════════╝\n");
        }

        /// <summary>
        /// Displays comprehensive help information for the game
        /// </summary>
        public static void PrintHelp()
        {
            Console.Clear();
            PrintGreen("╔═══════════════════════════════════════════════════════════════════╗\n");
            PrintGreen("║                         LINEUP - HELP MENU                        ║\n");
            PrintGreen("╚═══════════════════════════════════════════════════════════════════╝\n\n");

            // Game Overview
            PrintCyan("═══ GAME OVERVIEW ═══\n");
            Console.WriteLine("LineUp is a strategic disc-dropping game similar to Connect Four.");
            Console.WriteLine("Players take turns dropping discs into columns, trying to create");
            Console.WriteLine("a line of their discs (horizontal, vertical, or diagonal).\n");

            // Game Modes
            PrintCyan("═══ GAME MODES ═══\n");
            PrintYellow("Classic Mode:\n");
            Console.WriteLine("  • Customizable grid size");
            Console.WriteLine("  • Multiple disc types with special abilities");
            Console.WriteLine("  • Win by connecting discs in a line\n");

            PrintYellow("Basic Mode:\n");
            Console.WriteLine("  • Fixed 8x9 grid");
            Console.WriteLine("  • Only ordinary discs available");
            Console.WriteLine("  • Classic connect-the-discs gameplay\n");

            PrintYellow("Spin Mode:\n");
            Console.WriteLine("  • Fixed 8x9 grid");
            Console.WriteLine("  • Board rotates 90° clockwise every 5 turns");
            Console.WriteLine("  • Discs fall based on current orientation\n");

            // Disc Types
            PrintCyan("═══ DISC TYPES (Classic Mode) ═══\n");

            PrintYellow("Ordinary Disc (O/o):\n");
            Console.WriteLine("  Symbol: @ (Player 1), # (Player 2)");
            Console.WriteLine("  • Standard disc with no special effects");
            Console.WriteLine("  • Most abundant disc type\n");

            PrintYellow("Boring Disc (B/b):\n");
            Console.WriteLine("  Symbol: B (Player 1), b (Player 2)");
            Console.WriteLine("  • Drills through the entire lane");
            Console.WriteLine("  • Removes all discs in that column");
            Console.WriteLine("  • Converts to ordinary disc at the bottom");
            Console.WriteLine("  • Limited: 2 per player\n");

            PrintYellow("Exploding Disc (E/e):\n");
            Console.WriteLine("  Symbol: E (Player 1), e (Player 2)");
            Console.WriteLine("  • Destroys itself and all adjacent discs");
            Console.WriteLine("  • Affects 3x3 area around the disc");
            Console.WriteLine("  • Only triggers when matching symbol is played");
            Console.WriteLine("  • Limited: 2 per player\n");

            PrintYellow("Magnetic Disc (M/m):\n");
            Console.WriteLine("  Symbol: M (Player 1), m (Player 2)");
            Console.WriteLine("  • Attracts nearest ally disc in the same lane");
            Console.WriteLine("  • Pulls that disc one position closer");
            Console.WriteLine("  • Converts to ordinary disc after effect");
            Console.WriteLine("  • Limited: 2 per player\n");

            // How to Play
            PrintCyan("═══ HOW TO PLAY ═══\n");
            Console.WriteLine("1. Enter your move using the format: [disc type][lane number]");
            Console.WriteLine("   Examples: o3 (ordinary disc in lane 3)");
            Console.WriteLine("            b5 (boring disc in lane 5)");
            Console.WriteLine("            e7 (exploding disc in lane 7)\n");

            Console.WriteLine("2. Lane numbers are shown at the top of the grid");
            Console.WriteLine("3. Your disc will drop to the lowest available position");
            Console.WriteLine("4. Win by connecting enough discs in a row (based on grid size)\n");

            // In-Game Commands
            PrintCyan("═══ IN-GAME COMMANDS ═══\n");
            PrintYellow("/save\n");
            Console.WriteLine("  • Saves the current game state to a file");
            Console.WriteLine("  • Files stored in 'Saves' directory with timestamp\n");

            PrintYellow("/undo\n");
            Console.WriteLine("  • Undo the last two moves (both players)");
            Console.WriteLine("  • Must have at least 2 moves played\n");

            PrintYellow("/redo\n");
            Console.WriteLine("  • Redo previously undone moves\n");

            PrintYellow("/help\n");
            Console.WriteLine("  • Display this help menu\n");

            PrintYellow("/quit\n");
            Console.WriteLine("  • Exit the current game and return to main menu\n");

            // Win Condition
            PrintCyan("═══ WIN CONDITION ═══\n");
            Console.WriteLine("Connect a line of your discs equal to 10% of the total grid size");
            Console.WriteLine("(rounded down). Lines can be horizontal, vertical, or diagonal.\n");
            Console.WriteLine("Example: On a 6x7 grid (42 spaces), win length = 4 discs\n");

            // Tips
            PrintCyan("═══ STRATEGY TIPS ═══\n");
            Console.WriteLine("• Plan ahead - consider how your opponent might respond");
            Console.WriteLine("• Save special discs for critical moments");
            Console.WriteLine("• In Spin mode, anticipate how the board rotation will affect play");
            Console.WriteLine("• Use exploding discs to disrupt opponent's formations");
            Console.WriteLine("• Boring discs can clear troublesome columns\n");

            PrintGreen("╔═══════════════════════════════════════════════════════════════════╗\n");
            PrintGreen("║              Press ENTER to return to the main menu               ║\n");
            PrintGreen("╚═══════════════════════════════════════════════════════════════════╝\n");

            Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        /// Displays in-game help (shorter version for quick reference)
        /// </summary>
        public static void PrintInGameHelp()
        {
            Console.Clear();
            PrintGreen("╔═══════════════════════════════════════╗\n");
            PrintGreen("║             QUICK HELP                ║\n");
            PrintGreen("╚═══════════════════════════════════════╝\n\n");

            PrintCyan("MOVE FORMAT: [disc][lane]\n");
            Console.WriteLine("  o3 = ordinary disc, lane 3\n");

            PrintCyan("DISC TYPES:\n");
            Console.WriteLine("  O - Ordinary (standard disc)");
            Console.WriteLine("  B - Boring (drills lane)");
            Console.WriteLine("  E - Exploding (destroys 3x3)");
            Console.WriteLine("  M - Magnetic (pulls ally disc)\n");

            PrintCyan("COMMANDS:\n");
            Console.WriteLine("  /save  - Save game");
            Console.WriteLine("  /undo  - Undo last move");
            Console.WriteLine("  /quit  - Exit game");
            Console.WriteLine("  /help  - Show full help\n");

            PrintGreen("Press ENTER to continue...\n");
            Console.ReadLine();
            Console.Clear();
        }


        public static void PrintSaveFiles(string[] saveFiles)
        {
            PrintGreen("╔═══════════════════════════════════════╗\n");
            PrintGreen("║             Load Game                 ║\n");
            PrintGreen("╚═══════════════════════════════════════╝\n");
            Console.WriteLine("Please input the number of the file you'd like to load:");
            for (int i = 0; i < saveFiles.Length; i++)
            {
                PrintGreen($"[{i + 1}] ");
                Console.WriteLine($"{Path.GetFileName(saveFiles[i])}");
            }
        }

        public static void PrintWinner(bool P1, bool P2)
        {
            Console.Clear();
            if (P1 && P2)
            {
                PrintCyan("╔═══════════════════════════════════════════════════════════════╗\n");
                PrintCyan("║                          It's a Tie!                          ║\n");
                PrintCyan("╚═══════════════════════════════════════════════════════════════╝\n\n");
            }
            else if (P1)
            {
                PrintGreen("╔═══════════════════════════════════════════════════════════════╗\n");
                PrintGreen("║                          Player One Wins!                     ║\n");
                PrintGreen("╚═══════════════════════════════════════════════════════════════╝\n\n");
            }
            else
            {
                PrintGreen("╔═══════════════════════════════════════════════════════════════╗\n");
                PrintGreen("║                          Player Two Wins!                     ║\n");
                PrintGreen("╚═══════════════════════════════════════════════════════════════╝\n");
            }
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
                    if (num > 0 && num < 6)
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

        public static string? GetInputLoad(string[] saveFiles)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            try
            {
                int num = int.Parse(input);
                if (num < 1 || num > saveFiles.Length)
                {
                    PrintError($"Error: Input must be between 1 and {saveFiles.Length}\n");
                    Thread.Sleep(1000);
                    return null;
                }
                return saveFiles[num - 1];
            }
            catch (Exception e)
            {
                PrintError($"Error: {e.Message}\n");
                Thread.Sleep(1000);
                return null;
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
            if (config.SelectedGameMode == GameConfig.GameMode.Classic)
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
                if (ChangeGrid)
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
}
