using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerHealth : MonoBehaviour
{
    public GameObject healthSegmentPrefab;
    public AudioClip[] hitClips;
    public AudioClip deathClip;
    public AudioClip soundToPlay;
    [SerializeField] public AudioSource playerSourceAudio;
    public Sprite Empties;
    private List<Image> healthSegments = new List<Image>();
    public int maxHealth;
    public int currentHealth;
    public GameObject playerRef;
    [SerializeField] GameObject healthbarHolder;
    public GameObject GameOverScreen;
    private AudioSource healthbarSource;

    void Start()
    {
        healthbarSource = this.GetComponent<AudioSource>();
        initializeHealthBar();
    }

    public void initializeHealthBar()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if (healthSegments.Count < maxHealth)
            {
                GameObject segment = Instantiate(healthSegmentPrefab, this.transform.position, Quaternion.identity, healthbarHolder.transform);
                healthSegments.Add(segment.GetComponent<Image>());
            }
        }
        UpdateHealthBar();
    }

    public void TakeDamage(int health)
    {
        currentHealth = currentHealth - health;
        if (currentHealth > 0)
        {
            int randomIndex = Random.Range(0, hitClips.Length);
            AudioClip soundToPlay = hitClips[randomIndex];
            healthbarSource.PlayOneShot(soundToPlay);
        }
        if (currentHealth == 0)
        {
            healthbarSource.PlayOneShot(deathClip);
        }
        UpdateHealth(currentHealth);
    }

    public void UpdateHealth(int health)
    {
        currentHealth = health;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            playerRef.GetComponent<playerMovement>().setMoveLock(true);
            playerRef.GetComponent<playerMana>().ManaStop();
            GameOverScreen.SetActive(true);
        }
    }

    private void UpdateHealthBar()
    {
        for (int i = 0; i < healthSegments.Count; i++)
        {
            if (i < currentHealth)
            {
                healthSegments[i].GetComponent<Image>().sprite = healthSegmentPrefab.GetComponent<Image>().sprite;
            }
            else
            {
                healthSegments[i].GetComponent<Image>().sprite = Empties;
            }
        }
    }
}
