using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public Transform attackOrigin;
    public float attackRadius = 1f;
    public LayerMask enemyMask;

    public float cooldownTime = 0.5f;
    private float cooldownTimer = 0f;

    public int attackDamage = 25;
    public Animator animator;

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            if (Input.GetKey(KeyCode.K))
            {
                animator?.SetTrigger("Melee");

                Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, enemyMask);
                foreach (var enemy in enemiesInRange)
                {
                    HealthManager hm = enemy.GetComponent<HealthManager>();
                    if (hm != null)
                    {
                        hm.TakeDamage(attackDamage, transform.position);
                    }
                }

                cooldownTimer = cooldownTime;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackOrigin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
        }
    }
}
