using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class CompareHighestCard : ICardCompare
{
    public CardMatch Matches(int playerID, Card[] hand, Card[] community)
    {
        // Order cards by their value (descending order) and then select the values from the individual cards and put them in an array.
        return new CardMatch(playerID, "Highest Card", CardCombos.HighestCard, hand.OrderByDescending(c => c.Value).Select(c => c.Value).ToArray());
    }
}