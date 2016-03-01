using System.Collections;
using UnityEngine;
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
    private void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        rigidbody = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
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

    private void FixedUpdate()
    {
        Velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (touchedObject != null && touchedObject != other.gameObject)
            touchedObject.SendMessage("OffTouch", this, SendMessageOptions.DontRequireReceiver);
        //added onhoverExit from betmore
        other.GetComponent<BetMore>().OnExitHover();

        touchedObject = other.gameObject;
        touchedObject.SendMessage("OnTouch", this, SendMessageOptions.DontRequireReceiver);

        //added onhover from betmore
        other.GetComponent<BetMore>().OnHover();
    }

    private void OnTriggerExit(Collider other)
    {
        if (touchedObject != null && touchedObject == other.gameObject)
        {
            touchedObject.SendMessage("OffTouch", this, SendMessageOptions.DontRequireReceiver);
            //added onhoverExit from betmore
            other.GetComponent<BetMore>().OnExitHover();
            touchedObject = null;
        }
    }

    private void OnTriggerPressed()
    {
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
            //if we want to interact on the press action
            //HeldObject.GetComponent<BetMore>().EndTurn();
        }
    }

    private void OnTriggerReleased()
    {
        if (HeldObject == null)
            return;

        HeldObject.SendMessage("OnReleased", this, SendMessageOptions.DontRequireReceiver);

        if (heldJoint != null)
            Destroy(heldJoint);

        Rigidbody rb = HeldObject.GetComponent<Rigidbody>();

        if (rb != null)
            rb.velocity = Velocity;

        HeldObject = null;

        //if we want to interact on the press action
        HeldObject.GetComponent<BetMore>().EndTurn();
    }

    public void OnTeleport()
    {
        if (HeldObject != null)
            HeldObject.transform.position = transform.TransformPoint(heldObjectOffset);
    }
}