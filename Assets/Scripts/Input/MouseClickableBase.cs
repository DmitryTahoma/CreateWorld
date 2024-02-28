using UnityEngine;
using UnityEngine.InputSystem;

public abstract class MouseClickableBase : MonoBehaviour
{
	public bool Handled { get; set; } = false;
	public abstract void OnClick(InputAction.CallbackContext context);
}
