using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardHolder : MonoBehaviour {

    //list of the cards
    List<Card> cards = new List<Card>();

    //array of the cards positions in the room (cardslots)
    [SerializeField]
    private Transform[] cardslots;
    
    /// <summary>
    /// takes a card
    /// </summary>
    /// <param name="card">card from card manager</param>
    public void DealCard(Card card)
    {
        //adds the card to the list cards in cardhoolder
        cards.Add(card);
        //how many cards are in the list already, match index of new card with cardslot, and set that cardslot as parrent
        card.transform.SetParent( cardslots[cards.Count - 1]);
        //reset transform ( bug avoidance)
        card.transform.localPosition = Vector3.zero;
        card.transform.localRotation = Quaternion.identity;
    }
    /// <summary>
    /// get cards dealt to this holder  - a holder for each player and one for community
    /// </summary>
    /// <returns></returns>
    public Card[] GetCards()
    {
        return cards.ToArray();
    }

    /// <summary>
    /// reset cards that have been dealt to this holder
    /// </summary>
    public void RemoveAllCards()
    {
        foreach (var card in cards)
        {
            card.transform.parent = null;
            
        }
        cards.Clear();
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }


}
