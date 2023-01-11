using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct SpawnBounds
{
    public float x;
    public float y;
    public float z;
}

public class Spawner<T> : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] protected T SpawnedPrefab;
    [SerializeField] protected SpawnBounds SpawnBounds;
    
    protected T Spawn(GameObject spawnedPrefab, string name)
    {
        GameObject go = Instantiate(spawnedPrefab.gameObject, SpawnPos(), Random.rotation, transform);
        go.name = name;
        
        return go.GetComponent<T>();
    }

    protected Vector3 SetRandomPosition() => SpawnPos();

    private Vector3 SpawnPos()
    {
        var x = Random.Range(-SpawnBounds.x, SpawnBounds.x);
        var y = Random.Range(-SpawnBounds.y, SpawnBounds.y);
        var z = Random.Range(-SpawnBounds.z, SpawnBounds.z);
        return new Vector3(x, y, z);
    }
}