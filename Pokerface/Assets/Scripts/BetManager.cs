using UnityEngine;
using System.Collections;
using System;

public class BetManager : Photon.MonoBehaviour, IClicker
{
    [SerializeField]
    int chipValue;
    public void Bet()
    {
        Debug.Log(this.photonView.ownerId + " bet something");
    }
    public void CallOrCheck()
    {
        Debug.Log(this.photonView.ownerId + " check or called");
    }
    public void Fold()
    {
        Debug.Log(this.photonView.ownerId + " folds");
    }

    public void OnClick()
    {
        //throw new NotImplementedException();
    }

    public void OnExitHover()
    {

		GetComponent<Renderer>().material.color = Color.red;
		CrosshairTimerDisplay.Instance.Show();
       // throw new NotImplementedException();
    }

    public void OnHover()
    {

		GetComponent<Renderer>().material.color = Color.blue;
		CrosshairTimerDisplay.Instance.Show();
        //throw new NotImplementedException();
    }
}
