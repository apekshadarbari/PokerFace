using System.Collections;
using System.Reflection;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private TestRaycast gaze;
    private MeshRenderer crosshairVive;
    private MeshRenderer crosshairRift;
    private GameObject playerRig;

    private TestRaycast gazeEye;
    private TestRaycast gazeHead;
    private MouseLook mouseLook;

    [SerializeField, Header("Seats to swap between")]
    private GameObject[] seats;

    private bool seatChange;
    private bool gazeIsOn;

    // Use this for initialization
    private void Start()
    {
        gaze = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TestRaycast>();
        crosshairRift = GameObject.FindGameObjectWithTag("CrosshairRift").GetComponent<MeshRenderer>();
        crosshairVive = GameObject.FindGameObjectWithTag("CrosshairVive").GetComponent<MeshRenderer>();

        gazeEye = GameObject.Find("[CameraRig]/Camera (head)").GetComponent<TestRaycast>();
        gazeHead = GameObject.Find("[CameraRig]/Camera (head)/Camera (eye)").GetComponent<TestRaycast>();
        mouseLook = GameObject.Find("[CameraRig]/Camera (head)").GetComponent<MouseLook>();

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
        //toggle gaze input
        if (Input.GetKeyDown("g"))
        {
            if (gazeIsOn)
            {
                gazeIsOn = false;
                gazeHead.enabled = false;
                gazeEye.enabled = false;
                crosshairRift.enabled = false;
            }
            else
            {
                gazeIsOn = true;
                gazeHead.enabled = true;
                gazeEye.enabled = true;
                crosshairRift.enabled = true;
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