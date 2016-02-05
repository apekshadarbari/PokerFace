using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardHolder : MonoBehaviour {

    List<Card> cards = new List<Card>();

    [SerializeField]
    private Transform[] positions;
    
    public void DealCard(Card card)
    {
        cards.Add(card);
        card.transform.SetParent( positions[cards.Count - 1]);
        card.transform.localPosition = Vector3.zero;
        card.transform.localRotation = Quaternion.identity;
    }

    public Card[] GetCards()
    {
        return cards.ToArray();
    }

    public void RemoveAllCards()
    {
        foreach (var card in cards)
        {
            card.transform.parent = null;
            
        }
        cards.Clear();
    }


}
