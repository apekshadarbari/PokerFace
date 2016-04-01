using UnityEngine;
using UnityEngine.UI;

public class Wallet : Manager<Wallet>
{
    [SerializeField]
    private Text wallet;

    [SerializeField]
    private ChipsDisplay chipDisplay;

    private int walletValue;
    // Use this for initialization
    private void Start()
    {
        walletValue = WalletManager.Instance.Credits;
        //betValue = BetManager.Instance.ChipsToRaise;
    }

    ///// <summary>
    ///// Make the gameobject this script is attached to face the camera.
    ///// call this method in update to make it follow the camera
    ///// </summary>
    //private void FaceCamera()
    //{   // sets the Camera´s forward positioning towards the Camera.Main (main camera is tagged as such)
    //    transform.forward = (Camera.main.transform.position - transform.position).normalized;
    //    // in my case i had to rotate it the other way to make it work
    //    transform.Rotate(0, 180, 0); // delete if redundant - most likely
    //}
    // Update is called once per frame
    private void Update()
    {
        //betValue = BetManager.Instance.BetValue;
        //if (betValue >= 0) // turn off the bet text
        //{
        //}
        //transform.LookAt(Camera.main.transform.position);

        //FaceCamera();

        // -0.12 , 1.45, -0.3

        if (PhotonNetwork.player.ID == 2)
        {
            GameObject.FindGameObjectWithTag("HUDWallet").transform.localPosition = new Vector3(0.625f, 1.43f, 1.083f);
            GameObject.FindGameObjectWithTag("HUDWallet").transform.localRotation = Quaternion.Euler(90, 180, 0);
        }
        //walletValue = WalletManager.Instance.Credits;
        walletValue = WalletManager.Instance.TmpCredits;
        this.wallet.GetComponent<Text>().text = "Wallet: $" + walletValue.ToString();
        //    if (BetManager.Instance != null)
        //    {
        //        this.bet.GetComponent<Text>().text = "Bet: $" + BetManager.Instance.ChipsToRaise.ToString();
        //    }
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}