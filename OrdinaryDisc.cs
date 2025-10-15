

public class OrdinaryDisc : Disc
{
    public OrdinaryDisc(bool isPlayerOne_)
    {
        IsPlayerOne = isPlayerOne_;
        Symbol = IsPlayerOne ? "@" : "#";
    }

    public override bool ApplyEffects(ref Disc?[][] Board, int lane)
	{
        return false;
	}
}