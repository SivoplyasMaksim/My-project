using UnityEngine;

public class EnemyShip : Enemy
{
    [Header("Движение")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stopDistance = 5f;

    [Header("Стрельба")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] private int bulletDamage = 20;

    private Transform playerTransform;
    private float fireTimer;

    protected override void Awake()
    {
        base.Awake();

        // ✅ Ищем игрока по тегу
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (playerTransform == null)
        {
            Debug.LogError("❌ Игрок не найден! Убедитесь, что у игрока установлен тег 'Player'");
        }

        if (firePoint == null)
        {
            Debug.LogError("❌ FirePoint не назначен в Inspector!");
        }

        if (bulletPrefab == null)
        {
            Debug.LogError(" BulletPrefab не назначен в Inspector!");
        }
    }

    private void Update()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > stopDistance)
        {
            MoveTowardsPlayer();
        }
        else
        {
            RotateTowardsPlayer();
            TryShoot();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(
            transform.position,
            playerTransform.position,
            moveSpeed * Time.deltaTime
        );

        // Поворот в направлении движения
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void RotateTowardsPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void TryShoot()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= 1f / fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null || playerTransform == null) return;

        // ✅ Quaternion.identity = без поворота
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
        if (enemyBullet != null)
        {
            enemyBullet.Initialize(bulletSpeed, bulletDamage, playerTransform);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            healthComponent.TakeDamage(health);
        }
    }
}