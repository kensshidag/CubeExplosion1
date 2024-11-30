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
            cube.Died += UnregisterCube;
            cube.CubeSplitRequested += SpawnCubes;
        }
    }

    private void OnDisable()
    {
        foreach (var cube in _cubes)
        {
            cube.Died -= UnregisterCube;
            cube.CubeSplitRequested -= SpawnCubes;
        }
    }

    private void SpawnCubes(Cube cube)
    {
        UnregisterCube(cube);

        int cubesToSpawn = Random.Range(_minCubes, _maxCubes + 1);

        for (int i = 0; i < cubesToSpawn; i++)
        {
            Cube newCube = Instantiate(cube, cube.transform.position, Quaternion.identity);
            newCube.InitializeCube();
            newCube.Died += UnregisterCube;  
            newCube.CubeSplitRequested += SpawnCubes;
            _cubes.Add(newCube);
        }  

        _exploder.Explode(_cubes, cube.ExplosionForce, cube.ExplosionRadius, cube.transform.position);
    }

    private void UnregisterCube(Cube cube)
    {
        cube.Died -= UnregisterCube;
        cube.CubeSplitRequested -= SpawnCubes;
        _cubes.Remove(cube);
    }
}
