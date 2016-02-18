using UnityEngine;
using System.Collections;
using System;

public class BetMore : Photon.MonoBehaviour, IClicker
{
    enum BetAction
    {
        Add,
        Remove,
        Raise,
        Call,
        Fold
    }

    //TODO: Enumeration, private fields! pascal vs camel casing

    //the choice of fold, call , raise etc.
    [SerializeField]
    //private int choice;
    private BetAction action;

    BetManager betMan;

    AudioSource audioSrc;

    [SerializeField, Header("Audio - Hover -")]
    AudioClip hoverSound;//they all need hover...
    [SerializeField]
    AudioClip buttonPressed;

    // we can add them all here.. and then play them when needed... but then we add them a bunch of times.. all clicks for each buttonn..
    // we can add them to betmanager and then send the clip back here or at least an enum that tells it what to play..
    // we can create a method that takes an enum and then plays that sound or nothing...

    void Start()
    {
        audioSrc = this.GetComponent<AudioSource>();
        ////reset of the values (starting values)
        //chipsToIncrement = 5;
        //chipsToRaise = 0;s
        ////amountToCall = 0;
        ////find the pot

    }


    /// <summary>
    /// onclick function for each of the buttons that have to do with betting
    /// </summary>
    public void OnClick()
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
        Debug.Log("this button belongs to player: " + this.photonView.ownerId);
        switch (action)
        {
            case BetAction.Add:
                //Adding chips to raise
                betMan.AddChips();
                break;
            case BetAction.Remove:
                //Reducing chips to raise
                betMan.RemoveChips();
                break;
            case BetAction.Raise:
                //Raising 
                betMan.RaiseChips();
                break;
            case BetAction.Call:
                //Calling the last value
                betMan.CallCheck();
                break;
            case BetAction.Fold:
                //Folding cards
                betMan.Fold();
                break;
        }
    }

    /// <summary>
    /// change the color to see your looking at the button
    /// </summary>
	public void OnHover()
    {
        GetComponent<Renderer>().material.color = Color.red;
        CrosshairTimerDisplay.Instance.Show();
        audioSrc.clip = hoverSound; // change the
        audioSrc.Play();
    }

    //void HoverSound()
    //{
    //    audioSrc.PlayOneShot(hoverSound, 1f);

    //}

    /// <summary>
    /// change the color back
    /// </summary>
	public void OnExitHover()
    {
        GetComponent<Renderer>().material.color = Color.clear;
        CrosshairTimerDisplay.Instance.Show();
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
        }
        else
        {
        }
    }
}