using System.Collections;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public bool meleeIsAggroed = false;

    public bool meleeInRange = false;

    private bool NewtsDieOnce = false;

    private bool AttackBehaviour = false;

    public float meleeEnemyHealth;

    private Animator NewtAnimator;

    private AudioSource NewtAudioSource;

    public AudioClip NewtDeathClip;

    public AudioClip NewtAttackClip;

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
        NewtAudioSource = GetComponent<AudioSource>();
        NewtAnimator = GetComponent<Animator>();
        playerTargetForMeleeEnemy = GameManager.Instance.PlayerGO;
        meleeEnemyHealth = meleeEnemyHealth + Mathf.Pow(GameManager.Instance.eyeScale, GameManager.Instance.LevelsPassed - 1);
    }

    private void Update()
    {
        if (playerTargetForMeleeEnemy.GetComponent<playerHealth>().currentHealth <= 0)
        {
            meleeIsAggroed = false;
        }

        if (meleeEnemyHealth <= 0 && NewtsDieOnce == false)
        {
            NewtsDieOnce = true;
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

        if (meleeInRange == true && meleeIsAggroed == true && AttackBehaviour == false)
        {
            StartCoroutine(EnemyPause());
        }

    }
    IEnumerator EnemyPause()
    {
        AttackBehaviour = true;
        NewtAnimator.SetBool("NewtAttacking", true);
        yield return new WaitForSeconds(0.4f);
        NewtAudioSource.PlayOneShot(NewtAttackClip);
        GameObject EnemyAttackInRange = Instantiate(meleeEnemyAttackPrefab, meleeEnemyAttackSource.position, transform.rotation);
        NewtAnimator.SetBool("NewtAttacking", false);
        yield return new WaitForSeconds(0.9f);
        AttackBehaviour = false;
    }

    IEnumerator NewtDeath()
    {
        NewtAnimator.SetTrigger("NewtIsDead");
        NewtAudioSource.PlayOneShot(NewtDeathClip);
        yield return new WaitForSeconds(1f);
        Instantiate(enemyDrop, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
