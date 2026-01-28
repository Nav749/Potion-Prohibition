using UnityEngine;

public class EnemyTest : MonoBehaviour
{

    public bool isAggroed = false;

    public float testEnemyHealth;

    public GameObject playerTargetForEnemy;

    public GameObject aggroRangeForEnemy;  

    public GameObject EnemyAttackPrefab;

    public Transform EnemyAttackPosition;

    private Quaternion lookingRotatorEnemy;

    private Vector3 targetDirectionEnemy;

    private Vector3 enemyMovement;

    private int ticker = 0;

    public int enemySpeed = 1;

    public int enemyAttackSpeed = 10;

    public int enemyBulletSpeed = 1;

    public Rigidbody enemyRB;

    private void Update()
    {
        if(testEnemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        ticker += 1;

        targetDirectionEnemy = (playerTargetForEnemy.transform.position - transform.position).normalized;
        lookingRotatorEnemy = Quaternion.LookRotation(targetDirectionEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotatorEnemy, 85);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);



        if (isAggroed == true)
        {
            enemyMovement = transform.forward * enemySpeed;
            enemyRB.linearVelocity = new Vector3(enemyMovement.x, enemyRB.linearVelocity.y, enemyMovement.z);
            if(ticker >= enemyAttackSpeed * 60)
            {
                GameObject EnemyAttackInRange = Instantiate(EnemyAttackPrefab, EnemyAttackPosition.position, transform.rotation);
                EnemyAttackInRange.GetComponent<Rigidbody>().AddForce(EnemyAttackPosition.forward * enemyBulletSpeed, ForceMode.Impulse);
                ticker = 0;
            }
        }

        if(ticker >= 100000)
        {
            ticker = 0;
        }
    }

}
