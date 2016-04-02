using UnityEngine;

public class Reticle : MonoBehaviour
{
    public Camera CameraFacing;
    private Vector3 originalScale;

    // Use this for initialization
    [SerializeField]
    private float distance;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        //distance;
        if (Physics.Raycast(new Ray(CameraFacing.transform.position, CameraFacing.transform.rotation * Vector3.forward * 2.0f), out hit))
        {
            distance = hit.distance;
        }
        else
        {
            distance = CameraFacing.farClipPlane * .95f;
        }
        transform.LookAt(CameraFacing.transform.position);
        transform.position = CameraFacing.transform.position + CameraFacing.transform.rotation * Vector3.forward * distance;
        transform.Rotate(0.0f, 180.0f, 0.0f);
        if (distance < 100.0f)
        {
            distance *= 1 + 5 * Mathf.Exp(-distance);
        }
        transform.localScale = originalScale * distance;
    }
}