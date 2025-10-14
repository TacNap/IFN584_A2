

using System.ComponentModel;
using System.Dynamic;

public class Disc
{
	public string Symbol { get; protected set; }
	public bool IsPlayerOne { get; protected set; }
	
	public Disc(string symbol_)
    {
		Symbol = symbol_;
    }

	public virtual void ApplyEffects(Disc[][] Board, int lane)
	{
		Console.WriteLine("[Run]\tDisc, ApplyEffects");
	}
}