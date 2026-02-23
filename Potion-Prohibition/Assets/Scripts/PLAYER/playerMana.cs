using UnityEngine;
using UnityEngine.UI;

public class playerMana : MonoBehaviour
{
    public Image barImage;
    public bool ManaKill = false;
    private Mana mana;

    private void Awake()
    {
        mana = new Mana();
    }

    private void Update()
    {
        if (ManaKill == false)
        {
            mana.Update();
        }
        barImage.fillAmount = mana.GetManaNormalized();
    }

    public void ManaFire(int amount)
    {
        mana.TrySpendMana(amount);
    }

    public bool checkMana(int amount)
    {
        if (mana.GetMana() >= amount)
        {
            return true;
        }
        return false;
    }

    public void ManaStop()
    {
        mana.Empty();
        ManaKill = true;
        mana.Update();
    }
}

public class Mana
{
    public const int Mana_Max = 100;
    private float manaAmount;
    private float regenAmount;

    public Mana()
    {
        manaAmount = 0;
        regenAmount = 20;
    }

    public void Update()
    {
        manaAmount += regenAmount * Time.deltaTime;
        manaAmount = Mathf.Clamp(manaAmount, 0f, Mana_Max);
    }

    public void TrySpendMana(int amount)
    {
        if (manaAmount >= amount)
        {
            manaAmount -= amount;
        }
    }

    public void Empty()
    {
        manaAmount = 0;
    }

    public float GetManaNormalized()
    {
        return manaAmount / Mana_Max;
    }

    public float GetMana()
    {
        return manaAmount;
    }
}
