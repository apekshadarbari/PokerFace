<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkedPlayer : Photon.MonoBehaviour
{
    //private Vector3 correctPlayerPos = Vector3.zero; // We lerp towards this
    //private Quaternion correctPlayerRot = Quaternion.identity; // We lerp towards this

    // Use this for initialization

    public GameObject card;

    public GameObject avatar;
    public Transform playerGlobal;
    public Transform playerLocal;
    //public List<NetworkedPlayer> avatars = new List<NetworkedPlayer>();


    void Start()
    {

        Debug.Log("I'm instantiated!");

    }
    // 
    void Update()
    {
        if (photonView.isMine)
        {
            //seat tranform = desired transform for player
            Transform seatTrans = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID - 1];

            playerGlobal = GameObject.Find("Camera01 (origin)").transform;
            playerLocal = GameObject.Find("Camera01 (origin)/Camera01 (head)/Camera01 (eye)").transform;

            this.transform.position = (playerGlobal).transform.position;
            this.transform.rotation = (playerLocal).transform.rotation;

            //this.transform.localPosition = Vector3.zero;
            //stream.SendNext(GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats);
            
            //moves the camera to your seat
            GameObject.Find("Camera01 (origin)").transform.position = seatTrans.position;
            GameObject.Find("Camera01 (origin)/Camera01 (head)").transform.position = seatTrans.position;

            //we dont want to see ourselves
            avatar.SetActive(false);
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("pik lort");
                
                card = PhotonNetwork.Instantiate(card.name, Vector3.zero, Quaternion.identity, 0);
            }
            if (Input.GetKeyDown("space"))
            {
                Debug.Log("i pushed the button and i liked it");

                card.transform.Rotate(0, +180, 0);
            }

            //card.GetComponent<Rigidbody>().rotation.y

            //GameObject.Find("Camera01 (origin)").SetActive(true);


            //this.transform.localRotation = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID -1].rotation;
            //NetworkController.Seats[PhotonNetwork.player.ID - 1].transform.position;
        }
    }

    // Update is called once per frame
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {
            stream.SendNext(card.transform.rotation);

            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocal.localPosition);
            stream.SendNext(playerLocal.localRotation);

        }
        else
        {
            //GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats = (List<GameObject>)stream.ReceiveNext();
            card.transform.rotation = (Quaternion)stream.ReceiveNext();

            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();

            
        }
    }
}
=======
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkedPlayer : Photon.MonoBehaviour
{
    //private Vector3 correctPlayerPos = Vector3.zero; // We lerp towards this
    //private Quaternion correctPlayerRot = Quaternion.identity; // We lerp towards this

    // Use this for initialization


    public GameObject avatar;
    public Transform playerGlobal;
    public Transform playerLocal;
    //public List<NetworkedPlayer> avatars = new List<NetworkedPlayer>();


    void Start()
    {

        Debug.Log("I'm instantiated!");

    }
    // 
    void Update()
    {
        if (photonView.isMine)
        {
            Transform seatTrans = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID - 1];

            playerGlobal = GameObject.Find("Camera01 (origin)").transform;
            playerLocal = GameObject.Find("Camera01 (origin)/Camera01 (head)/Camera01 (eye)").transform;

            this.transform.position = (playerGlobal).transform.position;
            this.transform.rotation = (playerLocal).transform.rotation;

            //this.transform.localPosition = Vector3.zero;
            //stream.SendNext(GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats);
            GameObject.Find("Camera01 (origin)").transform.position = seatTrans.position;
            GameObject.Find("Camera01 (origin)/Camera01 (head)").transform.position = seatTrans.position;

            avatar.SetActive(false);
            //this.transform.localRotation = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID -1].rotation;
            //NetworkController.Seats[PhotonNetwork.player.ID - 1].transform.position;
        }

        //if (photonView.isMine && PhotonNetwork.player.ID == 2)
        //{

        //    playerGlobal = GameObject.Find("Camera02 (origin)").transform;
        //    playerLocal = GameObject.Find("Camera01 (origin)/Camera01 (head)/Camera01 (eye)").transform;

        //    this.transform.SetParent(playerLocal);
        //}
        //if (photonView.isMine && PhotonNetwork.player.ID == 2)
        //{

        //    playerGlobal = GameObject.Find("Camera01 (origin)").transform;
        //    playerLocal = GameObject.Find("Camera02 (origin)/Camera02 (head)/Camera02 (eye)").transform;

        //    this.transform.SetParent(playerLocal);
        //}

        //if (photonView.isMine)
        //{
        //Debug.Log("player is mine");



        //this.transform.SetParent(playerLocal);

        //for (int i = 0; i < SeatManager.seats.Count; i++)
        //{

        //GameObject.Find("[CameraRig]").transform.position = this.transform.localPosition;
        //    GameObject.Find("[CameraRig]/Camera (head)/Camera (eye)").transform.position = this.transform.position;


        //}
        //if (!photonView.isMine)
        //{
        //    transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
        //    transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
        //}




    }

    // Update is called once per frame
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {
          
            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocal.localPosition);
            stream.SendNext(playerLocal.localRotation);

        }
        else
        {
            //GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats = (List<GameObject>)stream.ReceiveNext();
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();

            
        }
    }
}
>>>>>>> da1a6e2190ac886a62af1e2b273e58d39b9f820d
