using System.Collections;
using UnityEngine;

public class FaceCam : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        FaceCamera();
    }

    /// <summary>
    /// Make the gameobject this script is attached to face the camera.
    /// call this method in update to make it follow the camera
    /// </summary>
    private void FaceCamera()
    {   // sets the Camera´s forward positioning towards the Camera.Main (main camera is tagged as such)
        transform.forward = (Camera.main.transform.position - transform.position).normalized;
        // in my case i had to rotate it the other way to make it work
        transform.Rotate(0, 180, 0); // delete if redundant - most likely
    }
}