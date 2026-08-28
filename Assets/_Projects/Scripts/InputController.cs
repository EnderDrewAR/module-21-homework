using UnityEngine; 

public class InputController: MonoBehaviour
{ 
    [SerializeField] private DragController _dragController;
    [SerializeField] private ExplosionController _explosionController;

    private void Update()
    { 
        if (Input.GetMouseButtonDown(0))
            _dragController.BeginDrag();

        if (Input.GetMouseButton(0))
            _dragController.Drag();

        if (Input.GetMouseButtonUp(0))
            _dragController.EndDrag();

        if (Input.GetMouseButtonDown(1))
            _explosionController.TryExplode();
    }
}