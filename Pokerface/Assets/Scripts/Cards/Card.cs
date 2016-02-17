using UnityEngine;
using System.Collections;
using System;

//globally callable suits
public enum Suits
{
    Diamond,
    Spade,
    Club,
    Heart,
}


public class Card : Photon.MonoBehaviour
{
    //the card value - higher means better card
    [SerializeField]
    private int value;
    //the cards suit
    [SerializeField]
    private Suits suit;

    public int Value
    {
        get { return value; }
    }
    public Suits Suit
    {
        get { return suit; }
    }

    [Obsolete]
    public string suitName; // TODO: NO PUBLIC FIELDS!!!

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
