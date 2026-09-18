using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] private int scoreValue = 100;
    protected int currentHealth; // ✅ protected вместо private

    public UnityEvent OnDeath;
    public UnityEvent<int> OnDamageTaken;

    protected virtual void Awake() // ✅ protected virtual вместо private
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage) // ✅ virtual
    {
        currentHealth -= damage;
        OnDamageTaken?.Invoke(damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die() // ✅ protected virtual
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
    public void SetScoreValue(int value)
    {
        scoreValue = value;
    }
}