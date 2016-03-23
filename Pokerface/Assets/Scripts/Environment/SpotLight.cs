using System.Collections;
using UnityEngine;

public class SpotLight : PhotonManager<SpotLight>
{
    //private float waitingTime = 0.01f; //20 times a second
    //private float normalRange = 3f; //normal range of light
    //private float flRange = 0.5f; //flickering range

    //[SerializeField]
    //private float flashTime = .3f;

    //private float timer;

    [SerializeField]
    private int round;

    private Light spotLight;

    public int Round
    { set { round = value; } }

    // Use this for initialization
    private void Start()
    {
        spotLight = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    private void Update()
    {
        //this.round = RoundManager.Instance.Round;

        //timer += Time.deltaTime;

        if (round != 0)
        {
            spotLight.enabled = true;
        }
        else
        {
            spotLight.enabled = false;
        }
        //switch (round)
        //{
        //    case 0: // starting cards dealt
        //        StartCoroutine(Flash());

        //        break;

        //    case 1: // flop dealt
        //        break;

        //    case 2: // turn dealth
        //        StartCoroutine(Flash());
        //        break;

        //    case 3: // river daelt
        //        StartCoroutine(Flash());
        //        break;

        //    case 4: // comparison and winner
        //        break;

        //    default:
        //        break;
        //}
    }
    public void LightRoundCount(int round)
    {
        this.round = round;
    }
    //public void LightFlash()
    //{
    //    while (true)
    //    {
    //        spotLight.enabled = !(spotLight.enabled); //toggle on/off the enabled property
    //        yield return new WaitForSeconds(waitingTime);
    //    }
    //}
    //private IEnumerator Flash()
    //{
    //    while (timer < flashTime)
    //    {
    //        //spotLight.enabled = !(spotLight.enabled); //toggle on/off the enabled property
    //        //yield return new WaitForSeconds(waitingTime);
    //        if (spotLight.range == normalRange) spotLight.range = flRange; else spotLight.range = normalRange;
    //        yield return new WaitForSeconds(waitingTime);
    //    }
    //}
}