using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// start button class
/// </summary>
public class StartButton : Photon.MonoBehaviour, IClicker
{
    [SerializeField]
    CardManager cardMan;
    [SerializeField]
    NetworkedPlayer playerCtrl;

    public void OnClick()
    {
        //if we are player one I.E. master
        if (PhotonNetwork.isMasterClient && photonView.isMine)
        {
            Debug.Log("Clicked");
            //set the start button to inactive
            gameObject.SetActive(false);
            //start game through the networked player
            playerCtrl.StartGame();
            //deal the cards
            //cardMan.Deal();
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
    ///hovering the start button
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
