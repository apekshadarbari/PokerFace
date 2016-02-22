using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField]
    private string cardName;

    float timeOffest;
    float speedOffset;
    float rotateTimeOffset;
    float rotateSpeedOffset;

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
        //card bob variables
        //timeOffest = UnityEngine.Random.Range( -50f, 50f );
        //speedOffset = UnityEngine.Random.Range(-1f, 1f);
        //rotateTimeOffset = UnityEngine.Random.Range(-50f, 50f);
        //rotateSpeedOffset = UnityEngine.Random.Range(-0.2f, 0.2f);

        transform.localRotation = Quaternion.identity;

        transform.localPosition = Vector3.zero;//added to make it instant
        
        //transform.localRotation = Quaternion.AngleAxis(90f, Vector3.left);
    }

    private void Update()
    {
        //     transform.position = Vector3.zero;
        //transform.rotation = Quaternion.identity;
        //card movement
        //transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.smoothDeltaTime);
        //transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.smoothDeltaTime);

        //card bob
        //transform.localPosition = transform.parent.up * Mathf.Sin((Time.time + timeOffest) * (3f + speedOffset)) * 0.2f;
        //transform.localRotation =
        //    Quaternion.AngleAxis(Mathf.Sin((Time.time + rotateTimeOffset) * (0.8f + rotateSpeedOffset)) * 2f,  transform.parent.up) *
        //    Quaternion.AngleAxis(Mathf.Sin((Time.time + rotateTimeOffset) * (0.8f + rotateSpeedOffset)) * 5f, transform.parent.forward);
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

        var obj = Instantiate( Resources.Load(string.Format("Card_{0}{1}", valueName, card.Suit))) as GameObject;
        obj.transform.SetParent(transform);
        //obj.transform.position = Vector3.zero;
        //obj.transform.rotation = Quaternion.identity;
    }
}
