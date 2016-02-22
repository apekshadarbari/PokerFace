using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletManager : Manager<WalletManager>
{
    [SerializeField]
    private int credits = 100;

    public int Credits
    {
        get { return credits; }
    }

    public void Deposit(int value)
    {
        credits += value;
    }

    public bool Withdraw(int value)
    {
        if (credits >= value)
        {
            credits -= value;
            return true;
        }
        else
        {
            return false;
        }
    }
}