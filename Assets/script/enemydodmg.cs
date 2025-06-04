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
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
            player.TakeDamage(damage, knockbackDir);
        }
    }
}
