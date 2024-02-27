using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	[SerializeField] private Transform cameraObj;

	public Transform FollowObject { get; set; }

	private void Update()
	{
		cameraObj.position = new Vector3(FollowObject.position.x, FollowObject.position.y, cameraObj.position.z);
	}
}
