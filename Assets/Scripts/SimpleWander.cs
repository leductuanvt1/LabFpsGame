using UnityEngine;

public class SimpleWander : MonoBehaviour
{
    public Vector3 centerPoint = Vector3.zero;  // Tâm quay
    public float radius = 5f;                   // Bán kính quỹ đạo
    public float angularSpeed = 30f;            // Tốc độ quay (độ/giây)
    public float height = 5f;                   // Độ cao cố định

    private float currentAngle;                 // Góc quay hiện tại

    void Update()
    {
        currentAngle += angularSpeed * Time.deltaTime;
        if (currentAngle > 360f) currentAngle -= 360f;

        float rad = currentAngle * Mathf.Deg2Rad;
        float x = centerPoint.x + Mathf.Cos(rad) * radius;
        float z = centerPoint.z + Mathf.Sin(rad) * radius;

        transform.position = new Vector3(x, height, z);

        // Quay mặt hướng về tâm (tuỳ thích)
        transform.LookAt(new Vector3(centerPoint.x, height, centerPoint.z));
    }
}
