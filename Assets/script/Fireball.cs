using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!other.isTrigger) // prevent hitting non-enemy objects
        {
            Destroy(gameObject);
        }
    }
}
