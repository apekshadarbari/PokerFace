using UnityEngine;
using System.Collections;

public interface ICardCompare
{
    CardMatch Matches(GameObject playerID, Card[] hand, Card[] community);
}
