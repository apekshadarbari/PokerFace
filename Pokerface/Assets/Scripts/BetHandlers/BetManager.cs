using System;
using UnityEngine;

public class BetManager : PhotonManager<BetManager>
{
    //[SerializeField]
    private Wallet walletText;

    [SerializeField]
    private int chipsToIncrement = 5;

    [SerializeField]
    private AudioManager audMan;

    [SerializeField]
    private int betValue;

    private int player;//int we get from PotManager.Instanceman

    //[SerializeField]
    //private bool playerOneChecks;

    //[SerializeField]
    //private bool playerTwoChecks;

    //how many chips are added each time the + button is used

    //how many chips are we betting

    //the value the player wants to raise
    //[SerializeField]
    //private int chipsToRaise;

    private RoundManager roundMan;

    //the players wallet I.E. chipvalue
    //[SerializeField]
    private bool wantsNextRound;

    private Canvas infoBoard;

    private int callValue;

    public int ChipsToRaise
    {
        get { return betValue; }
        set { betValue = value; }
    }

    private int p1BetVal;
    private int p2BetVal;

    public int P1BetVal
    {
        get { return p1BetVal; }
    }

    public int P2BetVal
    {
        get { return p2BetVal; }
    }

    public int BetValue
    {
        get { return betValue; }
    }

    private void Start()
    {
        player = PhotonNetwork.player.ID;
        walletText = GameObject.FindGameObjectWithTag("Wallet").GetComponent<Wallet>();

        //infoBoard = GameObject.FindGameObjectWithTag("InfoBoard").GetComponent<Canvas>();

        //reset of the values (starting values)
        betValue = 0;
        //amountToCall = 0;
        //find the PotManager.Instance

        audMan = GameObject.Find("AudioSource").GetComponent<AudioManager>();
        roundMan = GameObject.Find("Round").GetComponent<RoundManager>();

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

        //PotManager.Instance.PotManager.InstanceComparison(this.photonView.ownerId, 0);
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

    public void SetBetToCallValue()
    {
        // TODO: Call OnTurnStart from TurnSwitch instead!
        callValue = PotManager.Instance.GetCallValue(player);
        betValue = callValue;
    }

    private void Update()
    {
        //infoBoard.GetComponent<PhotonView>().RPC("TextAmountToCall", PhotonTargets.AllBuffered, this.photonView.ownerId, amountToCall);
    }
    /// <summary>
    /// add chips (plus button)
    /// </summary>
    public void IncreaseBet()
    {
        betValue = betValue + chipsToIncrement;
        //Debug.Log("add chips detected : " + betValue);
        BetvalueUpdate();
    }
    public void BetvalueUpdate()//might wanna give it player
    {
        if (player == 0)
        {
            player = 1;
        }

        Wallet.Instance.BetUpdate(betValue, this.player);
        ConfirmHUD.Instance.CurrentValueToCall(PotManager.Instance.GetCallValue(player));
        ConfirmHUD.Instance.CurrentBetValue(betValue);
    }
    /// <summary>
    /// remove chips - (minus button)
    /// </summary>
    public void DecreaseBet()
    {
        Debug.Log("remove chips detected ");
        if (betValue <= 0)
        {
            betValue = 0;
        }
        else
        {
            ResetBet();
        }
        BetvalueUpdate();
        //else betValue = betValue - chipsToIncrement;
    }
    public void ResetBet()
    {
        this.betValue = 0;
        //gameObject.GetComponent<PhotonView>().RPC("ResetBestTask", PhotonTargets.All);
        //ChipsToRaise = 0;
    }

    [PunRPC]
    private void ResetBestTask()
    {
        this.betValue = 0;
    }

    /// <summary>
    /// start the turn by updating the values
    /// </summary>
    public void OnTurnStart()
    {
        //Debug.Log("- OnTurnStart - Betmanager ");

        callValue = PotManager.Instance.GetCallValue(player); // get callvalue

        //ResetBet(); // reset the betvalue to 0
        BetvalueUpdate(); // update the values for the hud

        //SetBetToCallValue(); // setting the betvalue to the callvalue we got before
    }

    public void Bet()
    {
        // TODO: Call OnTurnStart from TurnSwitch instead!
        callValue = PotManager.Instance.GetCallValue(player);
        if (betValue < callValue)
        {
            SetBetToCallValue();
        }

        if (WalletManager.Instance.Withdraw(betValue))
        {
            //if (betValue < callValue)
            //{
            //    //throw new NotImplementedException("You have to at least match the other players bet...");
            //    wantsNextRound = true;

            //    Call(betValue);
            //}
            if (betValue > callValue)
            {
                wantsNextRound = false;
                Raise(betValue);
            }
            else if (callValue == 0)
            {
                wantsNextRound = true;
                Check();
            }
            else
            {
                wantsNextRound = true;
                //SetBetToCallValue();
                Call(betValue);
            }
            // End turn
            //Debug.Log("Calling TURNMANAGER OnTurnEnd");
            TurnManager.Instance.OnTurnEnd(this.photonView.ownerId, wantsNextRound);
            BetvalueUpdate();
        }
        else
        {
            throw new InvalidOperationException("Not enough credits in wallet for bet!");
        }
    }

    private void Check()
    {
        // ANNOUNCE CHECK
        if (player == 1)
        {
            //Debug.Log("Player One Checks");
            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Check);
        }
        if (player == 2)
        {
            //Debug.Log("Player Two Checks");
            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p2Check);
        }
    }

    private void Call(int betValue)
    {
        if (player == 1)
        {
            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Call);
        }
        else if (player == 2)
        {
            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p2Call);
        }

        PotManager.Instance.Bet(player, betValue);

        // ANNOUNCE CALL
    }

    private void Raise(int betValue)
    {
        // IF OPPONENT CAN'T MATCH
        //      REJECT RAISE

        // ANOUNCE RAISE
        if (player == 1)
        {
            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Raise);
        }
        else if (player == 2)
        {
            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p2Raise);
        }
        PotManager.Instance.Bet(player, betValue);
    }

    public void Fold()
    {
        if (WalletManager.Instance.Withdraw(betValue))
        {
            if (player == 1)
            {
                audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Fold);
            }
            else if (player == 2)
            {
                audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Fold);
            }
        }
        RoundManager.Instance.HandEnd(player, true);
        //roundMan.GetComponent<PhotonView>().RPC("HandEnd", PhotonTargets.All, player, true);
    }
    //void Update()
    //{
    //    betValue;
    //}
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}