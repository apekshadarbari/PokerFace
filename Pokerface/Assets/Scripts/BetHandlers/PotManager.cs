using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotManager : PhotonManager<PotManager>
{
    private int roundIncrement;
    private bool winnerFound;

    //TODO: Get rid og redundancy
    [SerializeField, Header("Player One´s Pot Value")]
    private int player1pot;

    [SerializeField, Header("Player Two´s Pot Value")]
    private int player2pot;

    [SerializeField]
    private ChipsDisplay potchipDisplay;

    [SerializeField]
    private ChipsDisplay p1PotchipDisplay;

    [SerializeField]
    private ChipsDisplay p2PotchipDisplay;

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

    public int TotalPotValue
    {
        get { return potValue + player1pot + player2pot; }
    }

    public int Player1pot
    {
        get { return player1pot; }
    }

    public int Player2pot
    {
        get { return player2pot; }
    }

    // Use this for initialization
    private void Start()
    {
        //potValue = 0; //reset to 0
        //player1pot = 0;
        //infoBoard = GameObject.FindGameObjectWithTag("InfoBoard").GetComponent<Canvas>();

        /*TESTING*/
        //player2pot = 15;
        //player1pot = 15;
    }

    public void Update()
    {
        //betMan.GetAmountToCall(player, amountToCall);

        //infoBoard.GetComponent<PhotonView>().RPC("TextPot", PhotonTargets.AllBuffered,chipValue);
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
                p1PotchipDisplay.Value = player1pot;
                p1PotchipDisplay.UpdateStacks();
                break;

            case 2:
                player2pot += betValue;
                p2PotchipDisplay.Value = player2pot;
                p2PotchipDisplay.UpdateStacks();
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
        potchipDisplay.Value = potValue;
        potchipDisplay.UpdateStacks();
        p1PotchipDisplay.Value = player1pot;
        p1PotchipDisplay.UpdateStacks();
        p2PotchipDisplay.Value = player2pot;
        p2PotchipDisplay.UpdateStacks();
    }

    [PunRPC]
    private void EndHandBehaviour()
    {
        potValue = 0;
    }

    public int GetCallValue(int player)
    {
        p1PotchipDisplay.Value = player1pot;
        p1PotchipDisplay.UpdateStacks();
        p2PotchipDisplay.Value = player2pot;
        p2PotchipDisplay.UpdateStacks();

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

    public void DumpIfEqual()
    {
        //Debug.Log("pots equal");
        if (player1pot != 0 && player1pot == player2pot)
        {
            //roundIncrement = 1;
            gameObject.GetComponent<PhotonView>().RPC("EndRoundBehaviour", PhotonTargets.All);
            //roundMan.GetComponent<PhotonView>().RPC("RoundEnd", PhotonTargets.All, roundIncrement);
            //Debug.Log("Hygge");
        }
    }

    /// <summary>
    /// take the player who wins and give the pot to that player
    /// </summary>
    [PunRPC]
    public void PotToPlayer(int player)
    {
        //if (player == 1)
        //{
        //    //player one has won
        //    //run end round behaviour to put the pots together
        //    //gameObject.GetComponent<PhotonView>().RPC("EndRoundBehaviour", PhotonTargets.All);

        //    //WalletManager.Instance.Deposit(potValue, player);

        //    //add total pot to that players wallet
        //    //TotalPotValue();
        //    //TODO: need to add whatever a player has already bet to the pot when someone folds.. /dump if fold or remake dumpifequal
        //}
        //else if (player == 2)
        //{
        //    //player two has won
        //    //gameObject.GetComponent<PhotonView>().RPC("EndRoundBehaviour", PhotonTargets.All);
        //    //WalletManager.Instance.Deposit(potValue);
        //}
        //else
        //{
        //    //no one wins
        //}
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}