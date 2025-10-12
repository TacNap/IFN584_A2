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
    
    public void PrintWinner(bool P1, bool P2)
    {
        Console.WriteLine("[Run]\t IOController | PrintWinner");
    }
    
    public string GetInputMenu()
    {
        Console.WriteLine("[Run]\t IOController | GetInputMenu");
        return "";
    }
    
    // public GameConfig GetInputNewGame()
    // {
    //     Console.WriteLine("[Run]\t IOController | GetInputNewGame");
    //     return null;
    // }
}