using UnityEngine;
using UnityEngine.UI;

public class Pot : Manager<Pot>
{
    [SerializeField]
    private Text pot;

    [SerializeField]
    private Text call;

    [SerializeField]
    private Text bet;

    private int potValue;
    private int callValue;
    private int betValue;

    // Use this for initialization
    private void Start()
    {
        potValue = PotManager.Instance.TotalPotValue;
    }

    // Update is called once per frame
    private void Update()
    {
        potValue = PotManager.Instance.TotalPotValue;
        //transform.LookAt(Camera.main.transform.position);
        transform.forward = (Camera.main.transform.position - transform.position).normalized;
        transform.Rotate(0, 180, 0);

        if (potValue == 0)
        {
            this.pot.GetComponent<Text>().text = "";
        }
        else
        {
            this.pot.GetComponent<Text>().text = "Pot: $" + potValue.ToString();
        }
        if (callValue == 0)
        {
            this.call.GetComponent<Text>().text = "";
        }
        else
        {
            this.call.GetComponent<Text>().text = "Call: $" + callValue.ToString();
        }
        //this.bet.GetComponent<Text>().text = "Your Bet: " + betValue.ToString();
    }

    public void HUDCallValue()
    {
        if (PhotonNetwork.player.ID == 1)
        {
            callValue = PotManager.Instance.GetCallValue(1);
            //callValue = PotManager.Instance.Player2pot;
            //betValue = PotManager.Instance.Player1pot;
        }
        else if (PhotonNetwork.player.ID == 2)
        {
            callValue = PotManager.Instance.GetCallValue(2);
            //callValue = PotManager.Instance.Player1pot;
            //betValue = PotManager.Instance.Player2pot;
        }
    }
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}