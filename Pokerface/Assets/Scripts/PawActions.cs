using UnityEngine;
using System.Collections;

public class PawActions : MonoBehaviour {
    public GameObject greenBtn;
    public GameObject foldBtn;
    public GameObject chipSpotlight;

    public GameObject foldHUD;
    public GameObject checkHUD;
    public GameObject callHUD;
    public GameObject raiseHUD;
    public GameObject walletHUD;

    public static bool isMyTurn;
    public static bool isCheck;
    public static bool isCall;

    void Awake() {
        foldHUD.SetActive(false);
        checkHUD.SetActive(false);
        callHUD.SetActive(false);
        raiseHUD.SetActive(false);
        walletHUD.SetActive(false);
    }

    void Start()
    {
        Debug.Log("Your opponent's turn!");
        StartCoroutine(beforeMyTurn());
        chipSpotlight.GetComponent<Light>().enabled = false;
    }
  
    IEnumerator beforeMyTurn()
    {
        yield return new WaitForSeconds(5f);
        isMyTurn = true;
        if (Random.value < 0.5f)
        {
            Debug.Log("Your opponent checks!");
            isCheck = true;
        }
        else {
            Debug.Log("Your opponent raises!");
            isCall = true;
        }
    }

    void Update() {
        if (isMyTurn) {
            greenBtn.GetComponent<Renderer>().material.color = Color.white;
            foldBtn.GetComponent<Renderer>().material.color = Color.white;
            foldHUD.SetActive(true);
            walletHUD.SetActive(true);
            chipSpotlight.GetComponent<Light>().enabled = true;

            if (isCheck)
            {
                checkHUD.SetActive(true);
            }
            else if (isCall)
            {
                callHUD.SetActive(true);
            }
            else {
                if (ChipMove.chipClicked)
                {
                    checkHUD.SetActive(false);
                    raiseHUD.SetActive(true);
                }
            }
        }
    }
}
