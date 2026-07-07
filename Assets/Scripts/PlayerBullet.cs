using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 25;
    [SerializeField] private float lifetime = 3f;

    private Vector2 direction;

    public void Initialize(Vector2 direction)
    {
        Debug.Log("Bullet direction: " + direction); // ✅ Для отладки
        this.direction = direction.normalized;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // ✅ Двигаем пулю в заданном направлении
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}