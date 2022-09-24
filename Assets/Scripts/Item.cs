using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private TooltipController itemTooltipController;
    [SerializeField] private PickUpType type;
    public void BaseInteract(Farmer farmer)
    {
        if (farmer.CurrentItem == PickUpType.None)
        {
            farmer.PickUpItem(type);
            Destroy(gameObject);
        }
    }

    public void ShowInteractTooltip(Farmer farmer, bool show)
    {
        if (show && farmer.CurrentItem == PickUpType.None)
            itemTooltipController?.ShowTooltip();
        else
            itemTooltipController?.HideTooltip();
    }
}
