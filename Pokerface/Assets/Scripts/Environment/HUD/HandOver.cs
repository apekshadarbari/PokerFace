using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandOver : PhotonManager<HandOver>
{
    private int player;

    [SerializeField]
    private float stayDelay;

    [SerializeField]
    private float speed = 1f;

    private bool hasBeenShown;

    private Vector3 targetPos;

    public Vector3 TargetPos
    { get { return targetPos; } }

    private Vector3 playerOne = new Vector3(.34f, 1.817f, -.38f);
    private Vector3 playerTwo = new Vector3(.34f, 1.817f, 1.2f);
    private Vector3 startPos = new Vector3(0f, 1.817f, .4f);

    // Use this for initialization
    private void Start()
    {
        WinIndication(1);
        transform.position = targetPos;

        hasBeenShown = true;
    }

    // Update is called once per frame
    private void Update()
    {
        //FaceCamera();

        if (!hasBeenShown)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine(Wait());
            if (transform.position != targetPos)
            {
                Vector3 dir = (targetPos - transform.position).normalized;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.smoothDeltaTime);
            }
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.transform.position = startPos;
        }
    }

    [PunRPC]
    private void ReceiveHandOver(bool show, ActionSound action)
    {
        this.hasBeenShown = show;

        switch (action)
        {
            case ActionSound.roundStarted:
                //this.handOverTxt.GetComponent<Text>().text = "Game Started";
                break;

            case ActionSound.p1Fold: // p2 gets the steak
                targetPos = playerTwo;
                break;

            case ActionSound.p2Fold:// p1 gets the steak
                targetPos = playerOne;
                break;

            case ActionSound.p1Win:// p1 gets the steak
                targetPos = playerOne;
                break;

            case ActionSound.p2Win:// p2 gets the steak
                targetPos = playerTwo;
                break;

            default:
                break;
        }
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

    //take current players turn and lerp toward the position
    public void WinIndication(int player)
    {
        this.player = player;
        if (this.player == 1)
        {
        }
        else if (this.player == 2)
        {
            targetPos = new Vector3(.78f, 1.817f, 1.127f);
        }
        // start position new Vector3(0f,1.817f,.4f);
    }

    private IEnumerator Wait()
    {
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(stayDelay);
        hasBeenShown = true;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(gameObject.transform.position);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
        }
    }
}