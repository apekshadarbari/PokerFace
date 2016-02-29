using UnityEngine;
using System.Collections;
using Valve.VR;

public class Controller : MonoBehaviour
{
    public bool debug = false;
    public Vector3 Velocity { get; internal set; }
    public GameObject HeldObject { get; internal set; }

    private SteamVR_TrackedObject trackedObject = null;
    private bool triggerIsPressed = false;
    private GameObject touchedObject = null;
    private new Rigidbody rigidbody = null;
    private Joint heldJoint = null;
    private Vector3 heldObjectOffset = Vector3.zero;

    private Vector3 lastPosition = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        rigidbody = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        VRControllerState_t state = new VRControllerState_t();
        bool success = SteamVR.instance.hmd.GetControllerState((uint)trackedObject.index, ref state);

        if (success == true)
        {
            //SteamVR_Controller.Device device = SteamVR_Controller.Input((int)deviceID);
            //if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) == true)
            if (SteamVR_Controller.Input((int)trackedObject.index).GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                OnTriggerPressed();
            }

            bool triggerPressed = (state.ulButtonPressed & SteamVR_Controller.ButtonMask.Trigger) != 0;

            if (triggerPressed == true)

            {

                Debug.Log("Trigger Pressed is true");
                if (triggerIsPressed == false)
                {
                    OnTriggerPressed();
                    triggerIsPressed = true;
                }
            }
            else if (triggerIsPressed == true)
            {
                OnTriggerReleased();
                triggerIsPressed = false;
            }
        }

        if (HeldObject != null)
        {
            if (heldJoint == null)
            {
                Rigidbody rb = HeldObject.GetComponentInParent<Rigidbody>() ?? HeldObject.GetComponentInChildren<Rigidbody>();

                if (rb != null)
                {
                    rb.AddForceAtPosition(Velocity, HeldObject.transform.position);
                }
            }
        }

        if (debug == true)
        {
            Debug.Log(name + " velocity = " + Velocity);
        }

    }

    void FixedUpdate()
    {
        Velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("On Trigger Enter");
        if (touchedObject != null && touchedObject != other.gameObject)
            touchedObject.SendMessage("OffTouch", this, SendMessageOptions.DontRequireReceiver);
		other.GetComponent<BetMore> ().OnHover();
        touchedObject = other.gameObject;
        touchedObject.SendMessage("OnTouch", this, SendMessageOptions.DontRequireReceiver);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("On Trigger Exit");
        if (touchedObject != null && touchedObject == other.gameObject)
        {
            touchedObject.SendMessage("OffTouch", this, SendMessageOptions.DontRequireReceiver);
            touchedObject = null;
			other.GetComponent<BetMore> ().OnExitHover();
        }
    }

    void OnTriggerPressed()
    {
        Debug.Log("On Trigger Pressed");
        if (touchedObject != null && HeldObject == null && heldJoint == null)
        {
            HeldObject = touchedObject;

            HeldObject.SendMessage("OnPressed", this, SendMessageOptions.DontRequireReceiver);

            heldObjectOffset = transform.InverseTransformPoint(HeldObject.transform.position) / transform.lossyScale.magnitude;

            Rigidbody rb = HeldObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.transform.SetParent(null);
                rb.isKinematic = false;
                heldJoint = touchedObject.AddComponent<FixedJoint>() as Joint;
                heldJoint.connectedBody = rigidbody;
            }
			HeldObject.GetComponent<BetMore> ().EndTurn ();
        }
    }

    void OnTriggerReleased()
    {
        Debug.Log("On Trigger Released");
        if (HeldObject == null)
            return;

        HeldObject.SendMessage("OnReleased", this, SendMessageOptions.DontRequireReceiver);

        if (heldJoint != null)
            Destroy(heldJoint);

        Rigidbody rb = HeldObject.GetComponent<Rigidbody>();

        if (rb != null)
            rb.velocity = Velocity;

        HeldObject = null;
    }

    public void OnTeleport()
    {
        if (HeldObject != null)
            HeldObject.transform.position = transform.TransformPoint(heldObjectOffset);
    }
}
