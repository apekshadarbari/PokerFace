using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class WordManager : MonoBehaviour {

	public Text potText;
	public Text walletText01;
	public Text walletText02;
	public Text betText01;
	public Text betText02;

	public int potMoney;
	public int walletMoney01;
	public int walletMoney02;
	public int betMoney01;
	public int betMoney02;

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
}