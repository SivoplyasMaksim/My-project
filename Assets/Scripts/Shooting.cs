using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;

    void Start()
    {
        mainCam = Camera.main;
        timer = 0f;
        canFire = true;
    }

    void Update()
    {
        // Получаем позицию мыши в мировых координатах
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Вычисляем направление (игнорируем Z для 2D)
        Vector2 direction = new Vector2(
            mouseWorldPos.x - transform.position.x,
            mouseWorldPos.y - transform.position.y
        ).normalized;

        // Поворот
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Таймер стрельбы
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        // Стрельба
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;

            GameObject bulletInstance = Instantiate(bullet, bulletTransform.position, Quaternion.identity);

            PlayerBullet playerBullet = bulletInstance.GetComponent<PlayerBullet>();
            if (playerBullet != null)
            {
                playerBullet.Initialize(direction);
            }
        }
    }
}