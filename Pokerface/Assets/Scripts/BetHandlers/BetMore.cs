using UnityEngine;

internal enum BetAction
{
    IncreaseAllInn,
    Increase10, Increase50, Increase100,
    ResetBet,
    Commit,
    Fold
}

public class BetMore : Photon.MonoBehaviour, IClicker
{
    //TODO: Enumeration, private fields! pascal vs camel casing

    //the choice of fold, call , raise etc.
    [SerializeField]
    private BetAction action;

    private BetManager betMan;

    private AudioSource audioSrc;

    [SerializeField, Header("Audio - Hover -")]
    private AudioClip hoverSound;//they all need hover...

    [SerializeField]
    private AudioClip buttonPressed;

    // we can add them all here.. and then play them when needed... but then we add them a bunch of times.. all clicks for each buttonn..
    // we can add them to betmanager and then send the clip back here or at least an enum that tells it what to play..
    // we can create a method that takes an enum and then plays that sound or nothing...

    private void Start()
    {
        audioSrc = this.GetComponent<AudioSource>();
        ////reset of the values (starting values)
        //chipsToIncrement = 5;
        //chipsToRaise = 0;s
        ////amountToCall = 0;
        ////find the PotManager.Instance
    }

    /// <summary>
    /// onclick function for each of the buttons that have to do with betting
    /// </summary>
    public void EndTurn()
    {
        // TODO : do enable instead of deactivate as it cuts off the sound of the click
        audioSrc.clip = buttonPressed;
        audioSrc.Play();

        //Debug.Log(RoundManager.Instance.PlayerTurn);
        //if (this.photonView.ownerId == 1)
        //{
        //    var btns = GameObject.FindGameObjectWithTag("Player1BetController");

        //    //betMan = GameObject.FindGameObjectWithTag("Player1BetController").GetComponent<BetManager>();
        //}
        //if (this.photonView.ownerId == 2)
        //{
        //    betMan = GameObject.FindGameObjectWithTag("Player2BetController").GetComponent<BetManager>();
        //}

        //TODO: move all of these into another script called betcontroller
        //Debug.Log("this button belongs to player: " + this.photonView.ownerId);s
        if (PhotonNetwork.player.ID == this.photonView.ownerId && RoundManager.Instance.PlayerTurn == PhotonNetwork.player.ID || PhotonNetwork.player.ID == RoundManager.Instance.PlayerTurn)
        {
            betMan = BetManager.Instance;
            BetManager.Instance.BetvalueUpdate();
            switch (action)
            {
                case BetAction.IncreaseAllInn:
                    //Adding chips to raise
                    betMan.IncreaseBet(500);
                    break;

                case BetAction.Increase10:
                    //Adding chips to raise
                    betMan.IncreaseBet(10);
                    break;

                case BetAction.Increase50:
                    //Adding chips to raise
                    betMan.IncreaseBet(50);
                    break;

                case BetAction.Increase100:
                    //Adding chips to raise
                    betMan.IncreaseBet(100);
                    break;

                case BetAction.ResetBet:
                    //Reducing chips to raises
                    betMan.DecreaseBet();
                    break;

                case BetAction.Commit:
                    //Calling the last value
                    //Debug.Log("hej");
                    /*  betMan.SetBetToCallValue();*///setss the bet to what it needs to be to call;
                    betMan.Bet();
                    break;

                case BetAction.Fold:
                    //Folding cards
                    betMan.Fold();
                    break;
            }
            //OnExitHover();
        }
        //else if (!this.photonView.isMine)
        //{
        //    throw new NotImplementedException("not yours");
        //}
    }

    /// <summary>
    /// change the color to see your looking at the button
    /// </summary>
	public void OnHover()
    {
        if (PhotonNetwork.player.ID == this.photonView.ownerId || PhotonNetwork.player.ID == RoundManager.Instance.PlayerTurn)
        {
            // GetComponent<Renderer>().material.color = new Color(.5f, 0f, 0f, 0.3f);
            GetComponent<Renderer>().material.color = Color.white;
            CrosshairTimerDisplay.Instance.Show();
            audioSrc.clip = hoverSound;
            audioSrc.Play();
        }
        //else if (!this.photonView.isMine)
        //{
        //    throw new NotImplementedException("not yours");
        //}
    }

    /// <summary>
    /// change the color back
    /// </summary>
    public void OnExitHover()
    {
        if (PhotonNetwork.player.ID == this.photonView.ownerId || PhotonNetwork.player.ID == RoundManager.Instance.PlayerTurn)
        {
            // GetComponent<Renderer>().material.color = new Color(0f, .5f, 0f, 0.3f);
            GetComponent<Renderer>().material.color = Color.grey;
            CrosshairTimerDisplay.Instance.Show();
        }
        //else if (!this.photonView.isMine)
        //{
        //    throw new NotImplementedException("not yours");
        //}
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(gameObject.transform.position);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
        }
    }
}