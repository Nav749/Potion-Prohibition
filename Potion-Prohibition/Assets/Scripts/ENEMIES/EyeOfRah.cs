using System.Collections;
using UnityEngine;

public class EyeOfRah : MonoBehaviour
{
    public bool rahIsAggroed = false;

    private bool rahRotation = true;

    public float rahEnemyHealth;

    public Animator RahAnimator;

    public GameObject playerTargetForRah;

    public EyeOfRahBeamAttack beam;

    private Quaternion lookingRotatorRah;

    private Vector3 targetDirectionRah;

    private float ticker = 0;

    public int rangedEnemyAttackSpeed = 50;

    public Rigidbody enemyRB;

    [SerializeField] GameObject enemyDrop;

    [SerializeField] GameObject spawnPoint;

    private void Start()
    {
        RahAnimator = GetComponent<Animator>();
        playerTargetForRah = GameManager.Instance.PlayerGO;
    }

    private void Update()
    {
        if (rahEnemyHealth <= 0)
        {
            StartCoroutine(RahDeath());
        }


        if (rahRotation == true)
        {
            targetDirectionRah = (playerTargetForRah.transform.position - transform.position).normalized;
            lookingRotatorRah = Quaternion.LookRotation(targetDirectionRah);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotatorRah, 85);
        }

        if (rahIsAggroed == true)
        {
            ticker += Time.deltaTime;

            if (ticker >= rangedEnemyAttackSpeed)
            {
                StartCoroutine(RahBeam());
                ticker = 0;
            }
        }
    }

    IEnumerator RahBeam()
    {
        rahRotation = false;
        rahIsAggroed = false;
        RahAnimator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.3f);
        beam.FireBeam();
        yield return new WaitForSeconds(0.3f);
        RahAnimator.SetBool("IsAttacking", false);
        rahRotation = true;
        rahIsAggroed = true;
    }

    IEnumerator RahDeath()
    {
        RahAnimator.SetTrigger("IsDead");
        yield return new WaitForSeconds(0.5f);
        Instantiate(enemyDrop, spawnPoint.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
