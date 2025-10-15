

public class BoringDisc : Disc
{
    public BoringDisc(string symbol_) : base(symbol_)
    {
    }

    public override bool ApplyEffects(Disc?[][] Board, int lane)
	{
		Console.WriteLine("[Run]\t BoringDisc, ApplyEffects");
		string PlayerDiscSymbol = (IsPlayerOne) ? "@" : "#";
		int DiscCount1 = 0;
		int DiscCount2 = 0;
		
		// Loop through played lane from the top
		for (int i = 0; i < Board.Length; i++)
		{
			Disc? d = Board[i][lane];
			if (d.Symbol == "@") DiscCount1 += 1;
			if (d.Symbol == "#") DiscCount2 += 1;

			// Check if disc = symbol
			if (d.Symbol == this.Symbol)
			{
				// Drill down to the bottom of the lane and Convert special disc into ordinary
				Disc NewDisc = new OrdinaryDisc(PlayerDiscSymbol);
				Disc?[] NewLane = new Disc[Board.Length];
				NewLane[0] = NewDisc;
				Board[i] = NewLane;

				// Return all disk to hands of respective players
				// ...
			}
		}

		return true;
	}
}