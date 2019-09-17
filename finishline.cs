using System;
using System.Collections.Generic;

// possible classes
// Die X
// Deck X
// Card X
// Player X
// Marker X
// FLMarker /
// Game or Board

public class Die
{
	// sides value color
	public int val;
	private int sides;
	public int color;
	
	public Die(int sides, int color)
	{
		this.sides = sides;
		this.color = color;
		this.val = 1;
	}
	
	public Die(int sides)
	{
		this.sides = sides;
		this.val = 1;
	}
	
	public Die()
	{
		this.sides = 6;
		this.val = 1;
	}
	
	public void Roll(Random rand)
	{
		this.val = rand.Next(1, this.sides + 1);
	}
}

public class Card
{
	public int suit;
	public int val;
	private Dictionary<int, string> SUIT_MAP = new Dictionary<int, string> {
		{0, "\u2663"},
		{1, "\u2660"},
		{2, "\u2665"},
		{3, "\u2666"}
	};
	private Dictionary<int, string> VAL_MAP = new Dictionary<int, string> {
		{1, "Ac"},
		{10, "10"},
		{11, "Ja"},
		{12, "Qu"},
		{13, "Ki"}
	};
	
	public Card(int val, int suit)
	{
		this.val = val;
		this.suit = suit;
	}
	
	public string Display()
	{
		if (this.val == 0)
		{
			return "Jkr";
		}
		
		if (this.VAL_MAP.ContainsKey(this.val))
		{
			return this.SUIT_MAP[this.suit] + this.VAL_MAP[this.val];
		}
		
		return this.SUIT_MAP[this.suit] + "0" + this.val;
		
	}
	
}

public class Deck
{
	public List<Card> cards = new List<Card>();
	
	public Deck(int[] suits, int[] values, int numJokers)
	{
		foreach(var suit in suits)	
		{
			foreach(var val in values)
			{
				this.cards.Add(new Card(val, suit));
			}
		}
		for (int jkr = 0; jkr < numJokers; jkr++)
		{
			this.cards.Add(new Card(0, 0));
		}
	}
	
	public void Shuffle(Random rand)
	{
		for (int index = this.cards.Count - 1; index > 0; index--) 
		{
			int position = rand.Next(index + 1);
			Card temp = this.cards[index];
			this.cards[index] = this.cards[position];
			this.cards[position] = temp;
		}
	}
}

public class Marker
{
	public int position;
	public string name;
	
	
	public Marker(string name)
	{
		this.position = -1;
		this.name = name;
	}
	
	public virtual void Move(int spaces) {
		this.position += spaces;
	}
}

public class FLMarker : Marker
{
	public bool stopped;
	public FLMarker(string name) : base(name)
	{
		this.stopped = false;	
	}
	
	public void Move(int spaces, int stopValue)
	{
		// preprocessing
		this.Move(spaces);
		// postprocess
	}
}

public class Player
{
	public Marker[] markers;
	public string name;
	
	public Player(string name, string[] markerNames){
		this.markers = new Marker[markerNames.Length];
		this.name = name;
		for (int markerName = 0; markerName < markerNames.Length; markerName++)
		{
			this.markers[markerName] = new Marker(markerNames[markerName]);
		}
	}
}

public class FinishLine
{
	private readonly int[] SUITS = new int[] {0, 1, 2, 3};
	private readonly int[] VALUES = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13};
	
	public Deck deck;
	public Die redDie;
	public Die blackDie;
	public Player player1;
	public int players;
	public Random Rand;
	
}
					
public class Program
{
	public static void Main()
	{
		
		Die redDie = new Die(6, 0xFF0000);
		var blackDie = new Die(6, 0x000000);
		var rand = new Random();
		var deck = new Deck(suits, values, 2);
		Console.WriteLine(deck.cards.Count);
		Console.WriteLine(deck.cards[23].Display());
		deck.Shuffle(rand);
		Console.WriteLine(deck.cards[23].Display());
	}
}