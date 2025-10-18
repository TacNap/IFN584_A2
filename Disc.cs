
namespace LineUP2
{
    public abstract class Disc
    {
        public string Symbol { get; protected set; }
        public bool IsPlayerOne { get; protected set; }

        public static Disc CreateDisc(char discType, bool isPlayerOne)
        {
            Disc disc = char.ToLower(discType) switch
            {
                'o' => new OrdinaryDisc(isPlayerOne),
                'b' => new BoringDisc(isPlayerOne),
                'e' => new ExplodingDisc(isPlayerOne),
                'm' => new MagneticDisc(isPlayerOne),
                _ => throw new ArgumentException($"Invalid disc type: {discType}")
            };

            return disc;
        }

        public abstract bool ApplyEffects(ref Disc?[][] Board, int lane);

        public abstract bool HasDiscRemaining(Player player);

        public abstract void WithdrawDisc(Player player);
    }
}