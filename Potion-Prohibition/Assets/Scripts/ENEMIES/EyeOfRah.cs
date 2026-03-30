using System.Collections;
using UnityEngine;

public class EyeOfRah : MonoBehaviour
{
    private bool OneLove = false;

    public bool rahIsAggroed = false;

    private bool rahRotation = true;

    public AudioClip RahAttackClip;

    public AudioClip RahDeathClip;

    public float rahEnemyHealth;

    public Animator RahAnimator;

    public GameObject playerTargetForRah;

    public EyeOfRahBeamAttack beam;

    private Quaternion lookingRotatorRah;

    private Vector3 targetDirectionRah;

    private AudioSource RahAudioSource;

    private float ticker = 0;

    public float rahEnemyAttackSpeed;

    private Rigidbody enemyRB;

    [SerializeField] GameObject enemyDrop;

    [SerializeField] GameObject spawnPoint;

    private void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        RahAudioSource = GetComponent<AudioSource>();
        RahAnimator = GetComponent<Animator>();
        playerTargetForRah = GameManager.Instance.PlayerGO;
        rahEnemyHealth = rahEnemyHealth + Mathf.Pow(GameManager.Instance.eyeScale, GameManager.Instance.LevelsPassed - 1);
    }

    private void Update()
    {
        if (playerTargetForRah.GetComponent<playerHealth>().currentHealth <= 0)
        {
            rahIsAggroed = false;
        }

        if (rahEnemyHealth <= 0 && OneLove == false)
        {
            OneLove = true;
            StartCoroutine(RahDeath());
        }


        if (rahRotation == true)
        {
            Vector3 target = new Vector3(playerTargetForRah.transform.position.x, playerTargetForRah.transform.position.y - 2f, playerTargetForRah.transform.position.z);
            targetDirectionRah = (target - transform.position).normalized;
            lookingRotatorRah = Quaternion.LookRotation(targetDirectionRah);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotatorRah, 85);
        }

        if (rahIsAggroed == true)
        {
            ticker += Time.deltaTime;

            if (ticker >= rahEnemyAttackSpeed)
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

        RahAudioSource.PlayOneShot(RahAttackClip);
        beam.FireBeam();

        yield return new WaitForSeconds(0.3f);
        RahAnimator.SetBool("IsAttacking", false);

        yield return new WaitForSeconds(0.5f);
        rahRotation = true;
        rahIsAggroed = true;
    }

    IEnumerator RahDeath()
    {
        RahAnimator.SetTrigger("IsDead");
        RahAudioSource.PlayOneShot(RahDeathClip);
        yield return new WaitForSeconds(0.9f);
        Instantiate(enemyDrop, spawnPoint.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
