using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkController : MonoBehaviour, IInteractable
{
    [SerializeField] private TooltipController trunkTooltipController;
    private Truck truck;

    public void Init(Truck truck)
    {
        this.truck = truck;
    }

    public void BaseInteract(Farmer farmer)
    {
        if (farmer.CurrentItem == PickUpType.Pumpkin)
        {
            if (truck.AddToTrunk())
            {
                farmer.DeliverPumpkin();
            }
        }
        else if (farmer.CurrentItem == PickUpType.None)
        {
            if (truck.RemoveFromTrunk())
            {
                farmer.PickUpItem(PickUpType.Pumpkin);
            }
        }
    }

    public void ShowBaseInteractTooltip(Farmer farmer, bool show)
    {
        if (show && farmer.CurrentItem == PickUpType.Pumpkin && !truck.IsFull)
        {
            trunkTooltipController?.ShowTooltip();
        }
        else
        {
            trunkTooltipController?.HideTooltip();
        }
    }

    public void SpecialInteract(Farmer farmer)
    {
        // throw new System.NotImplementedException();
    }

    public void ShowSpecialInteractTooltip(Farmer farmer, bool show)
    {
        // throw new System.NotImplementedException();
    }
}
