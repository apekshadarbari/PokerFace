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
    bool gameIsStarted;

    public void Start()
    {
        //if (PhotonNetwork.isMasterClient)
        //{
        //    gameIsStarted = false;
        //}
        //else
        //{
        //    gameIsStarted = true;
        //}

    }

    public void Update()
    {
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
            Debug.Log("Clicked");

            //start game through the networked player
            playerCtrl.StartGame();
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
