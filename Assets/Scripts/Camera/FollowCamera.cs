using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	[SerializeField] private Transform cameraObj;
	[SerializeField] private Transform followObject;

	private void Update()
	{
		cameraObj.position = new Vector3(followObject.position.x, followObject.position.y, cameraObj.position.z);
	}
}
