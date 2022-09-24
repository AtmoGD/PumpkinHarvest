using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDoorController : MonoBehaviour, IInteractable
{
    [SerializeField] private TooltipController carTooltipController;
    private Truck truck;

    public void Init(Truck truck)
    {
        this.truck = truck;
    }
    public void BaseInteract(Farmer farmer)
    {
        truck.FarmerEnter(farmer);
    }

    public void ShowInteractTooltip(Farmer farmer, bool show)
    {
        if (show)
            carTooltipController?.ShowTooltip();
        else
            carTooltipController?.HideTooltip();
    }
}
