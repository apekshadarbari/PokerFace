using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pot : Photon.MonoBehaviour
{
    [SerializeField]
    private Text pot;

    [SerializeField]
    private Text call;

    private int potValue;
    private int callValue;

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

        this.pot.GetComponent<Text>().text = "Pot: " + potValue.ToString();

        if (PhotonNetwork.player.ID == 1)
        {
            callValue = PotManager.Instance.Player2pot;
        }
        else if (PhotonNetwork.player.ID == 2)
        {
            callValue = PotManager.Instance.Player1pot;
        }
        this.call.GetComponent<Text>().text = "Call: " + callValue.ToString();
    }
}