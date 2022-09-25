using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PurchaseController : MonoBehaviour, IInteractable
{
    [SerializeField] public TooltipController tooltipController;
    [SerializeField] public GameObject purchasePanel;
    [SerializeField] private int price;
    [SerializeField] public TMP_Text priceText;
    [SerializeField] private GameObject purchaseObject;
    [SerializeField] private AudioSource purchaseSound;
    [SerializeField] private float destroyDelay = 1.5f;

    private void Start()
    {
        priceText.text = price.ToString();
    }

    public void BaseInteract(Farmer farmer)
    {
        if (farmer.CurrentItem == PickUpType.None && farmer.RemoveMoney(price))
        {
            purchaseObject.SetActive(true);
            purchasePanel.SetActive(false);
            tooltipController.HideTooltip();
            tooltipController = null;
            purchaseSound.Play();
            Destroy(gameObject, destroyDelay);
        }
    }

    public void ShowInteractTooltip(Farmer farmer, bool show)
    {
        if (show)
            tooltipController?.ShowTooltip();
        else
            tooltipController?.HideTooltip();
    }
}
