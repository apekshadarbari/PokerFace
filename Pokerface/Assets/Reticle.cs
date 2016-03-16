using UnityEngine;
using System.Collections;

public class Reticle : MonoBehaviour {


	public Camera CameraFacing;
	private Vector3 originalScale;
	// Use this for initialization
	void Start () {
	
		originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
		RaycastHit hit;
		float distance;
		if (Physics.Raycast (new Ray (CameraFacing.transform.position, CameraFacing.transform.rotation * Vector3.forward * 2.0f), out hit)) {

			distance = hit.distance;
		} else {
			distance = CameraFacing.farClipPlane * 0.95f;
		}
		transform.LookAt (CameraFacing.transform.position);
		transform.position = CameraFacing.transform.position + CameraFacing.transform.rotation * Vector3.forward * distance;
		transform.Rotate (0.0f, 180.0f, 0.0f);
		if (distance < 10.0f) {
			distance *= 1 + 2 * Mathf.Exp (-distance);
		}
		transform.localScale = originalScale * distance;

	}
}
