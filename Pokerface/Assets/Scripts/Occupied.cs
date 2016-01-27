﻿using UnityEngine;
using System.Collections;

public class Occupied : MonoBehaviour
{
    public bool IsOccupied = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool OccupySeat()
    {
        lock (this)
        {
            if (!IsOccupied)
            {
                IsOccupied = true;
                return true;
            }
            return false;
        }
    }

    public bool UnOccupySeat()
    {
        lock (this)
        {
            if (IsOccupied)
            {
                IsOccupied = false;
                return true;
            }
            return false;
        }
    }
}