using System.Collections;
using UnityEngine;

public class VRInputManager : MonoBehaviour
{
    private SteamVR vr;

    private GameObject crosshair;

    private GameObject seat;

    private Vector3 playerRig;

    private void Start()
    {
        //null for rift
        vr = SteamVR.instance;
        //Debug.Log(PhotonNetwork.countOfPlayers);
        //Debug.Log(PhotonNetwork.player.isLocal);
        //lighthouse if vive

        playerRig = GameObject.Find("[CameraRig]").transform.position;
        var vrSpace = GameObject.Find("[SteamVR]").transform.position;

        if (PhotonNetwork.player.isLocal)
        {
            //if the player is local, and not vive
        }
        if (vr != null)
        {
            string hmd = vr.hmd_TrackingSystemName.ToString();

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
                crosshair = GameObject.FindGameObjectWithTag("CrosshairRift");
                crosshair.GetComponent<MeshRenderer>().enabled = true;
                Debug.Log("Not Vive");
            }
        }
        else
        {
            crosshair = GameObject.FindGameObjectWithTag("CrosshairVive");
            crosshair.GetComponent<MeshRenderer>().enabled = true;
            Debug.Log("No VR device");
        }
        //Debug.Log(hmd);
    }
}