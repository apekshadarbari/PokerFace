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



    bool playerOneTurn;
    bool playerTwoTurn;

    //bool dealerTurn;

    int turn;

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
        Debug.Log("post awake owner " + photonView.ownerId.ToString());
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
    }

    void PlayerOneTrigger()
    {

    }

    public void OnClick()
    {

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

        turn++;
        Debug.Log("Turn: " + turn.ToString());
        Debug.Log("owner " + this.photonView.ownerId.ToString());
        
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)

    {
        if (stream.isWriting)
        {
            //send the next turn to next player
            //make it uninteractable to others

            stream.SendNext(photonView.ownerId);
            stream.SendNext(TurnTrigger.transform.position);
        }
        else
        {

            photonView.ownerId = (int)stream.ReceiveNext();
            this.TurnTrigger.transform.position = (Vector3)stream.ReceiveNext();
        }
    }
}




