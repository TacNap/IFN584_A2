
public class Computer : Player
{
	// Inherited constructor
	public Computer(int discBalance) : base(discBalance) { }
	public override void PlayTurn()
	{
		Console.WriteLine("[Run]\t Computer | PlayTurn");
	}
}