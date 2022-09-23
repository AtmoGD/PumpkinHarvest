using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Material> customerMaterials = new List<Material>();
    [SerializeField] private List<SkinnedMeshRenderer> customerModel = new List<SkinnedMeshRenderer>();
    [SerializeField] private Animator animator;
    [SerializeField] private bool isActive;
    [SerializeField] private int amountOfPumpkins;
    [SerializeField] private int moneyReward;

    private int amountPumpkinsLeft;

    private void Start()
    {
        amountPumpkinsLeft = amountOfPumpkins;
        UpdateCustomerMaterial();
    }

    public void BaseInteract(Farmer farmer)
    {
        print("BaseInteract on Customer");
        if (isActive && farmer.CurrentItem == PickUpType.Pumpkin)
        {
            farmer.DeliverPumpkin();
            amountPumpkinsLeft--;
            if (amountPumpkinsLeft <= 0)
            {
                farmer.AddMoney(moneyReward);
                animator.SetTrigger("Leave");
                isActive = false;
                print("Customer left");
            }
        }
    }

    public void ShowBaseInteractTooltip(Farmer farmer, bool show)
    {
        print("ShowBaseInteractTooltip on Customer");
        // if (isActive && farmer.CurrentItem == PickUpType.Pumpkin)
        // {
        //     if (show)
        //     {
        //         TooltipController.Instance.ShowTooltip("Deliver pumpkin");
        //     }
        //     else
        //     {
        //         TooltipController.Instance.HideTooltip();
        //     }
        // }
    }

    public void SpecialInteract(Farmer farmer)
    {
        // throw new System.NotImplementedException();
    }

    public void ShowSpecialInteractTooltip(Farmer farmer, bool show)
    {
        // throw new System.NotImplementedException();
    }

    public void UpdateCustomerMaterial()
    {
        Material newMaterial = customerMaterials[Random.Range(0, customerMaterials.Count)];
        foreach (SkinnedMeshRenderer meshRenderer in customerModel)
        {
            meshRenderer.material = newMaterial;
        }
    }
}
