using UnityEngine;
using System.Collections;
using System;

public class StartButton : Photon.MonoBehaviour, IClicker
{
    [SerializeField]
    CardManager cardMan;
    [SerializeField]
    NetworkedPlayer playerCtrl;

    public void OnClick()
    {
        if (PhotonNetwork.isMasterClient && photonView.isMine)
        {
            Debug.Log("Clicked");
            gameObject.SetActive(false);
            playerCtrl.StartGame();
            cardMan.Deal();
        }
    }

    public void OnExitHover()
    {
        if (PhotonNetwork.isMasterClient && photonView.isMine)
        {
            Debug.Log("stopped hovering");

            GetComponent<Renderer>().material.color = Color.blue;

            return;
        }

    }

    public void OnHover()
    {
        if (PhotonNetwork.isMasterClient && photonView.isMine)
        {
            Debug.Log("hovering");
            GetComponent<Renderer>().material.color = Color.clear;
            return;
        }
    }
}
