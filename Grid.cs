public class Grid
{
    // private Disc[][] Board;

    // public enum Orientation;

    // public Orientation orientation { get; private set; }
    public int WinLength { get; private set; }

    public int TurnCounter { get; private set; }

    // Methods
    void IncrementTurnCounter()
    {
        TurnCounter++;
    }

    void DecrementTurnCounter()
    {
        TurnCounter--;
    }

    // Needs to be updated in class diagram
    bool SetTurnCounter(int num)
    {
        if (num < 0) return false;
        TurnCounter = num;
        return true;
    }
}