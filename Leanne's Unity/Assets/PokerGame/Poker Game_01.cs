using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Poker
{
	class Card
	{
		public enum Suit
		{
			Spades,
			Hearts,
			Clubs,
			Diamonds
		}
		public enum Value
		{
			Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace

		}

		//properties
		public Suit MySuit { get; set;}
		public Value MyValue { get; set; }

	}
}