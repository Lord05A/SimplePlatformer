using UnityEngine;

public class EnemyChangeDirectionTrigger : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.ChangeDirection();
        }
    }
}
