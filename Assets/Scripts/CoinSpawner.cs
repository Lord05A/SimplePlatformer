using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private SpawnPoint _spawnPoint;

    private void Start()
    {
        Instantiate(_prefab, _spawnPoint.GetPosition(), Quaternion.identity);
    }

}
