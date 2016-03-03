using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
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
        betValue = BetManager.Instance.ChipsToRaise;
        if (betValue >= 0) // turn off the bet text
        {
        }
        //transform.LookAt(Camera.main.transform.position);
        transform.forward = (Camera.main.transform.position - transform.position).normalized;
        transform.Rotate(0, 180, 0);

        this.wallet.GetComponent<Text>().text = walletValue.ToString();

        this.bet.GetComponent<Text>().text = "Bet: " + betValue.ToString();
        if (PhotonNetwork.player.ID == 2)
        {
            GameObject.FindGameObjectWithTag("HUDWallet").transform.position = new Vector3(-0.33f, 1.361f, 1.665f);
        }
    }
}