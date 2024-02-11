using UnityEngine;

public class KeyboardMoving : MonoBehaviour
{
    private Vector2 movementVector = new Vector2();

    [SerializeField] private float sensitivity = 0.001f;
    [SerializeField] private int dragLength = 10;

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
