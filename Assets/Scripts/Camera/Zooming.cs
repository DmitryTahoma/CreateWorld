using UnityEngine;

public class Zooming : MonoBehaviour
{
    private Camera _camera;
    private bool _isZoomingPlus, _isZoomingMinus;

    public float Sensitivity = 0.1f,
            Maximum = 10f,
            Minimum = 3f;

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
            _camera.orthographicSize -= _camera.orthographicSize <= Minimum ? 0 : Sensitivity;
        if (_isZoomingMinus)
            _camera.orthographicSize += _camera.orthographicSize >= Maximum ? 0 : Sensitivity;
    }
}
