using UnityEngine;
using System.Collections;
using System;

public class BetMore : Photon.MonoBehaviour, IClicker
{

    //TODO: Enumeration, private fields! pascal vs camel casing

    //the choice of fold, call , raise etc.
    [SerializeField]
    private int choice;
    //the amount that needs to be called to keep playing
    [SerializeField]//so we can see it in the inspector
    private int amountToCall;
    //how many chips are added each time the + button is used
    private int chipsToIncrement;
    //how many chips are we betting
    private int chipsToBet;
    //the value the player wants to raise
    [SerializeField]
    private int chipsToRaise;
    //an instance of the turnswitch scripts
    TurnSwitch ts;
    //an instance of the pot
    PotManager pot;
    //the players wallet I.E. chipvalue
    [SerializeField]
    GameObject wallet;
    
    //lets us get and set the amount to call
    public int AmountToCall
    {
        get
        {
            return amountToCall;
        }

        set
        {
            amountToCall = value;
        }
    }

    public int ChipsToRaise
    {
        get
        {
            return chipsToRaise;
        }

        set
        {
            chipsToRaise = value;
        }
    }

    void Start()
    {
        //reset of the values (starting values)
        chipsToIncrement = 5;
        chipsToRaise = 0;
        AmountToCall = 0;
        //find the pot
        pot = GameObject.Find("pot").GetComponent<PotManager>();

        //if (this.photonView.ownerId == 1)
        //{
        //	this.photonView.transform.position = new Vector3(-1.588f, 0.34f, 1.51f);
        //}
        //if (this.photonView.ownerId == 2)
        //{
        //	this.photonView.transform.position = new Vector3(-2f, -0.75f, 3f);
        //}
    }

    /// <summary>
    /// add chips (plus button)
    /// </summary>
    public void AddChips()
    {
        chipsToRaise = chipsToRaise + chipsToIncrement;
        Debug.Log("add chips detected : " + chipsToRaise);
    }

    /// <summary>
    /// remove chips - (minus button)
    /// </summary>
    public void RemoveChips()
    {
        Debug.Log("remove chips detected ");
        if (chipsToRaise <= 0)
        {
            chipsToRaise = 0;
        }
        else chipsToRaise = chipsToRaise - chipsToIncrement;
    }

    /// <summary>
    /// onclick function for each of the buttons that have to do with betting
    /// </summary>
    public void OnClick()
    {
        ts = GameObject.FindGameObjectWithTag("TurnTrigger").GetComponent<TurnSwitch>();
        switch (choice)
        {
            case 1:
                //Adding chips to raise
                AddChips();
                break;
            case 2:
                //Reducing chips to raise
                RemoveChips();
                break;
            case 3:
                //Raising 
                RaiseChips();
                break;
            case 4:
                //Calling the last value
                CallCheck();
                break;
            case 5:
                //Folding cards
                Fold();
                break;
        }
    }

    /// <summary>
    /// call the current bet
    /// </summary>
	public void CallCheck()
    {
        Debug.Log("player " + this.photonView.ownerId + " checks/calls");

        chipsToBet = wallet.GetComponent<WalletManager>().GetChips(this.photonView.ownerId, AmountToCall);
        pot.AddChips(this.photonView.ownerId, chipsToBet);
        //	ts.GetComponent<TurnSwitch> ().potComparison(amt_to_call);
        AmountToCall = 0;
        ts.GetComponent<TurnSwitch>().OnClick();

        //if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
        //{
        //	return;
        //}
        //else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
        //{
        //	TurnSwitch.OnClick();
        //	return;
        //}
    }

    /// <summary>
    /// fold
    /// </summary>
	public void Fold()
    {
        if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
        {
            Debug.Log("player " + this.photonView.ownerId + " folds");
            WalletManager.player1ChipValue = pot.GetComponent<PotManager>().chipValue;
            pot.GetComponent<PotManager>().chipValue = 0;
            // needs more logic here to end game
            return;
        }
        else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
        {
            Debug.Log("player " + this.photonView.ownerId + " folds");
            //wallet.GetComponent<WalletManager> ().player2ChipValue = pot.GetComponent<PotManager>().chipValue;
            WalletManager.player2ChipValue = pot.chipValue;
            pot.GetComponent<PotManager>().chipValue = 0;
            // needs more logic here to end game
            return;
        }
    }

    /// <summary>
    /// raise button clicked
    /// </summary>
    public void RaiseChips()
    {
        //the chips we want to bet is set to the raised chips an used with the current user(owner - player ´s id)
        chipsToBet = wallet.GetComponent<WalletManager>().GetChips(this.photonView.ownerId, ChipsToRaise);
        //add the chips to the pot
        pot.AddChips(this.photonView.ownerId, chipsToBet);

        Debug.Log("chips to raise value is " + chipsToRaise + "the amount being sent on is " + ChipsToRaise);

        //tell the turnswitch the value we want to raise
        ts.GetComponent<TurnSwitch>().PotComparison(ChipsToRaise);

        //reset the chips we want to raise
        //chipsToRaise = 0;

        ts.GetComponent<TurnSwitch>().OnClick();
        
        //	if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 2)
        //	{
        //		Debug.Log ("player 2 transferring control to 1" );

        //GameObject.FindGameObjectWithTag ("Player2betController").GetComponentInChildren<MeshRenderer>().enabled = false;
        //GameObject.FindGameObjectWithTag ("Player2betController").GetComponentInChildren<SphereCollider> ().enabled = false;

        //TurnSwitch.OnClick();

        //Click the button
        //disable buttons for player2 here after this
        //	return;
        //}
        //else if (this.photonView.ownerId == PhotonNetwork.player.ID && PhotonNetwork.player.ID == 1)
        //{
        //	Debug.Log ("player 1 transferring control to 2" );
        //GameObject.FindGameObjectWithTag ("Player1betController").GetComponentInChildren<MeshRenderer>().enabled = false;
        //GameObject.FindGameObjectWithTag ("Player1betController").GetComponentInChildren<SphereCollider> ().enabled = false;
        //TurnSwitch.OnClick();

        //disable buttons for player1 here after this
        //	return;
        //}
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
        GetComponent<Renderer>().material.color = Color.white;
        CrosshairTimerDisplay.Instance.Show();
    }
}