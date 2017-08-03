using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	[Header("Cameras")]
	[Tooltip("The main player camera.")]
	public Camera MainCamera;
	[Tooltip("A camera placed looking out of the portal object.")]
	public Camera PortalCam;

	[Header("Locations")]
	[Tooltip("The portal to be walked through.")]
	public Transform SourcePortal;
	[Tooltip("The portal in the destination realm.")]
	public Transform DestinationPortal;
	
	private void FixedUpdate() {

		// Create a matrix to flip 180
		Matrix4x4 flipMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(180.0f, Vector3.up), new Vector3( 1.0f, 1.0f, 1.0f ));
		// Apply the 180 flip to the source location.
		Matrix4x4 invertedSource = flipMatrix * SourcePortal.worldToLocalMatrix;

		// Convert the main camera location to a Vector4 so that it can be modified by our inverted matrix.
		Vector4 mainCamV4 = new Vector4(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z, 1.0f);
		// Place the main camera in source space.
		Vector4 mainCamInverted = invertedSource * mainCamV4;
		// Convert back to a Vector3 for setting the portal cam position.
		Vector3 camTransform = new Vector3 (mainCamInverted.x, mainCamInverted.y, mainCamInverted.z);
		// Rotate the camera so that it looks the correct direction in source space.
		Quaternion camRotation = Quaternion.LookRotation(invertedSource.GetColumn(2), invertedSource.GetColumn(1)) * MainCamera.transform.rotation;

		// Set the portal camera's transforms according to the translated tranform from the main camera.
		PortalCam.transform.position = DestinationPortal.TransformPoint(camTransform);
		PortalCam.transform.rotation = DestinationPortal.rotation * camRotation;
	}
}