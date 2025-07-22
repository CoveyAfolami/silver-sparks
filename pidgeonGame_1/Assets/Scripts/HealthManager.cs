using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Optional: for level reload

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public GameObject bloodEffect;
    public GameObject deathEffect; // Optional

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar?.SetMaxHealth(maxHealth); // Safe null check
    }

    public void TakeDamage(int damage, Vector2 origin)
    {
        currentHealth -= damage;

        if (bloodEffect)
            Instantiate(bloodEffect, transform.position, Quaternion.identity);

        if (currentHealth <= 0)
        {
            Die();
        }

        healthBar?.SetCurrentHealth(currentHealth);
    }

    public void Die()
    {
        if (deathEffect)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        // Optionally reload the scene for player death
        if (CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Destroy(gameObject); // For enemies
        }
    }
}
