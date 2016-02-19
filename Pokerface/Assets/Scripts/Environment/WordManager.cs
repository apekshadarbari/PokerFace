using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class WordManager : Photon.MonoBehaviour
{
    [SerializeField]
    Text potValue;

    [SerializeField]
    Text walletValuePlayerOne;

    [SerializeField]
    Text walletValuePlayerTwo;

    [SerializeField]
    Text betPlayerOne;

    [SerializeField]
    Text betPlayerTwo;


    //int potMoney;
    //int walletMoney01;
    //int walletMoney02;
    //int betMoney01;
    //int betMoney02;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [PunRPC]
    void TextPot(int potValue)
    {
        this.potValue.GetComponent<Text>().text = "Current Pot: " + potValue.ToString();

    }
    [PunRPC]
    void TextAmountToCall(int player, int amountToCall)
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
    void TextWallet(int player, int walletValue)
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
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
        }
        else
        {
        }
    }
}