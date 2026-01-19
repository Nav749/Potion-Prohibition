using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public float bulletScriptDamage;
    public float bulletLifetime;

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
            other.GetComponent<EnemyTest>().testEnemyHealth -= bulletScriptDamage;
        }
        Destroy(gameObject);
    }
}
