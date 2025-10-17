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

		// Loop through played lane from the top
		for (int i = 0; i < Board.Length; i++)
		{
			if (Board[i][lane] == null) continue;
			Disc? d = Board[i][lane];

			// Check if disc = symbol
			if (d.Symbol == this.Symbol)
			{
				for (int j = i - 2; j >= 0; j--)
				{
					// Find the nearest ally in lane
					if (Board[j][lane].IsPlayerOne == IsPlayerOne)
					{
						// Swap place with disc right above
						Disc TempDisc = Board[j][lane];
						Board[j][lane] = Board[j + 1][lane];
						Board[j + 1][lane] = TempDisc;
						break;
					}
				}

				// Convert special disc into ordinary
				Board[i][lane] = new OrdinaryDisc(IsPlayerOne);
			}
		}

		return true;
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
