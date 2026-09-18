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
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
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