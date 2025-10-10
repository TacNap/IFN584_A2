

public class ExplodingDisc : Disc
{
	
	public override void ApplyEffects(Disc[][] Board, int lane)
	{
		Console.WriteLine("[Run]\tExplodingDisc, ApplyEffects");
	}
}