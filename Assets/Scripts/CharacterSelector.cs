using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(BoxCollider2D))]
public class CharacterSelector : MouseClickableBase
{
	private BoxCollider2D boxCollider2D;

	[SerializeField] private GameObject currentCharacter;
	[SerializeField] private GameObject button;
	[SerializeField] private FollowCamera followCamera;
	[SerializeField] private CharacterMovement characterMovement;

	private void Awake()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	public override void OnClick(InputAction.CallbackContext context)
	{
		if (!context.canceled) return;

		if (boxCollider2D.bounds.Contains(GameInput.Instance.GetMouseWorldPosition()))
		{
			bool newState = !button.activeSelf;
			button.SetActive(newState);
			if (newState)
			{
				followCamera.FollowObject = currentCharacter.transform;
				characterMovement.Character = currentCharacter;
			}
			else
			{
				followCamera.FollowObject = null;
				characterMovement.Character = null;
			}
			Handled = true;
		}
	}
}
