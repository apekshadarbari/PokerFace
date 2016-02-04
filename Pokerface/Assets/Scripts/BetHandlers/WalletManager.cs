using UnityEngine;
using System.Collections;

public class WalletManager : Photon.MonoBehaviour
{
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
        chipValue = 100;
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

    public void AddChips(int value)
    {
        chipValue += value;
        
    }

    public int GetChips(int value)
    {
        if (chipValue>value)
        {
            chipValue -= value;
        }
        else
        {
            value = chipValue;
            chipValue = 0;
        }
        return value;
        
    }

}
