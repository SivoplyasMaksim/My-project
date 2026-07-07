using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health = 50;
    [SerializeField] protected int damageOnCollision = 100;

    protected Health healthComponent;

    protected virtual void Awake()
    {
        healthComponent = GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.OnDeath.AddListener(OnEnemyDeath);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // Наносим урон игроку при столкновении
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageOnCollision);
            }
        }
    }

    protected virtual void OnEnemyDeath()
    {
        // Можно добавить эффекты (взрыв, частицы)
        Destroy(gameObject);
    }
}