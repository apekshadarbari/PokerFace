using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField]
    private string cardName;

    private Card card;

    public Card Card
    {
        get { return card; }
        set
        {
            card = value;
            cardName = card.ToString();
        }
    }

    public void LoadResource()
    {
        string valueName;
        switch (card.Value)
        {
            case 11:
                valueName = "J";
                break;
            case 12:
                valueName = "Q";
                break;
            case 13:
                valueName = "K";
                break;
            case 14:
                valueName = "A";
                break;

            default:
                valueName = card.Value.ToString();
                break;
        }

        var obj = Instantiate( Resources.Load(string.Format("Card_{0}{1}", valueName, card.Suit))) as GameObject;
        obj.transform.SetParent(transform);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
    }
}
