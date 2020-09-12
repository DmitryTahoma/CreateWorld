using UnityEngine;

public class Zooming : MonoBehaviour
{
    private Camera _camera;
    private bool _isZoomingPlus, _isZoomingMinus;

    [SerializeField] private float _sensitivity = 0.1f,
            _maximum = 10f,
            _minimum = 3f;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
            _isZoomingPlus = true;
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
            _isZoomingMinus = true;

        if (Input.GetKeyUp(KeyCode.Plus) || Input.GetKeyUp(KeyCode.KeypadPlus))
            _isZoomingPlus = false;
        if (Input.GetKeyUp(KeyCode.Minus) || Input.GetKeyUp(KeyCode.KeypadMinus))
            _isZoomingMinus = false;
    }

    private void FixedUpdate()
    {
        if (_isZoomingPlus)
            _camera.orthographicSize -= _camera.orthographicSize <= _minimum ? 0 : _sensitivity;
        if (_isZoomingMinus)
            _camera.orthographicSize += _camera.orthographicSize >= _maximum ? 0 : _sensitivity;
    }
}
