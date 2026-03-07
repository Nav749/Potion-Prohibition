using UnityEngine;

[CreateAssetMenu(fileName = "Timesmet", menuName = "Scriptable Objects/Timesmet")]
public class Timesmet : ScriptableObject
{
    [SerializeField] private int timesMet = 0;

    public void SetInt(int value)
    {
        timesMet = value;
    }

    public int GetInt() {return timesMet; }
}
