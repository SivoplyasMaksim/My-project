using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;

    private Rigidbody2D rigidbody;
    private Camera mainCamera;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();

        inputVector = inputVector.normalized;

        rigidbody.MovePosition(rigidbody.position + inputVector * (moveSpeed * Time.fixedDeltaTime));

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mouseWorldPos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
