using UnityEngine;

public class CharacterController : MonoBehaviour
{
	[SerializeField] private FollowCamera followCamera;
	[SerializeField] private CharacterMovement characterMovement;

	[SerializeField] private KeyboardMoving keyboardMoving;
	[SerializeField] private MouseMoving mouseMoving;

	public void ChangeManualControl()
	{
		bool newState = !followCamera.enabled;

		followCamera.enabled = newState;
		characterMovement.enabled = newState;

		keyboardMoving.enabled = !newState;
		mouseMoving.enabled = !newState;
	}
}
