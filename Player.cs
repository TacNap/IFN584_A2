
public class Player
{
	public Dictionary<string, int> DiscBalance { get; protected set; }

	public bool HasDiscRemaining()
	{
		Console.WriteLine("[Run]\t Player | HasDiscRemaining");
		return true;
	}
	public void WithdrawDisc(Disc disc)
	{
		Console.WriteLine("[Run]\t Player | WithdrawDisc");
	}
	public virtual void PlayTurn()
	{
		Console.WriteLine("[Run]\t Player | PlayTurn");
	}
}