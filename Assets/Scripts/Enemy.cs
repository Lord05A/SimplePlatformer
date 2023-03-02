using UnityEngine;

[RequireComponent(typeof(PhysicsMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private PhysicsMovement _physicsMovement;

    private Vector2 _leftDirection = Vector3.left;
    private Vector2 _rightDirection = Vector3.right;

    private Vector2 _currentDirection;

    private void Start()
    {
        float fiftyPercentChance = 0.5f;

        _currentDirection = Random.value > fiftyPercentChance
            ? _leftDirection 
            : _rightDirection;
    }

    private void Update()
    {        
        _physicsMovement.SetTargetVelocity(_currentDirection, false);
    }

    public void ChangeDirection()
    {
        _currentDirection = _currentDirection == _leftDirection 
            ? _rightDirection 
            : _leftDirection;
    }
}
