using UnityEngine;

public class InputController : MonoBehaviour
{
    private DragController _dragController;
    private ExplosionController _explosionController;
    private CameraModeSwitcher _cameraModeSwitcher;
    private Camera _camera;
    private LayerMask _groundMask;
    private float _maxDistance;
    private bool _isInitialized;

    public void Initialize(DragController dragController, ExplosionController explosionController,
        CameraModeSwitcher cameraModeSwitcher, Camera camera, LayerMask groundMask, float maxDistance)
    {
        _dragController = dragController;
        _explosionController = explosionController;
        _cameraModeSwitcher = cameraModeSwitcher;
        _camera = camera;
        _groundMask = groundMask;
        _maxDistance = maxDistance;
        _isInitialized = true;
    }

    private void Update()
    {
        if (_isInitialized == false)
            return;

        if (Input.GetKeyDown(KeyCode.F))
            _cameraModeSwitcher.SwitchNextMode();

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            _dragController.BeginDrag(ray);

        if (Input.GetMouseButton(0))
            _dragController.Drag(ray);

        if (Input.GetMouseButtonUp(0))
            _dragController.EndDrag();

        if (Input.GetMouseButtonDown(1)
            && Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _groundMask.value))
        {
            _dragController.EndDrag();
            _explosionController.Explode(hit.point);
        }
    }

    private void OnDisable()
    {
        if (_isInitialized)
            _dragController.EndDrag();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (_isInitialized && hasFocus == false)
            _dragController.EndDrag();
    }
}