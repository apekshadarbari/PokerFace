using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalletManager : Photon.MonoBehaviour
{

    List<GameObject> players;

    [SerializeField]
    int chipValue;

    Canvas infoBoard;

    public int ChipValue
    {
        get
        {
            return chipValue;
        }
    }

    void Start()
    {
        chipValue = 100;
        infoBoard = GameObject.FindGameObjectWithTag("InfoBoard").GetComponent<Canvas>();
    }

    void Update()
    {
        infoBoard.GetComponent<PhotonView>().RPC("TextWallet", PhotonTargets.AllBuffered, this.photonView.ownerId, chipValue);
    }

    [PunRPC]
    public void AddChipsToWallet(int value)
    {
        chipValue += value;

    }

    public int GetChips(int player, int value)
    {
        if (player == 1)
        {

            if (chipValue > value)
            {
                chipValue -= value;
            }

            else
            {
                value = chipValue;
                chipValue = 0;
                Debug.Log("player " + this.photonView.ownerId + " all-in");

            }
        }
        else if (player == 2)
        {
            if (chipValue > value)
            {
                chipValue -= value;
            }
            else
            {
                value = chipValue;
                chipValue = 0;

                Debug.Log("player " + this.photonView.ownerId + " all-in");

            }
        }
        return value;
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(chipValue);
        }
        else
        {
            chipValue = (int)stream.ReceiveNext();
        }
    }
}


