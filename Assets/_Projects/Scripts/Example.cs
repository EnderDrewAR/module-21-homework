using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField] private InputController _inputController;
    [SerializeField] private Camera _camera;
    [SerializeField] private List<CinemachineVirtualCamera> _cameras;
    [SerializeField] private LayerMask _draggableMask;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private ParticleSystem _explosionEffectPrefab;

    private void Awake()
    {
        DragController dragController = new DragController(_draggableMask, _groundMask, _maxDistance);
        ExplosionController explosionController = new ExplosionController(
            _draggableMask, _explosionRadius, _explosionForce, _explosionEffectPrefab);
        CameraModeSwitcher cameraModeSwitcher = new CameraModeSwitcher(_cameras);

        cameraModeSwitcher.SwitchNextMode();
        _inputController.Initialize(dragController, explosionController, cameraModeSwitcher,
            _camera, _groundMask, _maxDistance);
    }
}