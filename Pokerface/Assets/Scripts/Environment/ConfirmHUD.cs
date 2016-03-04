using System.Collections;
using UnityEngine;

internal enum ConfirmAction
{
    call,
    check,
    raise,
    fold,
    none,
}

public class ConfirmHUD : Manager<ConfirmHUD>
{
    [SerializeField]
    private ConfirmAction action;

    [SerializeField]
    private MeshRenderer callMesh;

    [SerializeField]
    private MeshRenderer checkMesh;

    [SerializeField]
    private MeshRenderer raiseMesh;

    [SerializeField]
    private MeshRenderer foldMesh;

    [SerializeField]
    private GameObject hud;

    private int callValue;
    private int betValue;

    private int player;
    private int playerInCtrl;

    private void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD");
        callMesh = GameObject.FindGameObjectWithTag("HUDCall").GetComponent<MeshRenderer>();
        checkMesh = GameObject.FindGameObjectWithTag("HUDCheck").GetComponent<MeshRenderer>();
        raiseMesh = GameObject.FindGameObjectWithTag("HUDRaise").GetComponent<MeshRenderer>();
        foldMesh = GameObject.FindGameObjectWithTag("HUDFold").GetComponent<MeshRenderer>();
        callMesh.enabled = false;
        checkMesh.enabled = true;
        raiseMesh.enabled = false;
        foldMesh.enabled = true;
    }
    private void Update()
    {
        this.player = PhotonNetwork.player.ID;
        HudController(player);

        if (this.player != playerInCtrl)
        {
            HudChange(ConfirmAction.none);
        }
        else if (this.player == playerInCtrl)
        {
            HudChange(ConfirmAction.fold);//makes fold visible

            if (betValue < callValue)
            {
                HudChange(ConfirmAction.call);
                //throw new NotImplementedException("You have to at least match the other players bet...");
            }
            else if (betValue > callValue)
            {
                HudChange(ConfirmAction.raise);
            }
            else if (callValue == betValue)
            {
                HudChange(ConfirmAction.check);
            }
            else
            {
                HudChange(ConfirmAction.call);
            }
        }

        transform.forward = (Camera.main.transform.position - transform.position).normalized;
    }

    private void HudChange(ConfirmAction action)
    {
        switch (action)
        {
            case ConfirmAction.call:
                callMesh.enabled = true;
                checkMesh.enabled = false;
                raiseMesh.enabled = false;
                break;

            case ConfirmAction.check:
                callMesh.enabled = false;
                checkMesh.enabled = true;
                raiseMesh.enabled = false;
                break;

            case ConfirmAction.raise:
                callMesh.enabled = false;
                checkMesh.enabled = false;
                raiseMesh.enabled = true;
                break;

            case ConfirmAction.none:
                callMesh.enabled = false;
                checkMesh.enabled = false;
                raiseMesh.enabled = false;
                foldMesh.enabled = false;
                break;

            case ConfirmAction.fold:
                foldMesh.enabled = true;
                break;

            default:
                break;
        }
    }

    public void CurrentValueToCall(int value)
    {
        this.callValue = value;
    }
    public void CurrentBetValue(int value)
    {
        this.betValue = value;
    }

    public void HudController(int player)
    {
        if (player == 1)
        {
            hud.transform.position = new Vector3(0.376f, 1.366f, -0.6f);
        }
        else if (player == 2)
        {
            hud.transform.position = new Vector3(0.376f, 2.234f, 1.549f);
            transform.Rotate(0, 180, 0);
        }
    }
    public void HudToggle(int player)
    {
        this.playerInCtrl = player;
    }
}