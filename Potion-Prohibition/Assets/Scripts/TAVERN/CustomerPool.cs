using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomerPool : MonoBehaviour
{
    [SerializeField] List<GameObject> customers;
    GameObject currentCustomer;
    Customer c;

    private void Start()
    {
        PoolCustomers();
        if(!GameManager.Instance.customerGenerated) ActivateCustomer();
        else
        {
            GameObject cus = this.gameObject.transform.GetChild(GameManager.Instance.customerNum).gameObject;
            currentCustomer = cus;
            currentCustomer.SetActive(true);
            GameManager.Instance.checkDone = false;
            currentCustomer.GetComponent<Customer>().lines = currentCustomer.GetComponent<Customer>().linesIntro;
        }
    }

    public void Update()
    {
        if (currentCustomer != null)
        {
            c = currentCustomer.GetComponent<Customer>();
        }
    }

    public void ActivateCustomer()
    {
        if (currentCustomer != null) currentCustomer.SetActive(false);
        int customerNum = Random.Range(0, customers.Count);
        GameObject customer = this.gameObject.transform.GetChild(customerNum).gameObject;
        while (customer == currentCustomer)
        {
            customerNum = Random.Range(0, customers.Count);
            customer = this.gameObject.transform.GetChild(customerNum).gameObject;
        }
        GameManager.Instance.customerNum = customerNum;
        currentCustomer = customer;
        GameManager.Instance.customerGenerated = true;
        currentCustomer.SetActive(true);
        GameManager.Instance.checkDone = false;
        currentCustomer.GetComponent<Customer>().lines = currentCustomer.GetComponent<Customer>().linesIntro;
        GameManager.Instance.PickRandomPotion();
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
