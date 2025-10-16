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
		foreach (var (type, balance) in DiscBalance)
		{
			if (balance < 1)
			{
				return false;
			}
		}
		return true;
	}
}