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

    public bool RiverIsDealt
    {
        get
        {
            return riverIsDealt;
        }

        set
        {
            riverIsDealt = value;
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
        riverIsDealt = false;
        Debug.Log("I´m started");
        turn = 0;
		Debug.Log ("Turn " + turn.ToString ());
        player1pot = 0;
        player2pot = 0;

        //StartTurn();


    }
    void Update()
    {
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

        if (turn == 1)
        {
            if (!riverIsDealt)
            {
                riverIsDealt = true;
                deckInteraction.Shuffle();
                deckInteraction.DealRiver();
                deckInteraction.CompareCards();

            }
        }
        //post river
        if (turn == 3)
        {
            Debug.Log("compare");
            deckInteraction.CompareCards();
        }

    }


    public void StartTurn()
    {

        //if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
        //{
        //    this.photonView.TransferOwnership(1);
        //    return;
        //}

        //turn++;


    }

    public void potComparison(int thisTurnbet)
	{	Debug.Log("entered pot comparison");

        if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
        {
            player2pot = player2pot + thisTurnbet;
        }
        else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
        {

            player1pot = player1pot + thisTurnbet;
        }

		if (player1pot == player2pot) {
			//turn++;
			player1pot = 0;
			player2pot = 0;

		} else if (player1pot > player2pot) {

			BetMore.amt_to_call = player1pot - player2pot;

		}
		else {
			BetMore.amt_to_call = player2pot - player1pot;
		}
        return;
    }



    public void OnClick()
    {
        turn++;
        Debug.Log("Turn: " + turn.ToString());
        if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
		{
			//GameObject.FindGameObjectWithTag ("Player2betController").GetComponentInChildren<MeshRenderer>().enabled = false;
			//GameObject.FindGameObjectWithTag ("Player2betController").GetComponentInChildren<SphereCollider> ().enabled = false;
			GameObject.FindGameObjectWithTag ("Player2betController").SetActive(false);
            this.photonView.TransferOwnership(1);
        }
        else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
        {
			//GameObject.FindGameObjectWithTag ("Player2betController").GetComponentInChildren<MeshRenderer>().enabled = false;
			//GameObject.FindGameObjectWithTag ("Player2betController").GetComponentInChildren<SphereCollider> ().enabled = false;
			GameObject.FindGameObjectWithTag ("Player1betController").SetActive(false);

            this.photonView.TransferOwnership(2);
        }

        Debug.Log("owner " + this.photonView.ownerId.ToString());

    }

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



