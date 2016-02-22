using UnityEngine;
using System.Collections;

public class VRInputManager : MonoBehaviour
{

    SteamVR vr;

    GameObject crosshair;

    GameObject seat;

    Vector3 playerRig;


    void Start()
    {
        //null for rift
        vr = SteamVR.instance;
        //Debug.Log(PhotonNetwork.countOfPlayers);
        Debug.Log(PhotonNetwork.player.isLocal);
        //lighthouse if vive
        string hmd = vr.hmd_TrackingSystemName.ToString();

        playerRig = GameObject.Find("[CameraRig]").transform.position;
        var vrSpace = GameObject.Find("[SteamVR]").transform.position;

        if (PhotonNetwork.player.isLocal)
        {
            //if the player is local, and not vive
        }
        if (hmd == "lighthouse")
        {
            //crosshair if gaze on enable crosshair on head 
            //seats set to Y.0
            crosshair = GameObject.FindGameObjectWithTag("CrosshairVive");
            crosshair.GetComponent<MeshRenderer>().enabled = true;
            //GameObject.Find("[CameraRig]").transform.position += Vector3.up * 1.4f;
  

            Debug.Log("Vive");
        }
        else
        {
            //gaze on eye
            //raycast from eye?
            //
            crosshair = GameObject.FindGameObjectWithTag("CrosshairRift");
            crosshair.GetComponent<MeshRenderer>().enabled = true;
            playerRig = GameObject.Find("[CameraRig]").transform.position += Vector3.up * 1.4f;
            Debug.Log("Not Vive");
        }

        //Debug.Log(hmd);
    }
}
