using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterMovement : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 7f;
	[SerializeField] private float jumpForce = 14f;

	private Rigidbody2D characterRigidbody;

	private GameObject character;
	public GameObject Character
	{
		get => character;
		set
		{
			character = value;
			if (character == null)
			{
				characterRigidbody = null;
			}
			else
			{
				characterRigidbody = character.GetComponent<Rigidbody2D>();
			}
		}
	}

	private void Update()
	{
		Vector2 movementVector = GameInput.Instance.GetMovementVector();
		characterRigidbody.velocityX = movementVector.x * moveSpeed;
		
		if (movementVector.y > 0)
		{
			characterRigidbody.velocityY = jumpForce;
		}
	}
}
