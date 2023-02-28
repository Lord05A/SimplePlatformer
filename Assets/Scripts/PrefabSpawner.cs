using UnityEngine;

public class PrefabSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private SpawnPoint _spawnPoint;

    private void Start()
    {
        Instantiate<T>(_prefab, _spawnPoint.GetPosition(), Quaternion.identity);
    }

}
