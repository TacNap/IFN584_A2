

public class OrdinaryDisc : Disc
{
    public OrdinaryDisc(string symbol_) : base(symbol_)
    {
    }

    public override bool ApplyEffects(Disc[][] Board, int lane)
	{
        return false;
	}
}