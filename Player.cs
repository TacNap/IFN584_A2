
public class Player
{
	public Dictionary<string, int> DiscBalance { get; protected set; }

	protected Player(int discBalance)
    {
		DiscBalance = new Dictionary<string, int>
		{
			["Ordinary"] = discBalance,
			["Boring"] = 2,
			["Explosive"] = 2,
			["Magnetic"] = 2
		};
    }
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