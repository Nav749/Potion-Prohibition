using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public bool rangedIsAggroed = false;

    public float rangedEnemyHealth;

    public GameObject playerTargetForRangedEnemy;  

    public GameObject rangedEnemyAttackPrefab;

    public Transform rangedEnemyAttackSource;

    private Quaternion lookingRotatorRangedEnemy;

    private Vector3 targetDirectionRangedEnemy;

    private Vector3 rangedEnemyMovement;

    private int ticker = 0;

    public int rangedEnemySpeed = 1;

    public int rangedEnemyAttackSpeed = 50;

    public int rangedEnemyBulletSpeed = 1;

    public Rigidbody enemyRB;

    private void Update()
    {
        if(rangedEnemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        targetDirectionRangedEnemy = (playerTargetForRangedEnemy.transform.position - transform.position).normalized;
        lookingRotatorRangedEnemy = Quaternion.LookRotation(targetDirectionRangedEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotatorRangedEnemy, 85);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (rangedIsAggroed == true)
        {
            ticker += 1;
            rangedEnemyMovement = transform.forward * rangedEnemySpeed;
            enemyRB.linearVelocity = new Vector3(rangedEnemyMovement.x, enemyRB.linearVelocity.y, rangedEnemyMovement.z);

            if(ticker >= rangedEnemyAttackSpeed * 60)
            {
                GameObject EnemyAttackInRange = Instantiate(rangedEnemyAttackPrefab, rangedEnemyAttackSource.position, transform.rotation);
                EnemyAttackInRange.GetComponent<Rigidbody>().AddForce(rangedEnemyAttackSource.forward * rangedEnemyBulletSpeed, ForceMode.Impulse);
                ticker = 0;
            }
        }
    }

}
