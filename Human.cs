
public class Human : Player
{
	// Inherited constructor 
	public Human(int discBalance) : base(discBalance) { }
	public override void PlayTurn()
	{
		Console.WriteLine("> get input from player");
		Console.ReadLine();
	}
}