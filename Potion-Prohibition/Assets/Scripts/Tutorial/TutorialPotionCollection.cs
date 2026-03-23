using System.Collections.Generic;
using UnityEngine;

public class TutorialPotionCollection : MonoBehaviour
{
    public List<Potion> PotionList;
    public Potion Potion;

    public bool CheckPotion(Potion check)
    {
        return Potion == check;
    }

}
