using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;

public class NetworkController : Photon.MonoBehaviour
{
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
    private string version = "0.1";
    private string roomName = "New_room";
    private TypedLobby lobbyName = new TypedLobby("New_Lobby", LobbyType.Default);
    public GameObject player;

    //public List<GameObject> players = new List<GameObject>();
    void Start()
    {

        PhotonNetwork.ConnectUsingSettings(version);

        Debug.Log("started");
    }

    void Update()
    {

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
    }

    void OnJoinedRoom()
    {
        Debug.Log("Connected to Room");
        StartGame();
    }
    void StartGame()
    {
        Debug.Log(PhotonNetwork.player.ID.ToString());
        //PhotonNetwork.Instantiate(player.name, Seats[PhotonNetwork.playerList.Length].position, Quaternion.identity, 0);
        PhotonNetwork.Instantiate(player.name, Seats[PhotonNetwork.player.ID - 1].position, Quaternion.identity, 0);
    }

    void OnLeftRoom()
    {
        //Debug.Log(PhotonNetwork.player.ID.ToString());
        //Debug.Log(PhotonNetwork.playerList.Length.ToString());
        //PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player.ID);
    }

    void OnRoomCleanup()
    {


    }
}
