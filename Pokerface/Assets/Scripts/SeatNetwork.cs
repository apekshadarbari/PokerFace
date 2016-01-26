using UnityEngine;
using System.Collections;
using System.Linq;

public class SeatNetwork : Photon.MonoBehaviour
{

    private static SeatNetwork instance;

    private Occupied[] seats;

    private bool[] isOccupied;

    public static SeatNetwork Instance
    {
        get
        {
            return instance;
        }
    }

    private void Start()
    {
        seats = GameObject.FindObjectsOfType<Occupied>();
        instance = this;
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

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isReading)
        {
            Debug.Log("jeg læser");
            for (int i = 0; i < seats.Length; i++)
            {
                seats[i].IsOccupied = (bool)stream.ReceiveNext();
            }

        }
        if (stream.isWriting)
        {
            Debug.Log("jeg skriver");
            for (int i = 0; i < seats.Length; i++)
            {
                stream.SendNext(seats[i].IsOccupied);

            }
        }

    }
}
