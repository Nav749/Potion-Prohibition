using UnityEngine;
using UnityEngine.SceneManagement;
public class buttonSceneLoad : MonoBehaviour
{
    public void ChangeSceneWithIndex (int index)
    {
        SceneManager.LoadScene(index);
    }
}
