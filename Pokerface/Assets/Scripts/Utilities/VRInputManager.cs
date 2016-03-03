using UnityEngine;

public class VRInputManager : PhotonManager<VRInputManager>
{
    [SerializeField, Header("The height of the player")]
    private float playerHeight;

    private SteamVR vr;

    private GameObject crosshair;

    private GameObject playerRig;

    private GameObject controller;
    private TestRaycast gazeEye;

    private TestRaycast gazeHead;
    private MouseLook mouseLook;

    private string hmdString;

    private void Start()
    {
        gazeEye = GameObject.Find("[CameraRig]/Camera (head)/Camera (eye)").GetComponent<TestRaycast>();
        mouseLook = GameObject.Find("[CameraRig]/Camera (head)").GetComponent<MouseLook>();

        //controller = GameObject.FindGameObjectWithTag("ControllerVive");

        //null for rift
        vr = SteamVR.instance;

        if (vr != null)
        {
            crosshair = GameObject.FindGameObjectWithTag("CrosshairRift");
            crosshair.GetComponent<MeshRenderer>().enabled = false;

            // turn off the htc vive controller script unless the vive is plugged in
            //controller.GetComponent<Controller>().enabled = true;

            //Debug.Log("Vive");
        }
        else
        {
            /*RIFT CROSSHAIR*/
            //gaze on eye
            //raycast from eye?
            //gazeHead.enabled = false;
            gazeEye.enabled = true;

            crosshair = GameObject.FindGameObjectWithTag("CrosshairRift");
            crosshair.GetComponent<MeshRenderer>().enabled = true;

            //controller.GetComponent<Controller>().enabled = false;

            Debug.Log("Not Vive");
            //playerRig.transform.position += Vector3.up * 1.2f;

            /*TESTING*/
            //enable mouselook for testting
            //maybe move it to a hotkey
            mouseLook.enabled = true;
        }
    }

    /// <summary>
    /// call at player instantiation
    /// </summary>
    public void DeviceSpecificSeating()
    {
        playerRig = GameObject.Find("[CameraRig]");
        var vrSpace = GameObject.Find("[SteamVR]").transform.position;
        //Debug.Log("Started Seating");
        //the hieght of the player + the room offset
        var seatY = playerHeight;
        if (vr == null)
        {
            //Debug.Log("Seated as non vr");

            playerRig.transform.position += Vector3.up * seatY; // for some reason the room is lower than it should be - if that gets fixed fix this
            //Debug.Log("Not Vive seating, given height -.95f + height of player");
        }
        else
        {
            //Debug.Log("Seated as Vive");

            //Debug.Log("ViveSeating - nothing changed");
        }
    }
}