using UnityEngine;
using System.Collections;

public class BetManager : Photon.MonoBehaviour
{
    [SerializeField]
    int chipValue;
    public void Bet()
    {
        Debug.Log(this.photonView.ownerId + " bet something");
    }
    public void CallOrCheck()
    {
        Debug.Log(this.photonView.ownerId + " check or called");
    }
    public void Fold()
    {
        Debug.Log(this.photonView.ownerId + " folds");
    }

}
