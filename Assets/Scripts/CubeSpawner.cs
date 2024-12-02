using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private List<Cube> _cubes;

    [SerializeField] private Exploder _exploder;

    private int _minCubes = 2;
    private int _maxCubes = 6;

    private void OnEnable()
    {
        foreach (var cube in _cubes)
        {
            cube.Died += ExplodeCube;
            cube.CubeSplitRequested += SpawnCubes;
        }
    }

    private void OnDisable()
    {
        foreach (var cube in _cubes)
        {
            cube.Died -= ExplodeCube;
            cube.CubeSplitRequested -= SpawnCubes;
        }
    }

    private void ExplodeCube(Cube cube)
    {
        _exploder.ExplodeAround(cube);
        UnregisterCube(cube);
    }

    private void SpawnCubes(Cube cube)
    {
        UnregisterCube(cube);

        int cubesToSpawn = Random.Range(_minCubes, _maxCubes + 1);

        List<Cube> cubesToExplode = new();

        for (int i = 0; i < cubesToSpawn; i++)
        {
            Cube newCube = Instantiate(cube, cube.transform.position, Quaternion.identity);
            newCube.Initialize();
            newCube.Died += UnregisterCube;
            newCube.Died += ExplodeCube;
            newCube.CubeSplitRequested += SpawnCubes;
            _cubes.Add(newCube);
            cubesToExplode.Add(newCube);
        }  

        _exploder.Explode(cubesToExplode, cube.ExplosionForce, cube.ExplosionRadius, cube.transform.position);
    }

    private void UnregisterCube(Cube cube)
    {
        cube.Died -= ExplodeCube;
        cube.CubeSplitRequested -= SpawnCubes;
        _cubes.Remove(cube);
    }
}
