﻿using System;
using System.Collections;
using UnityEngine;

public class BetManager : PhotonManager<BetManager>
{
    [SerializeField]
    private int chipsToIncrement = 5;

    [SerializeField]
    private AudioManager audMan;

    [SerializeField]
    private int betValue;

    private int player;//int we get from PotManager.Instanceman

    [SerializeField]
    private bool playerOneChecks;

    [SerializeField]
    private bool playerTwoChecks;

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
        get
        {
            return betValue;
        }

        set
        {
            betValue = value;
        }
    }

    private void Start()
    {
        player = PhotonNetwork.player.ID;
        //turnMan = GameObject.FindGameObjectWithTag("TurnManager").GetComponent<TurnManager>();

        infoBoard = GameObject.FindGameObjectWithTag("InfoBoard").GetComponent<Canvas>();

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
        Debug.Log("add chips detected : " + betValue);
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
        else betValue = betValue - chipsToIncrement;
    }

    ///// <summary>
    ///// raise button clicked
    ///// </summary>
    //public void RaiseChips()
    //{
    //    ts = GameObject.FindGameObjectWithTag("TurnTrigger").GetComponent<TurnSwitch>();

    //    //the chips we want to bet is set to the raised chips an used with the current user(owner - player ´s id)
    //    //we get chips from our wallet as the player id
    //    betValue = wallet.GetChips(this.photonView.ownerId, betValue);

    //    //add the chips to the PotManager.Instance TODO: should add to the player PotManager.Instance
    //    //PotManager.Instance.AddChips(chipsToBet);

    //    Debug.Log("chips to raise value is " + betValue + "the amount being sent on is " + ChipsToRaise);

    //    //tell the PotManager.Instance the value we want to raise

    //    //reset the chips we want to raise
    //    //chipsToRaise = 0;
    //    if (betValue == amountToCall)
    //    {
    //        CallCheck();
    //    }
    //    if (betValue > amountToCall)
    //    {
    //        PotManager.Instance.PotManager.InstanceComparison(this.photonView.ownerId, betValue);

    //        if (this.photonView.ownerId == 1)
    //        {
    //            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Raise);
    //        }
    //        if (this.photonView.ownerId == 2)
    //        {
    //            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p2Raise);
    //        }
    //        ts.GetComponent<TurnSwitch>().EndTurn();
    //    }
    //    //chipsToBet = wallet.GetComponent<WalletManager>().GetChips(this.photonView.ownerId, amountToCall);//we get chips equal to the amount needed to call

    //    //PotManager.Instance.AddChips(chipsToBet);//we add chips to the PotManager.Instance equal to the bet/call
    //}

    /// <summary>
    /// which player has to call how much?
    /// </summary>
    /// <param name="player"> player who needs to call / has bet the least</param>
    /// <param name="amountToCall"> the amount the player needs to call</param>

    //public void GetAmountToCall(int player, int amountToCall)
    //{
    //    //we set the scripts field values = the the values we get from the PotManager.Instance
    //    this.player = player;
    //    this.amountToCall = amountToCall;

    //    //we might be able to run call/check again here
    //    //CallCheck();
    //    //return player,amountToCall;
    //}

    ///// <summary>
    ///// call the current bet //TODO: make sure you cant call/raise after you run out of chips
    ///// </summary>
    //public void CallCheck()
    //{
    //    Debug.Log("player " + this.photonView.ownerId + " checks/calls");

    //    Debug.Log("amount to call = " + amountToCall);

    //    ts = GameObject.FindGameObjectWithTag("TurnTrigger").GetComponent<TurnSwitch>();

    //    if (amountToCall == 0)
    //    {
    //        //try to check with 0 added value to the PotManager.Instance

    //        if (this.photonView.ownerId == 1)
    //        {
    //            //we can tell the button to play the call sound for player a sound..
    //            //we should do it using an RPC though...
    //            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Check);
    //            //audMan.ButtonPressedAudio(ActionSound.p1Call);
    //        }
    //        else if (this.photonView.ownerId == 2)
    //        {
    //            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p2Check);
    //            //audMan.ButtonPressedAudio(ActionSound.p1Call);
    //        }
    //        PotManager.Instance.PotManager.InstanceComparison(this.photonView.ownerId, 0);
    //    }
    //    else
    //    {
    //        if (this.photonView.ownerId == 1)
    //        {
    //            //we can tell the button to play the call sound for player a sound..
    //            //we should do it using an RPC though...
    //            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Call);
    //            //audMan.ButtonPressedAudio(ActionSound.p1Call);
    //        }
    //        else if (this.photonView.ownerId == 2)
    //        {
    //            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p2Call);
    //            //audMan.ButtonPressedAudio(ActionSound.p1Call);
    //        }

    //        //TODO: get rid of redundancy in the wallet call
    //        betValue = wallet.GetChips(this.photonView.ownerId, amountToCall);//we get chips equal to the amount needed to call

    //        PotManager.Instance.AddChipsToPotManager.Instance(betValue);//we add chips to the PotManager.Instance equal to the bet/call

    //        //	ts.GetComponent<TurnSwitch> ().PotManager.InstanceComparison(amt_to_call);
    //        //AmountToCall = 0;
    //    }

    //    ts.EndTurn();//we send the turn on to our opponent
    //}

    /// <summary>
    /// fold
    /// </summary>

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }

    public void OnTurnStart()
    {
        callValue = PotManager.Instance.GetCallValue(player);
        SetBetToCallValue();
    }

    public void Bet()
    {
        // TODO: Call OnTurnStart from TurnSwitch instead!
        callValue = PotManager.Instance.GetCallValue(player);

        if (WalletManager.Instance.Withdraw(betValue))
        {
            if (betValue < callValue)
            {
                throw new NotImplementedException("You have to at least match the other players bet...");
            }
            else if (betValue > callValue)
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
                Call(betValue);
            }
            // End turn
            TurnManager.Instance.OnTurnEnd(this.photonView.ownerId, wantsNextRound);
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
        if (player == 1)
        {
            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Fold);
        }
        else if (player == 2)
        {
            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.p1Fold);
        }
        roundMan.GetComponent<PhotonView>().RPC("HandEnd", PhotonTargets.All, player, true);
    }
}