using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotManager : PhotonManager<PotManager>
{
    private int roundIncrement;
    private bool winnerFound;

    //TODO: Get rid og redundancy
    [SerializeField, Header("Player one has put in")]
    private int player1pot;

    [SerializeField, Header("Player two has put in")]
    private int player2pot;

    //[SerializeField, Header("The amount needed to call")]
    //private int amountToCall;

    private int player;

    //[SerializeField]
    //private WalletManager walletMan;

    [SerializeField, Header("")]
    private GameObject roundMan;

    private Canvas infoBoard;

    //private List<GameObject> players;

    [SerializeField]
    private int potValue;

    public int PotValue
    {
        get { return potValue; }
        private set { potValue = value; }
    }

    public int TotalPotValue
    {
        get { return potValue + player1pot + player2pot; }
    }

    // Use this for initialization
    private void Start()
    {
        PotValue = 0; //reset to 0
        //player1pot = 0;
        infoBoard = GameObject.FindGameObjectWithTag("InfoBoard").GetComponent<Canvas>();

        /*TESTING*/
        //player2pot = 15;
        //player1pot = 15;
    }

    //adds chips to the pot I.E. the pots chipvalue
    //do we need the player param here? TODO: find out if we ever use the player param here
    public void AddChipsToPot(int chips)
    {
        PotValue += chips;
    }

    public void Update()
    {
        //betMan.GetAmountToCall(player, amountToCall);

        //infoBoard.GetComponent<PhotonView>().RPC("TextPot", PhotonTargets.AllBuffered,chipValue);
    }

    [PunRPC]
    private void WinPotToPlayer(int player)
    {
        //if (winnerFound)
        //{
        if (player == 2)
        {
            var wallet = GameObject.FindGameObjectWithTag("Player2BetController").GetComponent<WalletManager>();
            wallet.Deposit(PotValue + player1pot + player2pot);
        }
        else if (player == 1)
        {
            var wallet = GameObject.FindGameObjectWithTag("Player1BetController").GetComponent<WalletManager>();
            wallet.Deposit(PotValue + player1pot + player2pot);
        }
        PotValue = 0;
        //}
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }

    public void Bet(int player, int betValue)
    {
        gameObject.GetComponent<PhotonView>().RPC("BetBehaviour", PhotonTargets.All, player, betValue);
    }

    [PunRPC]
    private void BetBehaviour(int player, int betValue)
    {
        Debug.LogFormat("Bet of ${0} received from player {1}", betValue, player);

        // Add bets to temporary player pot
        switch (player)
        {
            case 1:
                player1pot += betValue;
                break;

            case 2:
                player2pot += betValue;
                break;
        }

        // Update call value
        //CallValue = Mathf.Abs(player1pot - player2pot);
    }

    //private void CurrentRound(Round currentRound)
    //{
    //    this.currentRound = currentRound;
    //}
    /// <summary>
    /// terminologi for rounds turns and hand...  this is after all betting is over in a "...."
    /// </summary>
    [PunRPC]
    private void EndRoundBehaviour()
    {
        potValue += player1pot + player2pot;
        player1pot = 0;
        player2pot = 0;
    }

    public int GetCallValue(int player)
    {
        switch (player)
        {
            case 1:
                return Mathf.Max(player2pot - player1pot, 0);

            case 2:
                return Mathf.Max(player1pot - player2pot, 0);

            default:
                throw new NotImplementedException("Only two players!");
        }
    }

    public void DumpIfEqual(bool bothCheck)
    {
        //Debug.Log("pots equal");
        if (player1pot != 0 && player1pot == player2pot || bothCheck)
        {
            roundIncrement++;
            gameObject.GetComponent<PhotonView>().RPC("EndRoundBehaviour", PhotonTargets.All);
            roundMan.GetComponent<PhotonView>().RPC("RoundEnd", PhotonTargets.All, roundIncrement);
            Debug.Log("Hygge");
        }
    }
}