using System.Collections;
using UnityEngine;

internal enum chipLocation
{
    p1Wallet,
    p2Wallet,
    p1Bet,
    p2Bet,
    p1Pot,
    p2Pot,
    pot,
}

public class MoveChips : PhotonManager<MoveChips>
{
    private Transform targetLocation;

    /// <summary>
    /// show the movement of the chips
    /// </summary>
    private void Update()
    {
        //move toward the target position;

        //Destroy(this); // destroy when done
    }

    /// <summary>
    /// we need to know which transforms to move and to where
    /// we take the positions of the chips and move give them those transforms as target locations
    ///
    /// </summary>
    /// <param name="transforms">the transforms we want to move</param>
    ///   <param name="target">the location it moves to</param>
    private void MoveChipsTo(Transform[] transforms, chipLocation target)
    {
        switch (target)
        {
            case chipLocation.p1Wallet:
                break;

            case chipLocation.p2Wallet:
                break;

            case chipLocation.p1Bet:
                break;

            case chipLocation.p2Bet:
                break;

            case chipLocation.p1Pot:
                break;

            case chipLocation.p2Pot:
                break;

            case chipLocation.pot:
                break;

            default:
                break;
        }
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
        }
        else
        {
        }
    }
}