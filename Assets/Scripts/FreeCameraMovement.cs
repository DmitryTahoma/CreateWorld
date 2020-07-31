using UnityEngine;

public class FreeCameraMovement : MonoBehaviour
{
    private Camera MovingCamera;
    private Vector2 _relativePosition;
    private bool _relativePositionIsActive = false;
    private bool _isDragging = false;
    private float _dragValue;
    private int _dragIteration = 0;

    public float Sensetive = 0.001f; //Mouse sensitivity
    public int DragIterations = 25; //Braking length

    private void Start()
    {
        MovingCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && !_relativePositionIsActive)
        {
            _relativePosition = Input.mousePosition;
            _relativePositionIsActive = true;
        }
        if(Input.GetMouseButtonUp(1) && _relativePositionIsActive)
        {
            _relativePositionIsActive = false;
            _isDragging = true;
            _dragValue = Sensetive / DragIterations;
        }
    }

    private void FixedUpdate()
    {
        MouseMovingHandler();
        MouseDraggingHandler();        
    }

    private void TranslateWithSensetive(float sensetive)
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 cameraPosition = MovingCamera.transform.position;
        Vector2 direction = mousePosition - _relativePosition + cameraPosition;
        float distance = Mathf.Sqrt(Mathf.Pow(_relativePosition.x - mousePosition.x, 2) + Mathf.Pow(_relativePosition.y - mousePosition.y, 2));

        transform.Translate(direction.normalized * sensetive * distance);
    }

    private void MouseMovingHandler()
    {
        if (_relativePositionIsActive)
        {
            TranslateWithSensetive(Sensetive);
        }
    }

    private void MouseDraggingHandler()
    {
        if (_isDragging)
        {
            if (_dragIteration == DragIterations)
            {
                _isDragging = false;
                _dragIteration = 0;
            }
            else
            {
                _dragIteration++;
                TranslateWithSensetive(Sensetive - _dragIteration * _dragValue);
            }
        }
    }
}