public class GameController
{
    public bool IsMenuActive { get; set; }
    
    public void RunCommand(string input)
    {
        Console.WriteLine("[Run]\t GameController | RunCommand");
    }
    
    public Game GameFactory(GameConfig config)
    {
        Console.WriteLine("[Run]\t GameController | GameFactory");
        return null;
    }
    
    public void Start()
    {
        Console.WriteLine("[Run]\t GameController | Start");
    }
    
    public void NewGame()
    {
        Console.WriteLine("[Run]\t GameController | NewGame");
    }
    
    public void LoadGame()
    {
        Console.WriteLine("[Run]\t GameController | LoadGame");
    }
}