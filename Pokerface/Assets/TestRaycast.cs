using UnityEngine;
using System.Collections;

public class TestRaycast : MonoBehaviour
{
    private IClicker current;
    private float timer;
    [SerializeField]
    private float delay = 2f;

    public float Timer
    {
        get
        {
            return timer;
        }
    }
    public float Delay
    {
        get
        {
            return delay;
        }
    }

    void Start()
    {
        timer = delay;
    }

    // Update is called once per frame
    void Update()
    {
       // Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0f) / 2f);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
       // if (Physics.Raycast(ray, out hit))
		if(Physics.Raycast(transform.position, transform.forward, out hit))
		{
            //Debug.Log("i hit something");
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
            var c = hit.transform.GetComponent<IClicker>();

            if (c != null)
            {
                if (current == null)
                {
                    current = c;
                    current.OnHover();
                    timer = delay;
                }
                else if (current != c)
                {
                    current.OnExitHover();
                    current = c;
                    current.OnHover();
                    timer = delay;
                }

                if (Input.GetButtonDown("Fire1"))
                {
                    c.OnClick();
                    timer = 0f;
                }
                else if (Timer > 0f)
                {
                    timer -= Time.unscaledDeltaTime;

                    if (Timer <= 0f)
                    {
                        c.OnClick();
                    }

                }

            }
            else if (current != null)
            {
                current.OnExitHover();
                current = null;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        }

    }
}
