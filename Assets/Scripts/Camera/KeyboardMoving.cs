using UnityEngine;

public class KeyboardMoving : MonoBehaviour
{
    private Vector2 movementVector = Vector2.zero;

    [SerializeField] private float sensitivity = 0.5f;

    private void Update()
    {
		movementVector = GameInput.Instance.GetMovementVector();
	}

    private void FixedUpdate()
    {
        transform.Translate(movementVector * sensitivity);
        movementVector = new Vector2();
	}
}
