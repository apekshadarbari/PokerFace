using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        string[] suit = new string[]
        {
            "Club",
            "Diamond",
            "Heart",
            "Spade"
        };

        foreach (var v in value)
        {
            foreach (var s in suit)
            {
                cardPrefabs.Add(Resources.Load<GameObject>(prefix + v + s));
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
                int j = Random.Range(0, 51);
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
            matches.Add(compare.Matches(PhotonNetwork.player.ID, hand, community));
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
        matches.OrderByDescending(m => m.Combo);
    }

}
