using UnityEngine;

public class DragController
{
    private readonly LayerMask _draggableMask;
    private readonly LayerMask _groundMask;
    private readonly float _maxDistance;
    private IDraggable _currentItem;

    public DragController(LayerMask draggableMask, LayerMask groundMask, float maxDistance)
    {
        _draggableMask = draggableMask;
        _groundMask = groundMask;
        _maxDistance = maxDistance;
    }

    public void BeginDrag(Ray ray)
    {
        EndDrag();

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _draggableMask.value) == false)
            return;

        if (hit.collider.TryGetComponent(out IDraggable draggable) == false)
            return;

        _currentItem = draggable;
        _currentItem.BeginDrag();
    }

    public void Drag(Ray ray)
    {
        if (_currentItem == null)
            return;

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _groundMask.value))
            _currentItem.Drag(hit.point);
    }

    public void EndDrag()
    {
        if (_currentItem == null)
            return;

        _currentItem.EndDrag();
        _currentItem = null;
    }
}