using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(PhotonView))]
public class TurnSwitch : Photon.MonoBehaviour, IClicker
{
    CardManager cardRiver;

    bool playerOneTurn;
    bool playerTwoTurn;

    //test
    int turn = 4;

    PhotonPlayer player;

    [SerializeField]
    GameObject turnTrigger;

    public GameObject TurnTrigger
    {
        get
        {
            return turnTrigger;
        }

        set
        {
            turnTrigger = value;
        }
    }
    void Awake()
    {
        Debug.Log("pre awake owner " + photonView.ownerId.ToString());
        //if (this.photonView.ownerId == 0)
        //{
        //    this.photonView.TransferOwnership(1);
        //    playerOneTurn = true;
        //}


        Debug.Log("post awake owner " + photonView.ownerId.ToString());
    }

    void Start()
    {
        cardRiver = GameObject.Find("Card_Manager").GetComponent<CardManager>();
    }
    void Update()
    {
        switch (this.photonView.ownerId)
        {
            case 1:
                TurnTrigger.transform.position = new Vector3(4.32f, 0f, -0.54f);
                break;
            case 2:
                TurnTrigger.transform.position = new Vector3(-2.2f, 0f, 4.57f);
                break;
            default:
                break;
        }

        //river 
        if (turn == 5)
        {
            //CardManager.

            Debug.Log("river");
            cardRiver.DealRiver();
        }

        //post river
        if (turn == 9)
        {
            //CardManager.

            Debug.Log("river");
            cardRiver.compareCards();
        }

    }

    void PlayerOneTrigger()
    {

    }

    public void OnClick()
    {
        turn++;
        Debug.Log("Turn: " + turn.ToString());
        if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
        {
            this.photonView.TransferOwnership(1);
            return;
        }
        else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
        {
            this.photonView.TransferOwnership(2);
            return;
        }

        Debug.Log("owner " + this.photonView.ownerId.ToString());

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)

    {
        if (stream.isWriting)
        {
            //send the next turn to next player
            //make it uninteractable to others

            stream.SendNext(turn);

            stream.SendNext(photonView.ownerId);
            stream.SendNext(TurnTrigger.transform.position);
        }
        else
        {
            this.turn = (int)stream.ReceiveNext();
            photonView.ownerId = (int)stream.ReceiveNext();
            this.TurnTrigger.transform.position = (Vector3)stream.ReceiveNext();
        }
    }
}



