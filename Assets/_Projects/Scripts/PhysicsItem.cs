using System;
using UnityEngine;

public class PhysicsItem : MonoBehaviour, IDraggable, IExplosive
{
    [SerializeField] private float _dragSmoothness = 15f;

    private const float _dragHeight = 0.25f;
    
    private bool _isDragging;
    private Vector3 _targetPosition;
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (!_isDragging)
            return;

        float t = 1f - Mathf.Exp(
            -_dragSmoothness * Time.fixedDeltaTime
        );

        Vector3 newPosition = Vector3.Lerp(
            _rigidbody.position,
            _targetPosition,
            t
        );

        _rigidbody.MovePosition(newPosition);
    }

    public void BeginDrag()
    {
        _isDragging = true;
        _rigidbody.isKinematic = true;
    }

    public void Drag(Vector3 position)
    {
        position.y += _dragHeight;
        _targetPosition = position;
    }

    public void EndDrag()
    {
        _isDragging = false;
        _rigidbody.isKinematic = false;
    }

    public void ApplyExplosion(Vector3 center, float force, float radius)
    {
        _isDragging = false;
        _rigidbody.isKinematic = false;

        _rigidbody.AddExplosionForce(
            force,
            center,
            radius,
            0.5f,
            ForceMode.Impulse
        );
    }
}
