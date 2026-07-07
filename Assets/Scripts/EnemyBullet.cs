using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float lifetime = 3f;

    private float speed;
    private int damage;
    private Vector2 direction;

    public void Initialize(float speed, int damage, Transform target)
    {
        this.speed = speed;
        this.damage = damage;

        // ✅ Только вычисляем направление, БЕЗ поворота
        if (target != null)
        {
            direction = (target.position - transform.position).normalized;
        }

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // ✅ Просто летим в направлении игрока
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}