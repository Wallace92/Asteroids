using UnityEngine;

public class Spawner<T> : MonoBehaviour
{
    public T SpawnedPrefab;
    protected T Spawn(GameObject spawnedPrefab)
    {
        GameObject go = Instantiate(spawnedPrefab.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        T t = go.GetComponent<T>();
        return t;
    }
}