using UnityEngine;
using System.Collections;
using System;

public class BetMore : Photon.MonoBehaviour, IClicker
{
    [SerializeField]
    GameObject wallet;
	public int choice;
	public static int amt_to_call;
    int chipsToIncrement;
    PotManager pot;
    int chipsToBet;
    static int chipsToRaise;
	[SerializeField]
	TurnSwitch ts;

	/*public int ChipsToRaise
	{
		get
		{
			return chipsToRaise;
		}

		set
		{
			chipsToRaise = value;
		}
	}*/

    void Start()
    {
        chipsToIncrement = 5;
        chipsToRaise = 0;
		amt_to_call = 0;
        //if (this.photonView.ownerId == 1)
        //{
        //	this.photonView.transform.position = new Vector3(-1.588f, 0.34f, 1.51f);
        //}
        //if (this.photonView.ownerId == 2)
        //{
        //	this.photonView.transform.position = new Vector3(-2f, -0.75f, 3f);
        //}
        pot = GameObject.Find("pot").GetComponent<PotManager>();
    }


    public void addChips()
    {

        chipsToRaise = chipsToRaise + chipsToIncrement;
		Debug.Log("add chips detected : " + chipsToRaise);
    }

    public void removeChips()
    {
		Debug.Log("remove chips detected ");
        if (chipsToRaise <= 0)
        {

            chipsToRaise = 0;

        }
        else chipsToRaise = chipsToRaise - chipsToIncrement;
    }


    public void OnClick()
    {
       

        switch (choice)
        {

            case 1:
                //Adding chips to raise
                addChips();
                break;

            case 2:
                //Reducing chips to raise
                removeChips();
                break;

            case 3:
                //Raising 
                raiseChips();
                break;

            case 4:
                //Calling the last value
				callCheck();
                break;

			case 5:
				
				//Folding cards
				fold ();	
			break;

        }



    }

	public void callCheck(){

		Debug.Log("player " + this.photonView.ownerId + " checks/calls");

		chipsToBet = wallet.GetComponent<WalletManager> ().GetChips (this.photonView.ownerId, amt_to_call);
		pot.AddChips(this.photonView.ownerId, chipsToBet);
	//	ts.GetComponent<TurnSwitch> ().potComparison(amt_to_call);
		amt_to_call = 0;


		//if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
		//{
			ts.OnClick();
		//	return;
		//}
		//else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
		//{
		//	TurnSwitch.OnClick();
		//	return;
		//}



	}

	public void fold(){


		if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
		{
			Debug.Log("player " + this.photonView.ownerId + " folds");
			WalletManager.player1ChipValue = pot.GetComponent<PotManager>().chipValue;
			pot.GetComponent<PotManager>().chipValue = 0;
			// needs more logic here to end game
			return;
		}
		else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
		{
			Debug.Log("player " + this.photonView.ownerId + " folds");
			//wallet.GetComponent<WalletManager> ().player2ChipValue = pot.GetComponent<PotManager>().chipValue;
			WalletManager.player2ChipValue = pot.chipValue;
			pot.GetComponent<PotManager>().chipValue = 0;
			// needs more logic here to end game
			return;
		}

	}



    public void raiseChips(){


		chipsToBet = wallet.GetComponent<WalletManager> ().GetChips (this.photonView.ownerId, chipsToRaise);
		pot.AddChips (this.photonView.ownerId, chipsToBet);
		Debug.Log ("chips to raise value is " + chipsToRaise);
		ts.GetComponent<TurnSwitch> ().potComparison(chipsToRaise);
		chipsToRaise = 0;
		if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
		{
			Debug.Log ("player 2 transferring control to 1" );

			//GameObject.FindGameObjectWithTag ("Player2betController").GetComponentInChildren<MeshRenderer>().enabled = false;
			//GameObject.FindGameObjectWithTag ("Player2betController").GetComponentInChildren<SphereCollider> ().enabled = false;

			//TurnSwitch.OnClick();
			ts.OnClick();
			//disable buttons for player2 here after this
			return;
		}
		else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
		{
			Debug.Log ("player 1 transferring control to 2" );
			//GameObject.FindGameObjectWithTag ("Player1betController").GetComponentInChildren<MeshRenderer>().enabled = false;
			//GameObject.FindGameObjectWithTag ("Player1betController").GetComponentInChildren<SphereCollider> ().enabled = false;
			//TurnSwitch.OnClick();
			ts.OnClick();
			//disable buttons for player1 here after this
			return;
		}


	}



	public void OnHover()
	{	GetComponent<Renderer>().material.color = Color.red;
		CrosshairTimerDisplay.Instance.Show();
		//throw new NotImplementedException();
	}

	public void OnExitHover()
	{	GetComponent<Renderer>().material.color = Color.white;
		CrosshairTimerDisplay.Instance.Show();
		//throw new NotImplementedException();
	}


}