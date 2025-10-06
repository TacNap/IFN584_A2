using System.ComponentModel;

public class Grid
{
    // private Disc[][] Board;

    // public enum Orientation;

    // public Orientation orientation { get; private set; }
    public int WinLength { get; private set; }

    public int TurnCounter { get; private set; }

    // Methods
    public void IncrementTurnCounter()
    {
        TurnCounter++;
    }

    public void DecrementTurnCounter()
    {
        TurnCounter--;
    }

    // Needs to be updated in class diagram
    public bool SetTurnCounter(int num)
    {
        if (num < 0) return false;
        TurnCounter = num;
        return true;
    }

    // needs Player object parameters
    // Not implemented yet
    public bool IsTieGame()
    {
        Console.WriteLine("This method isn't implemented yet");
        return false;
    }

    public bool AddDisc(int lane)
    {
        Console.WriteLine("This method isn't implemented yet");
        return false;
    }

    // requires Orientation parameter
    public void ApplyGravity()
    {
        Console.WriteLine("This method isn't implemented yet");
        return;
    }

    public void Spin()
    {
        return;
    }

    // requires Orientation parameter
    public void DrawGrid()
    {
        return;
    }

    public bool CheckWinCondition()
    {
        return false;
    }
}
