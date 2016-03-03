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

        //transform.LookAt(Camera.main.transform.position);
        transform.forward = (Camera.main.transform.position - transform.position).normalized;
        transform.Rotate(0, 180, 0);

        this.wallet.GetComponent<Text>().text = walletValue.ToString();

        this.bet.GetComponent<Text>().text = "Bet: " + betValue.ToString();
    }
}