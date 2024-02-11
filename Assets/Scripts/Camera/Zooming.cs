using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Zooming : MonoBehaviour
{
	private Camera cameraComponent;

	[SerializeField] private float minimum = 3f;
	[SerializeField] private float maximum = 10f;
	[SerializeField] private float sensitivity = 0.1f;

	private void Awake()
	{
		cameraComponent = GetComponent<Camera>();
	}

	private void Update()
	{
		Vector2 mouseWheelVector = GameInput.Instance.GetMouseWheelVector();
		if (mouseWheelVector != Vector2.zero)
		{
			float size = cameraComponent.orthographicSize;
			size += -mouseWheelVector.y * sensitivity;
			if (size > maximum)
			{
				size = maximum;
			}
			if (size < minimum)
			{
				size = minimum;
			}
			cameraComponent.orthographicSize = size;
		}
	}
}
