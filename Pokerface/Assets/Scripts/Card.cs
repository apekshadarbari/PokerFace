using UnityEngine;
using System.Collections;

public enum Suits
{
    Diamond,
    Spade,
    Club,
    Heart,
}


public class Card : MonoBehaviour
{
    private int value;
    private Suits suit;

    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }
    public Suits Suit
    {
        get { return suit; }
        set { suit = value; }
    }
    public string suitName; // TODO: NO PUBLIC FIELDS!!!
}
