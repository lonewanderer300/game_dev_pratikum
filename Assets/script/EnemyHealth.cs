using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 50;
    private int currentHealth;
    
    [Header("Knockback Settings")]
    public float knockbackTime = 0.2f;
    public float knockbackThrust = 5f;
    
    [Header("Death Settings")]
    public GameObject deathEffect; // Optional death effect
    
    private bool isKnockedBack = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage, Vector2 knockbackDirection)
    {
        if (isKnockedBack) return; // Prevent stacking knockback
        
        currentHealth -= damage;
        
        // Flash effect when taking damage
        StartCoroutine(FlashEffect());
        
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Apply knockback
            StartCoroutine(HandleKnockback(knockbackDirection.normalized));
        }
    }
    
    void Die()
    {
        // Create death effect
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        
        // You might want to add score, drop items, etc. here
        
        Destroy(gameObject);
    }
    
    IEnumerator HandleKnockback(Vector2 direction)
    {
        isKnockedBack = true;
        rb.velocity = Vector2.zero;
        
        Vector2 force = direction * knockbackThrust * rb.mass;
        rb.AddForce(force, ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
    
    IEnumerator FlashEffect()
    {
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
        }
    }
    
    // Property to check if enemy is knocked back (useful for AI)
    public bool IsKnockedBack => isKnockedBack;
}