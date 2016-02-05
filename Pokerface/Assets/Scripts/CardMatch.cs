using UnityEngine;
using System.Collections;

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
    int playerID;

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

    public int PlayerID
    {
        get
        {
            return playerID;
        }

    }

    public CardMatch(int playerID,string name, CardCombos combo, params int[] values )
    {
        this.name = name;
        this.combo = combo;
        this.values = values;
        this.playerID = playerID;

        if (values.Length == 0)
        {
            values = new int[] { -1 };
        }
    }
}
