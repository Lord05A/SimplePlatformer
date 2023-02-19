using UnityEngine;

public class EnemyChangeDirectionTrigger : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        var enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.ChangeDirection();
        }
    }
}
