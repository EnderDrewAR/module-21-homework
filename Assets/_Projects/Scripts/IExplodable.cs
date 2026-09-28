using UnityEngine;

public interface IExplodable
{
    void ApplyExplosion(Vector3 center, float force, float radius);
}
