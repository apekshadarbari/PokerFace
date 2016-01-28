using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkedPlayer : Photon.MonoBehaviour
{
    //private Vector3 correctPlayerPos = Vector3.zero; // We lerp towards this
    //private Quaternion correctPlayerRot = Quaternion.identity; // We lerp towards this

    // Use this for initialization

    private int turnInt;

    public GameObject card;

    [SerializeField]
    GameObject betControl;

    public GameObject avatar;
    public Transform playerGlobal;
    public Transform playerLocal;

    public int TurnInt
    {
        get
        {
            return turnInt;
        }

        set
        {
            turnInt = value;
        }
    }

    //public List<NetworkedPlayer> avatars = new List<NetworkedPlayer>();
    [SerializeField]
    GameObject turnTrigger;
    void Awake()
    {

        //PhotonNetwork.Instantiate(turnTrigger.name, turnTrigger.transform.position, Quaternion.identity, 1);
    }

    void Start()
    {
        Debug.Log("I'm instantiated!");

        if (this.photonView.isMine)
        {
            betControl = PhotonNetwork.Instantiate(betControl.name, betControl.transform.position, Quaternion.identity, 0);
        }
    }

    void Update()
    {
        if (photonView.isMine)
        {
            //seat tranform = desired transform for player
            Transform seatTrans = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID - 1];

            playerGlobal = GameObject.Find("[CameraRig]").transform;
            playerLocal = GameObject.Find("[CameraRig]/Camera (head)/Camera (eye)").transform;

            this.transform.position = (playerGlobal).transform.position;
            this.transform.rotation = (playerLocal).transform.rotation;

            //this.transform.localPosition = Vector3.zero;
            //stream.SendNext(GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats);

            //moves the camera to your seat
            playerGlobal.position = seatTrans.position;
            //GameObject.Find("[CameraRig]/Camera (head)").transform.position = seatTrans.position;

            //we dont want to see ourselves
            //avatar.SetActive(false);


            //this.transform.localRotation = GameObject.Find("NetworkController").GetComponent<NetworkController>().Seats[PhotonNetwork.player.ID -1].rotation;
            //NetworkController.Seats[PhotonNetwork.player.ID - 1].transform.position;
        }
        if (PhotonNetwork.isMasterClient && photonView.isMine)
        {

            if (Input.GetKeyDown("e"))
            {
            //    Debug.Log("pik lort");

            //    card = PhotonNetwork.Instantiate(card.name, Vector3.zero, Quaternion.identity, 0);
                    turnTrigger = PhotonNetwork.Instantiate(turnTrigger.name, turnTrigger.transform.position, Quaternion.identity, 0);

            }
            if (Input.GetKeyDown("space"))
            {
                //TurnInt = turnSwitch.;
                Debug.Log("i pushed the button and i liked it");

                card.transform.Rotate(0, +180, 0);
                //PhotonNetwork.SetMasterClient();
                //if player id >= max players, dealer, then reset
                //PhotonNetwork.isMasterClient = false;
                //turnSwitch.GetComponent<PhotonView>().RPC("SwitchTurns", PhotonTargets.All, TurnInt);
            }



            //}
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
