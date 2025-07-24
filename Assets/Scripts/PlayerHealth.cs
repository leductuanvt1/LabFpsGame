using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);
        HUD.Instance.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0f)
        {
            Debug.Log("Player died!");
            // Xử lý game over tuỳ ý
        }
    }
}
