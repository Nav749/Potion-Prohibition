using System.Collections;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public bool rangedIsAggroed = false;

    private bool OneDeath = false;

    public bool rangedInrange = false;

    public float rangedEnemyHealth;

    public Animator RangedAnimator;

    public GameObject playerTargetForRangedEnemy;

    public GameObject rangedEnemyAttackPrefab;

    public Transform rangedEnemyAttackSource;

    private Quaternion lookingRotatorRangedEnemy;

    private Vector3 targetDirectionRangedEnemy;

    private Vector3 rangedEnemyMovement;

    public AudioSource DewdropSource;

    private float ticker = 0;

    public int rangedEnemySpeed = 1;

    public float rangedEnemyAttackSpeed = 3;

    public int rangedEnemyBulletSpeed = 1;

    public AudioClip DewdropAttackClip;

    public AudioClip DewdropDeathClip;

    public Rigidbody enemyRB;

    public bool tutorial;

    [SerializeField] GameObject EnemyDrop;
    private void Start()
    {
        if (playerTargetForRangedEnemy == null) playerTargetForRangedEnemy = GameManager.Instance.PlayerGO;
        RangedAnimator = GetComponent<Animator>();
        DewdropSource = GetComponent<AudioSource>();
        if (!tutorial)
        {
            rangedEnemyHealth = rangedEnemyHealth + Mathf.Pow(GameManager.Instance.eyeScale, GameManager.Instance.LevelsPassed - 1);
        }
        else
        {
            rangedEnemyHealth = 3;
        }
    }

    private void Update()
    {
        if (playerTargetForRangedEnemy.GetComponent<playerHealth>().currentHealth <= 0)
        {
            OneDeath = true;
            rangedIsAggroed = false;
        }

        if (rangedEnemyHealth <= 0 && OneDeath == false)
        {
            OneDeath = true;
            StartCoroutine(RangedDeath());
        }

        targetDirectionRangedEnemy = (playerTargetForRangedEnemy.transform.position - transform.position).normalized;
        lookingRotatorRangedEnemy = Quaternion.LookRotation(targetDirectionRangedEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotatorRangedEnemy, 85);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (rangedIsAggroed == true)
        {
            ticker += Time.deltaTime;

            if (rangedInrange == false)
            {
                rangedEnemyMovement = transform.forward * rangedEnemySpeed;
                enemyRB.linearVelocity = new Vector3(rangedEnemyMovement.x, enemyRB.linearVelocity.y, rangedEnemyMovement.z);
            }
            else
            {
                enemyRB.linearVelocity = new Vector3(0, 0, 0);
            }

            if (ticker >= rangedEnemyAttackSpeed && rangedIsAggroed == true)
            {
                StartCoroutine(RangedAttack());
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<playerBullet>() != null && OneDeath == false)
        {
            rangedIsAggroed = true;
            rangedEnemyHealth -= other.GetComponent<playerBullet>().bulletScriptDamage;
            this.GetComponent<DamageFlash>().CallDamageFlash();
        }
    }

    IEnumerator RangedAttack()
    {
        ticker = 0;
        RangedAnimator.SetTrigger("IsAttacking");
        yield return new WaitForSeconds(1f);
        DewdropSource.PlayOneShot(DewdropAttackClip);
        if (rangedIsAggroed == true)
        {
            GameObject EnemyAttackInRange = Instantiate(rangedEnemyAttackPrefab, rangedEnemyAttackSource.position, transform.rotation);
            EnemyAttackInRange.GetComponent<Rigidbody>().AddForce(rangedEnemyAttackSource.forward * rangedEnemyBulletSpeed, ForceMode.Impulse);
        }
        RangedAnimator.SetTrigger("backToIdle");
        yield return new WaitForSeconds(1f);
    }

    //THis is where the Enemy Dies.
    IEnumerator RangedDeath()
    {
        rangedIsAggroed = false;
        RangedAnimator.SetTrigger("IsDead");
        DewdropSource.PlayOneShot(DewdropDeathClip);
        yield return new WaitForSeconds(1f);
        Instantiate(EnemyDrop, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
