using UnityEngine;
using System.Collections;

public class HUDFloating : MonoBehaviour {
    void Update () {
        if (PawActions.isMyTurn) {
            transform.Rotate(0, 1f, 0);
        }       
    }
}
