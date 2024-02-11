using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
	public static GameInput Instance { get; private set; }

	private InputAction moveInputAction;

	private void Awake()
	{
		Instance = this;
		PlayerInput playerInput = GetComponent<PlayerInput>();
		moveInputAction = playerInput.currentActionMap.actions.First(x => x.name == "Move");
	}

	public Vector2 GetMovementVector()
	{
		return moveInputAction.ReadValue<Vector2>();
	}

	public Vector2 GetMouseWorldPosition()
	{
		return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
	}
}
