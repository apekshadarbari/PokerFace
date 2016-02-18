using UnityEngine;
using System.Collections;

public class ButtonTrigger : MonoBehaviour {
    public AudioClip hoverOver;
    public AudioClip pressBtn;
    public AudioClip buttonVoice;
    public AudioClip callVoice;

    //public GameObject tennisBall;

    private AudioSource buttonPlayer;
    private bool btnHoverPlayed;
    private bool playBtnPress;
    private bool btnPressPlayed;
    private bool voicePlayed;

    private float lastTime;
    private float timeLimit = 2.0f;

    private GameObject[] player1Btns = new GameObject[5];
    private GameObject[] player2Btns = new GameObject[5];

    void Start() {
        buttonPlayer = this.GetComponent<AudioSource>();

        player1Btns = GameObject.FindGameObjectsWithTag("Player1");
        player2Btns = GameObject.FindGameObjectsWithTag("Player2");
        for (int i = 0; i < 5; i++) {
            player1Btns[i].GetComponent<SphereCollider>().enabled = true;
            player2Btns[i].GetComponent<SphereCollider>().enabled = false;
        }
    }

    public void OnMouseOver() {
        this.GetComponent<Renderer>().material.color = Color.yellow;
        if (!btnHoverPlayed)
        {
            buttonPlayer.PlayOneShot(hoverOver, 1f);
            btnHoverPlayed = true;
        }
        timer();
        if (playBtnPress && !btnPressPlayed)
        {
            btnPressPlayed = true;
        }
        if (btnPressPlayed && !voicePlayed)
        {
            buttonPlayer.PlayOneShot(buttonVoice, 1f);
            voicePlayed = true;
            if (this.tag == "Player1" && this.name != "P1_Plus" && this.name != "P1_Minus")
            {
                for (int i = 0; i < 5; i++)
                {
                    player1Btns[i].GetComponent<SphereCollider>().enabled = false;
                    player2Btns[i].GetComponent<SphereCollider>().enabled = true;
                }
            }
            else if(this.tag == "Player2" && this.name != "P2_Plus" && this.name != "P2_Minus")
            {
                for (int i = 0; i < 5; i++)
                {
                    player1Btns[i].GetComponent<SphereCollider>().enabled = true;
                    player2Btns[i].GetComponent<SphereCollider>().enabled = false;
                }
            }
        }
    }

    public void OnMouseExit() {       
        this.GetComponent<Renderer>().material.color = Color.white;
        btnHoverPlayed = false;
        playBtnPress = false;
        btnPressPlayed = false;
        voicePlayed = false;
        timeLimit = 2;
    }

    private void timer()
    {
        if ((Time.time > lastTime + 1) && timeLimit > 0)
        {
            lastTime = Time.time;
            timeLimit--;
        }
        if (timeLimit == 0)
        {
            timeLimit = 0;
            playBtnPress = true;
        }
    }
}

