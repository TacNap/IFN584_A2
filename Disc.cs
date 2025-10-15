

public abstract class Disc
{
	public string Symbol { get; protected set; }
	public bool IsPlayerOne { get; protected set; }

	public abstract bool ApplyEffects(ref Disc?[][] Board, int lane);
}