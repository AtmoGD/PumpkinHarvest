using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerController : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Material> customerMaterials = new List<Material>();
    [SerializeField] private List<SkinnedMeshRenderer> customerModel = new List<SkinnedMeshRenderer>();
    [SerializeField] private TooltipController tooltipController;
    [SerializeField] private TMP_Text pumpkinsLeftText;
    [SerializeField] private TMP_Text moneyRewardText;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isActive;
    [SerializeField] private AudioSource customerPaidSound;
    public bool IsActive { get { return isActive; } }
    [SerializeField] private int amountOfPumpkins;
    [SerializeField] private int moneyReward;

    private int amountPumpkinsLeft;

    private void Start()
    {
        GameController.instance.InitCustomer(this);
        amountPumpkinsLeft = amountOfPumpkins;
        UpdateCustomerMaterial();

        if (isActive)
            BecomeActive(amountOfPumpkins, moneyReward);
    }

    public void BecomeActive(int pumpkinAmount, int reward)
    {
        amountOfPumpkins = pumpkinAmount;
        amountPumpkinsLeft = amountOfPumpkins;
        moneyReward = reward;
        isActive = true;
        animator.SetBool("Active", true);
        UpdatePumpkinsLeftText();
        UpdateMoneyRewardText();
    }

    public void BecomeInactive()
    {
        isActive = false;
        animator.SetBool("Active", false);
    }

    public void BaseInteract(Farmer farmer)
    {
        if (isActive && farmer.CurrentItem == PickUpType.Pumpkin)
        {
            TakePumpkin(farmer);

            if (amountPumpkinsLeft <= 0)
            {
                farmer.AddMoney(moneyReward);
                customerPaidSound.Play();
                BecomeInactive();
            }
        }
    }

    public void TakePumpkin(Farmer farmer)
    {
        farmer.DeliverPumpkin();
        amountPumpkinsLeft--;
        UpdatePumpkinsLeftText();
    }

    public void ShowInteractTooltip(Farmer farmer, bool show)
    {
        if (show)
            tooltipController?.ShowTooltip();
        else
            tooltipController?.HideTooltip();
    }

    public void UpdateCustomerMaterial()
    {
        Material newMaterial = customerMaterials[Random.Range(0, customerMaterials.Count)];

        foreach (SkinnedMeshRenderer meshRenderer in customerModel)
            meshRenderer.material = newMaterial;
    }

    public void UpdatePumpkinsLeftText()
    {
        pumpkinsLeftText.text = amountPumpkinsLeft.ToString();
    }

    public void UpdateMoneyRewardText()
    {
        moneyRewardText.text = moneyReward.ToString();
    }
}
