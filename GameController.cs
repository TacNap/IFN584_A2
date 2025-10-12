public class GameController
{
    // Properties
    private bool IsMenuActive { get; set; }

    public IOController io; // needs to be added to class diagram, or made static

    // Constructor
    public GameController()
    {
        IsMenuActive = true;
        io = new IOController();
    }
    
    // Methods
    public void RunCommand(string input)
    {
        Console.WriteLine("I run the commands!");
    }
    
    // public Game GameFactory(GameConfig config)
    // {
    //     Console.WriteLine("[Run]\t GameController | GameFactory");
    //     return null;
    // }

    
    public void Start()
    {
        while(IsMenuActive) // may need to place a variable here later
        {
            io.PrintMenu();
            string input = io.GetInputMenu();
            RunCommand(input);
        } 
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