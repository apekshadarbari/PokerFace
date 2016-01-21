using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour 
{

	public static int numberOfCards = 52;
	public Card[] cards = new Card[numberOfCards];
	public enum CardType : byte
	{
		Diamonds,
		Clubs,
		Hearts,
		Spades
	}

	public class Card
	{
		public CardType Type { get{return type;} set {type = value;} }
	//	public int CardNumber { get; private set; }
	//	public string Name { get; private set; }

		private CardType type;
		public int CardNumber;
		public string Name;

		public Card(int number, CardType type, string name )
		{
			CardNumber = number;
			Type = type;
			Name = name;


		}
	}
		


	// Use this for initialization
	void Start () 
	{
	
		#region Cards

//		cards = new Card[numberOfCards];
		//cards = GetComponents<Card>();
		int cardNumber = 1;



		for (int i=0; i < numberOfCards; i++){

			cards = GetComponents<Card>();
			Instantiate(cards[i]);
		}



		for(int i = 0 ; i < 1; i++)
		{
			if (i <= 12) {
				if (cardNumber == 1) {
					
					cards[i].Name =  "ADiamondPrefab";
				} else if (cardNumber > 1 && cardNumber <= 10) {
					cards [i].Name = cardNumber + "Diamond";
				}
				if (cardNumber == 11) {
					cards [i].Name = "JDiamond";
				}
				if (cardNumber == 12) {
					cards [i].Name = "QDiamond";
				}
				if (cardNumber == 13) {
					cards [i].Name = "KDiamond";
					cardNumber = 0;
				}
			}

			if (i > 12 && i <= 25) {
				
				if (cardNumber == 1) {
					cards [i].Name = "AClub";
				} else if (cardNumber > 1 && cardNumber <= 10) {
					cards [i].Name = cardNumber + "Club";
				}
				if (cardNumber == 11) {
					cards [i].Name = "JackOfClubs";
				}
				if (cardNumber == 12) {
					cards [i].Name = "QueenOfClubs";
				}
				if (cardNumber == 13) {
					cards [i].Name = "KingOfClubs";
					cardNumber = 0;
				}
			}
				

			if (i > 25 && i <= 38) 
			{	
				if (cardNumber == 1) {
					cards [i].Name = "AceOfHearts";
				} else if (cardNumber > 1 && cardNumber <= 10) {
					cards [i].Name = "Hearts" + cardNumber;
				}
				if (cardNumber == 11) {
					cards [i].Name = "JackOfHearts";
				}
				if (cardNumber == 12) {
					cards [i].Name = "QueenOfHearts";
				}
				if (cardNumber == 13) {
					cards [i].Name = "KingOfHearts";
					cardNumber = 0;
				}
			}

			if (i > 38 && i <= 51) {
				if (cardNumber == 1) {
					cards [i].Name = "AceOfSpades";
				} else if (cardNumber > 1 && cardNumber <= 10) {
					cards [i].Name = "Spades" + cardNumber;
				}
				if (cardNumber == 11) {
					cards [i].Name = "JackOfSpades";
				}
				if (cardNumber == 12) {
					cards [i].Name = "QueenOfSpades";
				}
				if (cardNumber == 13) {
					cards [i].Name = "KingOfSpades";
					cardNumber = 0;
				}
			}

			cardNumber++;

		}

		for (int i=0; i<52; i++){

			print(cards[i]);
		}


		#endregion

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.M))
		{
			
			ShuffleCards (cards);

	}

	
}
	/// <summary>
	/// Shuffles the cards.
	/// </summary>
	/// <param name="cards">Cards.</param>
	void ShuffleCards(Card[] cards)
{
	for (int i=0; i < cards.Length; i++)
	{
		int r= Random.Range(0,cards.Length-1);
		Card tmp = cards[i];
		cards[i] = cards[r];
		cards[r] = tmp;
			
		}
			
	}	

		
}


