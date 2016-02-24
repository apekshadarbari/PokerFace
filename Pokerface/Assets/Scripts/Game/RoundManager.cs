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

    // den der har turnswitchen har turen..
    /// <summary>
    /// who has the action?
    /// </summary>
    /// <param name="player"></param>
    [PunRPC]
    private void TurnChange(int receivingPlayer)
    {
        turnIndicater.GetComponent<PhotonView>().TransferOwnership(receivingPlayer);
        GetComponent<PhotonView>().RPC("CurrentPlayerTurn", PhotonTargets.All, receivingPlayer);
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
            turnIndicater.transform.position = new Vector3(2.8f, 1.35f, 0f);
        }
        else if (player == 2)
        {
            turnIndicater.transform.position = new Vector3(-2.75f, 1.35f, 0f);
        }
        TurnManager.Instance.OnTurnStart(player);
    }

    /// <summary>
    /// når begge spillere har checket, raised + re-raised, eller callet et raise
    /// - all in: skip through the next steps
    /// </summary>
    [PunRPC]
    private void RoundEnd(int round)
    {
        this.round = round;
        Debug.Log(round);
        //next round
        RoundStart(round);
    }
    /// <summary>
    /// når der skal gives nye kort til community, flop, turn, river
    /// spillernes tur cyklus restartes og alle individuelle pots lægges i den samlede
    /// </summary>
    [PunRPC]
    private void RoundStart(int round)
    {
        if (PhotonNetwork.isMasterClient)
        {
            // call flop etc
            switch (round)
            {
                case 0: // handStarted
                    cardMan.Shuffle();
                    cardMan.Deal();
                    break;

                case 1: // The Flop
                    cardMan.DealFlop();
                    break;

                case 2: // the Turn
                    cardMan.DealTurn();
                    break;

                case 3: // the River
                    cardMan.DealRiver();
                    break;

                case 4: // the End Comparison - who wins?
                    cardMan.CompareCards();
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
    private void HandEnd(bool fold)
    {
        //cardMan.CompareCards();
        //if (!fold)
        //{
        //    //next round
        //    RoundStart();
        //    //need the current enum so we can send on the next enum
        //}
        //else if (fold)
        //{
        //    //give whoever didnt fold the pot and remove all cards in the game
        //}
        //
    }

    /// <summary>
    /// Nye kort uddeles til spillerne
    /// </summary>
    [PunRPC]
    private void HandStart()
    {
        //if this is the first round
        //TurnIndicater - TennisBall - created
        PhotonNetwork.Instantiate(turnIndicater.name, turnIndicater.transform.position, Quaternion.identity, 0);

        //we shuffle and deal to starting cards

        playerTurn = turnIndicater.GetComponent<PhotonView>().ownerId;
        //Debug.Log(playerTurn);
        CurrentPlayerTurn(playerTurn);
        RoundStart(0);
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