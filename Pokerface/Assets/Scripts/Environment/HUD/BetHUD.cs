using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BetHUD : Manager<BetHUD>
{
    [SerializeField]
    private Text bet;

    private int betValue;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //p1 pos -.127f,1.3f,0.163f
        if (PhotonNetwork.player.ID == 2)
        {
            GameObject.FindGameObjectWithTag("HUDBet").transform.localPosition = new Vector3(-0.3f, 1.3f, 1.554f); ;
        }
        if (betValue == 0)
        {
            gameObject.GetComponent<Canvas>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Canvas>().enabled = true;
        }
    }
    public void BetUpdate(int betValue, int player)
    {
        if (player == 1)
        {
            //var betMan = GameObject.FindGameObjectWithTag("Player1BetController").GetComponent<BetManager>();
            this.betValue = betValue;
            this.bet.GetComponent<Text>().text = "Bet: $" + betValue.ToString();
            //chipDisplay.UpdateStacks();
        }
        else if (player == 2)
        {
            //var betMan = GameObject.FindGameObjectWithTag("Player2BetController").GetComponent<BetManager>();
            this.betValue = betValue;
            this.bet.GetComponent<Text>().text = "Bet: $" + betValue.ToString();
        }
    }
}