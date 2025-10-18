using Newtonsoft.Json;

public class MagneticDisc : Disc
{
    [JsonConstructor]
    public MagneticDisc([JsonProperty("IsPlayerOne")] bool isPlayerOne)
    {
        IsPlayerOne = isPlayerOne;
        Symbol = IsPlayerOne ? "M" : "m";
    }

	public override bool ApplyEffects(ref Disc?[][] Board, int lane)
	{
		Console.WriteLine("[Run]\t MagneticDisc, ApplyEffects");
		int laneIndex = lane - 1;

		// Loop through played lane from the top
		for (int i = 0; i < Board.Length; i++)
		{
			if (Board[i][laneIndex] == null) continue;
			Disc? d = Board[i][laneIndex];

			// Check if disc = symbol
			if (d.Symbol == this.Symbol)
			{
				for (int j = i + 2; j < Board.Length; j++)
				{
					// Find the nearest ally in lane
					if (Board[j][laneIndex].IsPlayerOne == IsPlayerOne)
					{
						// Swap place with disc right above
						Disc TempDisc = Board[j][laneIndex];
						Board[j][laneIndex] = Board[j - 1][laneIndex];
						Board[j - 1][laneIndex] = TempDisc;
						break;
					}
				}

				// Convert special disc into ordinary
				Board[i][laneIndex] = new OrdinaryDisc(IsPlayerOne);
			}
		}

		return true;
	}

	public override Disc Clone()
	{
		return new MagneticDisc(IsPlayerOne);
	}

    public override bool HasDiscRemaining(Player player)
	{
		return player.DiscBalance["Magnetic"] > 0;
	}
	
	public override void WithdrawDisc(Player player)
    {
		player.DiscBalance["Magnetic"]--;
    }
}
