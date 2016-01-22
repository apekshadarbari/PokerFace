using UnityEngine;
using System.Collections;

public class SeatAquire : MonoBehaviour
{
    private void Update()
    {
        if( SeatManager.Instance.IsSeatAvailable() )
        {
            // get seat
            // tell host this is my seat
            GameObject.Destroy(gameObject);
        }
    }
}
