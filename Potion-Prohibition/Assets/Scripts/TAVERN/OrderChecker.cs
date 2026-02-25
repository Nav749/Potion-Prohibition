using UnityEngine;

public class OrderChecker : MonoBehaviour
{
    [SerializeField] GameObject UI;
    public void OrderCheck()
    {
        GameManager.Instance.TimeToCheckOrder();
        Debug.Log(GameManager.Instance.correctOrder);
        UI.SetActive(false);
    }
}
