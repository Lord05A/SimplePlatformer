using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player != null)
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        Destroy(this.gameObject);
    }
}
