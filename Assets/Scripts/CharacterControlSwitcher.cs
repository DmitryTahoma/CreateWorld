using TMPro;
using UnityEngine;

public class CharacterControlSwitcher : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI buttonText;

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

		if (newState)
		{
			buttonText.text = "Stop Control";
		}
		else
		{
			buttonText.text = "Manual Control";
		}
	}
}
