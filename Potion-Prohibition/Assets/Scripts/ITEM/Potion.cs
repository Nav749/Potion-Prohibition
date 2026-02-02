using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/Potion")]
public class Potion : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite image;


    public string getName()
    {
        return name;
    }

    public Sprite getImage()
    {
        return image;
    }


}
