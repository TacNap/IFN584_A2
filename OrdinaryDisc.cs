

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

    public override bool HasDiscRemaining(Player player)
    {
        return player.DiscBalance["Ordinary"] > 0;
    }
    
    public override void WithdrawDisc(Player player)
    {
		player.DiscBalance["Ordinary"]--;
    }
}