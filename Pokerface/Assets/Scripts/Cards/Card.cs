using UnityEngine;
using System.Collections;
using System;
using ExitGames.Client.Photon;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//globally callable suits
public enum Suits
{
    Diamond,
    Spade,
    Club,
    Heart,
}

[Serializable]
public class Card //: Photon.MonoBehaviour
{
    //the card value - higher means better card
    private int value;
    //the cards suit
    private Suits suit;

    public int Value
    {
        get { return value; }
    }
    public Suits Suit
    {
        get { return suit; }
    }

    [Obsolete("NO FRICKING FUCKING PUBLIC FIELDS YOU ASS")]
    public string suitName;

    //static Card()
    //{
    //    PhotonPeer.RegisterType(typeof(Card), (byte)'W', Serialize, Deserialize);
    //}


    public Card( Suits suit, int value )
    {
        this.suit = suit;
        this.value = value;
    }

    public byte[] Serialize()
    {
        byte[] buffer = null;

        using (MemoryStream stream = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            buffer = stream.GetBuffer();
        }

        return buffer;
    }

    public static Card Deserialize(byte[] buffer)
    {
        using (MemoryStream stream = new MemoryStream(buffer))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (Card)formatter.Deserialize(stream);
        }
    }

    public override string ToString()
    {
        string valueName;
        switch( value )
        {
            case 11:
                valueName = "Jack";
                break;
            case 12:
                valueName = "Queen";
                break;
            case 13:
                valueName = "King";
                break;
            case 14:
                valueName = "Ace";
                break;

            default:
                valueName = value.ToString();
                break;
        }

        return string.Format("{0} of {1}s", valueName, suit);
    }
}
