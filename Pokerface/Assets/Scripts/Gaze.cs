using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gaze : MonoBehaviour
{
    private GameObject gazeGameObject;
    private Image image;
    private TestRaycast raycast;

    private float delay;

    private RaycastHit hit;
    private void Start()
    {
        image = GetComponent<Image>();
        delay = 2;
        raycast = GameObject.Find("[CameraRig]/Camera (head)/Camera (eye)").GetComponent<TestRaycast>();
    }

    private void Update()
    {
        if (gazeGameObject == null || raycast.Current == gazeGameObject)
        {
            if (raycast.GazeFraction == 1)
            {
                image.fillAmount += Time.deltaTime / delay;
            }
            else
                image.fillAmount = 0;
        }
    }
}