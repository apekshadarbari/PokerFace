using UnityEngine;
using System.Collections;
using System;

public class NetworkManager : Photon.MonoBehaviour
{

    private const string roomName = "RoomName";
    private TypedLobby lobbyName = new TypedLobby("New_Lobby", LobbyType.Default);
    private RoomInfo room;
    public GameObject player;

    bool server = true;

    // Use this for initialization
    void Start()
    {
        //game version numberhv
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //void OnGUI()
    //{
    //    if (!PhotonNetwork.connected)
    //    {
    //        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    //    }
    //    else if (PhotonNetwork.room == null)
    //    {
    //        //create room
    //        if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
    //        {
    //            PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 3, isOpen = true, isVisible = true }, lobbyName);
    //        }
    //        // Join Room
    //        if (roomsList != null)
    //        {
    //            for (int i = 0; i < roomsList.Length; i++)
    //            {
    //                if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].name))
    //                    PhotonNetwork.JoinRoom(roomsList[i].name);
    //            }
    //        }
    //    }
    //}
    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(lobbyName);
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.connectionStateDetailed.ToString();
            //server = true;

        }
       
        else if (PhotonNetwork.room == null)
        {

            ////create room
            //if (room == null)
            //{
            //    Debug.Log("created " + roomName);
            //    PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 3, isOpen = true, isVisible = true }, lobbyName);
            //    server = false;
            //}
            //// Join Room
            //if (room != null)
            //{
            //    //for (int i = 0; i < roomsList.Length; i++)
            //    //{
            //        Debug.Log("joined " + room.name);
            //        PhotonNetwork.JoinRoom(room.name);
                
            //}

        }
    }
    //void OnReceivedRoomListUpdate()
    //{
    //    //Debug.Log("Room was created");
    //     PhotonNetwork.JoinRoom(room.name);
    //}
    void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");

        RoomOptions roomOptions = new RoomOptions() { };
       
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
        
    }

    void OnJoinedRoom()
    {
        Debug.Log("Connected to Room");
        PhotonNetwork.Instantiate(player.name, Vector3.up * 5, Quaternion.identity, 0);
    }

}
