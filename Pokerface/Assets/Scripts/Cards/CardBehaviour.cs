#define MIKKEL
#undef MIKKEL
#define MIKKEL
#undef MIKKEL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField]
    private string cardName;

#if MIKKEL
    private float timeOffest;
    private float speedOffset;
    private float rotateTimeOffset;
    private float rotateSpeedOffset;
#endif

    private Card card;

    public Card Card
    {
        get { return card; }
        set
        {
            card = value;
            cardName = card.ToString();
        }
    }

    private void Start()
    {
#if MIKKEL
        //card bob variables
        timeOffest = UnityEngine.Random.Range(-50f, 50f);
        speedOffset = UnityEngine.Random.Range(-1f, 1f);
        rotateTimeOffset = UnityEngine.Random.Range(-50f, 50f);
        rotateSpeedOffset = UnityEngine.Random.Range(-0.2f, 0.2f);
#endif
    }

    public void FacePlayer()
    {
        transform.forward = (Camera.main.transform.position - transform.position).normalized;
    }

    private void Update()
    {
        //transform.position = Vector3.zero;
        //transform.rotation = Quaternion.identity;
#if MIKKEL
        //card movement
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.smoothDeltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.smoothDeltaTime);

        //card bob
        transform.localPosition = transform.parent.up * Mathf.Sin((Time.time + timeOffest) * (3f + speedOffset)) * 0.2f;
        transform.localRotation =
            Quaternion.AngleAxis(Mathf.Sin((Time.time + rotateTimeOffset) * (0.8f + rotateSpeedOffset)) * 2f, transform.parent.up) *
            Quaternion.AngleAxis(Mathf.Sin((Time.time + rotateTimeOffset) * (0.8f + rotateSpeedOffset)) * 5f, transform.parent.forward);
#endif
    }

    public void LoadResource()
    {
        string valueName;
        switch (card.Value)
        {
            case 11:
                valueName = "J";
                break;

            case 12:
                valueName = "Q";
                break;

            case 13:
                valueName = "K";
                break;

            case 14:
                valueName = "A";
                break;

            default:
                valueName = card.Value.ToString();
                break;
        }

        var obj = Instantiate(Resources.Load(string.Format("Cards/Card_{0}{1}", valueName, card.Suit))) as GameObject;
        obj.transform.SetParent(transform);
        //obj.transform.position = Vector3.zero;
        //obj.transform.rotation = Quaternion.identity;
    }
}