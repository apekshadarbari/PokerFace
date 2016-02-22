using UnityEngine;
using System.Collections;

public class VRInputManager : MonoBehaviour {

    SteamVR vr;

    void Start()
    {
        vr = SteamVR.instance;
        string hmdModel = vr.hmd_ModelNumber.ToString();
        //Vive DVT
        string hmdTracker = vr.hmd_TrackingSystemName.ToString();
        //lighthouse
        Debug.Log(hmdModel);
        Debug.Log(hmdTracker);
    }
}
