using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(PhotonView))]
public class TurnSwitch : Photon.MonoBehaviour
{
    /// <summary>
    /// functionality to be added
    /// </summary>
    //public static bool call;
    //private static bool raise;
    //private int pot;
    
        //

    CardManager cardRiver;

    //

    bool playerOneTurn;
    bool playerTwoTurn;

    //bool dealerTurn;
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





        //have a start button that instantiates the trigger?



        //Debug.Log("player " + PhotonNetwork.player.ID.ToString());
        Debug.Log("owner " + this.photonView.ownerId.ToString());
        //if (photonView.isMine && PhotonNetwork.player.ID == 1)
        //{
        //    Debug.Log("player 1 clicked");

        //    //if (PhotonNetwork.player.ID == this.photonView.ownerId && playerOneTurn)
        //    {
        //        //Debug.Log("moved in player 1");
        //        //TurnTrigger.transform.position = new Vector3(-2.2f, 0f, 4.57f);

        //        //hvad er bedst?=
        //        //if (PhotonNetwork.player.ID == 1 && playerOneTurn)
        //        {
        //            Debug.Log("From Owner: " + this.photonView.ownerId.ToString() + "and player" + PhotonNetwork.player.ID.ToString());

        //            Debug.Log("To Owner: " + this.photonView.ownerId.ToString());

        //            playerOneTurn = false;
        //            playerTwoTurn = true;
        //        }
        //    }
        //}
        ////if (photonView.isMine && PhotonNetwork.player.ID == 2)
        //{
        //    Debug.Log("player 2 clicked");
        //    //if (PhotonNetwork.player.ID == this.photonView.ownerId && playerTwoTurn)
        //    {

        //        //TurnTrigger.transform.position = new Vector3(4.32f, 0f, -0.54f);


        //        //hvad er bedst?
        //        //if (PhotonNetwork.player.ID == 2 && playerTwoTurn)
        //        {
        //            Debug.Log("From Owner: " + this.photonView.ownerId.ToString() + "and player" + PhotonNetwork.player.ID.ToString());
        //            this.photonView.TransferOwnership(1);
        //            Debug.Log("To Owner: " + this.photonView.ownerId.ToString());

        //            playerOneTurn = true;
        //            playerTwoTurn = false;
        //        }
        //    }
        //}
    }


    //[PunRPC]
    //void SwitchTurns(int t)
    //{
    //    Debug.Log("Turn: " + t.ToString());
    //    turn = t;
    //}

    //public void OnOwnershipRequest(object[] viewAndPlayer)
    //{
    //    PhotonView view = viewAndPlayer[0] as PhotonView;
    //    PhotonPlayer requestingPlayer = viewAndPlayer[1] as PhotonPlayer;

    //    Debug.Log("OnOwnershipRequest(): Player " + requestingPlayer + " requests ownership of: " + view + ".");
    //    if (this.TransferOwnershipOnRequest)
    //    {
    //        view.TransferOwnership(requestingPlayer.ID);
    //    }
    //}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)

    {
        if (stream.isWriting)
        {
            //send the next turn to next player
            //make it uninteractable to others


            //stream.SendNext((bool)playerOneTurn);
            //stream.SendNext((bool)playerTwoTurn);
            stream.SendNext(turn);

            stream.SendNext(photonView.ownerId);
            stream.SendNext(TurnTrigger.transform.position);
        }
        else
        {

            //playerOneTurn = (bool)stream.ReceiveNext();
            //playerTwoTurn = (bool)stream.ReceiveNext();
            //this.transform.position = (Vector3)stream.ReceiveNext();
            this.turn = (int)stream.ReceiveNext();
            photonView.ownerId = (int)stream.ReceiveNext();
            this.TurnTrigger.transform.position = (Vector3)stream.ReceiveNext();
        }
    }
}



