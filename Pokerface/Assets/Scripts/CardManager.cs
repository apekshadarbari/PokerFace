using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class CardManager : Photon.MonoBehaviour
{
    // Card combo compares
    ICardCompare[] cardCompares = new ICardCompare[]
    {
        new ComparePaires(),
        new CompareHighestCard(),
    };
    private GameObject cube;
    private GameObject sphere;
    private GameObject cylinder;

    private bool shuffled;
    TurnSwitch turnInteraction;

    public GameObject[] cards = new GameObject[52];
    private Stack<GameObject> cardStack;

    public int StraightFlush { get; private set; }

    void Start()
    {

        cube = GameObject.Find("PlayerOneHand");
        sphere = GameObject.Find("PlayerTwoHand");
        cylinder = GameObject.Find("CommunityCards");

        List<GameObject> cardPrefabs = new List<GameObject>();

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

        //foreach (var v in value)
        for( int i = 0; i < value.Length; i++ )
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

        cards = cardPrefabs.ToArray();

        Debug.Log("Hello from card manager(s)?");

    }
    public void Shuffle()
    {
        Debug.Log("Everyday I'm Shuffling");

        var shuffledCards = new GameObject[cards.Length];

        for (int i = 0; i < cards.Length; i++)
        {
            shuffledCards[i] = cards[i];
        }

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


        cardStack = new Stack<GameObject>(shuffledCards);
        //foreach (GameObject item in cardStack)
        //{
        //    Debug.Log(item);
        //}
        shuffled = true;
    }
    public void Deal()
    {
        Shuffle();
        cube = GameObject.Find("PlayerOneHand");
        sphere = GameObject.Find("PlayerTwoHand");

        if (shuffled)
        {
            Debug.Log("Dealing");

            for (int j = 0; j < 4; j++) //j is the number of cards going to be dealed
            {

                if (j % 2 == 0)//if j is even(number)
                {
                    DealCardTo(cube);
                }
                else //if j is odd //if player.id == 2 
                {
                    DealCardTo(sphere);
                }
            }
        }
    }
    public void DealRiver()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("river contains card");
            DealCardTo(cylinder);
        }
    }


    private void DealCardTo(GameObject receiver)
    {
        var card = cardStack.Pop();
        //foreach (var item in cardStack)
        //{
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
        return owner.GetComponent<CardHolder>().GetCards();
    }

    private List<CardMatch> GetMatches(GameObject player, Card[] community)
    {
        var hand = GetCards(player);
        List<CardMatch> matches = new List<CardMatch>();

        foreach (var compare in cardCompares)
        {
            matches.Add(compare.Matches(player, hand, community));
            Debug.Log(PhotonNetwork.player.ID);
            Debug.Log(hand);

        }


        return matches;
    }

    public void CompareCards()
    {
        Debug.Log("comparing");

        var matches = new List<CardMatch>();
        var community = GetCards(cylinder);

        matches.AddRange(GetMatches(cube, community));
        matches.AddRange(GetMatches(sphere, community));

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

        CardMatch winner = null;

        foreach (var c in combos)
        {
            var m = matches.FindAll(i => i.Combo == c);

            if (m.Count > 1)
            {
                Debug.Log("Multiple matches for " + c);

                for (int i = 0; i < m[0].Values.Length && i < m[1].Values.Length; i++)
                {
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

                if (winner != null)
                    break;
            }
            else if (m.Count == 1)
            {
                Debug.Log("One matches for " + c);

                winner = m.First();
                break;
            }
            else
            {
                Debug.Log("No matches for " + c );
            }
        }

        if (winner != null)
        {
            Debug.LogFormat("The winner is {0} with {1}", winner.Player.name, winner.Name);
        }
        else
        {
            throw new InvalidOperationException("No winner!!!");
        }
    }

}
