
namespace LineUp2
{
    public abstract class Disc
    {
        public string Symbol { get; protected set; } // The symbol that will be rendered to the console
        public bool IsPlayerOne { get; protected set; } // Does this disc belong to PlayerOne?
        public Dictionary<string, int>[]? DiscReturn { get; protected set; }

        /// <summary>
        /// Factory Method for creating Discs. 
        /// Declared here such that adding a new disc type doesn't require changing other classes.
        /// </summary>
        /// <param name="discType"></param>
        /// <param name="isPlayerOne"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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

        /// <summary>
        /// Creates a deep clone of the Disc object. 
        /// </summary>
        /// <returns></returns>
        public abstract Disc Clone();

        /// <summary>
        /// Applies special effects to the provided board.
        /// </summary>
        /// <param name="Board"></param>
        /// <param name="lane"></param>
        /// <returns></returns>
        public abstract bool ApplyEffects(ref Disc?[][] Board, int lane);

        /// <summary>
        /// Check if the given provided player has any of this disc type remaining
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public abstract bool HasDiscRemaining(Player player);

        /// <summary>
        /// Withdraw a disc of this type from the provided player
        /// </summary>
        /// <param name="player"></param>
        public abstract void WithdrawDisc(Player player);
    }
}