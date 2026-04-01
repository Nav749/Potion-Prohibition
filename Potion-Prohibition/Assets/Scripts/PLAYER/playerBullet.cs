using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public float bulletScriptDamage;
    public float bulletLifetime;
    private EnemyTest enemyL;
    private EnemyMelee enemyM;
    private EyeOfRah enemyR;

    private void Update()
    {
        bulletLifetime -= Time.deltaTime;
        if (bulletLifetime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
