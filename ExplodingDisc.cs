

public class ExplodingDisc : Disc
{
	
	public override void ApplyEffects(Disc[][] Board, int lane)
	{
		Console.WriteLine("[Run]\t ExplodingDisc, ApplyEffects");
		// Apply effect
			// Loop through played lane, check if disc = symbol
			// Destroy everything touching it, including itself
		for (int i = Board.Length - 1; i >= 0; i--)
		{
			Disc? d = Board[i][lane];
			if (d == null) continue;
			if (d.Symbol == this.Symbol)
			{

			}
		}		
		// Return (?)
	}
}