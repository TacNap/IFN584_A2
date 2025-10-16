

public class BoringDisc : Disc
{
    public BoringDisc(bool isPlayerOne_)
    {
        IsPlayerOne = isPlayerOne_;
        Symbol = IsPlayerOne ? "B" : "b";
    }

	public override bool ApplyEffects(ref Disc?[][] Board, int lane)
	{
		Console.WriteLine("[Run]\t BoringDisc, ApplyEffects");
		int DiscCount1 = 0;
		int DiscCount2 = 0;

		// Count discs of each player
		for (int i = 0; i < Board.Length; i++)
		{
			if (Board[i][lane] == null) continue;
			Disc? d = Board[i][lane];
			if (d.IsPlayerOne) DiscCount1 += 1;
			else DiscCount2 += 1;
		}

		// Drill the lane
		for (int i = 1; i < Board.Length; i++)
		{
			Board[i][lane] = null;
		}

		// Convert Boring to Ordinary at the bottom of the lane
		Board[0][lane] = new OrdinaryDisc(IsPlayerOne);

		// TODO: Return all disk to hands of respective players
		// ...

		return true;
	}

	public override bool HasDiscRemaining(Player player)
	{
		return player.DiscBalance["Boring"] > 0;
	}
	
	public override void WithdrawDisc(Player player)
    {
		player.DiscBalance["Boring"]--;
    }
}