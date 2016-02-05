using UnityEngine;
using System.Collections;

public interface ICardCompare
{
    CardMatch Matches(int playerID, Card[] hand, Card[] community);
}
