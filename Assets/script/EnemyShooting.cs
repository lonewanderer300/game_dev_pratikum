using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f; // Shots per second
    public float bulletSpeed = 8f;
    public float detectionRange = 12f;
    public LayerMask playerLayerMask = -1; // Which layers are considered player
    

    
    private float nextFireTime = 0f;
    private Transform player;
    
    void Start()
    {
        // Find player at start
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }
    
    void Update()
    {
        if (player == null) return;
        
        if (Time.time >= nextFireTime)
        {
            if (IsPlayerInRange())
            {
                Shoot(player.position);
                nextFireTime = Time.time + (1f / fireRate);
            }
        }
    }
    
    bool IsPlayerInRange()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        return distance <= detectionRange;
    }
    

    
    void Shoot(Vector3 targetPosition)
    {
        if (bulletPrefab == null || firePoint == null) return;
        
        Vector2 direction = (targetPosition - firePoint.position).normalized;
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        
        // Set bullet direction and speed
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = direction * bulletSpeed;
        }
        
        // Rotate bullet to face direction (optional, for visual purposes)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    // Optional: Draw detection range in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        // Draw line to player if in range
        if (player != null && IsPlayerInRange())
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}