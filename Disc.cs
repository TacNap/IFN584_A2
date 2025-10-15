

using System.ComponentModel;
using System.Dynamic;

public abstract class Disc
{
	public string Symbol { get; protected set; }
	public bool IsPlayerOne { get; protected set; }

	public Disc(string symbol_)
	{
		Symbol = symbol_;
	}

	// This will return true for any disc that has special effects.
	public abstract bool ApplyEffects(Disc[][] Board, int lane);
}