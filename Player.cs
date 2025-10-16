
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
	public bool HasDiscBalanceRemaining()
	{
		return (
			DiscBalance["Ordinary"] > 0 ||
			DiscBalance["Boring"] > 0 ||
			DiscBalance["Exploding"] > 0 ||
			DiscBalance["Magnetic"] > 0
			);
	}
	
	public virtual void PlayTurn()
	{
		Console.WriteLine("> get input from player");
		Console.ReadLine();
	}
}