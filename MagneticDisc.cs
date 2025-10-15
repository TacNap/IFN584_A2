

public class MagneticDisc : Disc
{
    public MagneticDisc(bool isPlayerOne)
    {
        Symbol = isPlayerOne ? "M" : "m";
    }

    public override bool ApplyEffects(Disc[][] Board, int lane)
	{
		Console.WriteLine("[Run]\t MagneticDisc, ApplyEffects");
		string PlayerDiscSymbol = (IsPlayerOne) ? "@" : "#";

		// Loop through played lane from the top
		for (int i = 0; i < Board.Length; i++)
		{
			Disc? d = Board[i][lane];

			// Check if disc = symbol
			if (d.Symbol == this.Symbol)
			{
				for (int j = i - 2; j >= 0; j--)
				{
					// Find the nearest ally in lane
					if (Board[j][lane].Symbol == PlayerDiscSymbol)
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
}