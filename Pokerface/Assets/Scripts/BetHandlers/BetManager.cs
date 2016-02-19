using UnityEngine;
using System.Collections;

public class BetManager : Photon.MonoBehaviour
{
    //the amount that needs to be called to keep playing
    [SerializeField]//so we can see it in the inspector
    private int amountToCall;

    private int player;//int we get from potman

    //how many chips are added each time the + button is used
    private int chipsToIncrement;
    //how many chips are we betting
    private int chipsToBet;
    //the value the player wants to raise
    [SerializeField]
    private int chipsToRaise;
    //an instance of the turnswitch scripts
    [SerializeField]
    TurnSwitch ts;
    //an instance of the pot
    PotManager pot;
    //the players wallet I.E. chipvalue
    [SerializeField]
    GameObject wallet;

    [SerializeField]
    AudioManager audMan;

    public int ChipsToRaise
    {
        get
        {
            return chipsToRaise;
        }

        set
        {
            chipsToRaise = value;

        }
    }

    void Start()
    {
        //reset of the values (starting values)
        chipsToIncrement = 5;
        chipsToRaise = 0;
        //amountToCall = 0;
        //find the pot
        pot = GameObject.Find("pot").GetComponent<PotManager>();

        audMan = GameObject.Find("AudioSource").GetComponent<AudioManager>();

        if (this.photonView.ownerId == 2)
        {
            this.tag = "Player2BetController";

            foreach (Transform t in this.transform)
            {
                t.gameObject.tag = "PlayerTwoButton";
            }
        }
        else if (this.photonView.ownerId == 1)
        {

            this.tag = "Player1BetController";

            foreach (Transform t in this.transform)
            {
                t.gameObject.tag = "PlayerOneButton";
            }
        }

        pot.PotComparison(this.photonView.ownerId, 0);
        //ts = GameObject.FindGameObjectWithTag("TurnTrigger").GetComponent<TurnSwitch>();


        //can be used for trading the buttons if we want ---
        //if (this.photonView.ownerId == 1)
        //{
        //	this.photonView.transform.position = new Vector3(-1.588f, 0.34f, 1.51f);
        //}
        //if (this.photonView.ownerId == 2)
        //{
        //	this.photonView.transform.position = new Vector3(-2f, -0.75f, 3f);
        //}
    }


    /// <summary>
    /// add chips (plus button)
    /// </summary>
    public void AddChips()
    {
        chipsToRaise = chipsToRaise + chipsToIncrement;
        Debug.Log("add chips detected : " + chipsToRaise);
    }

    /// <summary>
    /// remove chips - (minus button)
    /// </summary>
    public void RemoveChips()
    {
        Debug.Log("remove chips detected ");
        if (chipsToRaise <= 0)
        {
            chipsToRaise = 0;
        }
        else chipsToRaise = chipsToRaise - chipsToIncrement;
    }

    /// <summary>
    /// raise button clicked
    /// </summary>
    public void RaiseChips()
    {
        ts = GameObject.FindGameObjectWithTag("TurnTrigger").GetComponent<TurnSwitch>();


        //the chips we want to bet is set to the raised chips an used with the current user(owner - player ´s id)
        //we get chips from our wallet as the player id
        chipsToBet = wallet.GetComponent<WalletManager>().GetChips(this.photonView.ownerId, chipsToRaise);

        //add the chips to the pot TODO: should add to the player pot
        //pot.AddChips(chipsToBet);

        Debug.Log("chips to raise value is " + chipsToRaise + "the amount being sent on is " + ChipsToRaise);

        //tell the pot the value we want to raise

        //reset the chips we want to raise
        //chipsToRaise = 0;
        if (chipsToBet == amountToCall)
        {
            CallCheck();
        }
        if (chipsToBet > amountToCall)
        {
            pot.PotComparison(this.photonView.ownerId, chipsToBet);

            if (this.photonView.ownerId == 1)
            {
                audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Raise);
            }
            if (this.photonView.ownerId == 2)
            {
                audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p2Raise);
            }
            ts.GetComponent<TurnSwitch>().OnClick();
        }
        //chipsToBet = wallet.GetComponent<WalletManager>().GetChips(this.photonView.ownerId, amountToCall);//we get chips equal to the amount needed to call

        //pot.AddChips(chipsToBet);//we add chips to the pot equal to the bet/call


    }

    /// <summary>
    /// which player has to call how much?
    /// </summary>
    /// <param name="player"> player who needs to call / has bet the least</param>
    /// <param name="amountToCall"> the amount the player needs to call</param>

    public void GetAmountToCall(int player, int amountToCall)
    {
        //we set the scripts field values = the the values we get from the pot
        this.player = player;
        this.amountToCall = amountToCall;


        //we might be able to run call/check again here 
        //CallCheck();
        //return player,amountToCall;
    }

    /// <summary>
    /// call the current bet
    /// </summary>
    public void CallCheck()
    {
        Debug.Log("player " + this.photonView.ownerId + " checks/calls");

        Debug.Log("amount to call = " + amountToCall);

        ts = GameObject.FindGameObjectWithTag("TurnTrigger").GetComponent<TurnSwitch>();


        if (amountToCall == 0)
        {
            //try to check with 0 added value to the pot

            if (this.photonView.ownerId == 1)
            {
                //we can tell the button to play the call sound for player a sound..
                //we should do it using an RPC though...
                audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Check);
                //audMan.ButtonPressedAudio(ActionSound.p1Call);
            }
            else if (this.photonView.ownerId == 2)
            {
                audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p2Check);
                //audMan.ButtonPressedAudio(ActionSound.p1Call);
            }
            pot.PotComparison(this.photonView.ownerId, 0);
        }
        else
        {
            if (this.photonView.ownerId == 1)
            {
                //we can tell the button to play the call sound for player a sound..
                //we should do it using an RPC though...
                audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Call);
                //audMan.ButtonPressedAudio(ActionSound.p1Call);
            }
            else if (this.photonView.ownerId == 2)
            {
                audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p2Call);
                //audMan.ButtonPressedAudio(ActionSound.p1Call);
            }

            //TODO: get rid of redundancy in the wallet call
            chipsToBet = wallet.GetComponent<WalletManager>().GetChips(this.photonView.ownerId, amountToCall);//we get chips equal to the amount needed to call

            pot.AddChipsToPot(chipsToBet);//we add chips to the pot equal to the bet/call

            //	ts.GetComponent<TurnSwitch> ().potComparison(amt_to_call);
            //AmountToCall = 0;

        }

        ts.OnClick();//we send the turn on to our opponent

    }

    /// <summary>
    /// fold
    /// </summary>
	public void Fold()
    {
        ts = GameObject.FindGameObjectWithTag("TurnTrigger").GetComponent<TurnSwitch>();

        if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
        {
            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Fold);

            Debug.Log("player " + this.photonView.ownerId + " folds");
            //WalletManager.player1ChipValue = pot.GetComponent<PotManager>().chipValue; //what are we trying to do here?
            pot.GetComponent<PotManager>().chipValue = 0;//TODO: what are we trying to do here?
            // needs more logic here to end game
            return;
        }
        else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
        {
            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Fold);

            Debug.Log("player " + this.photonView.ownerId + " folds");
            //wallet.GetComponent<WalletManager> ().player2ChipValue = pot.GetComponent<PotManager>().chipValue;
            //WalletManager.player2ChipValue = pot.chipValue;
            pot.GetComponent<PotManager>().chipValue = 0;
            // needs more logic here to end game
            return;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }

}
