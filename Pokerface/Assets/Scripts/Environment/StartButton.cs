﻿using UnityEngine;
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
    bool gameIsStarted;
    [SerializeField]
    AudioManager audMan;




    public void Start()
    {
        audMan = GameObject.Find("AudioSource").GetComponent<AudioManager>();

        //TODO: changed so that masterclient is whoever makes the room
        if (PhotonNetwork.isMasterClient)
        {
            gameIsStarted = false;
        }     
        else if (!PhotonNetwork.isMasterClient)
        {
            //TODO: spawn a different message for player 2?
            gameObject.SetActive(false);
        }

    }

    public void Update()
    {
        //TODO: might be more efficient in onclick...
        if (gameIsStarted)
        {
            //set the start button to inactive
            gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        //if we are player one I.E. master
        if (PhotonNetwork.isMasterClient && photonView.isMine)
        {
            //Debug.Log("Clicked");
            audMan.GetComponent<PhotonView>().RPC("ButtonPressedAudio", PhotonTargets.All, ActionSound.roundStarted);

            //start game through the networked player //TODO: make start button better
            //playerCtrl.StartGame();
            //deal the cards

            //cardMan.Deal();
            gameIsStarted = true;
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

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(gameIsStarted);
        }
        else
        {
            gameIsStarted  = (bool)stream.ReceiveNext();
        }
    }
}


//what are we sending to the network?
//if (stream.isWriting)
//{
//    stream.SendNext(playerGlobal.position);
//    stream.SendNext(playerGlobal.rotation);
//    stream.SendNext(playerLocal.localPosition);
//    stream.SendNext(playerLocal.localRotation);

//}
////what are we receiving from the network?
//else
//{
//    this.transform.position = (Vector3)stream.ReceiveNext();
//    this.transform.rotation = (Quaternion)stream.ReceiveNext();
//    avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
//    avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
//}
