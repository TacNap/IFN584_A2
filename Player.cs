
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
	
	// Currently returns true if the Player has ANY discs remaining
	// Will need to create overrides for this, taking specific Disc Types as parameters
	public bool HasDiscRemaining()
	{
		return (
			DiscBalance["Ordinary"] > 0 ||
			DiscBalance["Boring"] > 0 ||
			DiscBalance["Explosive"] > 0 ||
			DiscBalance["Magnetic"] > 0
			);
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