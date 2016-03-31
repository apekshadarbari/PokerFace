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

    [SerializeField]
    private HandOver handOverHUDMan;

    // tilføje en måde at se hvis wallet vi har med at gøre ud fra pllayer.

    private void Start()
    {
        handOverHUDMan = GameObject.Find("WinIndicator").GetComponent<HandOver>();

        owningPlayer = PhotonNetwork.player.ID;
        this.tmpCredits = credits; // TODO : make more effeicints
    }

    private void Update()
    {
        owningPlayer = PhotonNetwork.player.ID;
        ChipCheck();
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
        if (player == 1)
        {
            handOverHUDMan.GetComponent<PhotonView>().RPC("ReceiveHandOver", PhotonTargets.All, false, ActionSound.p1Win);
        }
        else if (player == 2)
        {
            handOverHUDMan.GetComponent<PhotonView>().RPC("ReceiveHandOver", PhotonTargets.All, false, ActionSound.p2Win);
        }

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

    public void BuyIn(int player, int buyIn)
    {
        if (player == PhotonNetwork.player.ID)
        {
            Debug.Log("player " + player + " buying back in");
            if (credits <= 0)
            {
                this.credits += buyIn;
                this.tmpCredits += buyIn;
            }
            //GetPot(player);
        }
    }

    private void ChipCheck()
    {
        if (owningPlayer == 1)
        {
            if (tmpCredits < 10)
            {
                GameObject.FindGameObjectWithTag("P1Chip10").GetComponent<MeshCollider>().enabled = false;
            }
            if (tmpCredits < 50)
            {
                GameObject.FindGameObjectWithTag("P1Chip50").GetComponent<MeshCollider>().enabled = false;
            }
            if (tmpCredits < 100)
            {
                GameObject.FindGameObjectWithTag("P1Chip100").GetComponent<MeshCollider>().enabled = false;
            }
            else
            {
                GameObject.FindGameObjectWithTag("P1Chip10").GetComponent<MeshCollider>().enabled = true;
                GameObject.FindGameObjectWithTag("P1Chip50").GetComponent<MeshCollider>().enabled = true;
                GameObject.FindGameObjectWithTag("P1Chip100").GetComponent<MeshCollider>().enabled = true;
            }
        }
        if (owningPlayer == 2)
        {
            if (tmpCredits < 10)
            {
                GameObject.FindGameObjectWithTag("P2Chip10").GetComponent<MeshCollider>().enabled = false;
            }
            if (tmpCredits < 50)
            {
                GameObject.FindGameObjectWithTag("P2Chip50").GetComponent<MeshCollider>().enabled = false;
            }
            if (tmpCredits < 100)
            {
                GameObject.FindGameObjectWithTag("P2Chip100").GetComponent<MeshCollider>().enabled = false;
            }
            else
            {
                GameObject.FindGameObjectWithTag("P2Chip10").GetComponent<MeshCollider>().enabled = true;
                GameObject.FindGameObjectWithTag("P2Chip50").GetComponent<MeshCollider>().enabled = true;
                GameObject.FindGameObjectWithTag("P2Chip100").GetComponent<MeshCollider>().enabled = true;
            }
        }
    }
}