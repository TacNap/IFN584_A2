using Newtonsoft.Json;

namespace LineUp2
{
    public class OrdinaryDisc : Disc
    {
        [JsonConstructor]
        public OrdinaryDisc([JsonProperty("IsPlayerOne")] bool isPlayerOne)
        {
            IsPlayerOne = isPlayerOne;
            Symbol = IsPlayerOne ? "@" : "#";
        }

        public override bool ApplyEffects(ref Disc?[][] Board, int lane)
        {
            return false;
        }

        public override Disc Clone()
        {
            return new OrdinaryDisc(IsPlayerOne);
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
}
