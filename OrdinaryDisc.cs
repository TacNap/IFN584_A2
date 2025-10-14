

public class OrdinaryDisc : Disc
{
    public OrdinaryDisc(string symbol_) : base(symbol_)
    {
    }

    public override void ApplyEffects(Disc[][] Board, int lane)
	{
		Console.WriteLine("[Run]\tOrdinaryDisc, ApplyEffects");
	}
}