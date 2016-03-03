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
        //betValue = BetManager.Instance.BetValue;
        if (betValue >= 0) // turn off the bet text
        {
        }
        //transform.LookAt(Camera.main.transform.position);
        transform.forward = (Camera.main.transform.position - transform.position).normalized;
        transform.Rotate(0, 180, 0);
        if (PhotonNetwork.player.ID == 2)
        {
            GameObject.FindGameObjectWithTag("HUDWallet").transform.position = new Vector3(1.8f, 1.35f, .4f);
        }
    }

    public void BetUpdate(int betValue, int player)
    {
        if (player == 1)
        {
            var betMan = GameObject.FindGameObjectWithTag("Player1BetController").GetComponent<BetManager>();
            this.betValue = betValue;
            this.bet.GetComponent<Text>().text = "Bet: " + betValue.ToString();
        }
        else if (player == 2)
        {
            var betMan = GameObject.FindGameObjectWithTag("Player2BetController").GetComponent<BetManager>();
            this.betValue = betValue;
            this.bet.GetComponent<Text>().text = "Bet: " + betValue.ToString();
        }
        walletValue = WalletManager.Instance.Credits;
        this.wallet.GetComponent<Text>().text = walletValue.ToString();
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}