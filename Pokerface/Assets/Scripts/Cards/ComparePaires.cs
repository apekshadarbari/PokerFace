using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ComparePaires : ICardCompare
{
    /// <summary>
    /// matches - paires
    /// </summary>
    /// <param name="player"> the player </param>
    /// <param name="hand"> the cards in the players hand </param>
    /// <param name="community"> the available community cards </param>
    /// <returns></returns>
    public CardMatch Matches(GameObject player, Card[] hand, Card[] community)
    {
        //if the card in index 0 and index 1 have the same value I.E. they are a pair
        if (hand.Length >= 2 && hand[0].Value == hand[1].Value)
        {
            //return a card match
            return new CardMatch(player, "One Pair", CardCombos.OnePair, hand[0].Value);
        }
        //if not, we need to look at the cards in the community and see if there are any paires
        else
        {
            var paires = new List<int>();
            //loop through the cards in hand
            foreach (var i in hand)
            {
                //if there are any paires in the hand + community
                if (community.Any(c => c.Value == i.Value))
                {
                    paires.Add(i.Value);
                }
            }
            //switch through the amount of paires and add them to cardmatch
            switch (paires.Count)
            {
                case 2:
                    return new CardMatch(player, "Two Pair", CardCombos.TwoPair, paires.Max(), paires.Min());

                case 1:
                    return new CardMatch(player, "One Pair", CardCombos.OnePair, paires.First());

                case 0:
                default:
                    return new CardMatch(player, "-ERR", CardCombos.ERRR);
            }
        }
    }
}
