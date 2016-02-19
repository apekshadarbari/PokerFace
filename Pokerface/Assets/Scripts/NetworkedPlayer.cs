using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkedPlayer : Photon.MonoBehaviour
{
    // Use this for initialization
    bool gameIsStarted;
    //players avatar
    public GameObject avatar;
    //players camera rig / perspective
    public Transform playerGlobal;
    public Transform playerLocal;
    public Transform playerHead;

    //the turnswitch trigger
    [SerializeField]
    public GameObject turnTrigger;

    //players betcontrol for buttons
    [SerializeField]
    GameObject betControl;

    //the game pot
    [SerializeField]
    GameObject pot;

    //control of the cards
    [SerializeField]
    GameObject cardControl;


    void Start()
    {


        if (photonView.isMine) //TODO: check how much can be moved to start - making seats the Parents might make it easier to deal with but will require some restructuring
        {
            Transform seatTrans = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID - 1];

            //Transform seatTrans = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID - 1];

            //playerGlobal = GameObject.Find("[CameraRig]").transform;
            playerGlobal = GameObject.Find("[CameraRig]").transform;
            playerLocal = GameObject.Find("[CameraRig]/Camera (head)/Camera (eye)").transform;

            GameObject.Find("[SteamVR]").transform.position = seatTrans.position;

            playerGlobal.position = seatTrans.position;
            //this.transform.position = (playerGlobal).transform.position;
            //this.transform.rotation = (playerLocal).transform.rotation;



            ////seat tranform = desired transform for player
            //playerGlobal = GameObject.Find("[CameraRig]").transform;
            //playerLocal = GameObject.Find("[CameraRig]/Camera (head)/Camera (eye)").transform;
            //Transform vrSpace = GameObject.Find("[SteamVR]").transform;

            //playerGlobal.transform.SetParent(seatTrans);

            //playerLocal.transform.SetParent(seatTrans);
            //vrSpace.transform.SetParent(seatTrans);
            //playerGlobal.position = seatTrans.position;

            //avatar.SetActive(false);
        }

        //player 2 
        if (photonView.isMine && PhotonNetwork.player.ID == 2)
            {
                Debug.Log("betcontroller " + this.photonView.ownerId.ToString());
                //create a betcontroller for each player
                betControl = PhotonNetwork.Instantiate(betControl.name, betControl.transform.position, Quaternion.identity, 0);
                betControl.tag = "Player2BetController";

                foreach (Transform t in betControl.transform)
                {
                    t.gameObject.tag = "PlayerTwoButton";
                    t.GetComponent<MeshRenderer>().enabled = false;
                    t.GetComponent<SphereCollider>().enabled = false;
                }

            }

            //player 1
            if (photonView.isMine && PhotonNetwork.player.ID == 1)
            {
                Debug.Log("betcontroller " + this.photonView.ownerId.ToString());
                //create a betcontroller for each player
                betControl = PhotonNetwork.Instantiate(betControl.name, new Vector3(-0.2f, 0, -0.060f), Quaternion.Euler(0, 180, 0), 0);

                //test position - when no oculus
                //betControl = PhotonNetwork.Instantiate(betControl.name, new Vector3(3f, 0, 1), Quaternion.Euler(0, 208, 0), 0);

                betControl.tag = "Player1BetController";

                foreach (Transform t in betControl.transform)
                {
                    t.gameObject.tag = "PlayerOneButton";
                    t.GetComponent<MeshRenderer>().enabled = false;
                    t.GetComponent<SphereCollider>().enabled = false;
                }
            }

        //}
    }

    void Update()
    {
        if (gameIsStarted) //TODO: check for redundancy
        {
            turnTrigger.SetActive(true);
        }
        if (photonView.isMine) //TODO: check how much can be moved to start - making seats the Parents might make it easier to deal with but will require some restructuring
        {

            Transform seatTrans = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID - 1];

            //Transform seatTrans = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID - 1];

            //playerGlobal = GameObject.Find("[CameraRig]").transform;
            playerGlobal = GameObject.Find("[CameraRig]").transform;
            playerLocal = GameObject.Find("[CameraRig]/Camera (head)/Camera (eye)").transform;

            GameObject.Find("[SteamVR]").transform.position = seatTrans.position;

            playerGlobal.position = seatTrans.position;

            //TODO; might need another var for a player head for the pos tracking 
            this.transform.position = (playerLocal).transform.position;
            this.transform.rotation = (playerLocal).transform.rotation;

            //avatar.transform.position = (playerLocal).transform.position;
            //avatar.transform.rotation = (playerLocal).transform.rotation;
            ////seat tranform = desired transform for player

            //TESTING ;; CHANGE BACK 
            //Transform seatTrans = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID - 1];

            //playerGlobal = GameObject.Find("[CameraRig]").transform;
            //playerLocal = GameObject.Find("[CameraRig]/Camera (head)/Camera (eye)").transform;
            //GameObject.Find("[SteamVR]").transform.position = seatTrans.position;

            //this.transform.position = (playerGlobal).transform.position;
            //this.transform.rotation = (playerLocal).transform.rotation;

            //this.transform.localPosition = Vector3.zero;
            //stream.SendNext(GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats);

            //moves the camera to your seat
            //GameObject.Find("[CameraRig]/Camera (head)").transform.position = seatTrans.position;
            //playerGlobal.position = seatTrans.position;

            //we dont want to see ourselves
            //avatar.SetActive(false);

            //this.transform.localRotation = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID - 1].rotation;
            //NetworkController.Seats[PhotonNetwork.player.ID - 1].transform.position;

        }
    }

    public void StartGame()
    {
        //creates a betcontroller for each player and tags it for later reference
        if (PhotonNetwork.isMasterClient && photonView.isMine)
        {
            //start game button clicked
            //Debug.Log("Starting Game");

            //next turn button created
            PhotonNetwork.Instantiate(turnTrigger.name, turnTrigger.transform.position, Quaternion.identity, 0);
        }



        //TODO: Enable things when game is started, should be RPC

        //GameObject[] enablerArr;
        //enablerArr.

        //    GameObject.FindGameObjectsWithTag("PlayerOneButton");
        //enabler.a
        //    PlayerTwoButton
        //    Player2BetController
        //    Player1BetController
        //    TurnTrigger
    }



    // Update is called once per frame
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
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
