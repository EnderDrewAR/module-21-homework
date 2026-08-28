using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private LayerMask _draggableMask;    
    [SerializeField] private LayerMask _groundMask;
    
    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private float _explosionForce = 3f;
    
    [SerializeField] private ParticleSystem _explosionEffectPrefab;

    private Camera _camera;
    
    private IDraggable _current;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void TryExplode()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, 100f, _groundMask.value))
            return;
        
        Vector3 explosionCenter = hit.point;
        
        ParticleSystem explosionEffect = Instantiate(
            _explosionEffectPrefab,
            explosionCenter + Vector3.up * 0.1f,
            Quaternion.identity
        );

        explosionEffect.Play();

        Collider[] objectsExplosion = Physics.OverlapSphere(explosionCenter, _explosionRadius, _draggableMask.value);

        foreach (Collider objectExplosion in objectsExplosion)
        {
            if (objectExplosion.TryGetComponent(out IExplosive explosive))
            {
                explosive.ApplyExplosion(explosionCenter, _explosionForce, _explosionRadius);
            }
        }

    }
}
