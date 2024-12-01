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

    public void ExplodeAround(Cube cube)
    {
        float cubeVolume = cube.transform.localScale.x * cube.transform.localScale.y * cube.transform.localScale.z;
        float multiplier = cubeVolume / cube.CubeStartVolume + 1;

        Collider[] colliders = Physics.OverlapSphere(cube.transform.position, cube.ExplosionRadius * multiplier);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(cube.ExplosionForce * multiplier, cube.transform.position, cube.ExplosionRadius * multiplier);
            }                    
        }
    }
}
