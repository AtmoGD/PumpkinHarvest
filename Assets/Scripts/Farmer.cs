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
    [SerializeField] private Transform dropPosition;
    [SerializeField] private Vector3 interactBoxSize;
    [SerializeField] private float interactRadius = 1f;
    [SerializeField] private GameObject pumpkin;
    [SerializeField] private GameObject pumpkinPrefab;
    [SerializeField] private GameObject seed;
    [SerializeField] private GameObject seedPrefab;
    [SerializeField] private float seedingTime;
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private float wateringTime;
    [SerializeField] private float harvestingTime;
    [SerializeField] private PickUpType currentItem = PickUpType.None;
    public PickUpType CurrentItem { get { return currentItem; } }
    private IInteractable interactableInReach;
    private float isSeedingTimer = 0f;
    private float isWateringTimer = 0f;
    private float isHarvestingTimer = 0f;

    protected override void Start()
    {
        base.Start();

        UpdateMoneyText();
    }

    protected new void Update()
    {
        base.Update();

        if (isSeedingTimer > 0f)
        {
            isSeedingTimer -= Time.deltaTime;
            if (isSeedingTimer <= 0f)
                EndSeeding();
        }

        if (isWateringTimer > 0f)
        {
            isWateringTimer -= Time.deltaTime;
            if (isWateringTimer <= 0f)
                EndWatering();
        }

        if (isHarvestingTimer > 0f)
        {
            isHarvestingTimer -= Time.deltaTime;
            if (isHarvestingTimer <= 0f)
                EndHarvesting();
        }

        UpdateInteractableInReach();
    }

    public void StartSeeding()
    {
        animator.SetTrigger("IsWorking");
        isSeedingTimer = seedingTime;
    }

    public void EndSeeding()
    {
        animator.SetTrigger("StopWorking");
        isSeedingTimer = 0f;
    }

    public void StartWatering()
    {
        animator.SetTrigger("IsWatering");
        isWateringTimer = wateringTime;
    }

    public void EndWatering()
    {
        animator.SetTrigger("StopWatering");
        isWateringTimer = 0f;
    }

    public void StartHarvesting()
    {
        animator.SetTrigger("IsWorking");
        isHarvestingTimer = harvestingTime;
    }

    public void EndHarvesting()
    {
        animator.SetTrigger("StopWorking");
        isHarvestingTimer = 0f;
    }

    protected override bool CanInteract()
    {
        bool canInteract = true;

        if (isSeedingTimer > 0f || isWateringTimer > 0f || isHarvestingTimer > 0f)
            canInteract = false;

        return canInteract;
    }

    public override void OnBaseInteract()
    {
        if (!CanInteract())
            return;

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
        // RaycastHit[] hits = Physics.BoxCastAll(interactPoint.position, interactBoxSize, Vector3.up, Quaternion.identity, 0f);

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
                case PickUpType.Seed:
                    seed.SetActive(true);
                    break;
                case PickUpType.Water:
                    water.SetActive(true);
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
                    Instantiate(pumpkinPrefab, dropPosition.position, Quaternion.identity);
                    break;
                case PickUpType.Seed:
                    Instantiate(seedPrefab, dropPosition.position, Quaternion.identity);
                    break;
                case PickUpType.Water:
                    Instantiate(waterPrefab, dropPosition.position, Quaternion.identity);
                    break;
            }

            ResetItem();

            return true;
        }

        return false;
    }

    public void DeliverPumpkin()
    {
        ResetItem();
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

    public void ResetItem()
    {
        currentItem = PickUpType.None;
        animator.SetBool("HasItem", false);
        pumpkin.SetActive(false);
        seed.SetActive(false);
        water.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactPoint.position, interactRadius);
        // Gizmos.DrawWireCube(interactPoint.position, interactBoxSize);
    }
}
