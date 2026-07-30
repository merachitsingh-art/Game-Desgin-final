using UnityEngine;
using UnityEngine.UI;

public class MonsterAI : MonoBehaviour
{
    [Header("Monster Stats")]
    public float maxHealth = 50f;
    private float currentHealth;
    public float moveSpeed = 3f;

    [Header("Detection & Combat Settings")]
    public float detectionRadius = 5f; // Monster will ignore player if further than this
    public float attackRadius = 1.2f;    // Distance monster stops to attack
    public float attackRate = 0.5f;      // Attacks per second (0.5 = once every 2 seconds)
    private float nextAttackTime = 0f;

    [Header("UI Setup")]
    public Slider healthSlider;

    [Header("Player Tracking")]
    public Transform player;

    void Start()
    {
        currentHealth = maxHealth;

        // Auto-assign player if the slot is empty
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        // Setup initial health bar value
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 1. Only do anything if player is inside detection range
        if (distanceToPlayer <= detectionRadius)
        {
            // 2. If close enough to attack, stop and attack on cooldown
            if (distanceToPlayer <= attackRadius)
            {
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + (1f / attackRate);
                }
            }
            // 3. Otherwise, chase the player
            else
            {
                transform.position = Vector2.MoveTowards(
                    transform.position, 
                    player.position, 
                    moveSpeed * Time.deltaTime
                );
            }
        }
        // If outside detectionRadius, the monster does nothing!
    }

    void Attack()
    {
        // Add your damage to player / attack animation trigger here
        Debug.Log(gameObject.name + " attacks!");
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // Draws a blue circle in Scene View to visually tune detection range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}