using UnityEngine;
using System.Collections;
using System;

public class BetMore : Photon.MonoBehaviour, IClicker
{

    //TODO: Enumeration, private fields! pascal vs camel casing

    //the choice of fold, call , raise etc.
    [SerializeField]
    private int choice;

    BetManager betMan;
    

    void Start()
    {

        ////reset of the values (starting values)
        //chipsToIncrement = 5;
        //chipsToRaise = 0;
        ////amountToCall = 0;
        ////find the pot
        //pot = GameObject.Find("pot").GetComponent<PotManager>();

        if (this.photonView.ownerId == 1)
        {
            betMan = GameObject.FindGameObjectWithTag("Player1BetController").GetComponent<BetManager>();
        }
        if (this.photonView.ownerId == 2)
        {
            betMan = GameObject.FindGameObjectWithTag("Player2BetController").GetComponent<BetManager>();

        }
    }


    /// <summary>
    /// onclick function for each of the buttons that have to do with betting
    /// </summary>
    public void OnClick()
    {
        //TODO: move all of these into another script called betcontroller
        Debug.Log("this button belongs to player: " + this.photonView.ownerId);
        switch (choice)
        {
            case 1:
                //Adding chips to raise
                betMan.AddChips();
                break;
            case 2:
                //Reducing chips to raise
                betMan.RemoveChips();
                break;
            case 3:
                //Raising 
                betMan.RaiseChips();
                break;
            case 4:
                //Calling the last value
                betMan.CallCheck();
                break;
            case 5:
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
    }

    /// <summary>
    /// change the color back
    /// </summary>
	public void OnExitHover()
    {
        GetComponent<Renderer>().material.color = Color.clear;
        CrosshairTimerDisplay.Instance.Show();
    }
}