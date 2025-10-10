public class IOController
{
    public void PrintMenu()
    {
        Console.WriteLine("[Run]\t IOController | PrintMenu");
    }
    
    public void PrintWinner(bool P1, bool P2)
    {
        Console.WriteLine("[Run]\t IOController | PrintWinner");
    }
    
    public string GetInputMenu()
    {
        Console.WriteLine("[Run]\t IOController | GetInputMenu");
        return string.Empty;
    }
    
    public GameConfig GetInputNewGame()
    {
        Console.WriteLine("[Run]\t IOController | GetInputNewGame");
        return null;
    }
}