using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemydodmg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int damage = 20;
    public float knockbackForce = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
{
    Debug.Log("Trigger entered by: " + collision.name);
    
    // Check if collision is player OR player's parent
    PlayerController player = collision.GetComponent<PlayerController>();
    if (player == null)
    {
        player = collision.GetComponentInParent<PlayerController>();
        Debug.Log("Checking parent for PlayerController");
    }
    
    if (player != null)
    {
        Debug.Log("Player found! Dealing damage");
        Vector2 knockbackDir = (player.transform.position - transform.position).normalized;
        player.TakeDamage(damage, knockbackDir);
    }
    else
    {
        Debug.Log("No PlayerController found on: " + collision.name);
    }
}
}
