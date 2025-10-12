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
    public void RunCommand(int input)
    {
        switch (input)
        {
            case 1:
                NewGame();
                break;
            case 2:
                Console.WriteLine("Load Game");
                break;
            case 3:
                Console.WriteLine("Help");
                break;
            case 4:
                Console.WriteLine("Bye Bye!");
                IsMenuActive = false;
                break;    
            default:
                io.PrintError("Error: Unknown Command");
                break;
        }
    }
    
    public Game GameFactory(GameConfig config)
    {
        Console.WriteLine("[Run]\t GameController | GameFactory");
        return null;
    }

    
    public void Start()
    {
        while(IsMenuActive) // may need to place a variable here later
        {
            io.PrintMenu();
            int input = io.GetInputMenu();
            RunCommand(input);
        } 
    }
    
    public void NewGame()
    {
        GameConfig config = io.GetInputNewGame();
        Game game = GameFactory(config);
        game.GameLoop();
    }
    
    public void LoadGame()
    {
        Console.WriteLine("[Run]\t GameController | LoadGame");
    }
}