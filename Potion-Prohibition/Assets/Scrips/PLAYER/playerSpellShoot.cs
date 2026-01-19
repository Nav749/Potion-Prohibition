using System.Threading;
using UnityEngine;

public class playerSpellShoot : MonoBehaviour
{
    public float spellBulletSpeed;
    public float spellBulletDamage;
    public float spellBulletFireRate;
    public bool fullAuto;

    public Transform spellBulletSpawnLocation;
    public GameObject spellBulletPrefab;

    private float bulletTimer;

    private void Update()
    {
        if(bulletTimer > 0)
        {
            bulletTimer -= Time.deltaTime / spellBulletFireRate;
        }

        if (fullAuto)
        {
            if (Input.GetButton("Fire1") && bulletTimer <= 0)
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && bulletTimer <= 0)
            {
                Shoot();
            }
        }
       
    }

    void Shoot()
    {
        GameObject spellBullet = Instantiate(spellBulletPrefab, spellBulletSpawnLocation.position , Quaternion.identity, GameObject.FindGameObjectWithTag("WorldObjectHolder").transform);
        spellBullet.GetComponent<Rigidbody>().AddForce(spellBulletSpawnLocation.forward * spellBulletSpeed, ForceMode.Impulse);
        spellBullet.GetComponent<playerBullet>().bulletScriptDamage = spellBulletDamage;

        bulletTimer = 1;
    }
}
