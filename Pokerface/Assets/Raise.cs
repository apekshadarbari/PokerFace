using UnityEngine;
using System.Collections;
using System;

public class Raise : Photon.MonoBehaviour, IClicker
{
	[SerializeField]
	GameObject wallet;
	int chipsToIncrement;
	PotManager pot;
	int chipsToRaise;
	TurnSwitch ts;

	void Start()
	{
		chipsToIncrement = 5;
		chipsToRaise = 0;
		pot = GameObject.Find("pot").GetComponent<PotManager>();
	}



	public void OnClick()
	{
		Debug.Log("player " + this.photonView.ownerId + "raises");


		Bet.chipsToRaise = Bet.chipsToRaise + chipsToIncrement;

	


	}

	public void OnHover()
	{
		throw new NotImplementedException();
	}

	public void OnExitHover()
	{
		throw new NotImplementedException();
	}






}
