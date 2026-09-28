using UnityEngine;

public class ExplosionController
{
    private readonly LayerMask _explodableMask;
    private readonly float _explosionRadius;
    private readonly float _explosionForce;
    private readonly ParticleSystem _explosionEffectPrefab;

    public ExplosionController(LayerMask explodableMask, float explosionRadius,
        float explosionForce, ParticleSystem explosionEffectPrefab)
    {
        _explodableMask = explodableMask;
        _explosionRadius = explosionRadius;
        _explosionForce = explosionForce;
        _explosionEffectPrefab = explosionEffectPrefab;
    }

    public void Explode(Vector3 center)
    {
        if (_explosionEffectPrefab != null)
        {
            ParticleSystem effect = Object.Instantiate(
                _explosionEffectPrefab, center + Vector3.up * 0.1f, Quaternion.identity);
            effect.Play();
        }

        Collider[] colliders = Physics.OverlapSphere(center, _explosionRadius, _explodableMask.value);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IExplodable explodable))
                explodable.ApplyExplosion(center, _explosionForce, _explosionRadius);
        }
    }
}