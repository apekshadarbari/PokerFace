using UnityEngine;
using System.Collections;

public class Fold : Photon.MonoBehaviour
{
	PotManager pot;
	WalletManager wallet;

	public void OnClick()
	{


		if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
		{
			Debug.Log("player " + this.photonView.ownerId + " folds");
		//	wallet.GetComponent<WalletManager> ().player1ChipValue = pot.GetComponent<PotManager>().chipValue;
		//	pot.GetComponent<PotManager>().chipValue = 0;
			// needs more logic here to end game
			return;
		}
		else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
		{
			Debug.Log("player " + this.photonView.ownerId + " folds");
	//		wallet.GetComponent<WalletManager> ().player2ChipValue = pot.GetComponent<PotManager>().chipValue;
	//		pot.GetComponent<PotManager>().chipValue = 0;
			// needs more logic here to end game
			return;
		}
	}
}
