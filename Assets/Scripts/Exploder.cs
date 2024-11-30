using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void Explode(List<Cube> cubes, float explosionForce, float explosionRadius, Vector3 explosionPosition)
    {
        foreach (Cube cube in cubes)
        {
            if (cube.TryGetComponent(out Rigidbody cubeRigidbody))
            {
                cubeRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            }
        }
    }
}
