using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ComparePaires : ICardCompare
{
    public CardMatch Matches(int playerID, Card[] hand, Card[] community)
    {
        if (hand.Length >= 2 && hand[0].Value == hand[1].Value)
        {
            return new CardMatch(playerID, "One Pair", CardCombos.OnePair, hand[0].Value);
        }
        else
        {
            var paires = new List<int>();

            foreach (var i in hand)
            {
                if (community.Any(c => c.Value == i.Value))
                {
                    paires.Add(i.Value);
                }
            }

            switch (paires.Count)
            {
                case 2:
                    return new CardMatch(playerID, "Two Pair", CardCombos.TwoPair, paires.Max(), paires.Min());

                case 1:
                    return new CardMatch(playerID, "One Pair", CardCombos.OnePair, paires.First());

                case 0:
                default:
                    return new CardMatch(playerID, "-ERR", CardCombos.ERRR);
            }
        }
    }
}
