using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger hit player!");
            PlayerHealth ph = other.GetComponentInParent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
