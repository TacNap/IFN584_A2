using Newtonsoft.Json;

public class ExplodingDisc : Disc
{
    [JsonConstructor]
    public ExplodingDisc([JsonProperty("IsPlayerOne")] bool isPlayerOne)
    {
        IsPlayerOne = isPlayerOne;
        Symbol = IsPlayerOne ? "E" : "e";
    }

	public override bool ApplyEffects(ref Disc?[][] Board, int lane)
	{
		Console.WriteLine("[Run]\t ExplodingDisc, ApplyEffects");
		int laneIndex = lane - 1;

		// Loop through played lane from the top
		for (int i = 0; i < Board.Length; i++)
		{
			if (Board[i][laneIndex] == null) continue;

			// Check if disc = symbol
			if (Board[i][laneIndex].Symbol == this.Symbol)
			{
				int left = laneIndex > 0 ? laneIndex - 1 : laneIndex;
				int right = laneIndex < Board[0].Length - 1 ? laneIndex + 1 : laneIndex;
				int up = i < Board.Length - 1 ? i + 1 : i;
				int down = i > 0 ? i - 1 : 0;

				// Destroy everything touching it, including itself
				Board[i][laneIndex] = null;
				Board[i][right] = null;
				Board[i][left] = null;
				Board[up][laneIndex] = null;
				Board[up][right] = null;
				Board[up][left] = null;
				Board[down][laneIndex] = null;
				Board[down][right] = null;
				Board[down][left] = null;
			}
		}

		return true;
	}

	public override Disc Clone()
	{
		return new ExplodingDisc(IsPlayerOne);
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
