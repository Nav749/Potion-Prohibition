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
        ActivateCustomer();
    }

    public void Update()
    {
        if (currentCustomer != null) c = currentCustomer.GetComponent<Customer>();

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
    }

    void PoolCustomers()
    {
        foreach (GameObject customer in customers)
        {
            GameObject spawnedCustomer = Instantiate(customer, this.transform.position, Quaternion.identity, this.transform);
            spawnedCustomer.transform.Rotate(0f, 90f, 0f, Space.Self);
            spawnedCustomer.SetActive(false);
        }
    }

    public void StartSpeaking()
    {
        if (c.canStart)
        {
            c.StartDialogue();
            c.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            c.isSpeaking = true;
        }
    }

    public void OrderToggle()
    {
        c.isOrdering = !c.isOrdering;
    }
}
