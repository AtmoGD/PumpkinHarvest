using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum PickUpType
{
    None,
    Water,
    Seed,
    Pumpkin
}

public enum FarmerState
{
    Idle,
    Walking,
    Seeding,
    Watering,
    Harvesting
}

public class Farmer : Controllable
{

    [SerializeField] int money = 0;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] private Transform interactPoint;
    [SerializeField] private float interactRadius = 1f;
    [SerializeField] private GameObject pumpkin;
    [SerializeField] private GameObject pumpkinPrefab;
    [SerializeField] private GameObject seed;
    [SerializeField] private GameObject seedPrefab;
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject waterPrefab;
    private PickUpType currentItem = PickUpType.None;
    public PickUpType CurrentItem { get { return currentItem; } }
    private IInteractable interactableInReach;

    protected override void Start()
    {
        base.Start();

        UpdateMoneyText();
    }

    protected new void Update()
    {
        base.Update();

        UpdateInteractableInReach();
    }

    public override void OnBaseInteract()
    {
        if (interactableInReach != null)
        {
            interactableInReach.BaseInteract(this);
        }
        else if (currentItem != PickUpType.None)
        {
            DropItem();
        }
    }

    public void UpdateInteractableInReach()
    {
        RaycastHit[] hits = Physics.SphereCastAll(interactPoint.position, interactRadius, Vector3.up, 0f);

        foreach (RaycastHit hit in hits)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                ResetInteractableInReach();
                interactableInReach = interactable;
                interactableInReach.ShowInteractTooltip(this, true);
                return;
            }
        }

        interactableInReach?.ShowInteractTooltip(this, false);
        interactableInReach = null;
    }

    public void ResetInteractableInReach()
    {
        interactableInReach?.ShowInteractTooltip(this, false);
        interactableInReach = null;
    }

    public bool PickUpItem(PickUpType _item)
    {
        if (CurrentItem == PickUpType.None)
        {
            currentItem = _item;
            UpdateAnimator();

            switch (currentItem)
            {
                case PickUpType.Pumpkin:
                    pumpkin.SetActive(true);
                    break;
            }

            return true;
        }

        return false; ;
    }

    public void UpdateAnimator()
    {
        animator.SetBool("HasItem", currentItem != PickUpType.None);
    }

    public bool DropItem()
    {
        if (CurrentItem != PickUpType.None)
        {
            switch (currentItem)
            {
                case PickUpType.Pumpkin:
                    ResetPumpkin();
                    Instantiate(pumpkinPrefab, pumpkin.transform.position, Quaternion.identity);
                    break;
            }

            return true;
        }

        return false;
    }

    public void DeliverPumpkin()
    {
        ResetPumpkin();
    }

    public void AddMoney(int amount)
    {
        money += amount;

        UpdateMoneyText();
    }

    public bool RemoveMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;

            UpdateMoneyText();

            return true;
        }
        return false;
    }

    private void UpdateMoneyText()
    {
        moneyText.text = money.ToString();
    }

    public void ResetPumpkin()
    {
        currentItem = PickUpType.None;
        animator.SetBool("HasItem", false);
        pumpkin.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactPoint.position, interactRadius);
    }
}
