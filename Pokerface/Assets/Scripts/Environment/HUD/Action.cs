using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Action : PhotonManager<Action>
{
    [SerializeField]
    private float stayDelay = 5f;

    [SerializeField]
    private Text actionTxt;

    private bool hasBeenShown;
    private bool active;
    private int currentPlayerTurn;

    private int raisedBy;

    public bool HasBeenShown
    { get { return hasBeenShown; } set { hasBeenShown = value; } }

    private void Start()
    {
        hasBeenShown = true;
        active = TurnIndicator.Instance.InPosition;
    }

    private void Update()
    {
        active = TurnIndicator.Instance.InPosition;
        currentPlayerTurn = RoundManager.Instance.PlayerTurn;
        Vector3 indicatorPos = TurnIndicator.Instance.TargetPos;
        gameObject.transform.position = new Vector3(indicatorPos.x, indicatorPos.y * 1.01f, indicatorPos.z);
        FaceCamera();

        if (currentPlayerTurn != PhotonNetwork.player.ID) // if it is not my turn
        {
            gameObject.GetComponent<Canvas>().enabled = false; // dont show me the HUD
        }
        else // if it is my turn
        {
            if (active && !hasBeenShown) // and the indicator is in its place
            {
                gameObject.GetComponent<Canvas>().enabled = true; // show me the HUD
                gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
                StartCoroutine(Wait());
            }
            else
            {
                gameObject.GetComponent<Canvas>().enabled = false; //dont show me the HUD
                gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            }
        }
    }

    [PunRPC]
    private void ReceiveAction(bool show, ActionSound action)
    {
        this.hasBeenShown = show;
        switch (action)
        {
            case ActionSound.p1Call:
                this.actionTxt.GetComponent<Text>().text = "Called";
                break;

            case ActionSound.p1Check:
                this.actionTxt.GetComponent<Text>().text = "Checked";
                break;

            case ActionSound.p1Raise:
                this.actionTxt.GetComponent<Text>().text = "Raised :$ " + raisedBy;
                break;

            case ActionSound.p2Call:
                this.actionTxt.GetComponent<Text>().text = "Called";
                break;

            case ActionSound.p2Check:
                this.actionTxt.GetComponent<Text>().text = "Checked";
                break;

            case ActionSound.p2Raise:
                this.actionTxt.GetComponent<Text>().text = "Raised :$ " + raisedBy;
                break;

            default:
                break;
        }
    }

    [PunRPC]
    public void ReceiveRaiseValue(int player, int betValue)
    {
        raisedBy = betValue;
    }

    /// <summary>
    /// Make the gameobject this script is attached to face the camera.
    /// call this method in update to make it follow the camera
    /// </summary>
    public void FaceCamera()
    {   // sets the Camera´s forward positioning towards the Camera.Main (main camera is tagged as such)
        transform.forward = (Camera.main.transform.position - transform.position).normalized;
        // in my case i had to rotate it the other way to make it work
        transform.Rotate(0, 180, 0); // delete if redundant - most likely
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(stayDelay);
        hasBeenShown = true;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
        }
        else
        {
        }
    }
}