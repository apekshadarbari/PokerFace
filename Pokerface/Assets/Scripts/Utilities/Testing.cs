using System.Collections;
using System.Reflection;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private MeshRenderer crosshairVive;
    private MeshRenderer crosshairRift;
    private GameObject playerRig;

    [SerializeField, Header("Seats to swap between")]
    private GameObject[] seats;

    private bool seatChange;

    // Use this for initialization
    private void Start()
    {
        crosshairRift = GameObject.FindGameObjectWithTag("CrosshairRift").GetComponent<MeshRenderer>();
        crosshairVive = GameObject.FindGameObjectWithTag("CrosshairVive").GetComponent<MeshRenderer>();

        playerRig = GameObject.Find("[CameraRig]");
        var vrSpace = GameObject.Find("[SteamVR]").transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        /*TESTING FOR MOUSE EXHCHANGE WITH RIFT*/
        if (Input.GetKeyDown("k"))
        {
            if (crosshairVive.enabled == true)
            {
                crosshairRift.enabled = true;
                crosshairVive.enabled = false;
            }
            else
            {
                crosshairRift.enabled = false;
                crosshairVive.enabled = true;
            }
        }
        if (Input.GetKeyDown("z"))
        {
            if (seatChange)
            {
                playerRig.transform.position = seats[0].transform.position;
                seatChange = false;
            }
            else
            {
                playerRig.transform.position = seats[1].transform.position;
                seatChange = true;
            }
            VRInputManager.Instance.DeviceSpecificSeating();
        }
        if (Input.GetKeyDown("i"))
        {
            Debug.ClearDeveloperConsole();
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.ActiveEditorTracker));
            var type = assembly.GetType("UnityEditorInternal.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }
    }
}