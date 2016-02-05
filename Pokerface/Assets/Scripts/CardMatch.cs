using UnityEngine;
using System.Collections;
using System.Linq;

public enum CardCombos
{
    RoyalFlush = 10,
    StraightFlush = 9,
    FourOfAKind = 8,
    FullHouse = 7,
    Flush = 6,
    Straight = 5,
    ThreeOfAKind = 4,
    TwoPair = 3,
    OnePair = 2,
    HighestCard = 1,
    ERRR = -1,
}

public class CardMatch
{
    string name;
    CardCombos combo;
    int[] values;
    GameObject player;

    public string Name
    {
        get
        {
            return name;
        }
    }

    public CardCombos Combo
    {
        get { return combo; }
    }
    public int[] Values
    {
        get
        {
            return values;
        }
    }

    public GameObject Player
    {
        get
        {
            return player;
        }

    }

    public CardMatch(GameObject playerID, string name, CardCombos combo, params int[] values )
    {
        this.name = name;
        this.combo = combo;
        this.values = values.OrderByDescending(i => i).ToArray();
        this.player = playerID;

        if (values.Length == 0)
        {
            values = new int[] { -1 };
        }

    }
}
