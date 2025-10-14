

public class ExplodingDisc : Disc
{
    public ExplodingDisc(string symbol_) : base(symbol_)
    {
    }

    public override void ApplyEffects(Disc?[][] Board, int lane)
	{
		Console.WriteLine("[Run]\t ExplodingDisc, ApplyEffects");
		// Loop through played lane from the top
		for (int i = 0; i < Board.Length; i++)
		{
			Disc? d = Board[i][lane];

			// Check if disc = symbol
			if (d.Symbol == this.Symbol)
			{
				// Destroy everything touching it, including itself
				Board[i][lane] = null;
				Board[i][lane + 1] = null;
				Board[i][lane - 1] = null;
				Board[i + 1][lane] = null;
				Board[i + 1][lane + 1] = null;
				Board[i + 1][lane - 1] = null;
				Board[i - 1][lane] = null;
				Board[i - 1][lane + 1] = null;
				Board[i - 1][lane + 1] = null;
			}
		}
		
		// Return (?)
	}
}