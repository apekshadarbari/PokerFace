using UnityEngine;
using System.Collections;

public class BetMore : Photon.MonoBehaviour
{
    [SerializeField]
    GameObject wallet;

    PotManager pot;

    void Start()
    {
        if (this.photonView.ownerId == 1)
        {
            this.photonView.transform.position = new Vector3(3.75f, -0.75f, 0.35f);
        }
        if (this.photonView.ownerId == 2)
        {
            this.photonView.transform.position = new Vector3(-2f, -0.75f, 3f);
        }
        pot = GameObject.Find("pot").GetComponent<PotManager>();
    }

    public void OnClick()
    {
        Debug.Log("clicking");
        int chipsToBet= wallet.GetComponent<WalletManager>().GetChips(5);
        pot.AddChips(this.photonView.ownerId, chipsToBet);

    }
}
