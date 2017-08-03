using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour {

	[Tooltip("The main player camera.")]
	public Camera mainCam;
	[Tooltip("The marker inside the other portal.")]
	public Transform otherMarker;

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("MainCamera")) {
			mainCam.transform.position = new Vector3 (otherMarker.position.x, mainCam.transform.position.y, otherMarker.position.z);
		}
	}
}
