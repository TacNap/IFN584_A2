using Newtonsoft.Json;

namespace LineUP2
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
