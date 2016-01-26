<<<<<<< HEAD
﻿using UnityEngine;
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
=======
﻿using UnityEngine;
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
>>>>>>> da1a6e2190ac886a62af1e2b273e58d39b9f820d
