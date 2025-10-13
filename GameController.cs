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
        switch (config.SelectedGameMode)
        {
            case GameConfig.GameMode.Classic:
                return new LineUpClassic();
            case GameConfig.GameMode.Basic:
                return new LineUpBasic();
            case GameConfig.GameMode.Spin:
                return new LineUpSpin();
        }
            
        return null;
    }

    // Typical program entry point
    public void Start()
    {
        while (IsMenuActive) // may need to place a variable here later
        {
            io.PrintMenu();
            int input = io.GetInputMenu();
            RunCommand(input);
        }
    }
    
    /// <summary>
    /// Prompts the user for game configuration.
    /// Then creates a Game object and runs the main loop
    /// </summary>
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