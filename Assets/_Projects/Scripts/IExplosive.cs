using UnityEngine;

public interface IExplosive
{
    void ApplyExplosion(Vector3 center, float force, float radius);
}