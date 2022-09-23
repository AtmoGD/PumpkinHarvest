using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseController : MonoBehaviour, IInteractable
{
    [SerializeField] public TooltipController tooltipController;

    [SerializeField] private int price;
    [SerializeField] private GameObject purchaseObject;

    public void BaseInteract(Farmer farmer)
    {
        throw new System.NotImplementedException();
    }

    public void ShowBaseInteractTooltip(Farmer farmer, bool show)
    {
        throw new System.NotImplementedException();
    }

    public void SpecialInteract(Farmer farmer)
    {
        throw new System.NotImplementedException();
    }

    public void ShowSpecialInteractTooltip(Farmer farmer, bool show)
    {
        throw new System.NotImplementedException();
    }
}
