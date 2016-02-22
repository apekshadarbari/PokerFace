using UnityEngine;
using System.Collections;

public class VRInputManager : MonoBehaviour
{

    SteamVR vr;

    [SerializeField]
    GameObject crosshair;

    [SerializeField]
    GameObject seat;

    void Start()
    {
        //null for rift
        vr = SteamVR.instance;
        
        //lighthouse if vive
        string hmd = vr.hmd_TrackingSystemName.ToString();

        if (hmd == "lighthouse")
        {
            //crosshair if gaze on enable crosshair on head 
            //seats set to Y.0
            crosshair = GameObject.FindGameObjectWithTag("CrosshairVive");
            crosshair.GetComponent<MeshRenderer>().enabled = true;
            Debug.Log("Vive");
        }
        else
        {
            //gaze on eye
            //raycast from eye?
            //
            crosshair = GameObject.FindGameObjectWithTag("CrosshairRift");
            crosshair.GetComponent<MeshRenderer>().enabled = true;
            Debug.Log("Not Vive");
        }

        Debug.Log(hmd);
    }
}
