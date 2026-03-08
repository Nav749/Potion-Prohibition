using UnityEngine;

public class CustomerDialogue : MonoBehaviour
{
    [Tooltip("Put the Customer Combos as a 2 digit number.  ie. 13 corresponds to customers 1 and 3")]
    [SerializeField] int[] customerCombos;

    [SerializeField] Sprite[] Customers;

    public int customerInt1;
    public int customerInt2;

    private Sprite customer1;
    private Sprite customer2;
    private int combo = 00;

    void Start()
    {
        combo = customerCombos[Random.Range(0, customerCombos.Length)];
        SpawnCustomers();
    }

    public void SpawnCustomers()
    {
        customerInt1 = combo / 10;
        customerInt2 = combo % 10;

        this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Customers[customerInt1];
        this.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite= Customers[customerInt2];
        
    }
}
