using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ChipsDisplay))]
public class ChipDisplayNetwork : Photon.MonoBehaviour
{
    private ChipsDisplay cd;
    private int currentValue;

    // Use this for initialization
    private void Start()
    {
        cd = gameObject.GetComponent<ChipsDisplay>();
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            if (currentValue != cd.Value)
            {
                currentValue = cd.Value;

                // Stream new value
                stream.SendNext(currentValue);
            }
        }
        else
        {
            // Read value from stream
            currentValue = (int)stream.ReceiveNext();

            // Update display
            cd.Value = currentValue;
            cd.UpdateStacks();
        }
    }
}