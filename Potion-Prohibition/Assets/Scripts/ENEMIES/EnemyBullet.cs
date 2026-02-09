using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float enemyBulletLifetime;

    public int enemyBulletDamage = 1;

    private void Update()
    {
        enemyBulletLifetime -= Time.deltaTime;

        if (enemyBulletLifetime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<playerMovement>() != null)
        {
            other.GetComponent<playerHealth>().TakeDamage(enemyBulletDamage);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
