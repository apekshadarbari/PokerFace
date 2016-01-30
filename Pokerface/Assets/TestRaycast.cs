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
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        }

    }
}
