﻿using System.Collections;
using UnityEngine;

public class ChipsDisplay : Photon.MonoBehaviour
{
    [SerializeField]
    private float chipHeight;

    [SerializeField]
    private int value; // værdien der skal flyttes

    public int Value
    {
        set { this.value = value; }
    }

    [SerializeField]
    private int[] weight; // fordelingen af forskellige typer chips

    [SerializeField]
    private Transform[] transforms; // de objecter der skal ændre sig

    [SerializeField]
    private int[] chipSizes = new int[3] { 10, 50, 100 }; // størrelser af chips - i værdi

    /// <summary>
    /// køres når der trækkes value fra wallet eler pot.
    /// </summary>
    [PunRPC]
    public void UpdateStacks()
    {
        int[] chips = new int[chipSizes.Length];
        int remaining = value;

        // Divide by weight/ priority
        for (int i = 0; i < weight.Length; i++)
        {
            int s = chipSizes[i];
            int r = remaining / s;

            if (r >= weight[i])
            {
                chips[i] += weight[i];
                remaining -= weight[i] * s;
            }
        }

        // Divide remaining by value
        for (int i = chipSizes.Length - 1; i >= 0; i--) // reverse for
        {
            int s = chipSizes[i]; // nuværende chipstørrelse
            int r = remaining / s; // number of chips for value

            chips[i] += r; // add chips to buffer
            remaining -= s * r; // remove the value we take out for chips
        }

        if (remaining != 0)
            Debug.LogError("FAIL TO DIVIDE ALL CHIPS!!"); // did we fuck something up?

        // Update positions
        for (int i = 0; i < transforms.Length; i++)
        {
            //y = størrelsen på chips
            var p = transforms[i].localPosition;
            p.y = chips[i] * chipHeight;
            transforms[i].localPosition = p;
        }

        //debugging
        //for (int i = 0; i < chips.Length; i++)
        //{
        //    Debug.LogFormat("{0} x {1}", chips[i], chipSizes[i]);
        //}
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(gameObject.transform.position);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
        }
    }
}