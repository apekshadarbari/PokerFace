using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkController : Photon.MonoBehaviour
{
    //public Transform[] testForm;

    //public GameObject camRig;

    [SerializeField]
    private Transform[] seats;
    //public Transform[] cams;
    //private List<GameObject> seats = new List<GameObject>();

    public Transform[] Seats
    {
        get
        {
            return seats;
        }

        set
        {
            seats = value;
        }
    }
    private string version = "0.1";
    private string roomName = "New_room";
    private TypedLobby lobbyName = new TypedLobby("New_Lobby", LobbyType.Default);
    //RoomInfo[] roomsList;
    //public int seatMax = PhotonNetwork.room.playerCount;
    //private RoomInfo room;
    public GameObject player;


    public List<GameObject> players = new List<GameObject>();




    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(version);

        //OnConnectedToMaster();
        Debug.Log("started");
    }
    void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //    JoinOrCreate();
        //}

        //if (Input.anyKeyDown)
        //{
        //    server = false;
        //    Debug.Log("server = true");
        //}

    }

    //void OnConnectedToMaster()
    //{
    //    Debug.Log("hej");
    //    PhotonNetwork.JoinLobby(lobbyName);

    //}
    //void OnReceivedRoomListUpdate()
    //{
    //    roomsList = PhotonNetwork.GetRoomList();
    //    if (!PhotonNetwork.connected)
    //    {
    //        PhotonNetwork.connectionStateDetailed.ToString();
    //    }
    //    else if (PhotonNetwork.room == null)
    //    {
    //        Debug.Log("room length : " + PhotonNetwork.GetRoomList().Length);


    //        Debug.Log("if no room joined any key for create");

    //        //create room

    //        if (PhotonNetwork.GetRoomList().Length == 0)
    //        {
    //            //if (server)
    //            //{
    //            Debug.Log("created " + roomName);
    //            PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions() { maxPlayers =  }, lobbyName);
    //            //PhotonNetwork.Instantiate(seats);
    //            //PhotonNetwork.Instantiate();
    //            server = false;
    //        }
    //        // Join Room
    //        if (PhotonNetwork.GetRoomList().Length != 0)
    //        {
    //            for (int i = 0; i < roomsList.Length - 1; i++)
    //            {
    //                Debug.Log("joined " + roomsList[i].name);
    //                PhotonNetwork.JoinRoom(roomsList[i].name);
    //            }
    //        }
    //    }
    //    //roomsList = PhotonNetwork.GetRoomList();
    //}
    void OnJoinedLobby()
    {


        Debug.Log("Joined Lobby!");

        RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);


        //PhotonNetwork.JoinOrCreateRoom();
    }

    void OnJoinedRoom()
    {
        Debug.Log("Connected to Room");

        //if (PhotonNetwork.room.playerCount == 2)
        //{
        StartGame();

        //}
    }
    void StartGame()
    {
        //new GameObject("seat", typeof(SeatAquire));
        //if (PhotonNetwork.player.ID == 1)
        //{
        Debug.Log(PhotonNetwork.player.ID.ToString());
        PhotonNetwork.Instantiate(player.name, Seats[PhotonNetwork.player.ID - 1].position, Quaternion.identity, 0);

        //PhotonNetwork.Instantiate(player.name, seats[PhotonNetwork.player.ID - 1].position, seats[PhotonNetwork.player.ID -1].rotation, 0);
        //GameObject tmpPlayer = PhotonNetwork.Instantiate(player.name, seats[PhotonNetwork.player.ID - 1].position, Quaternion.identity, 0);
        //GameObject tmpCam =
        //PhotonNetwork.Instantiate(camRig.name, seats[PhotonNetwork.player.ID - 1].transform.position, Quaternion.identity, 0);
        //PhotonNetwork.Instantiate("SeatNetwork", Vector3.zero, Quaternion.identity, 0);
        //GameObject.Find("[CameraRig]").transform.position = seats[i].transform.position;
        //player.GetComponent<NetworkController>().camRig = cam;
        //tmpCam.SetActive(true);
        //tmpPlayer.GetComponent<NetworkedPlayer>().playerGlobal = tmpCam.transform;
        //tmpPlayer.GetComponent<NetworkedPlayer>().playerLocal = tmpCam.transform.Find("Camera (head)/Camera (eye)").gameObject.transform;

        //Debug.Log(i.ToString());
        //Debug.Log(seats[i].ToString());
        //break;
        //}



        //for (int i = 0; i <= seatMax; i++)
        //{
        //    PhotonNetwork.Instantiate(player.name,
        //                              seats[i].transform.position,
        //                              seats[i].transform.rotation,
        //                              0);
        //}
        //for (int i = 0; i < seats.Count; i++)
        //{
        //    if (seats[i].GetComponent<Occupied>().OccupySeat())
        //    {

        //    }

        //}
    }





    //void Create()
    //{
    //    PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 10 }, null);
    //}

}
