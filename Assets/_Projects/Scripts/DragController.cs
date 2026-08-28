using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] private LayerMask _draggableMask;
    [SerializeField] private LayerMask _groundMask;
    
    
    private Camera _camera;
    
    private IDraggable _currentItem;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    public void BeginDrag()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, 100f, _draggableMask.value))
            return;
        
        if (!hit.collider.TryGetComponent(out IDraggable draggable))
            return;
        
        _currentItem = draggable;
        _currentItem.BeginDrag();
    }

    public void Drag()
    {
        if (_currentItem == null)
            return;
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _groundMask.value))
        {
            _currentItem.Drag(hit.point);
        }
    }

    public void EndDrag()
    {
        if (_currentItem == null)
            return;

        _currentItem.EndDrag();
        _currentItem = null; 
    }

}
