using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Префабы")]
    [SerializeField] private GameObject cometPrefab;
    [SerializeField] private GameObject enemyShipPrefab;

    [Header("Настройки спавна")]
    [SerializeField] private float cometSpawnInterval = 2f;
    [SerializeField] private float shipSpawnInterval = 5f;
    [SerializeField] private int maxEnemies = 20;

    private float cometTimer;
    private float shipTimer;
    private int currentEnemyCount;

    private void Update()
    {
        if (currentEnemyCount >= maxEnemies) return;

        // Спавн комет
        cometTimer += Time.deltaTime;
        if (cometTimer >= cometSpawnInterval)
        {
            SpawnComet();
            cometTimer = 0f;
        }

        // Спавн кораблей
        shipTimer += Time.deltaTime;
        if (shipTimer >= shipSpawnInterval)
        {
            SpawnEnemyShip();
            shipTimer = 0f;
        }
    }

    private void SpawnComet()
    {
        GameObject comet = Instantiate(cometPrefab);
        Comet cometComponent = comet.GetComponent<Comet>();
        if (cometComponent != null)
        {
            cometComponent.InitializeFromEdge();
        }
        currentEnemyCount++;

        // Уменьшаем счетчик при уничтожении
        Destroy(comet, 10f); // таймаут на всякий случай
    }

    private void SpawnEnemyShip()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        Instantiate(enemyShipPrefab, spawnPosition, Quaternion.identity);
        currentEnemyCount++;
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Camera cam = Camera.main;
        float height = cam.orthographicSize * 2;
        float width = height * cam.aspect;

        return new Vector2(
            Random.Range(-width / 2, width / 2),
            Random.Range(-height / 2, height / 2)
        );
    }
}
