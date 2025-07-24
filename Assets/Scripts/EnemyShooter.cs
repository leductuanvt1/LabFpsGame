using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject fireballPrefab;        // Gán prefab fireball qua Inspector
    public Transform player;                 // Tham chiếu tới player trong scene
    public float fireInterval = 3f;          // Thời gian giữa các lần bắn
    public float fireballSpeed = 10f;        // Tốc độ viên đạn

    private float fireTimer;

    void Update()
    {
        if (player == null) return;

        fireTimer -= Time.deltaTime;

        // Quay mặt về player
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // không quay lên xuống
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);

        if (fireTimer <= 0f)
        {
            ShootAtPlayer();
            fireTimer = fireInterval;
        }
    }

    void ShootAtPlayer()
    {
        GameObject fireball = Instantiate(
                    fireballPrefab,
                    transform.position + transform.forward * 1f, // đẩy ra trước 1m
                    Quaternion.identity);

        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            rb.linearVelocity = dir * fireballSpeed;
        }
    }

}
