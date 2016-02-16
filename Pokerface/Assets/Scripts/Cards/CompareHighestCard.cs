using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class CompareHighestCard : ICardCompare
{/// <summary>
/// matching cards by value to find the highest value card
/// </summary>
/// <param name="player">the player</param>
/// <param name="hand">the players hand of cards</param>
/// <param name="community">the available commmunity cards</param>
/// <returns></returns>
    public CardMatch Matches(GameObject player, Card[] hand, Card[] community)
    {
        // Order cards by their value (descending order) and then select the values from the individual cards and put them in an array.
        return new CardMatch(player, "Highest Card", CardCombos.HighestCard, hand.OrderByDescending(c => c.Value).Select(c => c.Value).ToArray());
    }
}