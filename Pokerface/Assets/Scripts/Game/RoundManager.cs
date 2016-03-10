using UnityEngine;

public enum Round
{
    HandStart,
    Flop,
    Turn,
    River,
    HandEnd,
}

public class RoundManager : PhotonManager<RoundManager>
{
    //turnindicator has cardmanager in it
    //[SerializeField]
    //private GameObject turnIndicater;

    [SerializeField]
    private TurnManager turnMan;

    [SerializeField]
    private CardManager cardMan;

    private int playerTurn;
    private int player;

    private int round;

    [SerializeField]
    private bool playerOneWantsNextRound;

    [SerializeField]
    private bool playerTwoWantsNextRound;

    // den der har turnswitchen har turen..
    /// <summary>
    /// who has the action?
    /// </summary>
    /// <param name="player"></param>
    [PunRPC]
    private void TurnChange(int player, bool wantsNextRound, int receivingPlayer)
    {
        //CurrentPlayerTurn(player);
        GetComponent<PhotonView>().RPC("CurrentPlayerTurn", PhotonTargets.All, receivingPlayer);
        //Debug.Log("CALLED CurrentPlayerTurn");

        if (player == 1 && wantsNextRound)
        {
            playerOneWantsNextRound = true;
        }
        if (player == 2 && wantsNextRound)
        {
            playerTwoWantsNextRound = true;
        }
        if (player == 1 && !wantsNextRound || player == 2 && !wantsNextRound)
        {
            playerOneWantsNextRound = false;
            playerTwoWantsNextRound = false;
        }
        //if we receive true from both players we can increment the round by 1
        if (playerOneWantsNextRound && playerTwoWantsNextRound)
        {
            PotManager.Instance.DumpIfEqual();
            //gameObject.GetComponent<PhotonView>().RPC("RoundEnd", PhotonTargets.AllBuffered, 1);
            RoundEnd(1);
        }
        //Debug.Log("TurnChange DONE");
        //BetManager.Instance.ResetBet();
        //ConfirmHUD.Instance.HudToggle(player);
    }

    [PunRPC]
    private void CurrentPlayerTurn(int player)
    {
        //if (player == 0) // shouldnt be necessary
        //{
        //    player = 1;
        //}
        //Wallet.Instance.BetUpdate(0, player);

        //BetManager.Instance.BetvalueUpdate();// update the betvalue - reset it
        this.playerTurn = player;

        TurnIndicator.Instance.TurnIndication(player); // send the turntrigger to current  player

        ConfirmHUD.Instance.HudToggle(player);
        TurnManager.Instance.OnTurnStart(player);
        //turnIndicater.GetComponent<PhotonView>().TransferOwnership(player);
        Debug.Log("CurrentPlayerTurn: " + player);
    }

    /// <summary>
    /// når begge spillere har checket, raised + re-raised, eller callet et raise
    /// - all in: skip through the next steps
    /// </summary>
    [PunRPC]
    private void RoundEnd(int round)
    {
        this.round += round;
        Debug.Log("Round Number: " + this.round);
        //next round
        //gameObject.GetComponent<PhotonView>().RPC("RoundStart", PhotonTargets.AllBuffered, this.round);
        RoundStart(this.round);
    }

    /// <summary>
    /// når der skal gives nye kort til community, flop, turn, river
    /// spillernes tur cyklus restartes og alle individuelle pots lægges i den samlede
    /// </summary>
    [PunRPC]
    private void RoundStart(int round)
    {
        PotManager.Instance.DumpIfEqual();
        playerOneWantsNextRound = false;
        playerTwoWantsNextRound = false;

        /* BetManager.Instance.ResetBet(); */// reset the betvalues

        if (PhotonNetwork.isMasterClient)
        //if (PhotonNetwork.player.ID == playerTurn)
        {
            //Debug.Log(PhotonNetwork.player.ID + " " + playerTurn);
            // call flop etc
            switch (round)
            {
                case 0: // handStarted
                    cardMan.Shuffle();
                    cardMan.Deal();
                    //Debug.Log("Cards shuffled and dealt");
                    break;

                case 1: // The Flop
                    cardMan.DealFlop();
                    Debug.Log("Flop Dealt");
                    break;

                case 2: // the Turn
                    cardMan.DealTurn();
                    Debug.Log("Turn Dealt");
                    break;

                case 3: // the River
                    cardMan.DealRiver();
                    Debug.Log("River Dealt");
                    break;

                case 4: // the End Comparison - who wins
                    Debug.Log("Hand over finding winner...");
                    //gameObject.GetComponent<PhotonView>().RPC("HandEnd", PhotonTargets.AllBuffered, 0, false);
                    HandEnd(0, false);
                    break;

                default:
                    break;
            }
        }
    }

    /// <summary>
    /// hånden er færdig og kortene fjernes.
    /// en spiller vinder ved:
    /// en spiller folder, eller én spiller har en bedre hånd og vinder
    /// Potten gives til vinderen
    /// </summary>
    [PunRPC]
    public void HandEnd(int player, bool fold)
    {
        if (!fold)
        {
            cardMan.CompareCards();
            //next round
            gameObject.GetComponent<PhotonView>().RPC("RemoveCard", PhotonTargets.AllBuffered);
            //need the current enum so we can send on the next enum
        }
        else if (fold)
        {
            if (player == 1)
            {
                WalletManager.Instance.ReceivePot(2);
            }
            else if (player == 2)
            {
                WalletManager.Instance.ReceivePot(1);
            }

            Debug.Log(player + " Folded");
            //cardMan.CompareCards();//compares the cards

            //give whoever didnt fold the pot and remove all cards in the game
            gameObject.GetComponent<PhotonView>().RPC("RemoveCard", PhotonTargets.AllBuffered);
        }
        gameObject.GetComponent<PhotonView>().RPC("HandStart", PhotonTargets.AllBuffered);
        //HandStart();

        //PotManager.Instance
        //round = 0;
    }

    /// <summary>
    /// Nye kort uddeles til spillerne
    /// </summary>
    [PunRPC]
    private void HandStart()
    {
        //if this is the first round
        //Debug.Log(playerTurn);
        gameObject.GetComponent<PhotonView>().RPC("CurrentPlayerTurn", PhotonTargets.AllBuffered, 1);//TODO MAKE THIS DYNAMIC (hand 2 player two is the dealer and should start the game etc.)
        //CurrentPlayerTurn(1);
        this.round = 0;//set the round back to 0
        //gameObject.GetComponent<PhotonView>().RPC("RoundStart", PhotonTargets.AllBuffered, 0);
        RoundStart(0);
    }

    [PunRPC]
    private void RemoveCard()
    {
        //var fold = GameObject.FindGameObjectsWithTag("CardSlot");
        var holders = GameObject.FindGameObjectsWithTag("CardHolder");

        foreach (var slot in holders)
        {
            slot.GetComponent<CardHolder>().RemoveAllCards();

            //foreach (Transform c in s.transform)
            //{
            //    GameObject.Destroy(c.gameObject);
            //}
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