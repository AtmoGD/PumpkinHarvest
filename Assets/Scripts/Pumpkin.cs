using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour, IInteractable
{
    public void BaseInteract(Farmer farmer)
    {
        if (farmer.CurrentItem == PickUpType.None)
        {
            farmer.PickUpItem(PickUpType.Pumpkin);
            Destroy(gameObject);
        }
    }

    public void ShowBaseInteractTooltip(Farmer farmer, bool show)
    {
        // throw new System.NotImplementedException();
    }

    public void SpecialInteract(Farmer farmer)
    {
        print("SpecialInteract on Pumpkin");
    }

    public void ShowSpecialInteractTooltip(Farmer farmer, bool show)
    {
        // throw new System.NotImplementedException();
    }
}
