using System.Collections;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public bool rangedIsAggroed = false;

    public bool rangedInrange = false;

    public float rangedEnemyHealth;

    public Animator RangedAnimator;

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

    private void Start()
    {
        playerTargetForRangedEnemy = GameManager.Instance.PlayerGO;
        RangedAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (rangedEnemyHealth <= 0)
        {
            StartCoroutine(RangedDeath());
        }

        targetDirectionRangedEnemy = (playerTargetForRangedEnemy.transform.position - transform.position).normalized;
        lookingRotatorRangedEnemy = Quaternion.LookRotation(targetDirectionRangedEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotatorRangedEnemy, 85);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (rangedIsAggroed == true)
        {
            ticker += 1;

            if (rangedInrange == false)
            {
                rangedEnemyMovement = transform.forward * rangedEnemySpeed;
                enemyRB.linearVelocity = new Vector3(rangedEnemyMovement.x, enemyRB.linearVelocity.y, rangedEnemyMovement.z);
            }
            else
            {
                enemyRB.linearVelocity = new Vector3(0, 0, 0);
            }

            if (ticker >= rangedEnemyAttackSpeed * 60 && rangedIsAggroed == true)
            {
                StartCoroutine(RangedAttack());
            }
        }
    }

    IEnumerator RangedAttack()
    {
        ticker = 0;
        RangedAnimator.SetTrigger("IsAttacking");
        yield return new WaitForSeconds(1f);
        GameObject EnemyAttackInRange = Instantiate(rangedEnemyAttackPrefab, rangedEnemyAttackSource.position, transform.rotation);
        EnemyAttackInRange.GetComponent<Rigidbody>().AddForce(rangedEnemyAttackSource.forward * rangedEnemyBulletSpeed, ForceMode.Impulse);
        RangedAnimator.SetTrigger("backToIdle");
        yield return new WaitForSeconds(1f);
    }

    //THis is where the Enemy Dies.
    IEnumerator RangedDeath()
    {
        rangedIsAggroed = false;
        RangedAnimator.SetTrigger("IsDead");
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
}
