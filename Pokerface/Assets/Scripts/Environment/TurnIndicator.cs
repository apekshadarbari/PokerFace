using System.Collections;
using UnityEngine;

public class TurnIndicator : PhotonManager<TurnIndicator>
{
    //[SerializeField]
    //private Transform indicatorTrans;

    private int player;

    [SerializeField]
    private float speed = 1f;

    [SerializeField, Header("Turn( mulitplied by speed)")]
    private float degreesPerUpdate = 180f;

    private Vector3 targetPos;

    // Use this for initialization
    private void Start()
    {
        TurnIndication(1);
        transform.position = targetPos;
        //indicatorTrans = this.gameObject.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position != targetPos)
        {
            Vector3 dir = (targetPos - transform.position).normalized;
            transform.rotation *= Quaternion.AngleAxis((degreesPerUpdate * speed) * Time.smoothDeltaTime, Vector3.Cross(Vector3.up, dir)); // change degrees up for faster roll
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.smoothDeltaTime);
            //transform.Rotate(1, 0, 1);
        }
    }

    //take current players turn and lerp toward the position
    public void TurnIndication(int player)
    {
        this.player = player;
        if (this.player == 1)
        {
            targetPos = new Vector3(.78f, 1.87f, -.33f);
        }
        else if (this.player == 2)
        {
            targetPos = new Vector3(.78f, 1.87f, 1.127f);
        }
        //Debug.Log("TURNINDICATION COMMENSING");
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(gameObject.transform.position);
            stream.SendNext(gameObject.transform.rotation); // if we add ball rolling later
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext(); // if we add ball rolling later
        }
    }
}