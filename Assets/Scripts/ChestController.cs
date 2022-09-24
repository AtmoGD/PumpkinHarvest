using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator animator;
    [SerializeField] private TooltipController tooltipController;
    [SerializeField] private float openTime = 1f;
    private float openTimer = 0f;

    private void Start()
    {
        openTimer = openTime;
    }

    private void Update()
    {
        openTimer -= Time.deltaTime;
    }

    public void BaseInteract(Farmer farmer)
    {
        if (openTimer < 0f && farmer.CurrentItem == PickUpType.None)
        {
            farmer.PickUpItem(PickUpType.Seed);
            openTimer = openTime;
            animator.SetTrigger("Open");
        }
    }

    public void ShowInteractTooltip(Farmer farmer, bool show)
    {
        if (show && farmer.CurrentItem == PickUpType.None)
            tooltipController?.ShowTooltip();
        else
            tooltipController?.HideTooltip();
    }
}
