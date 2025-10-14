

public class MagneticDisc : Disc
{
	
	public override void ApplyEffects(Disc[][] Board, int lane)
	{
		Console.WriteLine("[Run]\t MagneticDisc, ApplyEffects");
		// Apply effect
		// Loop through played lane, check if disc = symbol
		// Find the nearest ally in lane
		// Swap place with disc right above
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