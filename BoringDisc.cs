

public class BoringDisc : Disc
{
	
	public override void ApplyEffects(Disc[][] Board, int lane)
	{
		Console.WriteLine("[Run]\t BoringDisc, ApplyEffects");
		// Apply effect
			// Loop through played lane, check if disc = symbol
			// Drill down to the bottom of the lane
			// Return all disk to hands of respective players
		for (int i = Board.Length - 1; i >= 0; i--)
		{
			Disc? d = Board[i][lane];
			if (d == null) continue;
			if (d.Symbol == this.Symbol)
			{

			}
		}
		// Convert special disc into ordinary
		
		// Return (?)
	}
}