using UnityEngine;

public class KeyboardMoving : MonoBehaviour
{
    private const int _speed = 300;

    private Vector2 _relativePosition, _draggingTo;
    private bool _isDragging, _isMovingUp, _isMovingLeft, _isMovingDown, _isMovingRight;
    private float _dragValue;
    private int _dragIteration;

    [SerializeField] private float _sensitivity = 0.001f;
    [SerializeField] private int _dragLength = 10;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            _isMovingUp = true;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            _isMovingLeft = true;
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            _isMovingDown = true;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            _isMovingRight = true;
        if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) && _isMovingUp)
        {
            _isMovingUp = false;
            _draggingTo.Set(_draggingTo.x, _speed);
            KeyUp();
        }
        if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) && _isMovingLeft)
        {
            _isMovingLeft = false;
            _draggingTo.Set(-_speed, _draggingTo.y);
            KeyUp();
        }
        if ((Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) && _isMovingDown)
        {
            _isMovingDown = false;
            _draggingTo.Set(_draggingTo.x, -_speed);
            KeyUp();
        }
        if ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) && _isMovingRight)
        {
            _isMovingRight = false;
            _draggingTo.Set(_speed, _draggingTo.y);
            KeyUp();
        }
    }

    private void FixedUpdate()
    {
        KeyboardMovingHandler();
        KeyboardDraggingHandler();
    }

    private void KeyUp()
    {
        _isDragging = true;
        _dragValue = _sensitivity / _dragLength;
    }

    private void TranslateWithSensetive(Vector2 directionArrow, float sensetive)
    {
        Vector2 cameraPosition = transform.position;
        Vector2 direction = directionArrow - _relativePosition + cameraPosition;
        float distance = Mathf.Sqrt(Mathf.Pow(_relativePosition.x - directionArrow.x, 2) + Mathf.Pow(_relativePosition.y - directionArrow.y, 2));

        transform.Translate(direction.normalized * sensetive * distance);
    }

    private void KeyboardMovingHandler()
    {
        if (_isMovingUp || _isMovingLeft || _isMovingDown || _isMovingRight)
        {
            _relativePosition = transform.position;
            if (_isMovingUp)
                TranslateWithSensetive(new Vector2(0, _speed), _sensitivity);
            if (_isMovingLeft)
                TranslateWithSensetive(new Vector2(-_speed, 0), _sensitivity);
            if (_isMovingDown)
                TranslateWithSensetive(new Vector2(0, -_speed), _sensitivity);
            if (_isMovingRight)
                TranslateWithSensetive(new Vector2(_speed, 0), _sensitivity);
        }
    }

    private void KeyboardDraggingHandler()
    {
        if (_isDragging)
        {
            if (_dragIteration == _dragLength)
            {
                _isDragging = false;
                _dragIteration = 0;
                _draggingTo.Set(0, 0);
            }
            else
            {
                _dragIteration++;
                _relativePosition = transform.position;
                TranslateWithSensetive(_draggingTo, _sensitivity - _dragIteration * _dragValue);
            }
        }
    }
}
