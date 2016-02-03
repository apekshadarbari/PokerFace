using UnityEngine;
using System.Collections;

public class CrosshairTimerDisplay : MonoBehaviour
{

    private static CrosshairTimerDisplay instance;

    public static CrosshairTimerDisplay Instance
    {
        get
        {
            return instance;
        }
    }

    private void Start()
    {
        instance = this;
    }

    public void Show()
    {
        this.enabled = true;
        

    }
    public void Hide()
    {
        this.enabled = false;

    }
}
