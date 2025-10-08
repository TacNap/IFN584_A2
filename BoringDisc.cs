

public class BoringDisc : Disc
{
	
	public override void ApplyEffects(Disc[][] Board, int lane)
	{
		Console.WriteLine("[Run]\tOrdinaryDisc, ApplyEffects");
	}
}