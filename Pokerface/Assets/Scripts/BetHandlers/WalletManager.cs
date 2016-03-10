using UnityEngine;

public class WalletManager : Manager<WalletManager>
{
    [SerializeField]
    private int credits = 100;

    public int Credits
    {
        get { return credits; }
    }

    public void Deposit(int value)
    {
        credits += value;
    }

    public bool Withdraw(int value)
    {
        if (credits >= value)
        {
            credits -= value;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ReceivePot(int player)
    {
        //Debug.Log("player " + PhotonNetwork.player.ID + " should receive pot");
        Debug.Log("player " + player + " should receive the pot");
        if (player == PhotonNetwork.player.ID)
        {
            Debug.Log("player " + player + " receiving pot");

            GetPot(player);
        }
    }

    public void GetPot(int player)
    {
        var myMoney = PotManager.Instance.TotalPotValue;
        Deposit(myMoney);
        Debug.Log(myMoney + " added to player " + player + "´s wallet");
        PotManager.Instance.GetComponent<PhotonView>().RPC("EndRoundBehaviour", PhotonTargets.All);
        PotManager.Instance.GetComponent<PhotonView>().RPC("EndHandBehaviour", PhotonTargets.All);

        ///måske skal den bare hente ligemeget hvad og så hvis den er = med min spiller så får den den rent faktisk
    }
}