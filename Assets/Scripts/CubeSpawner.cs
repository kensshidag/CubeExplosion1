using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner cubeSpawner;

    private void Awake()
    {
        cubeSpawner = this;
    }

    public void SpawnCubes(GameObject cube, Vector3 spawnPosition, Vector3 spawnCubeScale, int spawnCount, out List<GameObject> cubes)
    {
        cubes = new List<GameObject>();
        Renderer renderer;

        for (int i = 0; i < spawnCount; ++i)
        {
            GameObject newCube = Instantiate(cube, spawnPosition, Quaternion.identity);
            newCube.transform.localScale = spawnCubeScale;
            newCube.TryGetComponent<Renderer>(out renderer);
            SetRandomColor(renderer);
            cubes.Add(newCube);
        }
    }

    private void SetRandomColor(Renderer renderer)
    {
        renderer.material.color = new Color(
            Random.value,
            Random.value,
            Random.value);
    }
}
