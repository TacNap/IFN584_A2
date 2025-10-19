namespace LineUp2
{
	public class Player
	{
		/// <summary>
        /// Discs available to the current player
        /// </summary>
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
				if (balance > 0)
				{
					return true;
				}
			}
			return false;
		}

		public void ResetDiscBalance(Dictionary<string, int> DiscBalance_)
		{
			DiscBalance = DiscBalance_;
		}

		public void ReturnDisc(Dictionary<string, int> discReturned)
		{
			foreach (string key in discReturned.Keys)
			{
				if (DiscBalance.TryGetValue(key, out int value))
				{
					DiscBalance[key] = value + discReturned[key];
				}
			}
		}
	}
}