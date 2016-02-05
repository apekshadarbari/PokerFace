/*

using UnityEngine;
using System.Collections;
using System;

public class Fold : Photon.MonoBehaviour, IClicker
{
    PotManager pot;
    WalletManager wallet;
    void Start()
    {
        //if (this.photonView.ownerId == 1)
        //{
        //    this.photonView.transform.position = new Vector3(-2.517f, 0.315f, 0.444f);
        //}
        //if (this.photonView.ownerId == 2)
        //{
        //    this.photonView.transform.position = new Vector3(-2f, -0.75f, 3f);
        //}
    }


    public void OnClick()
    {


        if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
        {
            Debug.Log("player " + this.photonView.ownerId + " folds");
            wallet.GetComponent<WalletManager> ().player1ChipValue = pot.GetComponent<PotManager>().chipValue;
            pot.GetComponent<PotManager>().chipValue = 0;
            // needs more logic here to end game
            return;
        }
        else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
        {
            Debug.Log("player " + this.photonView.ownerId + " folds");
             wallet.GetComponent<WalletManager> ().player2ChipValue = pot.GetComponent<PotManager>().chipValue;
             pot.GetComponent<PotManager>().chipValue = 0;
            // needs more logic here to end game
            return;
        }
    }

    public void OnHover()
    {

		GetComponent<Renderer> ().material.color = Color.red;
		CrosshairTimerDisplay.Instance.Show();
		//throw new NotImplementedException();
    }

    public void OnExitHover()
    {


		GetComponent<Renderer> ().material.color = Color.blue;
		CrosshairTimerDisplay.Instance.Show();
       // throw new NotImplementedException();
    }
}
*/