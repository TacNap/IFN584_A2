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
    
    public PlayerMode SelectedPlayerMode { get; set; }
    public GameMode SelectedGameMode { get; set; }  
    
    // Initial number of rows
    public int GridHeight { get; set; }

    // Initial number of columns
    public int GridWidth { get; set; }

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




}