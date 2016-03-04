using System.Collections;
using UnityEngine;

public class TestRaycast : MonoBehaviour
{
    //use iclicker
    private IClicker current;

    private float timer;

    [Header("The time in seconds it takes to activate onclick with gaze")]
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

    private void Start()
    {
        timer = delay;
    }

    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0f) / 2f);

        /*TESTING*/
        /*UNCOMMENT FOR USING MOUSE*/
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        //if (Physics.Raycast(ray, out hit))
        /*UNCOMMENT FOR USING GAZE*/
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            //Debug.Log("i hit something");
            //draw a line we can see in the scene
            Debug.DrawLine(transform.position, hit.point, Color.cyan);

            //am i hitting something with an iclicker??
            var c = hit.transform.GetComponent<IClicker>();

            //if yes
            if (c != null)
            {
                // we are hovering
                if (current == null)
                {
                    current = c;
                    //call hover of what we are hovering
                    current.OnHover();
                    //start the timer
                    timer = delay;
                }
                //if we are no longer hovering an object but start hovering something else right away
                else if (current != c)
                {
                    //call onexithover of what we hovered
                    current.OnExitHover();
                    //change what we are hovering
                    current = c;
                    //call onhover
                    current.OnHover();
                    //reset the timer to 0 as to not mess up the gaze
                    timer = delay;
                }
                //use fire1 to click
                if (Input.GetButtonDown("Fire1"))
                {
                    c.EndTurn();
                    timer = 0f;
                }
                //wait for the gaze timer to reach 2 seconds and click for you
                else if (Timer > 0f)
                {
                    timer -= Time.unscaledDeltaTime;

                    if (Timer <= 0f)
                    {
                        c.EndTurn();
                    }
                }
            }
            // if we stopped hovering and arent hovering something else hoverable call only exit and set current to null
            else if (current != null)
            {
                current.OnExitHover();
                current = null;
            }
        }
        else
        {
            //draw red if we arent hitting anything
            Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        }
    }
}