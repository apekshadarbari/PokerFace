using UnityEngine;
using System.Collections;

public class BetMore : Photon.MonoBehaviour, IClicker
{	
	[SerializeField]
	GameObject wallet;
	int chipsToIncrement;
	PotManager pot;
	int chipsToBet;
	int chipsToRaise;
	TurnSwitch ts;
	void Start()
	{
		chipsToIncrement = 5;
		chipsToRaise = 0;
		if (this.photonView.ownerId == 1)
		{
			this.photonView.transform.position = new Vector3(3.75f, -0.75f, 0.35f);
		}
		if (this.photonView.ownerId == 2)
		{
			this.photonView.transform.position = new Vector3(-2f, -0.75f, 3f);
		}
		pot = GameObject.Find("pot").GetComponent<PotManager>();
	}


	public void addChips(){

		chipsToRaise = chipsToRaise + chipsToIncrement;
	}

	public void removeChips(){

		if (chipsToRaise <= 0) {

			chipsToRaise = 0;

		}
		else chipsToRaise = chipsToRaise - chipsToIncrement;
	}


	public void OnClick(int choice)
	{	
		Debug.Log ("player " + this.photonView.ownerId + "raises");

		switch (choice) {

		case 1:
			//Adding chips to raise
			addChips ();
			break;

		case 2:
			//Reducing chips to raise
			removeChips ();
			break;

		case 3:
			//Raising 
			raiseChips(chipsToRaise);
			break;

		case 4:
			//Calling the last value
			break;

		}



	}



	public void raiseChips(int chipsToRaise){


		chipsToBet = wallet.GetComponent<WalletManager> ().GetChips (this.photonView.ownerId, chipsToRaise);
		pot.AddChips (this.photonView.ownerId, chipsToBet);
		ts.GetComponent<TurnSwitch> ().potComparison (chipsToRaise);
		Debug.Log ("player " + this.photonView.ownerId + " raised" + chipsToRaise + "chips");
		//The other player needs to get a message to call this value

		if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
		{
			this.photonView.TransferOwnership(1);
			//disable buttons for player2 here after this
			return;
		}
		else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
		{
			this.photonView.TransferOwnership(2);
			//disable buttons for player1 here after this
			return;
		}


	}



}