using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class Dialogue : ScriptableObject
{
    public int combo;
    public string[] dialogue;

    public int GetCombo()
    {
        return combo;
    }

    public string[] GetDialogue()
    {
        return dialogue;
    }
}
