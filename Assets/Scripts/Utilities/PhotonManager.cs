using System.Collections;
using UnityEngine;

public class PhotonManager<T> : Photon.MonoBehaviour where T : PhotonManager<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = this as T;
    }
}