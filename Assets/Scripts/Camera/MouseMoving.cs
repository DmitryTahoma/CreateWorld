using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMoving : MouseClickableBase
{
    private bool isAttached = false;
    private Vector2 shift = Vector2.zero;

    private void Update()
    {
        if (isAttached)
        {
            Vector3 newPos = GameInput.Instance.GetMouseWorldPosition() + shift;
            newPos.z = transform.position.z;
			transform.position = Vector3.LerpUnclamped(newPos, transform.position, 2f);
			UpdateShift();
		}
	}

	public override void OnClick(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			isAttached = true;
			UpdateShift();
			Handled = true;
		}

		if (context.canceled)
		{
			isAttached = false;
			shift = Vector2.zero;
		}
	}

	private void UpdateShift()
    {
		shift = (Vector2)transform.position - GameInput.Instance.GetMouseWorldPosition();
	}
}
