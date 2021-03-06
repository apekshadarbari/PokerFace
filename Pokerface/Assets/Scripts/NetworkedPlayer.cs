﻿using UnityEngine;

public class NetworkedPlayer : Photon.MonoBehaviour
{
    // Use this for initialization
    private bool gameIsStarted;

    //players avatar
    public GameObject avatar;

    //players camera rig / perspective
    public Transform playerRig;

    private Vector3 vrSpace;

    public Transform playerLocal;
    public Transform playerGlobal;

    //the turnswitch trigger
    //[SerializeField]
    //public GameObject turnTrigger;

    //players betcontrol for buttons
    [SerializeField]
    private GameObject betControl;

    //control of the cards
    [SerializeField]
    private GameObject cardControl;

    private void Start()
    {
        if (photonView.isMine) //TODO: check how much can be moved to start - making seats the Parents might make it easier to deal with but will require some restructuring
        {
            Transform seatTrans = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID - 1];

            playerRig = GameObject.Find("[CameraRig]").transform;
            vrSpace = GameObject.Find("[SteamVR]").transform.position;
            vrSpace = seatTrans.position;

            playerGlobal = GameObject.Find("[CameraRig]/Camera (head)").transform;
            playerLocal = GameObject.Find("[CameraRig]/Camera (head)/Camera (eye)").transform;

            playerRig.position = seatTrans.position;

            VRInputManager.Instance.DeviceSpecificSeating();

            avatar.SetActive(false);
        }

        //player 1
        if (photonView.isMine && PhotonNetwork.player.ID == 1)
        {
            //Debug.Log("betcontroller " + this.photonView.ownerId.ToString());

            //create a betcontroller for each player
            betControl = PhotonNetwork.Instantiate(betControl.name, betControl.transform.position, Quaternion.identity, 0);

            /*TESTING*/
            //test position - when no oculus
            //betControl = PhotonNetwork.Instantiate(betControl.name, new Vector3(3f, 0, 1), Quaternion.Euler(0, 208, 0), 0);

            betControl.tag = "Player1BetController";

            foreach (Transform t in betControl.transform)
            {
                t.gameObject.tag = "PlayerOneButton";
                t.GetComponent<MeshRenderer>().enabled = false;
                t.GetComponent<MeshCollider>().enabled = false;
                //t.GetComponent<SphereCollider>().enabled = false;
            }
        }

        //player 2
        if (photonView.isMine && PhotonNetwork.player.ID == 2)
        {
            Debug.Log("betcontroller " + this.photonView.ownerId.ToString());
            //create a betcontroller for each player
            betControl = PhotonNetwork.Instantiate(betControl.name, new Vector3(0.621f, 0.814f, 0.78f), Quaternion.Euler(0, 180, 0), 0);
            betControl.tag = "Player2BetController";

            foreach (Transform t in betControl.transform)
            {
                t.gameObject.tag = "PlayerTwoButton";
                t.GetComponent<MeshRenderer>().enabled = false;
                t.GetComponent<MeshCollider>().enabled = false;
                //t.GetComponent<SphereCollider>().enabled = false;
            }
        }

        // Ensure the player is facing the table
        FaceTable();

        //}
    }

    private void FaceTable()
    {
        //var transform = GameObject.Find("[CameraRig]/Camera (head)").transform;
        var transform = playerRig;
        var target = Vector3.up * transform.position.y;
        transform.transform.forward = (target - transform.position).normalized;
    }

    private void Update()
    {
        if (photonView.isMine) //TODO: check how much can be moved to start - making seats the Parents might make it easier to deal with but will require some restructuring
        {
            //TODO; might need another var for a player head for the pos tracking
            this.transform.position = (playerGlobal).transform.position;
            this.transform.rotation = (playerLocal).transform.rotation;
        }
    }

    // Update is called once per frame
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //what are we sending to the network?
        if (stream.isWriting)
        {
            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocal.localPosition);
            stream.SendNext(playerLocal.localRotation);
        }
        //what are we receiving from the network?
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}