using System.Collections;
using UnityEngine;

internal enum ActionSound
{
    roundStarted,

    p1Call,
    p1Check,
    p1Raise,
    p1Fold,

    p2Call,
    p2Check,
    p2Raise,
    p2Fold,

    p1Win,
    p2Win,
}

public class AudioManager : MonoBehaviour
{
    private AudioSource src;

    [SerializeField]
    private AudioClip roundStarted; //can probably be moved..

    [Header("Player One Sounds")]
    [SerializeField]
    private AudioClip playerOneCalled;

    [SerializeField]
    private AudioClip playerOneChecked;

    [SerializeField]
    private AudioClip playerOneRaised;

    [SerializeField]
    private AudioClip playerOneFolded;

    [Header("Player Two Sounds)")]
    [SerializeField]
    private AudioClip playerTwoCalled;

    [SerializeField]
    private AudioClip playerTwoChecked;

    [SerializeField]
    private AudioClip playerTwoRaised;

    [SerializeField]
    private AudioClip playerTwoFolded;

    public void Start()
    {
        src = this.GetComponent<AudioSource>();
    }

    [PunRPC]
    private void ButtonPressedAudio(ActionSound soundToPlay)
    {
        switch (soundToPlay)
        {
            case ActionSound.roundStarted:
                src.clip = roundStarted;
                break;

            case ActionSound.p1Call:
                src.clip = playerOneCalled;
                break;

            case ActionSound.p1Check:
                src.clip = playerOneChecked;
                break;

            case ActionSound.p1Raise:
                src.clip = playerOneRaised;
                break;

            case ActionSound.p1Fold:
                src.clip = playerOneFolded;
                break;

            case ActionSound.p2Call:
                src.clip = playerTwoCalled;
                break;

            case ActionSound.p2Check:
                src.clip = playerTwoChecked;
                break;

            case ActionSound.p2Raise:
                src.clip = playerTwoRaised;
                break;

            case ActionSound.p2Fold:
                src.clip = playerTwoFolded;
                break;

            default:
                break;
        }
        src.Play();
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
        }
        else
        {
        }
    }
}