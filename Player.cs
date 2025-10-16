public class Player
{
	public Dictionary<string, int> DiscBalance { get; protected set; }

	public bool IsHuman { get; set; }

	public Player(Dictionary<string, int> discBalance, bool isHuman = true)
	{
		// Change this to accept a dictionary. 
		this.DiscBalance = discBalance;
		IsHuman = isHuman;
	}

	// Currently returns true if the Player has ANY discs remaining
	public bool HasDiscBalanceRemaining()
	{
		// Change this to iterate through the dictionary, regardless of contents.
		return (
			DiscBalance["Ordinary"] > 0 ||
			DiscBalance["Boring"] > 0 ||
			DiscBalance["Exploding"] > 0 ||
			DiscBalance["Magnetic"] > 0
			);
	}
}