using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CustomerData
{
    public int amountOfPumpkins;
    public int amountOfMoney;
}

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] private GameObject menuCamera;
    [SerializeField] private GameObject gameCamera;
    [SerializeField] private bool startOnLoad = false;
    [SerializeField] private float startGameDelay = 2f;
    [SerializeField] private float gameLoopTime = 1f;
    [SerializeField] private float chanceForNewCustomer = 0.1f;
    [SerializeField] private int minCustomersActive = 2;
    [SerializeField] private int maxCustomersActive = 4;
    [SerializeField] private List<CustomerController> customers = new List<CustomerController>();
    [SerializeField] private List<CustomerData> customerData = new List<CustomerData>();
    public int ActiveCustomerCount { get { return customers.FindAll(c => c.IsActive).Count; } }
    public List<CustomerController> InActiveCustomers { get { return customers.FindAll(c => !c.IsActive); } }
    public bool IsGameRunning { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        IsGameRunning = false;
    }

    private void Start()
    {
        if (startOnLoad)
            StartCoroutine(StartGameDelayed());
    }

    public void StartGame()
    {
        IsGameRunning = true;

        menuCamera.SetActive(false);
        gameCamera.SetActive(true);

        int amountOfCustomers = UnityEngine.Random.Range(minCustomersActive, maxCustomersActive + 1);

        for (int i = 0; i < amountOfCustomers; i++)
        {
            StartRandomCustomer();
        }

        StartCoroutine(GameLoop());
    }

    IEnumerator StartGameDelayed()
    {
        yield return new WaitForSeconds(startGameDelay);
        StartGame();
    }

    IEnumerator GameLoop()
    {
        yield return new WaitForSeconds(gameLoopTime);

        if (ActiveCustomerCount < maxCustomersActive)
        {
            if (UnityEngine.Random.Range(0f, 1f) < chanceForNewCustomer)
            {
                StartRandomCustomer();
            }
        }

        StartCoroutine(GameLoop());
    }

    public void InitCustomer(CustomerController customer)
    {
        customers.Add(customer);
    }

    public void StartRandomCustomer()
    {
        CustomerData data = customerData[UnityEngine.Random.Range(0, customerData.Count)];
        InActiveCustomers[UnityEngine.Random.Range(0, InActiveCustomers.Count)].BecomeActive(data.amountOfPumpkins, data.amountOfMoney);
    }

    public void CustomerFinished(CustomerController customer)
    {
        if (ActiveCustomerCount < minCustomersActive)
            StartRandomCustomer();
    }

}
