using System.Collections;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Vector2 sensitivity = Vector2.one;

    //private Quaternion offset;
    private float x;

    private float y;

    private void Start()
    {
        //offset = transform.localRotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MouseHide();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            MouseShow();
        }
        if (Input.GetMouseButton(1))
        {
            var horizontal = GetRotation("Mouse X", Vector3.up, ref y, sensitivity.y, false, -89f, 89f);
            var vertical = GetRotation("Mouse Y", Vector3.left, ref x, sensitivity.x, true, -89f, 89f);
            //transform.rotation = horizontal * vertical;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, horizontal * vertical, Time.smoothDeltaTime * 10f);
        }
    }
    private Quaternion GetRotation(string name, Vector3 axis, ref float angle, float multiplier, bool clamp, float min, float max)
    {
        float input = Input.GetAxisRaw(name);
        angle += input * multiplier;
        if (clamp)
            angle = Mathf.Clamp(angle, min, max);
        return Quaternion.AngleAxis(angle, axis);
    }
    private void MouseShow()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void MouseHide()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}