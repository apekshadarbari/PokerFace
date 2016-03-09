using System.Collections;
using UnityEngine;

public class BtnController : PhotonManager<BtnController>
{
    // Use this for initialization
    private void Start()
    {
        if (this.photonView.ownerId == 2)
        {
            this.tag = "Player2BetController";

            foreach (Transform t in this.transform)
            {
                t.gameObject.tag = "PlayerTwoButton";
            }
        }
        else if (this.photonView.ownerId == 1)
        {
            this.tag = "Player1BetController";

            foreach (Transform t in this.transform)
            {
                t.gameObject.tag = "PlayerOneButton";
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //stream.SendNext(gameIsStarted);
        }
        else
        {
            //gameIsStarted  = (bool)stream.ReceiveNext();
        }
    }
}