using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SeatManager : MonoBehaviour
{
    
    
    private static SeatManager instance;

    private Occupied[] seats;

    private bool[] isOccupied;

    public static SeatManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Start()
    {
        instance = this;
        seats = transform.GetComponentsInChildren<Occupied>();
        isOccupied = new bool[seats.Length];
        for (int i = 0; i < isOccupied.Length; i++)
        {
            isOccupied[i] = true;
        }
    }

    public bool IsSeatAvailable()
    {
        return isOccupied.Any(b => !b); 
    }

    public int GetFirstAvailableSeat()
    {
        for (int i = 0; i < isOccupied.Length; i++)
        {
            if (!isOccupied[i])
            {
                return i;
            }
        }
        return -1;
    }
}
