using System.Collections;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public bool meleeIsAggroed = false;

    public bool meleeInRange = false;

    public float meleeEnemyHealth;

    private Animator NewtAnimator;

    public GameObject playerTargetForMeleeEnemy;

    public GameObject meleeEnemyAttackPrefab;

    public Transform meleeEnemyAttackSource;

    private Quaternion lookingRotatorMeleeEnemy;

    private Vector3 targetDirectionMeleeEnemy;

    private Vector3 meleeEnemyMovement;

    public int meleeEnemySpeed = 1;

    public Rigidbody meleeEnemyRB;

    [SerializeField] GameObject enemyDrop;

    private void Start()
    {
        NewtAnimator = GetComponent<Animator>();
        playerTargetForMeleeEnemy = GameManager.Instance.PlayerGO;
    }

    private void Update()
    {
        if (meleeEnemyHealth <= 0)
        {
            StartCoroutine(NewtDeath());
        }

        targetDirectionMeleeEnemy = (playerTargetForMeleeEnemy.transform.position - transform.position).normalized;
        lookingRotatorMeleeEnemy = Quaternion.LookRotation(targetDirectionMeleeEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotatorMeleeEnemy, 85);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (meleeIsAggroed == true && meleeInRange == false)
        {
            meleeEnemyMovement = transform.forward * meleeEnemySpeed;
            meleeEnemyRB.linearVelocity = new Vector3(meleeEnemyMovement.x, meleeEnemyRB.linearVelocity.y, meleeEnemyMovement.z);
        }
        else
        {
            meleeEnemyRB.linearVelocity = new Vector3(0, 0, 0);
        }

        if (meleeInRange == true && meleeIsAggroed == true)
        {
            StartCoroutine(EnemyPause());
        }

    }
    IEnumerator EnemyPause()
    {
        meleeIsAggroed = false;
        NewtAnimator.SetBool("NewtAttacking", true);
        yield return new WaitForSeconds(0.4f);
        GameObject EnemyAttackInRange = Instantiate(meleeEnemyAttackPrefab, meleeEnemyAttackSource.position, transform.rotation);
        NewtAnimator.SetBool("NewtAttacking", false);
        yield return new WaitForSeconds(0.9f);
        meleeIsAggroed = true;
    }

    IEnumerator NewtDeath()
    {
        NewtAnimator.SetTrigger("NewtIsDead");
        yield return new WaitForSeconds(0.6f);
        Instantiate(enemyDrop, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
