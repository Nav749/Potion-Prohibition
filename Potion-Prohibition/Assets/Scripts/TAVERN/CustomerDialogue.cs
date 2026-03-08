using UnityEngine;

public class CustomerDialogue : MonoBehaviour
{
    [SerializeField] Dialogue[] customerCombos;

    [SerializeField] Sprite[] Customers;

    public Dialogue currentCustomers;

    public int customerInt1;
    public int customerInt2;

    private Sprite customer1;
    private Sprite customer2;
    private int combo = 00;

    void Start()
    {
        currentCustomers = customerCombos[Random.Range(0, customerCombos.Length)];
        combo = currentCustomers.GetCombo();
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
