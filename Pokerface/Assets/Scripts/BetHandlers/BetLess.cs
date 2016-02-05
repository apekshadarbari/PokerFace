using UnityEngine;
using System.Collections;
using System;

public class BetLess : Photon.MonoBehaviour, IClicker
{
    public void OnClick()
    {
        throw new NotImplementedException();
    }

    public void OnExitHover()
    {

		GetComponent<Renderer>().material.color = Color.red;
		CrosshairTimerDisplay.Instance.Show();
       // throw new NotImplementedException();
    }

    public void OnHover()
    {

		GetComponent<Renderer> ().material.color = Color.blue;
		CrosshairTimerDisplay.Instance.Show();
       // throw new NotImplementedException();
    }

    void Start()
    {
        //if (this.photonView.ownerId == 1)
        //{
        //    this.photonView.transform.position = new Vector3(-2.257f, 0.34f, 1.054f);
        //}
        //if (this.photonView.ownerId == 2)
        //{
        //    this.photonView.transform.position = new Vector3(-2f, -0.75f, 3f);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
