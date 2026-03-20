using UnityEngine;

[CreateAssetMenu(fileName = "TutorialSO", menuName = "Scriptable Objects/TutorialSO")]
public class TutorialSO : ScriptableObject
{
    [SerializeField] string[] dialogue;

    public string[] GetDialogue()
    {
        return dialogue;
    }
}
