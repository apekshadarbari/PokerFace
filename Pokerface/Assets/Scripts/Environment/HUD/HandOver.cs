using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandOver : PhotonManager<HandOver>
{
    [SerializeField]
    private float stayDelay = 3f;

    [SerializeField]
    private Text handOverTxt;

    private bool hasBeenShown;

    // Use this for initialization
    private void Start()
    {
        hasBeenShown = true;
    }

    // Update is called once per frame
    private void Update()
    {
        FaceCamera();

        if (!hasBeenShown)
        {
            gameObject.GetComponent<Canvas>().enabled = true;
            StartCoroutine(Wait());
        }
        else
        {
            gameObject.GetComponent<Canvas>().enabled = false;
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

            case ActionSound.p1Fold:
                this.handOverTxt.GetComponent<Text>().text = "Player 1 Folds";
                break;

            case ActionSound.p2Fold:
                this.handOverTxt.GetComponent<Text>().text = "Player 2 Folds";
                break;

            case ActionSound.p1Win:
                this.handOverTxt.GetComponent<Text>().text = "Player 1 Wins!";
                break;

            case ActionSound.p2Win:
                this.handOverTxt.GetComponent<Text>().text = "Player 2 Wins!";
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

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(stayDelay);
        gameObject.GetComponent<Canvas>().enabled = false;
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