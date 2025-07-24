using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HoverWander : MonoBehaviour
{
    public float moveSpeed = 2f;               // Tốc độ bay
    public float changeDirectionTime = 2f;     // Thời gian đổi hướng
    public float hoverHeight = 5f;             // Độ cao cố định
    private Vector3 moveDirection;             // Hướng di chuyển hiện tại
    private float timer;                       // Bộ đếm thời gian
    private Rigidbody rb;                      // Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;                 // Vô hiệu trọng lực để lơ lửng
        rb.freezeRotation = true;              // Không tự quay khi va chạm
        ChooseNewDirection();
    }

    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;

        if (timer <= 0f)
        {
            ChooseNewDirection();
        }

        // Vị trí mới, giữ Y cố định ở hoverHeight
        Vector3 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        newPosition.y = hoverHeight;

        rb.MovePosition(newPosition);

        // Xoay về hướng bay
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 0.1f));
        }
    }

    void ChooseNewDirection()
    {
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        timer = changeDirectionTime;
    }
}
