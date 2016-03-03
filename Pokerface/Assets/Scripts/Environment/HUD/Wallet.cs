using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : Photon.MonoBehaviour
{
    [SerializeField]
    private Text wallet;

    [SerializeField]
    private Text bet;

    private int walletValue;
    private int betValue;
    // Use this for initialization
    private void Start()
    {
        walletValue = WalletManager.Instance.Credits;
    }

    // Update is called once per frame
    private void Update()
    {
        walletValue = WalletManager.Instance.Credits;
        //betValue = BetManager.Instance.BetValue;
        if (betValue >= 0) // turn off the bet text
        {
        }
        //transform.LookAt(Camera.main.transform.position);
        transform.forward = (Camera.main.transform.position - transform.position).normalized;
        transform.Rotate(0, 180, 0);

        if (PhotonNetwork.player.ID == 1)
        {
            var betMan = GameObject.FindGameObjectWithTag("Player1BetController").GetComponent<BetManager>();
            betValue = betMan.ChipsToRaise;
        }
        else if (PhotonNetwork.player.ID == 2)
        {
            GameObject.FindGameObjectWithTag("HUDWallet").transform.position = new Vector3(-0.33f, 1.361f, 1.665f);
            var betMan = GameObject.FindGameObjectWithTag("Player2BetController").GetComponent<BetManager>();
            betValue = betMan.ChipsToRaise;
        }

        this.wallet.GetComponent<Text>().text = walletValue.ToString();

        this.bet.GetComponent<Text>().text = "Bet: " + betValue.ToString();
    }
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}