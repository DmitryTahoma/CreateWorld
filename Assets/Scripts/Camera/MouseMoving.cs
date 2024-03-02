using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMoving : MouseClickableBase
{
	private const float maxStickingSqrMagnitude = 0.01f;

    private bool isAttached = false;
    private Vector2 shift = Vector2.zero;

	private bool isSticked = false;
	private Vector3 startPosition = Vector3.zero;

    private void Update()
    {
        if (isAttached)
        {
            Vector3 newPos = GameInput.Instance.GetMouseWorldPosition() + shift;
            newPos.z = transform.position.z;
			transform.position = Vector3.LerpUnclamped(newPos, transform.position, 2f);
			UpdateShift();

			if (!isSticked)
			{
				isSticked = (startPosition - transform.position).sqrMagnitude > maxStickingSqrMagnitude;
			}

			if (!GameInput.Instance.IsCursorOverWindow())
			{
				StopAttach();
			}
		}
	}

	public override void OnClick(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			isAttached = true;
			UpdateShift();
			isSticked = false;
			startPosition = transform.position;
			Handled = true;
		}

		if (context.canceled)
		{
			StopAttach();
		}
	}

	private void UpdateShift()
    {
		shift = (Vector2)transform.position - GameInput.Instance.GetMouseWorldPosition();
	}

	private void StopAttach()
	{
		isAttached = false;
		shift = Vector2.zero;
		Handled = isSticked;
		isSticked = false;
		startPosition = transform.position;
	}
}
