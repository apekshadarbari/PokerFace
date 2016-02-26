using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : Photon.MonoBehaviour
{
    //list of the cards
    private List<Card> cards = new List<Card>();

    //array of the cards positions in the room (cardslots)
    [SerializeField]
    private Transform[] cardslots;

    [SerializeField]
    private bool isCommunity;

    /// <summary>
    /// takes a card
    /// </summary>
    /// <param name="card">card from card manager</param>
    [PunRPC]
    private void DealCard(byte[] memoryBuffer)
    {
        Card card = Card.Deserialize(memoryBuffer);

        //adds the card to the list cards in cardhoolder
        cards.Add(card);
        var behaviour = new GameObject("card_behaviour", typeof(CardBehaviour)).GetComponent<CardBehaviour>();
        behaviour.Card = card;
        behaviour.LoadResource();
        //how many cards are in the list already, match index of new card with cardslot, and set that cardslot as parrent
        behaviour.transform.SetParent(cardslots[cards.Count - 1], true);
        behaviour.transform.localPosition = Vector3.zero;
        behaviour.transform.localRotation = Quaternion.identity;

        if (isCommunity)
            behaviour.FacePlayer();
        //reset transform ( bug avoidance)
        //behaviour.transform.localPosition = Vector3.zero;
        //behaviour.transform.localRotation = Quaternion.identity;
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
        //foreach (var card in cards)
        //{
        //    card.transform.parent = null;
        //}
        //Debug.Log("Removcards please");

        cards.Clear();
        foreach (var s in cardslots)
        {
            foreach (var b in s.GetComponentsInChildren<CardBehaviour>())
            {
                Destroy(b.gameObject);
            }
        }
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        ////what are we sending to the network?
        if (stream.isWriting)
        {
            //stream.SendNext(cards);
            //stream.SendNext(cardslots);
        }
        ////what are we receiving from the network?
        else
        {
            //this.cards = (List<Card>)stream.ReceiveNext();
            //this.cardslots = (Transform[])stream.ReceiveNext();
        }
    }

    //private void ReceiveCard( Card card )
    //{
    //    var behaviour = new GameObject("card_behavior", typeof(CardBehaviour)).GetComponent<CardBehaviour>();
    //    behaviour.Card = card;
    //    behaviour.LoadResource();

    //    behaviour.transform.SetParent(- );
    //    behaviour.transform.position = Vector3.zero;
    //    behaviour.transform.rotation = Quaternion.identity;
    //}
}