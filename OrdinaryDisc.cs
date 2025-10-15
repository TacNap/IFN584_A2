

public class OrdinaryDisc : Disc
{
    public OrdinaryDisc(bool isPlayerOne)
    {
        Symbol = isPlayerOne ? "@" : "#";
    }

    public override bool ApplyEffects(Disc[][] Board, int lane)
	{
        return false;
	}
}