namespace LineUpGame
{
    public abstract class Game
    {
        // Core Components
        public Grid Grid { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        public string MoveSequence { get; set; }
        public IOController IOController { get; set; }
        public FileController FileController { get; set; }

        // Constructor
        public Game(GameConfig config, IOController ioController, FileController fileController)
        {
            Grid = ;
            IOController = ioController;
            FileController = fileController;
            MoveSequence = string.Empty;

        }
        public abstract void GameLoop();
        public virtual Disc CreateDisc()
        {

        }

        public virtual void ResetGame()
        {
 
        }

    }
}
