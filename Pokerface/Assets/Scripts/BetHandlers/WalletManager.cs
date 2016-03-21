using UnityEngine;

public class WalletManager : Manager<WalletManager>
{
    [SerializeField]
    private int credits = 100;

    [SerializeField]
    private int tmpCredits;

    [SerializeField]
    private ChipsDisplay p1ChipDisplay;

    [SerializeField]
    private ChipsDisplay p2ChipDisplay;

    public int Credits
    {
        get { return credits; }
    }

    public int TmpCredits
    {
        get { return tmpCredits; }
        set { tmpCredits = value; }
    }

    [SerializeField]
    private int owningPlayer;

    // tilføje en måde at se hvis wallet vi har med at gøre ud fra pllayer.

    private void Start()
    {
        owningPlayer = PhotonNetwork.player.ID;
        this.tmpCredits = credits; // TODO : make more effeicints
    }

    private void Update()
    {
        owningPlayer = PhotonNetwork.player.ID;
        OwnerCheck(); //TODO: make this more efficient
    }
    public void Deposit(int value)
    {
        credits += value;
        OwnerCheck();
    }

    public bool Withdraw(int value)
    {
        if (credits >= value)
        {
            credits -= value;
            OwnerCheck();

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TemporaryWithdraw(int value)
    {
        if (tmpCredits >= value)
        {
            tmpCredits -= value;
            OwnerCheck();

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

    private void OwnerCheck()
    {
        if (owningPlayer == 1)
        {
            p1ChipDisplay.Value = tmpCredits;
            p1ChipDisplay.UpdateStacks();
        }
        else if (owningPlayer == 2)
        {
            p2ChipDisplay.Value = tmpCredits;
            p2ChipDisplay.UpdateStacks();
        }
    }
}