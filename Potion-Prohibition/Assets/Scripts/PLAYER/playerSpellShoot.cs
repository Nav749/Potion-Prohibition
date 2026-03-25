using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class playerSpellShoot : MonoBehaviour
{
    public AudioSource ZapMaker;
    public AudioClip zap;
    public float spellBulletSpeed;
    public float spellBulletDamage;
    public float spellBulletFireRate;
    public bool fullAuto;
    public bool tavernNeutral = false;
    public Transform spellBulletSpawnLocation;
    public GameObject Wand;
    public GameObject spellBulletPrefab;
    public playerMana mana;
    public Sprite Idle;
    public Sprite shoot;
    public Image UIHand;

    private bool isCrafting = false;

    private float bulletTimer;

    private void Update()
    {
        if (tavernNeutral == false && !isCrafting)
        {
            if (bulletTimer > 0)
            {
                bulletTimer -= Time.deltaTime / spellBulletFireRate;
            }

            if (fullAuto)
            {
                if (Input.GetButton("Fire1") && bulletTimer <= 0)
                {
                    StartCoroutine(Shoot());
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1") && bulletTimer <= 0)
                {
                    StartCoroutine(Shoot());
                }
            }
        }
        else
        {
            Wand.SetActive(false);
        }
    }

    IEnumerator Shoot()
    {
        if (mana.checkMana(20) == true)
        {
            UIHand.sprite = shoot;
            mana.ManaFire(20);
            ZapMaker.PlayOneShot(zap);
            GameObject spellBullet = Instantiate(spellBulletPrefab, spellBulletSpawnLocation.position, Quaternion.identity, GameObject.FindGameObjectWithTag("WorldObjectHolder").transform);
            spellBullet.GetComponent<Rigidbody>().AddForce(spellBulletSpawnLocation.forward * spellBulletSpeed, ForceMode.Impulse);
            spellBullet.GetComponent<playerBullet>().bulletScriptDamage = spellBulletDamage;

            bulletTimer = 1;
            yield return new WaitForSeconds(0.1f);
            UIHand.sprite = Idle;
        }
    }

    public void setCrafting(bool input)
    {
        isCrafting = input;
    }
}
