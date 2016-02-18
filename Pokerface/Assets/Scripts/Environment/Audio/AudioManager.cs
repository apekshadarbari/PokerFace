using UnityEngine;
using System.Collections;
enum ActionSound
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
}
public class AudioManager : MonoBehaviour
{
    
    AudioSource src;

    [SerializeField]
    AudioClip roundStarted; //can probably be moved..

    [Header("Player One Sounds")]

    [SerializeField]
    AudioClip playerOneCalled;
    [SerializeField]
    AudioClip playerOneChecked;
    [SerializeField]
    AudioClip playerOneRaised;
    [SerializeField]
    AudioClip playerOneFolded;

    [Header("Player Two Sounds)")]
    [SerializeField]
    AudioClip playerTwoCalled;
    [SerializeField]
    AudioClip playerTwoChecked;
    [SerializeField]
    AudioClip playerTwoRaised;
    [SerializeField]
    AudioClip playerTwoFolded;

    public void Start()
    {
        src = this.GetComponent<AudioSource>();

    }

    [PunRPC]
    void ButtonPressedAudio(ActionSound soundToPlay)
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

}
