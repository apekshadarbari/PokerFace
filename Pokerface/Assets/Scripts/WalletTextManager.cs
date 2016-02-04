using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WalletTextManager : MonoBehaviour {

	public int walletMoney;

	// Use this for initialization
	void Start () {
		walletMoney = 100;
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Text> ().text = "Wallet:" + walletMoney.ToString ();
	}
}
