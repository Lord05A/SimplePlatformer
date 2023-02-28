using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private const string StateParameter = "State";

    [SerializeField] private PhysicsMovement _playerMovement;

    private Animator _animator;    

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();

        _playerMovement.PlayerStateChanged += OnPlayerStateChanged;
    }

    private void OnPlayerStateChanged(PlayerState playerState)
    {
        SetStateParameter(playerState);
    }   

    private void SetStateParameter(PlayerState state)
    {
        _animator.SetInteger(StateParameter, (int)state);
    }    
}
