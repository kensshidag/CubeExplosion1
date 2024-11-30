using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private float _splitChance = 1.0f;
    [field:SerializeField] public float ExplosionForce { get; private set; } = 300f;
    [field:SerializeField] public float ExplosionRadius { get; private set; } = 2f;

    private float _scaleDivider = 2f;
    private float _chanceDivider = 2f;

    private Renderer _renderer;

    public event Action<Cube> CubeSplitRequested;
    public event Action<Cube> Died;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        if (CanSplit())
        {
            CubeSplitRequested?.Invoke(this);
        }
        else
        {
            Died?.Invoke(this);
        }

        Destroy(gameObject);
    }

    public void InitializeCube()
    {
        _splitChance /= _chanceDivider;
        transform.localScale /= _scaleDivider;
        _renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    private bool CanSplit()
    {
        float randomValue = UnityEngine.Random.value;

        return _splitChance >= randomValue;
    }
}
