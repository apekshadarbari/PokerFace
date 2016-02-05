using UnityEngine;
using System.Collections;
using System;

public class CheckCall : Photon.MonoBehaviour, IClicker
{


    [SerializeField]
    GameObject wallet;
    TurnSwitch ts;
    PotManager pot;
    int chipsToBet;
    void Start()
    {
        //if (this.photonView.ownerId == 1)
        //{
        //	this.photonView.transform.position = new Vector3(-0.932f, 0.315f, 1.722f);
        //}
        //if (this.photonView.ownerId == 2)
        //{
        //	this.photonView.transform.position = new Vector3(-2f, -0.75f, 3f);
        //}
        pot = GameObject.Find("pot").GetComponent<PotManager>();
    }

    public void OnClick(int amt_to_call)
    {
        Debug.Log("player " + this.photonView.ownerId + " checks/calls");

        		chipsToBet = wallet.GetComponent<WalletManager> ().GetChips (this.photonView.ownerId, amt_to_call);
        pot.AddChips(this.photonView.ownerId, chipsToBet);
        ts.GetComponent<TurnSwitch>().potComparison(amt_to_call);




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






    }

    public void OnHover()
    {
        throw new NotImplementedException();
    }

    public void OnExitHover()
    {
		
        throw new NotImplementedException();
    }

    public void OnClick()
    {
        throw new NotImplementedException();
    }
}
