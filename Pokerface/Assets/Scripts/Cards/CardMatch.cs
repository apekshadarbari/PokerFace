using UnityEngine;
using System.Collections;
using System.Linq;

//the comboniations of cards in poker, + a value to rank them by
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
    /// <summary>
    /// reference for the combinations of cards, their values and names
    /// example of the use of this function return new CardMatch(player, "Two Pair", CardCombos.TwoPair, paires.Max(), paires.Min());
    /// </summary>
    /// <param name="playerID">the player</param>
    /// <param name="name">the name of the combo</param>
    /// <param name="combo">the combo itself</param>
    /// <param name="values">values, can be more than one as a player can have more than one pair or a flush and a pair</param>
    public CardMatch(GameObject playerID, string name, CardCombos combo, params int[] values )
    {
        this.name = name;
        this.combo = combo;
        //order the combos by value
        this.values = values.OrderByDescending(i => i).ToArray();
        this.player = playerID;

        if (values.Length == 0)
        {
            values = new int[] { -1 };
        }

    }
}
