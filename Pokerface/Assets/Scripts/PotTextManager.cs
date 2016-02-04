using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PotTextManager : MonoBehaviour {

	public GameObject potText;
	public int potMoney;

	// Use this for initialization
	void Start () {
		potMoney = 0;
	}
	
	// Update is called once per frame
	void Update () {
		potText.GetComponent<Text> ().text = "Pot:" + potMoney.ToString ();
	}
}
