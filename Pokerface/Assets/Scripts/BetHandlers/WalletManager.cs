﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalletManager : Photon.MonoBehaviour
{

	List<GameObject> players;
	public int player1ChipValue;
	public int player2ChipValue;
    [SerializeField]
    int chipValue;

    public int ChipValue
    {
        get
        {
            return chipValue;
        }
    }

    void Start()
    {
       chipValue = 0;
		player1ChipValue = 100;
		player2ChipValue = 100;

        //switch (this.photonView.ownerId)
        //{
        //    case 1:
        //        chipValue = 100;
        //        break;
        //    case 2:
        //        chipValue = 100;
        //        break;
        //    default:
        //        break;
        //}
    }

  /*  public void AddChips(int value)
    {
        chipValue += value;
        
    } */

    public int GetChips(int player, int value)
    {    

		if (player == 1) {
			
			if (player1ChipValue>value)
			{
				player1ChipValue -= value;

			}
			else
			{
				value = player1ChipValue;
				player1ChipValue = 0;
				Debug.Log ("player " + this.photonView.ownerId + " all-in");

			}
		} else if (player == 2) {
			if (player2ChipValue>value)
			{
				player2ChipValue -= value;

			}
			else
			{
				value = player2ChipValue;
				player2ChipValue = 0;

				Debug.Log ("player " + this.photonView.ownerId + " all-in");

			}
		}
		WordManager.walletMoney01 = player1ChipValue; 
		WordManager.walletMoney02 = player2ChipValue; 
      
        return value;
        
    }

}
