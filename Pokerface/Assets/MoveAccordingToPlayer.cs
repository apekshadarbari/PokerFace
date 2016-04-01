using System.Collections;
using UnityEngine;

public class MoveAccordingToPlayer : MonoBehaviour
{
    private float planePos;
    private void Start()
    {
    }

    private void Update()
    {
        if (PhotonNetwork.player.ID == 1)
        {
            //planePos = gameObject.transform.localPosition.z + .001f;
            gameObject.transform.localPosition = new Vector3(0, 0, .01f); // i dont even....
        }
        else if (PhotonNetwork.player.ID == 2)
        {
            //planePos = gameObject.transform.localPosition.z + -.01f;
            gameObject.transform.localPosition = new Vector3(0, 0, .01f);
        }
    }
}