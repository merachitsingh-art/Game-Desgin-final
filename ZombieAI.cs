using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public Transform player;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float stoppingDistance = 1f;

    [Header("Attack")]
    public int damage = 10;
    public float attackCooldown = 1f;

    private float attackTimer;

    private void Update()
    {
        // Make sure a player has been assigned
        if (player == null)
            return;

        // Find the distance between the zombie and player
        float distance = Vector2.Distance(transform.position, player.position);

        // Chase the player
        if (distance > stoppingDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Attack when close to the player
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }

                attackTimer = attackCooldown;
            }
        }
    }
}