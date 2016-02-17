using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

//[RequireComponent(typeof(PhotonView))]
public class TurnSwitch : Photon.MonoBehaviour, IClicker
{

    bool gameIsStarted;
    bool flopIsDealt;
    //test turn-counter
    int turn;
    [SerializeField]
    GameObject turnTrigger;
    //intereaction with cards
    CardManager deckInteraction;
    //BetMore betInteraction;

    PotManager pot;

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
        //make sure the owner id is 1 from the start
        if (this.photonView.ownerId == 0)
        {
            this.photonView.TransferOwnership(1);
        }

        Debug.Log("post awake owner " + photonView.ownerId.ToString());
    }

    void Start()
    {
        //turntrigger gets instantiated after start button is clicked
        //then we use the cardmanager built into it
        deckInteraction = GetComponent<CardManager>();

        //pot = GameObject.Find("pot").GetComponent<PotManager>();

        //betInteraction = GetComponent<BetMore>();

        if (deckInteraction == null)
        {
            Debug.Log("No Deck");
        }

        //game just started so we set everything to 0
        flopIsDealt = false;
        gameIsStarted = false;
        turn = 0;

        Debug.Log("I´m started");
        Debug.Log("Turn " + turn.ToString());

    }

    void Update()
    {
        //make sure the trigger is in the right position
        switch (this.photonView.ownerId)
        {
            case 1:
                TurnTrigger.transform.position = new Vector3(2.8f, .4f, 0f);
                break;
            case 2:
                TurnTrigger.transform.position = new Vector3(-2.2f, 0f, 4.57f);
                break;
            default:
                break;
        }

        //go through the turns, to see what needs to happen
        switch (turn)
        {
            //game (round) just started 
            case 0:
                if (!gameIsStarted) //make sure we only do it once
                {
                    //make sure we only run this once in the scene


                    //TODO: try without masteclient? does player two get the righht cards still?
                    if (PhotonNetwork.isMasterClient)
                    {
                        deckInteraction.GetComponent<PhotonView>().RPC("Shuffle", PhotonTargets.AllBufferedViaServer);
                        deckInteraction.GetComponent<PhotonView>().RPC("Deal", PhotonTargets.AllBufferedViaServer);
                        
                        //we shuffle and deal to starting cards
                        //deckInteraction.Shuffle();
                        //deckInteraction.Deal();

                        gameIsStarted = true;
                    }
                }
                break;
            //deal the flop
            case 1:
                if (!flopIsDealt) //make sure we only do it once
                {
                    flopIsDealt = true;
                    deckInteraction.GetComponent<PhotonView>().RPC("DealFlop", PhotonTargets.AllBufferedViaServer);

                    //temp compare for testing
                    //deckInteraction.CompareCards();
                    deckInteraction.GetComponent<PhotonView>().RPC("CompareCards", PhotonTargets.AllBufferedViaServer);

                }
                break;
            //deal the turn
            case 2:
                break;
            //deal the river
            case 3:
                //reset round after the game is over
                //turn = 0;
                break;
            //compare cards
            case 4:
                //compare the cards
                deckInteraction.CompareCards();
                break;
            default:
                break;
        }
    }

    /// <summary>
    ///  when clicking the turnswitch I.E. we want to switch turn or are done with our turn and or can do no more
    /// </summary>
    public void OnClick()
    {
        //increment the turn
        turn++;

        Debug.Log("Turn: " + turn.ToString());
        Debug.Log("this.photonView.ownerId: " + this.photonView.ownerId);
        Debug.Log("PhotonNetwork.player.ID: " + PhotonNetwork.player.ID);

        //if it was player two´s turn
        if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
        {
            //		GameObject.FindGameObjectWithTag ("Player2betController").GetComponentInChildren<MeshRenderer>().enabled = false;
            //		GameObject.FindGameObjectWithTag ("Player2betController").GetComponentInChildren<SphereCollider> ().enabled = false;

            Debug.Log("player 2 transferring control to 1");

            //deactivate the buttons
            GameObject.FindGameObjectWithTag("Player2BetController").SetActive(false);
            //GameObject.FindGameObjectWithTag("Player1BetController").SetActive(true);


            //transfor ownership to the other player
            this.photonView.TransferOwnership(1);
        }
        //if it was player one´s turn
        else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
        {
            //			GameObject.FindGameObjectWithTag ("Player2betController").GetComponentInChildren<MeshRenderer>().enabled = false;
            //			GameObject.FindGameObjectWithTag ("Player2betController").GetComponentInChildren<SphereCollider> ().enabled = false;

            //deactivate the buttons
            GameObject.FindGameObjectWithTag("Player1BetController").SetActive(false);
            //GameObject.FindGameObjectWithTag("Player2BetController").SetActive(true);

            Debug.Log("player 1 transferring control to 2");

            //transfor ownership to the other player
            this.photonView.TransferOwnership(2);
        }

        Debug.Log("owner " + this.photonView.ownerId.ToString());

    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        //{
        if (stream.isWriting)
        {
            //send the next turn to next player
            //make it uninteractable to others

            stream.SendNext(gameIsStarted);
            stream.SendNext(flopIsDealt);

            stream.SendNext(turn);

            stream.SendNext(photonView.ownerId);
            stream.SendNext(TurnTrigger.transform.position);
        }
        else
        {
            this.gameIsStarted = (bool)stream.ReceiveNext();
            this.flopIsDealt = (bool)stream.ReceiveNext();

            this.turn = (int)stream.ReceiveNext();

            photonView.ownerId = (int)stream.ReceiveNext();
            this.TurnTrigger.transform.position = (Vector3)stream.ReceiveNext();
        }
    }

    public void OnHover()
    {
        GetComponent<Renderer>().material.color = Color.red;
        CrosshairTimerDisplay.Instance.Show();
    }

    public void OnExitHover()
    {
        GetComponent<Renderer>().material.color = Color.white;
        CrosshairTimerDisplay.Instance.Hide();
    }
}



