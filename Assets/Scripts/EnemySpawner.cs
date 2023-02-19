using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private SpawnPoint _spawnPoint;

    private void Start()
    {
        Instantiate(_prefab, _spawnPoint.GetPosition(), Quaternion.identity);
    }

}
