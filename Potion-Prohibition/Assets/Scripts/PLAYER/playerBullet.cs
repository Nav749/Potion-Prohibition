using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public float bulletScriptDamage;
    public float bulletLifetime;
    private EnemyTest enemyL;

    private void Update()
    {
        bulletLifetime -= Time.deltaTime;

        if(bulletLifetime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyTest>() != null)
        {
            enemyL = other.GetComponent<EnemyTest>();
            enemyL.rangedEnemyHealth = enemyL.rangedEnemyHealth - bulletScriptDamage;
            enemyL.rangedIsAggroed = true;

        }
        Destroy(gameObject);
    }
}
