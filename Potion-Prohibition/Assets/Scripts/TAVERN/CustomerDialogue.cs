using UnityEngine;

public class CustomerDialogue : MonoBehaviour
{
    [Tooltip("Put the Customer Combos as a 2 digit number.  ie. 13 corresponds to customers 1 and 3")]
    [SerializeField] int[] customerCombos;

    private int customer1;
    private int customer2;
    private int combo = 00;

    void Start()
    {
        combo = customerCombos[Random.Range(0, customerCombos.Length)];
    }

    void SpawnCustomers()
    {
        Debug.Log(combo);
    }
}
