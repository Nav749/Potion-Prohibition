using UnityEngine;
using UnityEngine.UI;

public class playerMana : MonoBehaviour
{
    public Image barImage;
    private Mana mana;

    private void Awake()
    {
        mana = new Mana();
    }

    private void Update()
    {
        mana.Update();
        barImage.fillAmount = mana.GetManaNormalized();
    }

    public bool ManaFire(int amount)
    {
        return mana.TrySpendMana(amount);
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

    public bool TrySpendMana(int amount)
    {
        if (manaAmount >= amount)
        {
            manaAmount -= amount;
            return true;
        }
        return false;
    }

    public float GetManaNormalized()
    {
        return manaAmount / Mana_Max;
    }
}
