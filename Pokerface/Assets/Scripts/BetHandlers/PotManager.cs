using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PotManager : Photon.MonoBehaviour
{

    //TODO: Get rid og redundancy
    [SerializeField, Header("Player one has put in")]
    int player1pot;
    [SerializeField, Header("Player two has put in")]
    int player2pot;

    [SerializeField, Header("The amount needed to call")]
    int amountToCall;
    int player;

    [SerializeField]
    BetManager betMan;

    List<GameObject> players;

    [SerializeField]
    public int chipValue;

    public int ChipValue
    {
        get
        {
            return chipValue;
        }
    }

    // Use this for initialization
    void Start()
    {

        chipValue = 0;
        //player1pot = 0;

        //for testing purposes 
        //player2pot = 15;

    }

    //adds chips to the pot I.E. the pots chipvalue
    //do we need the player param here? TODO: find out if we ever use the player param here
    public void AddChipsToPot(int chips)
    {
        chipValue += chips;
    }

    public void Update()
    {
        //betMan.GetAmountToCall(player, amountToCall);
        

    }

    //public int GetAmountToCall()
    //{
    //    return;
    //}

    /// <summary>
    /// Compares the pot ... 
    /// </summary>
    /// <param name="thisTurnbet">bet this turn</param>
    /// 
    [PunRPC]
    void PotComparison(int player, int thisTurnbet)
    {
        Debug.Log("entered pot comparison");
        Debug.Log("player: " + player + " bet: " + thisTurnbet);

        //we add the bet to the players pot
        //if the turnswitch belongs to player 2
        if (player == 2)
        {
            //add the players bet to their pot
            Debug.Log("player: " + player + " bet: " + thisTurnbet);
            betMan = GameObject.FindGameObjectWithTag("Player2BetController").GetComponent<BetManager>();
        }
        //if the turnswitch belongs to player 1
        else if (player == 1)
        {
            player1pot = player1pot + thisTurnbet;
            Debug.Log("player: " + player + " bet: " + thisTurnbet);
            betMan = GameObject.FindGameObjectWithTag("Player1BetController").GetComponent<BetManager>();
        }


        //if one of the pots have more value than the other then we need to change the amount to call
        if (player1pot != player2pot)
        {
            //if player 1 has bet more 
            if (player1pot > player2pot)
            {
                //set amount to call
                //send it back with a player id to the betmore checkcall function
                amountToCall = player1pot - player2pot;
                //betMan.GetAmountToCall(player, amountToCall);
                Debug.Log("player 2 has to call " + amountToCall);
            }
            else if (player2pot > player1pot)//vice versa
            {
                amountToCall = player2pot - player1pot;
                Debug.Log("player " + player + " has to call " + amountToCall);
            }
            else
            {
                Debug.Log("ERROR in the amount to call");
            }
        }
        //if both pots are the same set them to 0
        if (player1pot == player2pot)
        {
            //if the pots are the same we can add them to the pot and they no longer belong to a player
            AddChipsToPot(player1pot + player2pot);
            //reset the players pots
            player1pot = 0;
            player2pot = 0;

            //check occured
        }
        betMan.GetComponent<PhotonView>().RPC("GetAmountToCall", PhotonTargets.All, player, amountToCall);

        //return;
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(chipValue);
            stream.SendNext(player1pot);
            stream.SendNext(player2pot);
        }
        else
        {
            chipValue = (int)stream.ReceiveNext();
            player1pot = (int)stream.ReceiveNext();
            player2pot = (int)stream.ReceiveNext();
        }
    }
}
