using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WordManager : Photon.MonoBehaviour
{
    [SerializeField]
    private Text pot;

    [SerializeField]
    private Text walletValuePlayerOne;

    [SerializeField]
    private Text walletValuePlayerTwo;

    [SerializeField]
    private Text betPlayerOne;

    [SerializeField]
    private Text betPlayerTwo;

    //int PotManager.InstanceMoney;
    //int walletMoney01;
    //int walletMoney02;
    //int betMoney01;
    //int betMoney02;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    [PunRPC]
    private void TextPot(int pot)
    {
        this.pot.GetComponent<Text>().text = "Current PotManager.Instance: " + pot.ToString();
    }
    [PunRPC]
    private void TextAmountToCall(int player, int amountToCall)
    {
        if (player == 1)
        {
            this.betPlayerOne.GetComponent<Text>().text = "Current Bet: " + amountToCall.ToString();
        }
        else if (player == 2)
        {
            this.betPlayerTwo.GetComponent<Text>().text = "Current Bet: " + amountToCall.ToString();
        }
    }

    [PunRPC]
    private void TextWallet(int player, int walletValue)
    {
        if (player == 1)
        {
            this.walletValuePlayerOne.GetComponent<Text>().text = "Chip Value: " + walletValue.ToString();
        }
        else if (player == 2)
        {
            this.walletValuePlayerTwo.GetComponent<Text>().text = "Chip Value: " + walletValue.ToString();
        }
    }
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
        }
        else
        {
        }
    }
}