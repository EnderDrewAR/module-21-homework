using UnityEngine;

public interface IDraggable 
{
    void BeginDrag();
    void Drag(Vector3 position);
    void EndDrag();
}
