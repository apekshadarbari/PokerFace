using UnityEngine;
using System.Collections;
using System;

public class Bet : Photon.MonoBehaviour, IClicker
{
	[SerializeField]
	GameObject wallet;
	int chipsToIncrement;
	PotManager pot;
	int chipsToBet;
	public static int chipsToRaise;
	TurnSwitch ts;

	void Start()
	{
		chipsToBet = 0;
		pot = GameObject.Find("pot").GetComponent<PotManager>();
	}




	public void OnClick()
	{
		Debug.Log("player " + this.photonView.ownerId + "raises");



	}

	public void OnHover()
	{
		throw new NotImplementedException();
	}

	public void OnExitHover()
	{
		throw new NotImplementedException();
	}

	/*
	public static void raiseChips(int chipsToRaise){


//		chipsToBet = wallet.GetComponent<WalletManager> ().GetChips (this.photonView.ownerId, chipsToRaise);
//		pot.AddChips (this.photonView.ownerId, chipsToBet);
//		ts.GetComponent<TurnSwitch> ().potComparison (chipsToRaise);
//		Debug.Log ("player " + this.photonView.ownerId + " raised" + chipsToRaise + "chips");
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
*/


}
