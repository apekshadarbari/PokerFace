using UnityEngine;
using System.Collections;

public class Fold : Photon.MonoBehaviour
{


    public void OnClick()
    {
        Debug.Log("player " + this.photonView.ownerId + " folds");
    }
}
