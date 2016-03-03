using UnityEngine;

public class TurnManager : PhotonManager<TurnManager>
{
    private GameObject[] btnOne;
    private GameObject[] btnTwo;

    [SerializeField]
    private GameObject roundMan;

    private void Start()
    {
    }

    /// <summary>
    /// se hvad der skal calles
    /// hvad har modspilleren gjort?
    /// </summary>
    public void OnTurnStart(int player)
    {
        BetManager.Instance.OnTurnStart();
        btnOne = GameObject.FindGameObjectsWithTag("PlayerOneButton");
        btnTwo = GameObject.FindGameObjectsWithTag("PlayerTwoButton");

        //set players buttons to active..
        //other players buttons should be inactive
        switch (player)
        {
            case 1:
                foreach (var btn in btnOne) // meshcolliders skal nok af og de skal ha rigid bods
                {
                    btn.GetComponent<MeshRenderer>().enabled = true;
                    btn.GetComponent<MeshCollider>().enabled = true;

                    //btn.GetComponent<SphereCollider>().enabled = true;
                }
                foreach (var btn in btnTwo)
                {
                    btn.GetComponent<MeshRenderer>().enabled = false;
                    btn.GetComponent<MeshCollider>().enabled = false;

                    //btn.GetComponent<SphereCollider>().enabled = false;
                }
                break;

            case 2:
                foreach (var btn in btnOne)
                {
                    btn.GetComponent<MeshRenderer>().enabled = false;
                    btn.GetComponent<MeshCollider>().enabled = false;

                    //btn.GetComponent<SphereCollider>().enabled = false;
                }
                foreach (var btn in btnTwo)
                {
                    btn.GetComponent<MeshRenderer>().enabled = true;
                    btn.GetComponent<MeshCollider>().enabled = true;

                    //btn.GetComponent<SphereCollider>().enabled = true;
                }

                break;

            default:
                break;
        }
    }

    /// <summary>
    ///hvad gør spilleren?
    ///send data til roundmanager så infoen kan bruges i næste playerturn
    /// </summary>
    public void OnTurnEnd(int player, bool wantsNextRound)
    {
        //if it was player one´s turn
        if (player == 1)
        {
            Debug.Log("player 1 transferring control to 2");

            //transfor ownership to the other player
            roundMan.GetComponent<PhotonView>().RPC("TurnChange", PhotonTargets.AllBuffered, player, wantsNextRound, 2);
        }
        //if it was player two´s turn
        else if (player == 2)
        {
            Debug.Log("player 2 transferring control to 1");
            roundMan.GetComponent<PhotonView>().RPC("TurnChange", PhotonTargets.AllBuffered, player, wantsNextRound, 1);
        }
    }
}