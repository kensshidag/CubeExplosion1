using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 300f;
    [SerializeField] private float _explosionRadius = 2f;
    [SerializeField] private float _splitChance = 1f;

    private float _scaleDivider = 2f;
    private float _chanceDivider = 2f;
    private int _minCubes = 2;
    private int _maxCubes = 6;

    private List<GameObject> _cubes;

    private void OnMouseDown()
    {
        float chanceToDestroy = Random.Range(0f, 1f);

        if (chanceToDestroy > _splitChance)
        {
            Destroy(gameObject);
        }
        else
        {
            _splitChance /= _chanceDivider;

            Destroy(gameObject);

            int cubeSpawnCount = Random.Range(_minCubes, _maxCubes + 1);

            CubeSpawner.cubeSpawner.SpawnCubes(this.gameObject, this.transform.position, transform.localScale / _scaleDivider, cubeSpawnCount, out _cubes);

            Exploder.exploder.Explode(_cubes, _explosionForce, _explosionRadius, this.transform.position);           
        }
    }
}
