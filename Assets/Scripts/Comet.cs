using UnityEngine;

public class Comet : Enemy
{
    [SerializeField] private float speed = 5f;

    private Vector2 direction;
    private Transform playerTransform;

    protected override void Awake()
    {
        base.Awake();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomDirection();
    }

    private void Update()
    {
        // Движение по прямой
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void SetRandomDirection()
    {
        // Случайное направление (нормализованный вектор)
        direction = Random.insideUnitCircle.normalized;
    }

    // Альтернатива: спавн за экраном с направлением к центру
    public void InitializeFromEdge()
    {
        Vector2 spawnPosition = GetRandomEdgePosition();
        transform.position = spawnPosition;

        // Направление к центру экрана с небольшим отклонением
        Vector2 toCenter = (Vector2.zero - spawnPosition).normalized;
        direction = RotateVector(toCenter, Random.Range(-30f, 30f));
    }

    private Vector2 GetRandomEdgePosition()
    {
        Camera cam = Camera.main;
        float height = cam.orthographicSize * 2;
        float width = height * cam.aspect;

        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: return new Vector2(Random.Range(-width / 2, width / 2), cam.orthographicSize + 1); // верх
            case 1: return new Vector2(Random.Range(-width / 2, width / 2), -cam.orthographicSize - 1); // низ
            case 2: return new Vector2(cam.orthographicSize * cam.aspect + 1, Random.Range(-height / 2, height / 2)); // право
            case 3: return new Vector2(-cam.orthographicSize * cam.aspect - 1, Random.Range(-height / 2, height / 2)); // лево
            default: return Vector2.zero;
        }
    }

    private Vector2 RotateVector(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        // Уничтожаем комету при столкновении с игроком
        if (collision.gameObject.CompareTag("Player"))
        {
            healthComponent.TakeDamage(health); // мгновенная смерть
        }
    }
}