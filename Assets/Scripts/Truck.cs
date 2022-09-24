using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : Controllable
{
    [SerializeField] private CarDoorController carDoor;
    [SerializeField] private TrunkController trunk;
    [SerializeField] private Transform farmerExitPoint;
    [SerializeField] private List<GameObject> pumpkins = new List<GameObject>();
    [SerializeField] private float velocityThreshold = 0.2f;
    private int currentTrunkSize = 0;
    public bool IsFull { get { return currentTrunkSize >= pumpkins.Count; } }
    private Farmer farmer;

    protected override void Start()
    {
        base.Start();
        UpdatePumpkinsOnTrunk();
        carDoor.Init(this);
        trunk.Init(this);
    }

    public void FarmerEnter(Farmer farmerEntered)
    {
        farmer = farmerEntered;
        farmer.ResetInteractableInReach();
        farmer.gameObject.SetActive(false);
        playerController.SetCurrentControllable(this);
    }

    public void FarmerExit(Farmer farmerEntered)
    {
        farmer.transform.position = farmerExitPoint.position;
        farmer.ResetVelocity();
        farmer.UpdateAnimator();
        farmer.gameObject.SetActive(true);

        playerController.SetCurrentControllable(farmer);
        farmer = null;
    }

    public bool AddToTrunk()
    {
        if (currentTrunkSize < pumpkins.Count)
        {
            currentTrunkSize++;
            UpdatePumpkinsOnTrunk();
            return true;
        }
        return false;
    }

    public bool RemoveFromTrunk()
    {
        if (currentTrunkSize > 0)
        {
            currentTrunkSize--;
            UpdatePumpkinsOnTrunk();
            return true;
        }
        return false;
    }

    private void UpdatePumpkinsOnTrunk()
    {
        for (int i = 0; i < pumpkins.Count; i++)
        {
            pumpkins[i].SetActive(i < currentTrunkSize);
        }
    }

    public override void OnBaseInteract()
    {
        if (characterController.velocity.magnitude > velocityThreshold)
        {
            return;
        }

        FarmerExit(farmer);
    }
}
