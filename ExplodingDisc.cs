

public class ExplodingDisc : Disc
{
    public ExplodingDisc(bool isPlayerOne_)
    {
        IsPlayerOne = isPlayerOne_;
        Symbol = IsPlayerOne ? "E" : "e";
    }

	public override bool ApplyEffects(ref Disc?[][] Board, int lane)
	{
		Console.WriteLine("[Run]\t ExplodingDisc, ApplyEffects");

		// Loop through played lane from the top
		for (int i = 0; i < Board.Length; i++)
		{
			if (Board[i][lane] == null) continue;
			Disc? d = Board[i][lane];

			// Check if disc = symbol
			if (d.Symbol == this.Symbol)
			{
				int left = lane > 0 ? lane - 1 : lane;
				int right = lane < Board[0].Length - 1 ? lane + 1 : lane;
				int up = i < Board.Length - 1 ? i + 1 : i;
				int down = i > 0 ? i - 1 : 0;

				// Destroy everything touching it, including itself
				Board[i][lane] = null;
				Board[i][right] = null;
				Board[i][left] = null;
				Board[up][lane] = null;
				Board[up][right] = null;
				Board[up][left] = null;
				Board[down][lane] = null;
				Board[down][right] = null;
				Board[down][left] = null;
			}
		}

		return true;
	}

	public override bool HasDiscRemaining(Player player)
	{
		return player.DiscBalance["Exploding"] > 0;
	}
	
	public override void WithdrawDisc(Player player)
    {
		player.DiscBalance["Exploding"]--;
    }
}