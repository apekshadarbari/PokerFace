﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

//[RequireComponent(typeof(PhotonView))]
public class TurnSwitch : Photon.MonoBehaviour, IClicker
{
    CardManager deckInteraction;

    bool playerOneTurn;
    bool playerTwoTurn;
	int player1pot;
	int player2pot;
	int amt_to_call;
    bool riverIsDealt;

    //test
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
        Debug.Log("pre awake owner " + photonView.ownerId.ToString());
        //if (this.photonView.ownerId == 0)
        //{
        //    this.photonView.TransferOwnership(1);
        //    playerOneTurn = true;
        //}

        // deckInteraction = GameObject.Find("CardController").GetComponent<CardManager>();'

        //deckInteraction.Shuffle(); 

        Debug.Log("post awake owner " + photonView.ownerId.ToString());
    }

    void Start()
    {
		deckInteraction = GetComponent<CardManager>();
		if (deckInteraction == null)
		{
			Debug.Log("No Deck");
		}

		turn = 0;
		player1pot = 0;
		player2pot = 0;
	
		StartTurn ();

      
    }
    void Update()
    {
        switch (this.photonView.ownerId)
        {
            case 1:
                TurnTrigger.transform.position = new Vector3(4.01f, 0f, -0.54f);
                break;
            case 2:
                TurnTrigger.transform.position = new Vector3(-2.2f, 0f, 4.57f);
                break;
            default:
                break;
        }
        //river 

        if (turn == 2)
        {
            //CardManager.

            //Debug.Log("river");
          
            if (!riverIsDealt)
            {
                deckInteraction.DealRiver();
                riverIsDealt = true;
            }
        }
        //post river
        if (turn == 3)
        {
            //CardManager.

            Debug.Log("river");
            deckInteraction.compareCards();
        }

    }


	public void StartTurn(){

		if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
		{
			this.photonView.TransferOwnership(1);
			return;
		}

		turn++;


	}

	public void potComparison(int thisTurnbet){

		if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2) {
			player2pot = player2pot + thisTurnbet;

			WordManager.betMoney02 = player2pot; 

		} else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1) {

			player1pot = player1pot + thisTurnbet;
			WordManager.betMoney01 = player1pot; 
		}

		if (player1pot == player2pot) {
			turn++;
			player1pot = 0;
			player2pot = 0;

		} else if (player1pot > player2pot) {
			CheckCall.amt_to_call = player1pot - player2pot;
		} else {
			CheckCall.amt_to_call = player2pot - player1pot;
		}


		return;
	}



	public void OnClick(){

	}

   /* public void OnClick()
    {
        turn++;
        Debug.Log("Turn: " + turn.ToString());
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
        if (turn == 4)
        {
            riverIsDealt = true;
        }
    
        Debug.Log("owner " + this.photonView.ownerId.ToString());

    }*/

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)

    {
        if (stream.isWriting)
        {
            //send the next turn to next player
            //make it uninteractable to others

            stream.SendNext(turn);

            stream.SendNext(photonView.ownerId);
            stream.SendNext(TurnTrigger.transform.position);
        }
        else
        {
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



