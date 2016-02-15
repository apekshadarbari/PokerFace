using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class CardManager : Photon.MonoBehaviour
{
    // Card combo compares - TODO: add more comparers
    ICardCompare[] cardCompares = new ICardCompare[]
    {
        new ComparePaires(),
        new CompareHighestCard(),
    };

    //TODO: rename, create array of players to loop through
    [Header("Player One")]
    [SerializeField]
    private GameObject playerOneHand;
    [Header("Player Two")]
    [SerializeField]
    private GameObject PlayerTwoHand;
    [Header("Community Cards")]
    [SerializeField]
    private GameObject communityCards;

    //whether or not the cards have been shuffled
    private bool shuffled;

    //instance of the turnswitch script
    TurnSwitch turnInteraction;
    //all the cards in the deck
    public GameObject[] cards = new GameObject[52];
    //shuffled cards to look at
    //[SerializeField]
    private Stack<GameObject> cardStack;

    public int StraightFlush { get; private set; }

    void Start()
    {
        ///find the hands and associated with the players etc
        playerOneHand = GameObject.Find("PlayerOneHand");
        PlayerTwoHand = GameObject.Find("PlayerTwoHand");
        communityCards = GameObject.Find("CommunityCards");

        //list for the cards, 
        List<GameObject> cardPrefabs = new List<GameObject>();

        //the possible names of the prefabs and add them to the list
        string prefix = "Card_";
        string[] value = new string[]
        {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "J",
            "Q",
            "K",
            "A"
        };
        Suits[] suit = new Suits[]
        {
            Suits.Club,
            Suits.Diamond,
            Suits.Heart,
            Suits.Spade
        };

        //
        for (int i = 0; i < value.Length; i++)
        {
            foreach (var s in suit)
            {
                var prefab = Resources.Load<GameObject>(prefix + value[i] + s);
                //var card = prefab.GetComponent<Card>();
                //card.Suit = s;
                //card.Value = i + 2;
                cardPrefabs.Add(prefab);
            }
        }
        //put the prefabs in the list
        cards = cardPrefabs.ToArray();

        //Debug.Log("Hello from card manager(s)?");

    }
    /// <summary>
    /// shuffle the cards
    /// </summary>
    public void Shuffle()
    {
        Debug.Log("Everyday I'm Shuffling");

        //shuffle as many cards as there are cards in the deck
        var shuffledCards = new GameObject[cards.Length];

        //add them to shuffled cards
        for (int i = 0; i < cards.Length; i++)
        {
            shuffledCards[i] = cards[i];
        }
        //shuffle them 5 times
        for (int iteration = 0; iteration < 5; iteration++)
        {
            for (int i = 0; i < shuffledCards.Length; i++)
            {
                var c = shuffledCards[i];
                int j = UnityEngine.Random.Range(0, 51);
                shuffledCards[i] = shuffledCards[j];
                shuffledCards[j] = c;
            }
        }

        //put the shuffled cards in the stack
        cardStack = new Stack<GameObject>(shuffledCards);
        //foreach (GameObject item in cardStack)
        //{
        //    Debug.Log(item);
        //}

        //cards are now shuffled
        shuffled = true;
    }
    public void Deal()
    {
        //TODO: Fix that we have to shuffle here and look into whether or not the hands being set here is necesary
        //if (!shuffled)
        //{
        //    Shuffle();
        //}

        //playerOneHand = GameObject.Find("PlayerOneHand");
        //PlayerTwoHand = GameObject.Find("PlayerTwoHand");


        //if (shuffled)
        //{
        Debug.Log("Dealing");
        //cards are dealt
        //4 cards  - 2 each
        for (int j = 0; j < 4; j++) //j is the number of cards going to be dealed
        {

            if (j % 2 == 0)//if j is even(number)
            {
                DealCardTo(playerOneHand);
            }
            else //if j is odd //if player.id == 2 
            {
                DealCardTo(PlayerTwoHand);
            }
        }
        //}
    }
    /// <summary>
    /// Deal the flop
    /// to the community
    /// </summary>
    public void DealFlop()
    {
        //if (!shuffled)
        //{
        //    Shuffle();

        //}
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("flop contains card");
            DealCardTo(communityCards);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver">the plyers or community</param>
    private void DealCardTo(GameObject receiver)
    {
        //pop the top card in the stack
        var card = cardStack.Pop();
        //foreach (var item in cardStack)
        //{
        //instantiate it where #the receiver
        card = PhotonNetwork.Instantiate(card.name, receiver.transform.position, receiver.transform.rotation, 0);
        receiver.GetComponent<CardHolder>().DealCard(card.GetComponent<Card>());

        ////}
        //card.transform.parent = receiver.transform;
        //card.transform.SetParent(receiver.transform, true);

        //card.transform.SetParent(receiver.transform);
        //card.transform.position = Vector3.zero;

        //if set parent kommer til at virke -
        //compare
    }
    /// <summary>
    /// get the cards that the owner holds - cardholder
    /// </summary>
    /// <param name="owner">player or the community</param>
    /// <returns></returns>
    private Card[] GetCards(GameObject owner)
    {
        //var cards = new List<Card>();

        //for (int i = 0; i < owner.transform.childCount; i++)
        //{
        //    var c = owner.transform.GetChild(i).gameObject.GetComponent<Card>();
        //    if (c != null)
        //    {
        //        cards.Add(c);
        //    }
        //}

        //return cards.ToArray();
        //
        return owner.GetComponent<CardHolder>().GetCards();
    }
    /// <summary>
    /// any matches in the current hands + community
    /// </summary>
    /// <param name="player"></param>
    /// <param name="community"></param>
    /// <returns></returns>
    private List<CardMatch> GetMatches(GameObject player, Card[] community)
    {
        var hand = GetCards(player);
        //creates a list for the matches
        List<CardMatch> matches = new List<CardMatch>();

        //runs through the list of cardcomparers
        foreach (var compare in cardCompares)
        {
            //adds any matches
            matches.Add(compare.Matches(player, hand, community));

            //Debug.Log(PhotonNetwork.player.ID);
            //Debug.Log(hand);

        }


        return matches;
    }

    /// <summary>
    /// compares cards
    /// </summary>
    public void CompareCards()
    {
        Debug.Log("comparing");

        //list of matches
        var matches = new List<CardMatch>();

        //cards from the community pile
        var community = GetCards(communityCards);

        //adds the cards from each player to the matches + the community cards - to see if there are any matches
        matches.AddRange(GetMatches(playerOneHand, community));
        matches.AddRange(GetMatches(PlayerTwoHand, community));

        // Sort so higher combos are at the top
        //matches.OrderByDescending(m => m.Combo);
        var combos = new CardCombos[]
        {
            CardCombos.RoyalFlush,
            CardCombos.StraightFlush,
            CardCombos.FourOfAKind,
            CardCombos.FullHouse,
            CardCombos.Flush,
            CardCombos.Straight,
            CardCombos.ThreeOfAKind,
            CardCombos.TwoPair,
            CardCombos.OnePair,
            CardCombos.HighestCard,
        };

        //we havent found a winner yet
        CardMatch winner = null;

        //runs through the different possible combos
        foreach (var c in combos)
        {
            //find all matches of the combos in the matches list
            var m = matches.FindAll(i => i.Combo == c);

            //if there are more than one match for a combo
            if (m.Count > 1)
            {
                Debug.Log("Multiple matches for " + c);

                //run through to see who´s combo is better
                for (int i = 0; i < m[0].Values.Length && i < m[1].Values.Length; i++)
                {
                    //if match at index 0 is better than that at index 1 or vice versa
                    if (m[0].Values[i] > m[1].Values[i])
                    {
                        winner = m[0];
                        break;
                    }
                    else if (m[1].Values[i] > m[0].Values[i])
                    {
                        winner = m[1];
                        break;
                    }
                }
                //if there is a winner step out of the loop
                if (winner != null)
                    break;
            }
            //if there is only one match
            else if (m.Count == 1)
            {
                Debug.Log("One matches for " + c);

                //there is only one!
                winner = m.First();
                break;
            }
            //there are no matches for the current combo
            else
            {
                Debug.Log("No matches for " + c);
            }
        }

        //if there is a winner return the name and how they won
        if (winner != null)
        {
            Debug.LogFormat("The winner is {0} with {1}", winner.Player.name, winner.Name);
        }
        //otherwise there is no winner and we throw an exception - should very rarely happen
        else
        {
            throw new InvalidOperationException("No winner!!!");
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
