public class GameConfig
{
    // Defines the options for player modes
    public enum PlayerMode
    {
        HvH,
        HvC,
    }

    // Defines the options for game modes
    public enum GameMode
    {
        Classic,
        Basic,
        Spin,
    }
    
    public PlayerMode SelectedPlayerMode { get; private set; }
    public GameMode SelectedGameMode { get; private set; }  
    
    // Initial number of rows
    public int GridHeight { get; private set; }

    // Initial number of columns
    public int GridWidth { get; private set; }

    // Constructor
    public GameConfig(
        PlayerMode playerMode = PlayerMode.HvH,
        GameMode gameMode = GameMode.Classic,
        int GridHeight = 6,
        int GridWidth = 7)
    {
        SelectedPlayerMode = playerMode;
        SelectedGameMode = gameMode;
        this.GridHeight = GridHeight;
        this.GridWidth = GridWidth;
    }

    public bool SetGameMode(GameMode mode)
    {
        if (!Enum.IsDefined(typeof(GameMode), mode))
        {
            return false;
        }

        SelectedGameMode = mode;
        return true;
    }

    public bool SetPlayerMode(PlayerMode mode)
    {
        if (!Enum.IsDefined(typeof(PlayerMode), mode))
        {
            return false;
        }

        SelectedPlayerMode = mode;
        return true;
    }

    public bool SetGridHeight(int rows)
    {
        if (rows < 1 || rows > 10)
        {
            return false;
        }

        GridHeight = rows;
        return true;
    }

    public bool SetGridWidth(int cols)
    {
        if (cols < 1 || cols > 10 || cols < GridHeight)
        {
            return false;
        }

        GridWidth = cols;
        return true;
    }
}
