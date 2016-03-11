using UnityEngine;

public class NetworkController : Photon.MonoBehaviour
{
    //an array of seats in the room
    [SerializeField]
    private Transform[] seats;

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

    //current version
    [Header("the current version of the build - change when testing seperately")]
    [SerializeField]
    private string version = "0.1";

    [SerializeField]
    private string roomName = "New_room";

    private TypedLobby lobbyName = new TypedLobby("New_Lobby", LobbyType.Default);

    //the player TODO: FIX THIS CLASS , FIND OUT WHAT A GROUP CAN BE USED FOR WHEN INSTANTIATING OBJECTS HERE.
    public GameObject player;

    [SerializeField]
    private GameObject startgameButton;

    private void Start()
    {
        //the current version of the game, change at milestones
        PhotonNetwork.ConnectUsingSettings(version);
        //Debug.Log("Started version: " + version);
    }

    private void Update()
    {
    }

    /// <summary>
    /// For Later use if lobby is implemented
    /// </summary>
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

    //step before joinging the rooom, changed if acutal lobby implemented
    private void OnJoinedLobby()
    {
        //Debug.Log("Joined Lobby!");

        //set room options as needed
        RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 2 };
        //If there is no room, create one, otherwise join
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
        //PhotonNetwork.JoinOrCreateRoom("", roomOptions, TypedLobby.Default);

        //PhotonNetwork.JoinRandomRoom();
    }

    private void OnJoinedRoom()
    {
        //when a room is joined
        //Debug.Log("Connected to Room");
        //run startgame
        StartGame();
    }

    private void StartGame()
    {
        //which player am i?
        //Debug.Log(PhotonNetwork.player.ID.ToString());

        //instantiates the player at the corresponding seat
        //Debug.Log(PhotonNetwork.playerList.Length);
        //if (PhotonNetwork.playerList.Length >= 2)
        //{
        //    //if (PhotonNetwork.isMasterClient)
        //    //{
        //PhotonNetwork.InstantiateSceneObject(startgameButton.name, startgameButton.transform.position, startgameButton.transform.rotation, 0, null);
        //}
        //}

        PhotonNetwork.Instantiate(player.name, Seats[PhotonNetwork.player.ID - 1].position, Quaternion.identity, 0);

        //TODO: make sure that this makes it instantiate the button only the one time
    }

    private void OnLeftRoom()
    {
        //Debug.Log(PhotonNetwork.player.ID.ToString());
        //Debug.Log(PhotonNetwork.playerList.Length.ToString());
        //PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player.ID);
    }

    private void OnRoomCleanup()
    {
    }
}