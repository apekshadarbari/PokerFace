using UnityEngine;
using System.Collections;

public class TestRaycast : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            //Debug.Log("i hit something");
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
            var c = hit.transform.GetComponent<IClicker>();
            if (c !=null)
            {
                c.OnClick();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        }

    }
}
