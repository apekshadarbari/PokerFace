using System.Collections;
using UnityEngine;

public enum Round
{
    HandStart,
    Flop,
    Turn,
    River,
    HandEnd,
}

public class RoundManager : Photon.MonoBehaviour
{
    //turnindicator has cardmanager in it
    [SerializeField]
    private GameObject turnIndicater;

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
        CurrentPlayerTurn(player);
        GetComponent<PhotonView>().RPC("CurrentPlayerTurn", PhotonTargets.All, receivingPlayer);

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
            RoundEnd(1);
        }
        //ConfirmHUD.Instance.HudToggle(player);
    }

    [PunRPC]
    private void CurrentPlayerTurn(int player)
    {
        if (player == 0)
        {
            player = 1;
        }
        if (player == 1)
        {
            turnIndicater.transform.position = new Vector3(.78f, 1.18f, -.42f);
        }
        else if (player == 2)
        {
            turnIndicater.transform.position = new Vector3(-.175f, 1.179f, 1.29f);
        }
        ConfirmHUD.Instance.HudToggle(player);
        TurnManager.Instance.OnTurnStart(player);
        //turnIndicater.GetComponent<PhotonView>().TransferOwnership(player);
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

        if (PhotonNetwork.isMasterClient)
        {
            // call flop etc
            switch (round)
            {
                case 0: // handStarted
                    cardMan.Shuffle();
                    cardMan.Deal();
                    Debug.Log("Cards shuffled and dealt");
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
    private void HandEnd(int player, bool fold)
    {
        if (!fold)
        {
            cardMan.CompareCards();
            //next round
            RoundStart(0);
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
            //cardMan.CompareCards();//compares the cards

            //give whoever didnt fold the pot and remove all cards in the game
            gameObject.GetComponent<PhotonView>().RPC("RemoveCard", PhotonTargets.AllBuffered);
            RoundStart(0);
        }
        //PotManager.Instance
        round = 0;
    }

    /// <summary>
    /// Nye kort uddeles til spillerne
    /// </summary>
    [PunRPC]
    private void HandStart()
    {
        //if this is the first round
        //TurnIndicater - TennisBall - created
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Instantiate(turnIndicater.name, turnIndicater.transform.position, Quaternion.identity, 0);
        }

        //we shuffle and deal to starting cards

        playerTurn = turnIndicater.GetComponent<PhotonView>().ownerId;
        //Debug.Log(playerTurn);
        CurrentPlayerTurn(playerTurn);
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