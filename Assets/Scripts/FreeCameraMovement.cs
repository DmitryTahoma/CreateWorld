using UnityEngine;

public class FreeCameraMovement : MonoBehaviour
{
    private const int _keyboardSpeed = 300;

    private Vector2 _relativePosition, _draggingToK;
    private bool _rmb, _isDraggingM, _isDraggingK, 
        _isMovingUp, _isMovingLeft, _isMovingDown, _isMovingRight;
    private float _dragValue;
    private int _dragIteration;

    public float MouseSensitivity = 0.001f, //Mouse sensitivity
        KeyboardSensitivity = 0.001f; //Keyboard sensitivity
    public int MouseDragIterations = 25, //Braking length of moving mouse
        KeyboardDragIterations = 10; //Braking length of moving keyboard

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
            _isDraggingM = true;
            _dragValue = MouseSensitivity / MouseDragIterations;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            _isMovingUp = true;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            _isMovingLeft = true;
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            _isMovingDown = true;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            _isMovingRight = true;
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            _isMovingUp = false;
            _draggingToK.Set(_draggingToK.x, _keyboardSpeed);
            KeyUp();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            _isMovingLeft = false;
            _draggingToK.Set(-_keyboardSpeed, _draggingToK.y);
            KeyUp();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            _isMovingDown = false;
            _draggingToK.Set(_draggingToK.x, -_keyboardSpeed);
            KeyUp();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            _isMovingRight = false;
            _draggingToK.Set(_keyboardSpeed, _draggingToK.y);
            KeyUp();
        }
    }

    private void KeyUp()
    {
        _isDraggingK = true;
        _dragValue = KeyboardSensitivity / KeyboardDragIterations;
    }

    private void FixedUpdate()
    {
        MouseMovingHandler();
        MouseDraggingHandler();

        KeyboardMovingHandler();
        KeyboardDraggingHandler();
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
            TranslateWithSensetive(Input.mousePosition, MouseSensitivity);
        }
    }

    private void MouseDraggingHandler()
    {
        if (_isDraggingM)
        {
            if (_dragIteration == MouseDragIterations)
            {
                _isDraggingM = false;
                _dragIteration = 0;
            }
            else
            {
                _dragIteration++;
                TranslateWithSensetive(Input.mousePosition, MouseSensitivity - _dragIteration * _dragValue);
            }
        }
    }

    private void KeyboardMovingHandler()
    {
        if (_isMovingUp || _isMovingLeft || _isMovingDown || _isMovingRight)
        {
            _relativePosition = transform.position;
            if (_isMovingUp)
                TranslateWithSensetive(new Vector2(0, _keyboardSpeed), KeyboardSensitivity);
            if (_isMovingLeft)
                TranslateWithSensetive(new Vector2(-_keyboardSpeed, 0), KeyboardSensitivity);
            if (_isMovingDown)
                TranslateWithSensetive(new Vector2(0, -_keyboardSpeed), KeyboardSensitivity);
            if (_isMovingRight)
                TranslateWithSensetive(new Vector2(_keyboardSpeed, 0), KeyboardSensitivity);
        }
    }

    private void KeyboardDraggingHandler()
    {
        if(_isDraggingK)
        {
            if(_dragIteration == KeyboardDragIterations)
            {
                _isDraggingK = false;
                _dragIteration = 0;
                _draggingToK.Set(0, 0);
            }
            else
            {
                _dragIteration++;
                _relativePosition = transform.position;
                TranslateWithSensetive(_draggingToK, KeyboardSensitivity - _dragIteration * _dragValue);
            }
        }
    }
}