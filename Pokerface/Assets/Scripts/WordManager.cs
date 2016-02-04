using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class WordManager : MonoBehaviour {

	public Text potText;
	public Text walletText01;
	public Text walletText02;
	public Text betText01;
	public Text betText02;

	public static int potMoney;
	public static int walletMoney01;
	public static int walletMoney02;
	public static int betMoney01;
	public static int betMoney02;

	// Use this for initialization
	void Start () {
		potMoney = 0;
		walletMoney01 = 100;
		walletMoney02 = 100;
		betMoney01 = 0;
		betMoney02 = 0;
	}

	// Update is called once per frame
	void Update () {
		potText.GetComponent<Text> ().text = "Pot: " + potMoney.ToString ();
		walletText01.GetComponent<Text> ().text = "Wallet : " + walletMoney01.ToString ();
		walletText02.GetComponent<Text> ().text = "Wallet : " + walletMoney02.ToString ();
		betText01.GetComponent<Text> ().text = "Bet: " + betMoney01.ToString ();
		betText02.GetComponent<Text> ().text = "Bet: " + betMoney02.ToString ();
	}

	//If we're updating the text on the canvas through the update function, do we reall

	/*
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)

	{
		if (stream.isWriting)
		{
			//send the next turn to next player
			//make it uninteractable to others

			stream.SendNext(potText);

			stream.SendNext(walletText01);
			stream.SendNext(walletText02);
			stream.SendNext(betText01);
			stream.SendNext(betText02);

		}
		else
		{
			this.turn = (int)stream.ReceiveNext();
			photonView.ownerId = (int)stream.ReceiveNext();
			this.TurnTrigger.transform.position = (Vector3)stream.ReceiveNext();
		}
	}
	*/
}