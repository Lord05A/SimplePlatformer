using System;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    [SerializeField] Player _playerPrefab;

    private Player _player;

    public void Init(Vector3 startPosition, Action OnPlayerDead)
    {
        if (_player == null)
        {
            _player = Instantiate(_playerPrefab);
        }

        _player.transform.position = startPosition;
        _player.Init(OnPlayerDead);
    }
}
