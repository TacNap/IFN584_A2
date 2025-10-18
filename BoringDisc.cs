using Newtonsoft.Json;

namespace LineUp2
{
	public class BoringDisc : Disc
	{
		[JsonConstructor]
		public BoringDisc([JsonProperty("IsPlayerOne")] bool isPlayerOne)
		{
			DiscReturn = new Dictionary<string, int>[2];
			IsPlayerOne = isPlayerOne;
			Symbol = IsPlayerOne ? "B" : "b";
		}

		public override bool ApplyEffects(ref Disc?[][] Board, int lane)
		{
			int DiscCount1 = 0;
			int DiscCount2 = 0;

			int laneIndex = lane - 1;

			// Count discs of each player
			for (int i = 0; i < Board.Length; i++)
			{
				if (Board[i][laneIndex] == null) continue;
				Disc? d = Board[i][laneIndex];
				if (d.Symbol == this.Symbol) continue;
				if (d.IsPlayerOne) DiscCount1 += 1;
				else DiscCount2 += 1;
			}

			// Drill the lane
			for (int i = 0; i < Board.Length; i++)
			{
				Board[i][laneIndex] = null;
			}

			// Convert Boring to Ordinary at the bottom of the lane
			Board[^1][laneIndex] = new OrdinaryDisc(IsPlayerOne);

			// Return all disk to hands of respective players
			Dictionary<string, int> discDict1 = new()
			{
				["Ordinary"] = DiscCount1
			};
			Dictionary<string, int> discDict2 = new()
			{
				["Ordinary"] = DiscCount2
			};
			DiscReturn[0] = discDict1;
			DiscReturn[1] = discDict2;

			return true;
		}

		public override Disc Clone()
		{
			return new BoringDisc(IsPlayerOne);
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
}
