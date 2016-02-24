using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

//[RequireComponent(typeof(PhotonView))]
public class TurnSwitch : PhotonManager<TurnSwitch>, IClicker
{
    private bool gameIsStarted;
    private bool flopIsDealt;

    //test turn-counter
    private int turn;

    [SerializeField]
    private GameObject turnTrigger;

    //intereaction with cards
    private CardManager deckInteraction;

    //BetMore betInteraction;

    private GameObject[] btnOne;
    private GameObject[] btnTwo;

    private PotManager pot;

    public GameObject TurnTrigger
    {
        get { return turnTrigger; }
        set { turnTrigger = value; }
    }

    public int Turn
    {
        get
        {
            return turn;
        }

        set
        {
            turn = value;
        }
    }

    protected override void Awake()
    {
        //base.Awake();
        ////Debug.Log("pre awake owner " + photonView.ownerId.ToString());
        ////make sure the owner id is 1 from the start
        if (this.photonView.ownerId == 0)
        {
            this.photonView.TransferOwnership(1);
        }
        //Debug.Log("post awake owner " + photonView.ownerId.ToString());
    }

    private void Start()
    {
        if (this.photonView.ownerId == 0)
        {
            this.photonView.TransferOwnership(1);
        }
        //turntrigger gets instantiated after start button is clicked
        //then we use the cardmanager built into it
        //deckInteraction = GetComponent<CardManager>();

        //btnOne = GameObject.FindGameObjectsWithTag("PlayerOneButton");
        //btnTwo = GameObject.FindGameObjectsWithTag("PlayerTwoButton");

        //if (deckInteraction == null)
        //{
        //    //Debug.Log("No Deck");
        //}

        ////game just started so we set everything to 0
        //flopIsDealt = false;
        //gameIsStarted = false;

        //TODO > untill the 2 pot amounts this should not be changed
        //Turn = 0;

        //Debug.Log("I´m started");
        //Debug.Log("Turn " + Turn.ToString());
    }

    private void Update()
    {
        //make sure the trigger is in the right position
        //switch (this.photonView.ownerId)
        //{
        //    case 1:
        //        TurnTrigger.transform.position = new Vector3(2.8f, 1.35f, 0f);
        //        foreach (var btn in btnTwo)
        //        {
        //            btn.GetComponent<MeshRenderer>().enabled = false;
        //            btn.GetComponent<SphereCollider>().enabled = false;
        //        }
        //        foreach (var btn in btnOne)
        //        {
        //            btn.GetComponent<MeshRenderer>().enabled = true;
        //            btn.GetComponent<SphereCollider>().enabled = true;
        //        }

        //        break;

        //    case 2:
        //        TurnTrigger.transform.position = new Vector3(-2.75f, 1.35f, 0f);
        //        //deactivate the buttons //TODO: Make more effecient
        //        foreach (var btn in btnTwo)
        //        {
        //            btn.GetComponent<MeshRenderer>().enabled = true;
        //            btn.GetComponent<SphereCollider>().enabled = true;
        //        }
        //        foreach (var btn in btnOne)
        //        {
        //            btn.GetComponent<MeshRenderer>().enabled = false;
        //            btn.GetComponent<SphereCollider>().enabled = false;
        //        }

        //        break;

        //    default:
        //        break;
        //}
        //switch (Turn)
        //{
        //    //game (round) just started
        //    case 0:
        //        if (!gameIsStarted) //make sure we only do it once
        //        {
        //            //make sure we only run this once in the scene

        //            //TODO: try without masteclient? does player two get the righht cards still?
        //            if (PhotonNetwork.isMasterClient)
        //            {
        //                //we shuffle and deal to starting cards
        //                deckInteraction.Shuffle();
        //                deckInteraction.Deal();

        //                gameIsStarted = true;
        //            }
        //        }
        //        break;
        //    //deal the flop
        //    case 1:
        //        //if (!flopIsDealt) //make sure we only do it once
        //        //{
        //        //    if (PhotonNetwork.isMasterClient)
        //        //    {
        //        //        //deckInteraction.DealFlop();
        //        //        //deckInteraction.GetComponent<PhotonView>().RPC("CompareCards", PhotonTargets.AllBufferedViaServer);

        //        //        //deckInteraction.GetComponent<PhotonView>().RPC("DealFlop", PhotonTargets.AllBufferedViaServer);

        //        //        //temp compare for testing
        //        //        //deckInteraction.CompareCards();
        //        //        flopIsDealt = true;
        //        //    }
        //        //}
        //        break;
        //    //deal the turn
        //    case 2:
        //        break;
        //    //deal the river
        //    case 3:
        //        //reset round after the game is over
        //        //turn = 0;
        //        break;
        //    //compare cards
        //    case 4:
        //        //compare the cards
        //        deckInteraction.CompareCards();
        //        break;

        //    default:
        //        break;
        //}

        //go through the turns, to see what needs to happen
    }

    /// <summary>
    ///  when clicking the turnswitch I.E. we want to switch turn or are done with our turn and or can do no more
    /// </summary>
    public void EndTurn()
    {
        //Turn++;
        //gameObject.GetComponent<PhotonView>().RPC("TurnChange", PhotonTargets.AllBuffered, Turn, gameIsStarted, flopIsDealt);

        //increment the turn

        //Debug.Log("Turn: " + Turn);
        //Debug.Log("this.photonView.ownerId: " + this.photonView.ownerId);
        //Debug.Log("PhotonNetwork.player.ID: " + PhotonNetwork.player.ID);

        //PotManager.Instance.DumpIfEqual();

        ////if it was player two´s turn
        //if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
        //{
        //    //deactivate the buttons //TODO: Make more effecient

        //    Debug.Log("player 2 transferring control to 1");

        //    //dump temp bets (remake when making round mech)

        //    //transfor ownership to the other player
        //    this.photonView.TransferOwnership(1);
        //}
        ////if it was player one´s turn
        //else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
        //{
        //    Debug.Log("player 1 transferring control to 2");

        //    //transfor ownership to the other player
        //    this.photonView.TransferOwnership(2);
        //}

        //Debug.Log("owner " + this.photonView.ownerId.ToString());
    }

    //[PunRPC]
    //private void TurnChange(int turn, bool gameStarted, bool flopDealt)
    //{
    //    this.Turn = turn;
    //    Debug.Log("Turn: " + turn);
    //    this.gameIsStarted = gameStarted;
    //    this.flopIsDealt = flopDealt;
    //    //return this.Turn;
    //    BetManager.Instance.OnTurnStart();
    //}

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(photonView.ownerId);
            stream.SendNext(TurnTrigger.transform.position);
        }
        else
        {
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