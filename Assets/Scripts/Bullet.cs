using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject wanderTargetPrefab;     // Gán prefab qua Inspector
    public float respawnDelay = 5f;           // Thời gian chờ hồi sinh (giây)
    private void OnCollisionEnter(Collision objectWeHit)
    {
        if(objectWeHit.gameObject.CompareTag("Target"))
        {
            print("hit " + objectWeHit.gameObject.name + " !");

            CreateBulletImpactEffect(objectWeHit);

            Destroy(gameObject);
        }

        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            print("hit a wall");

            CreateBulletImpactEffect(objectWeHit);

            Destroy(gameObject);
        }

        if (objectWeHit.gameObject.CompareTag("wanderTarget"))
        {
            print("hit a wander");
            CreateBulletImpactEffect(objectWeHit);
            HUD.Instance.AddKill();

            Vector3 respawnPosition = objectWeHit.transform.position;
            Quaternion respawnRotation = objectWeHit.transform.rotation;

            // Lưu radius & angularSpeed từ SimpleWander của object cũ
            SimpleWander wander = objectWeHit.gameObject.GetComponent<SimpleWander>();
            float savedRadius = wander.radius;
            float savedAngularSpeed = wander.angularSpeed;

            Destroy(objectWeHit.gameObject);
            GameManager.Instance.StartGlobalCoroutine(RespawnWander(respawnPosition, respawnRotation, savedRadius, savedAngularSpeed));
            Destroy(gameObject);

            Debug.Log("Respawn coroutine started...");
        }
    }

    System.Collections.IEnumerator RespawnWander(Vector3 position, Quaternion rotation, float radius, float angularSpeed)
    {
        yield return new WaitForSeconds(respawnDelay);

        if (wanderTargetPrefab == null)
        {
            Debug.LogError("wanderTargetPrefab is NULL! Did you assign it in the Inspector?");
            yield break;
        }

        GameObject newWander = Instantiate(wanderTargetPrefab, position, rotation);
        Debug.Log("Wander target respawned at " + position);

        SimpleWander wanderScript = newWander.GetComponent<SimpleWander>();
        wanderScript.radius = radius;
        wanderScript.angularSpeed = angularSpeed;

        // Gán lại tham chiếu player cho EnemyShooter
        EnemyShooter shooter = newWander.GetComponent<EnemyShooter>();
        if (shooter != null)
        {
            // Ví dụ tìm player bằng tag:
            GameObject playerGO = GameObject.FindWithTag("Player");
            if (playerGO != null)
            {
                shooter.player = playerGO.transform;
            }
            else
            {
                Debug.LogError("Không tìm thấy Player trong scene để gán cho EnemyShooter!");
            }
        }
    }



    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );

        hole.transform.SetParent(objectWeHit.gameObject.transform);

    }
}


