using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _visual;

    private PlayerMovement _playerMovement;
    private Health _health;

    private Vector3 _leftFaceVector;
    private Vector3 _rightFaceVector;

    private void Awake()
    {
        _playerMovement = this.GetComponent<PlayerMovement>();
        _health = this.GetComponent<Health>();

        _playerMovement.Move += OnMove;

        _leftFaceVector = new Vector3(_visual.localScale.x * (-1f), _visual.localScale.y, _visual.localScale.z);
        _rightFaceVector = _visual.localScale;
    }

    public void Init(Action OnPlayerDead)
    {
        _health.Init();

        _health.Dead += OnPlayerDead;
        _health.Dead += OnDead;

        _playerMovement.UnlockInput();
    }

    private void OnMove(Vector2 movement)
    {
        int movementSign = Math.Sign(movement.x);

        if (movementSign == -1)
        {
            _visual.localScale = _leftFaceVector;
        }
        else if (movementSign == 1)
        {
            _visual.localScale = _rightFaceVector;
        }
    }   

    private void OnDead()
    {
        _playerMovement.LockInput();
    }
}
