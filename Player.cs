
public class Player
{
	public Dictionary<string, int> DiscBalance { get; protected set; }

	protected Player(int discBalance)
	{
		DiscBalance = new Dictionary<string, int>
		{
			["Ordinary"] = discBalance,
			["Boring"] = 2,
			["Exploding"] = 2,
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
			DiscBalance["Exploding"] > 0 ||
			DiscBalance["Magnetic"] > 0
			);
	}

	// NOT IDEAL. This breaks Open/Closed principle.
	// We'd have to move this method to Disc otherwise, so I'll leave here for now. 
	public bool HasDiscRemaining(Disc disc)
	{
		return disc switch
		{
			OrdinaryDisc => DiscBalance["Ordinary"] > 0,
			ExplodingDisc => DiscBalance["Exploding"] > 0,
			BoringDisc => DiscBalance["Boring"] > 0,
			MagneticDisc => DiscBalance["Magnetic"] > 0,
			_ => false
		};
	}

	public void WithdrawDisc(Disc disc)
	{
		Console.WriteLine("[Run]\t Player | WithdrawDisc");
	} 
	public virtual void PlayTurn()
	{
		Console.WriteLine("> get input from player");
		Console.ReadLine();
	}
}