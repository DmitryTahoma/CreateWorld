using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterMovement : MonoBehaviour
{
	[SerializeField] private GameObject character;
	[SerializeField] private float moveSpeed = 7f;
	[SerializeField] private float jumpForce = 14f;

	private Rigidbody2D characterRigidbody;

	private void Awake()
	{
		characterRigidbody = character.GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		characterRigidbody.velocityX = Input.GetAxis("Horizontal") * moveSpeed;
		
		if (Input.GetButtonDown("Jump"))
		{
			characterRigidbody.velocityY = jumpForce;
		}
	}
}
