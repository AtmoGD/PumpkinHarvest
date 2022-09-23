using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PumpkinState
{
    Empty,
    Growing,
    Ready
}

public class PumpkinPlaceController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject pumpkin;
    [SerializeField] private float targetScale = 1f;
    [SerializeField] private float waterUsagePerSecond = 1f;
    [SerializeField] private float maxWaterFill = 100f;
    [SerializeField] private float waterNeededToGrow = 150f;

    [SerializeField] private TooltipController seedTooltip;
    [SerializeField] private TooltipController waterTooltip;
    [SerializeField] private TooltipController harvestTooltip;

    [SerializeField] private PumpkinState state = PumpkinState.Empty;
    [SerializeField] private float currentWaterFill = 0f;
    [SerializeField] private float currentGrow = 0f;

    private void Start()
    {
        UpdatePumpkinScale();
    }

    private void Update()
    {
        if (state == PumpkinState.Growing && currentWaterFill > 0)
        {
            float waterUsage = waterUsagePerSecond * Time.deltaTime;

            if (currentWaterFill > waterUsage)
            {
                currentWaterFill -= waterUsage;
                currentGrow += waterUsage;

                UpdatePumpkinScale();

                if (currentGrow >= waterNeededToGrow)
                    state = PumpkinState.Ready;
            }
            else
            {
                currentWaterFill = 0;
            }
        }

        print(state);
    }

    public void UpdatePumpkinScale()
    {
        float scale = Mathf.Lerp(0f, targetScale, currentGrow / waterNeededToGrow);

        pumpkin.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void BaseInteract(Farmer farmer)
    {
        print("BaseInteract on Pumpkin");

        switch (state)
        {
            case PumpkinState.Empty:
                StartGrowing(farmer);
                break;
            case PumpkinState.Growing:
                StartWatering(farmer);
                break;
            case PumpkinState.Ready:
                StartHarvesting(farmer);
                break;
        }

        HideAllTooltips();
    }

    public void ShowBaseInteractTooltip(Farmer farmer, bool show)
    {
        if (show)
        {
            switch (state)
            {
                case PumpkinState.Empty:
                    if (farmer.CurrentItem == PickUpType.None)
                        seedTooltip.ShowTooltip();
                    break;
                case PumpkinState.Growing:
                    if (currentWaterFill <= 0f && farmer.CurrentItem == PickUpType.None)
                        waterTooltip.ShowTooltip();
                    break;
                case PumpkinState.Ready:
                    if (farmer.CurrentItem == PickUpType.None)
                        harvestTooltip.ShowTooltip();
                    break;
            }
        }
        else
        {
            HideAllTooltips();
        }
    }

    public void HideAllTooltips()
    {
        seedTooltip.HideTooltip();
        waterTooltip.HideTooltip();
        harvestTooltip.HideTooltip();
    }

    public void SpecialInteract(Farmer farmer)
    {

    }

    public void ShowSpecialInteractTooltip(Farmer farmer, bool show)
    {

    }

    public void StartGrowing(Farmer farmer)
    {
        if (farmer.CurrentItem == PickUpType.None)
        {
            state = PumpkinState.Growing;
        }
    }

    public void StartWatering(Farmer farmer)
    {
        if (currentWaterFill <= 0f && farmer.CurrentItem == PickUpType.None)
        {
            currentWaterFill = maxWaterFill;
        }
    }

    public void StartHarvesting(Farmer farmer)
    {
        if (farmer.PickUpItem(PickUpType.Pumpkin))
        {
            state = PumpkinState.Empty;
            currentGrow = 0f;
            currentWaterFill = 0f;
            UpdatePumpkinScale();
        }
    }
}
