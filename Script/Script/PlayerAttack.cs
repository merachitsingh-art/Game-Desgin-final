using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;      // Drag AttackPoint object here
    public float attackRange = 0.8f;   // Size of attack radius
    public float attackDamage = 5f;    // Damage per hit
    public float attackRate = 2f;      // Attacks per second
    private float nextAttackTime = 0f;

    void Update()
    {
        // Press Space or Left Mouse Click to attack
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // Detect all colliders inside the attack circle
        Collider2D[] hitMonsters = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        // Deal damage to any monster caught in the attack
        foreach (Collider2D enemy in hitMonsters)
        {
            MonsterAI monster = enemy.GetComponent<MonsterAI>();
            if (monster != null)
            {
                monster.TakeDamage(attackDamage);
            }
        }
    }

    // Displays a red circle in Scene view showing attack range
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}