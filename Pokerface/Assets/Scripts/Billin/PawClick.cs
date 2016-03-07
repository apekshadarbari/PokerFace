using UnityEngine;
using System.Collections;

public class PawClick : MonoBehaviour {
    public static bool chipmove;

    void OnMouseUp() {
        if (PawActions.isMyTurn) {
            if (ChipMove.chipClicked) {
                chipmove = true;
            }
        }
    }
}
