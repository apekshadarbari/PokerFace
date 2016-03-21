using System;
using UnityEngine;

public class BetManager : PhotonManager<BetManager>
{
    //[SerializeField]
    //private Wallet walletText;
    [SerializeField]
    private ChipsDisplay p1ChipDisplay;

    [SerializeField]
    private ChipsDisplay p2ChipDisplay;

    [SerializeField]
    private int chipsToIncrement = 5;

    [SerializeField]
    private AudioManager audMan;

    [SerializeField]
    private int betValue;

    private int player;

    private RoundManager roundMan;

    private bool wantsNextRound;

    private int callValue;

    private int playerTurn;

    private void Start()
    {
        //player = PhotonNetwork.player.ID;
        //Debug.Log(player);

        //reset of the values (starting values)
        betValue = 0;
        //amountToCall = 0;
        //find the PotManager.Instance

        audMan = GameObject.Find("AudioSource").GetComponent<AudioManager>();
        roundMan = GameObject.Find("Round").GetComponent<RoundManager>();

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

    public void GetPlayerTurn(int playerTurn)
    {
        this.playerTurn = playerTurn;
    }

    private void Update()
    {
        if (player == 0)
        {
            player = 1;
        }

        if (playerTurn == 1) // TODO: make this more effecient
        {
            p1ChipDisplay.Value = betValue;
            p2ChipDisplay.Value = 0;
        }
        else if (playerTurn == 2)
        {
            p1ChipDisplay.Value = 0;
            p2ChipDisplay.Value = betValue;
        }
        p1ChipDisplay.UpdateStacks();
        p2ChipDisplay.UpdateStacks();
    }
    public void SetBetToCallValue()
    {
        // TODO: Call OnTurnStart from TurnSwitch instead!
        callValue = PotManager.Instance.GetCallValue(player);
        betValue = callValue;
    }

    /// <summary>
    /// add chips (plus button)
    /// </summary>
    public void IncreaseBet(int increment)
    {
        this.chipsToIncrement = increment;
        betValue = betValue + chipsToIncrement;

        WalletManager.Instance.TemporaryWithdraw(chipsToIncrement);

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

        if (player == 1)
        {
            p1ChipDisplay.Value = betValue;
            p1ChipDisplay.UpdateStacks();
        }
        else if (player == 2)
        {
            p2ChipDisplay.Value = betValue;
            p2ChipDisplay.UpdateStacks();
        }
    }

    /// <summary>
    /// remove chips - (minus button)
    /// </summary>
    public void DecreaseBet()
    {
        // Debug.Log("remove chips detected ");
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
        WalletManager.Instance.TmpCredits = WalletManager.Instance.Credits;
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
    public void OnTurnStart(int player)
    {
        this.player = player;
        //Debug.LogFormat("- OnTurnStart {0} - Betmanager ", player);

        callValue = PotManager.Instance.GetCallValue(player); // get callvalue

        ResetBet(); // reset the betvalue to 0
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
            BetvalueUpdate();
            TurnManager.Instance.OnTurnEnd(player, wantsNextRound);
        }
        else
        {
            return;
            //throw new InvalidOperationException("Not enough credits in wallet for bet!");
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
        //RoundManager.Instance.HandEnd(player, true);
        roundMan.GetComponent<PhotonView>().RPC("HandEnd", PhotonTargets.All, player, true);
    }
    //void Update()
    //{
    //    betValue;
    //}
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}