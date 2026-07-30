using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damageAmount = 20;

    // Triggers (ghosts, hitboxes, sensor zones)
    private void OnTriggerEnter2D(Collider2D other) => TryDamagePlayer(other.gameObject);
    private void OnTriggerStay2D(Collider2D other) => TryDamagePlayer(other.gameObject);

    // Solid Physics Collisions (solid enemies/monsters)
    private void OnCollisionEnter2D(Collision2D collision) => TryDamagePlayer(collision.gameObject);
    private void OnCollisionStay2D(Collision2D collision) => TryDamagePlayer(collision.gameObject);

    private void TryDamagePlayer(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}