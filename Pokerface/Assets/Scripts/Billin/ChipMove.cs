using UnityEngine;
using System.Collections;

public class ChipMove : MonoBehaviour {
    bool chipStop;
    public static bool chipClicked;

    void OnMouseUp() {
        if (PawActions.isMyTurn) {
            transform.Translate(0, 0.25f, 0);
            chipClicked = true;
            PawActions.isCheck = false;
        }
    }

    void Update() {
        if (PawClick.chipmove && !chipStop) {
            transform.Translate(0, 0.75f * Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.name == "PotArea") {
            chipStop = true;
        }
    }
}
