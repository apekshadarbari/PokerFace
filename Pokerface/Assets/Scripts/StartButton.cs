using UnityEngine;
using System.Collections;
using System;

public class StartButton : MonoBehaviour, IClicker
{

    CardManager cardMan;
    NetworkedPlayer playerCtrl;

    public void OnClick()
    {
        Debug.Log("starting game");
        playerCtrl.StartGame();
        cardMan.Deal();
    }

    public void OnExitHover()
    {
        Debug.Log("hovering");
        return;
    }

    public void OnHover()
    {
        Debug.Log("stopped hovering");
        return;
    }
}
