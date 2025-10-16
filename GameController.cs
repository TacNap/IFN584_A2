public class GameController
{
    // Properties
    private bool IsMenuActive { get; set; }

    private FileController file { get; set; }

    // Constructor
    public GameController()
    {
        IsMenuActive = true;
        file = new FileController();
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
                IOController.PrintError("Error: Unknown Command");
                break;
        }
    }
    
    public Game GameFactory(GameConfig config)
    {
        switch (config.SelectedGameMode)
        {
            case GameConfig.GameMode.Classic:
                return new LineUpClassic(
                    config.GridHeight,
                    config.GridWidth,
                    config.SelectedPlayerMode == GameConfig.PlayerMode.HvH ? true : false
                );
            case GameConfig.GameMode.Basic:
                return new LineUpBasic(
                    config.SelectedPlayerMode == GameConfig.PlayerMode.HvH ? true : false
                );
            case GameConfig.GameMode.Spin:
                return new LineUpSpin(
                    config.SelectedPlayerMode == GameConfig.PlayerMode.HvH ? true : false
                );
        }
        IOController.PrintError("Error: Unrecognised GameMode");
        return null;
    }

    // Typical program entry point
    public void Start()
    {
        while (IsMenuActive) // may need to place a variable here later
        {
            IOController.PrintMenu();
            int input = IOController.GetInputMenu();
            RunCommand(input);
        }
    }
    
    /// <summary>
    /// Prompts the user for game configuration.
    /// Then creates a Game object and runs the main loop
    /// </summary>
    public void NewGame()
    {
        GameConfig config = IOController.GetInputNewGame();
        Game game = GameFactory(config);
        game.GameLoop();
    }
    
    public void LoadGame()
    {
        IOController.PrintSaveFiles(file.GetSaves());
        //IOController.GetInputLoad();
        //Game = GameDeserialization(filePath);
        //Game.GameLoop();
    }
}