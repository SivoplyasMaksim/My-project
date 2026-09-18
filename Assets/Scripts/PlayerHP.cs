using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [Header("Настройки игрока")]
    [SerializeField] private float invincibilityDuration = 1f;

    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;

    protected override void Awake() // ✅ override
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void TakeDamage(int damage) // ✅ override
    {
        if (isInvincible) return;

        base.TakeDamage(damage);

        if (currentHealth > 0)
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        float timer = 0f;
        while (timer < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    protected override void Die()
    {
        Debug.Log("Игрок уничтожен!");

        // ✅ Сохраняем рекорд
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.SaveHighScore();
        }

        // Показываем меню Game Over
        if (GameOverMenu.Instance != null)
        {
            GameOverMenu.Instance.ShowMenu();
        }

        Time.timeScale = 0f;
        enabled = false;
    }
}