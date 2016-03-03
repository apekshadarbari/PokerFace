using System;
using System.Collections;
using UnityEngine;

public class CardListener : Photon.MonoBehaviour, IClicker
{
    public void EndTurn()
    {
        throw new NotImplementedException();
    }

    public void OnExitHover()
    {
        GetComponent<Renderer>().material.color = Color.grey;
    }

    public void OnHover()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}