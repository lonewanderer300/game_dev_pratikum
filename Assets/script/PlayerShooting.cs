using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f; // Shots per second
    public float bulletSpeed = 10f;
    public float detectionRange = 15f;
    public LayerMask enemyLayerMask = -1; // Which layers are considered enemies
    

    
    private float nextFireTime = 0f;
    private Transform nearestEnemy;
    
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                Shoot(nearestEnemy.position);
                nextFireTime = Time.time + (1f / fireRate);
            }
        }
    }
    
    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearest = null;
        float shortestDistance = detectionRange;
        
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearest = enemy.transform;
            }
        }
        
        return nearest;
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        // Draw line to nearest enemy if found
        if (nearestEnemy != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, nearestEnemy.position);
        }
    }
}