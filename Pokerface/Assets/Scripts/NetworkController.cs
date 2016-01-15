using UnityEngine;
using System.Collections;

public class NetworkController : Photon.MonoBehaviour {

    private const string roomName = "RoomName";
    private TypedLobby lobbyName = new TypedLobby("New_Lobby", LobbyType.Default);
    private RoomInfo room;
    public GameObject player;

    void Start () {
        
        PhotonNetwork.ConnectUsingSettings("v4.2");	
	}
	void Update ()
    {

    }

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
    void OnJoinedLobby(){
        
        Debug.Log("Joined Lobby!");
        RoomOptions roomOptions = new RoomOptions(){};
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }
    
    void OnJoinedRoom()
    {
        Debug.Log("Connected to Room");
        PhotonNetwork.Instantiate(player.name, Vector3.up * 5, Quaternion.identity, 0);
    }
}
