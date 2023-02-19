using System;
using UnityEngine;

[RequireComponent(typeof(PhysicsMovement))]
public class PlayerMovement : MonoBehaviour
{
    public event Action<Vector2> Move;

    [SerializeField] private PhysicsMovement _physicsMovement;

    private bool _isLockedInput = true;

    public void Awake()
    {
        _physicsMovement = this.GetComponent<PhysicsMovement>();
    }

    public void LockInput()
    {
        _isLockedInput = true;
        _physicsMovement.SetTargetVelocity(Vector2.zero, false);
    }

    public void UnlockInput()
    {
        _isLockedInput = false;
    }

    private void Update()
    {
        if (_isLockedInput)
        {
            return;
        }

        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);

        bool isTryJump = Input.GetKey(KeyCode.Space);       

        _physicsMovement.SetTargetVelocity(targetVelocity, isTryJump);

        if (targetVelocity.sqrMagnitude > 0)
        {
            Move?.Invoke(targetVelocity);
        }
    }    
}
