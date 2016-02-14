using UnityEngine;
using System.Collections;

public class CrosshairTimerDisplay : MonoBehaviour
{
    //TODO: finish timer display for the gaze
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
    /// <summary>
    /// show the timer display - done when hovering before click is called
    /// </summary>
    public void Show()
    {
        this.enabled = true;
    }

    /// <summary>
    /// stop showing the timer - done when we stop hovering something or click is done
    /// </summary>
    public void Hide()
    {
        this.enabled = false;

    }
}
