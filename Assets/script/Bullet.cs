using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public int damage = 25;
    public float lifetime = 5f;
    public float knockbackForce = 3f;
    public bool isPlayerBullet = true; // Set this based on who fired the bullet
    
    [Header("Effects")]
    public GameObject hitEffect; // Optional hit effect prefab
    
    void Start()
    {
        // Destroy bullet after lifetime
        Destroy(gameObject, lifetime);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if bullet should hit this target
        if (ShouldHitTarget(other))
        {
            // Create hit effect
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }
            
            // Deal damage
            DealDamage(other);
            
            // Destroy bullet
            Destroy(gameObject);
        }
        // Destroy bullet if it hits walls (optional - remove if you don't want this)
        else if (other.CompareTag("Wall"))
        {
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    
    bool ShouldHitTarget(Collider2D target)
    {
        if (isPlayerBullet)
        {
            // Player bullets hit enemies
            return target.CompareTag("Enemy");
        }
        else
        {
            // Enemy bullets hit player
            return target.CompareTag("Player");
        }
    }
    
    void DealDamage(Collider2D target)
    {
        if (isPlayerBullet && target.CompareTag("Enemy"))
        {
            // Deal damage to enemy
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Vector2 knockbackDir = (target.transform.position - transform.position).normalized;
                enemyHealth.TakeDamage(damage, knockbackDir);
            }
        }
        else if (!isPlayerBullet && target.CompareTag("Player"))
        {
            // Deal damage to player
            PlayerController player = target.GetComponent<PlayerController>();
            if (player != null)
            {
                Vector2 knockbackDir = (target.transform.position - transform.position).normalized;
                player.TakeDamage(damage, knockbackDir);
            }
        }
    }
}