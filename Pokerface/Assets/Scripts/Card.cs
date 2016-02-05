using UnityEngine;
using System.Collections;
using System;

public enum Suits
{
    Diamond,
    Spade,
    Club,
    Heart,
}


public class Card : MonoBehaviour
{
    [SerializeField]
    private int value;
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
}
