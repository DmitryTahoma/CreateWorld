using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
	public static GameInput Instance { get; private set; }

	private InputAction moveInputAction;
	private InputAction scrollInputAction;

	[SerializeField] private MouseClickableBase[] leftMouseHandlers;

	private void Awake()
	{
		Instance = this;
		PlayerInput playerInput = GetComponent<PlayerInput>();
		moveInputAction = playerInput.currentActionMap.actions.First(x => x.name == "Move");
		scrollInputAction = playerInput.currentActionMap.actions.First(x => x.name == "Mouse wheel");
	}

	public Vector2 GetMovementVector()
	{
		return moveInputAction.ReadValue<Vector2>();
	}

	public Vector2 GetMousePosition()
	{
		return Mouse.current.position.ReadValue();
	}

	public Vector2 GetMouseWorldPosition()
	{
		return Camera.main.ScreenToWorldPoint(GetMousePosition());
	}

	public Vector2 GetMouseViewportPosition()
	{
		return Camera.main.ScreenToViewportPoint(GetMousePosition());
	}

	public Vector2 GetMouseWheelVector()
	{
		return scrollInputAction.ReadValue<Vector2>();
	}

	public bool IsCursorOverWindow()
	{
		return Camera.main.rect.Contains(GetMouseViewportPosition());
	}

	public void LeftClickHandle(InputAction.CallbackContext context)
	{
		if (!IsCursorOverWindow())
		{
			return;
		}

		foreach (MouseClickableBase obj in leftMouseHandlers)
		{
			obj.Handled = false;
			obj.OnClick(context);
			if (obj.Handled) break;
		}
	}
}
