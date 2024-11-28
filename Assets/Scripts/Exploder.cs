using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public static Exploder exploder;

    public void Awake()
    {
        exploder = this;
    }

    public void Explode(float explosionForce, float explosionRadius, Vector3 explosionPosition)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        Rigidbody rigidbody;

        foreach (Collider collider in colliders)
        {
            collider.TryGetComponent<Rigidbody>(out rigidbody);

            rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
        }
    }

    public void Explode(List<GameObject> cubes, float explosionForce, float explosionRadius, Vector3 explosionPosition)
    {
        Rigidbody cubeRigidbody;

        foreach (GameObject cube in cubes)
        {
            cube.TryGetComponent<Rigidbody>(out cubeRigidbody);

            cubeRigidbody.AddExplosionForce(explosionForce, cube.transform.position, explosionRadius);
        }
    }
}
