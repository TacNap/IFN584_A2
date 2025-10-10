public class GameConfig
{
    public enum PlayerMode
    {
        HvH,
        HvC,
    }

    public enum GameMode
    {
        Classic,
        Basic,
        Spin,
    }
    
    public PlayerMode SelectedPlayerMode { get; set; }
    public GameMode SelectedGameMode { get; set; }  
    public int GridSize { get; set; }


}