using System.Collections.Generic;
using UnityEngine;

public class CustomerPool : MonoBehaviour
{
    [SerializeField] List<GameObject> customers;
    GameObject currentCustomer;
    Customer c;

    private void Start()
    {
        PoolCustomers();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ActivateCustomer();
        }
    }

    public void ActivateCustomer()
    {
        if (currentCustomer != null) currentCustomer.SetActive(false);
        currentCustomer = this.gameObject.transform.GetChild(Random.Range(0, customers.Count)).gameObject;
        currentCustomer.SetActive(true);
        c = currentCustomer.GetComponent<Customer>();
        c.StartDialogue();
    }

    void PoolCustomers()
    {
        foreach (GameObject customer in customers)
        {
            GameObject spawnedCustomer = Instantiate(customer, Vector3.zero, Quaternion.identity, this.transform);
            spawnedCustomer.SetActive(false);
        }
    }
}
