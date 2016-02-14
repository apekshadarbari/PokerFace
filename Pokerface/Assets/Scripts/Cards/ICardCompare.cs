using UnityEngine;
using System.Collections;

public interface ICardCompare
{
    /// <summary>
    /// Interface for comparison of cards, all children use this I.E. comparer scripts
    /// </summary>
    /// <param name="playerID">the player who has the combo</param>
    /// <param name="hand">the hand of cardds</param>
    /// <param name="community">the available community cards</param>
    /// <returns></returns>
    CardMatch Matches(GameObject playerID, Card[] hand, Card[] community);
}
