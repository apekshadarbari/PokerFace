using UnityEngine;

internal enum BetAction
{
    IncreaseBet,
    DecreaseBet,
    CommitBet,
    CallOrCheck,
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

        if (this.photonView.ownerId == 1)
        {
            betMan = GameObject.FindGameObjectWithTag("Player1BetController").GetComponent<BetManager>();
        }
        if (this.photonView.ownerId == 2)
        {
            betMan = GameObject.FindGameObjectWithTag("Player2BetController").GetComponent<BetManager>();
        }

        //TODO: move all of these into another script called betcontroller
        //Debug.Log("this button belongs to player: " + this.photonView.ownerId);s
        if (PhotonNetwork.player.ID == this.photonView.ownerId)
        {
            switch (action)
            {
                case BetAction.IncreaseBet:
                    //Adding chips to raise
                    betMan.IncreaseBet();
                    break;

                case BetAction.DecreaseBet:
                    //Reducing chips to raises
                    betMan.DecreaseBet();
                    break;

                case BetAction.CommitBet:
                    //accepting the bet size and commiting it
                    //betMan.Bet();
                    break;

                case BetAction.CallOrCheck:
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
        if (PhotonNetwork.player.ID == this.photonView.ownerId)
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
        if (PhotonNetwork.player.ID == this.photonView.ownerId)
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
        }
        else
        {
        }
    }
}