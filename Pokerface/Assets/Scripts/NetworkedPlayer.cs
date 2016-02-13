using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkedPlayer : Photon.MonoBehaviour
{
    // Use this for initialization
    bool gameIsStarted;
    public GameObject avatar;
    public Transform playerGlobal;
    public Transform playerLocal;

    [SerializeField]
    public GameObject turnTrigger;

    [SerializeField]
    GameObject betControl;

    [SerializeField]
    GameObject pot;

    [SerializeField]
    GameObject cardControl;

    GameObject vrUI;



    void Start()
    {
        vrUI = GameObject.Find("UI");
        if (PhotonNetwork.isMasterClient && photonView.isMine && PhotonNetwork.player.ID == 1)
        {
            //vrUI.SetActive(true);
        }
        if (!PhotonNetwork.isMasterClient && photonView.isMine && PhotonNetwork.player.ID == 2)
        {
            //vrUI.SetActive(false);
            //GameObject.Find("UI").SetActive(false);
        }
        if (photonView.isMine && PhotonNetwork.player.ID == 2)
        {
            Debug.Log("betcontroller " + this.photonView.ownerId.ToString());
            //create a betcontroller for each player
            betControl = PhotonNetwork.Instantiate(betControl.name, betControl.transform.position, Quaternion.identity, 0);

        }
        if (photonView.isMine && PhotonNetwork.player.ID == 1)
        {
            Debug.Log("betcontroller " + this.photonView.ownerId.ToString());
            //create a betcontroller for each player
            betControl = PhotonNetwork.Instantiate(betControl.name, new Vector3(-0.224f, 0, -0.064f), Quaternion.Euler(0, 180, 0), 0);

        }



    }

    void Update()
    {
        if (gameIsStarted)
        {
            turnTrigger.SetActive(true);
        }
        if (photonView.isMine)
        {
            //PhotonNetwork.InstantiateSceneObject(cardControl.name, cardControl.transform.position, cardControl.transform.rotation, 0, null);


      
            //seat tranform = desired transform for player
            Transform seatTrans = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID - 1];

            playerGlobal = GameObject.Find("[CameraRig]").transform;
            playerLocal = GameObject.Find("[CameraRig]/Camera (head)/Camera (eye)").transform;
            GameObject.Find("[SteamVR]").transform.position = seatTrans.position;

            this.transform.position = (playerGlobal).transform.position;
            this.transform.rotation = (playerLocal).transform.rotation;

            //this.transform.localPosition = Vector3.zero;
            //stream.SendNext(GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats);

            //moves the camera to your seat
            playerGlobal.position = seatTrans.position;
            //GameObject.Find("[CameraRig]/Camera (head)").transform.position = seatTrans.position;

            //we dont want to see ourselves
            avatar.SetActive(false);

            //this.transform.localRotation = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID -1].rotation;
            //NetworkController.Seats[PhotonNetwork.player.ID - 1].transform.position;
        }
    }

    public void StartGame()
    {
        if (PhotonNetwork.isMasterClient && photonView.isMine)
        {
            //temporary start game button
            Debug.Log("Starting Game");

            //GameObject.Find("UI").SetActive(false);

            //next turn button
            PhotonNetwork.Instantiate(turnTrigger.name, turnTrigger.transform.position, Quaternion.identity, 0);
        }


    }

    // Update is called once per frame
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(vrUI);

            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocal.localPosition);
            stream.SendNext(playerLocal.localRotation);

        }
        else
        {
            this.vrUI = (GameObject)stream.ReceiveNext();

            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
