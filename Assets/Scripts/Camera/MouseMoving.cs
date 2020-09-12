using UnityEngine;

public class MouseMoving : MonoBehaviour
{
    private Vector2 _relativePosition;
    private bool _rmb, _isDragging;
    private float _dragValue;
    private int _dragIteration;

    public float Sensitivity = 0.001f;
    public int DragLength = 25;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !_rmb)
        {
            _relativePosition = Input.mousePosition;
            _rmb = true;
        }
        if (Input.GetMouseButtonUp(1) && _rmb)
        {
            _rmb = false;
            _isDragging = true;
            _dragValue = Sensitivity / DragLength;
        }
    }

    private void FixedUpdate()
    {
        MouseMovingHandler();
        MouseDraggingHandler();
    }

    private void TranslateWithSensetive(Vector2 directionArrow, float sensetive)
    {
        Vector2 cameraPosition = transform.position;
        Vector2 direction = directionArrow - _relativePosition + cameraPosition;
        float distance = Mathf.Sqrt(Mathf.Pow(_relativePosition.x - directionArrow.x, 2) + Mathf.Pow(_relativePosition.y - directionArrow.y, 2));

        transform.Translate(direction.normalized * sensetive * distance);
    }

    private void MouseMovingHandler()
    {
        if (_rmb)
        {
            TranslateWithSensetive(Input.mousePosition, Sensitivity);
        }
    }

    private void MouseDraggingHandler()
    {
        if (_isDragging)
        {
            if (_dragIteration == DragLength)
            {
                _isDragging = false;
                _dragIteration = 0;
            }
            else
            {
                _dragIteration++;
                TranslateWithSensetive(Input.mousePosition, Sensitivity - _dragIteration * _dragValue);
            }
        }
    }
}
