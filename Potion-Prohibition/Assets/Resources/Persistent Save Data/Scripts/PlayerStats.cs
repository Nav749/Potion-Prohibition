using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int health;
    public int damage;
    public bool newDay;

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public int GetHealth()
    {
        return this.health;
    }

    public int GetDamage()
    {
        return this.damage;
    }

    public void IncrementHealth()
    {
        this.health += 1;
    }

    public void IncrementDamage()
    {
        this.damage += 1;
    }

    public void SetBool(bool day)
    {
        this.newDay = day;
    }

    public bool GetBool()
    {
        return this.newDay;
    }
}
