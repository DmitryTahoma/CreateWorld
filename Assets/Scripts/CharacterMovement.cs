using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterMovement : MonoBehaviour
{
	[SerializeField] private GameObject character;

	private Rigidbody2D characterRigidbody;

	private void Awake()
	{
		characterRigidbody = character.GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		character.transform.position = character.transform.position + new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
	}
}
