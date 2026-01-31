using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public bool meleeIsAggroed = false;

    public bool meleeInRange = false;

    public float meleeEnemyHealth;

    public GameObject playerTargetForMeleeEnemy;

    public GameObject meleeEnemyAttackPrefab;

    public Transform meleeEnemyAttackSource;

    private Quaternion lookingRotatorMeleeEnemy;

    private Vector3 targetDirectionMeleeEnemy;

    private Vector3 meleeEnemyMovement;

    private int meleeTick = 0;

    public int meleeEnemySpeed = 1;

    public int meleeEnemyAttackSpeed = 50;

    public Rigidbody meleeEnemyRB;

    private void Update()
    {
        if (meleeEnemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        targetDirectionMeleeEnemy = (playerTargetForMeleeEnemy.transform.position - transform.position).normalized;
        lookingRotatorMeleeEnemy = Quaternion.LookRotation(targetDirectionMeleeEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotatorMeleeEnemy, 85);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (meleeIsAggroed == true)
        {
            meleeTick += 1;
            meleeEnemyMovement = transform.forward * meleeEnemySpeed;
            meleeEnemyRB.linearVelocity = new Vector3(meleeEnemyMovement.x, meleeEnemyRB.linearVelocity.y, meleeEnemyMovement.z);

            if (meleeTick >= meleeEnemyAttackSpeed * 60 && meleeInRange == true)
            {

            }
        }

    }
}
