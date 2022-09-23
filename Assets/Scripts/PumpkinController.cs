using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PumpkinState
{
    Empty,
    Growing,
    Ready
}

public class PumpkinController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject pumpkin;
    [SerializeField] private float targetScale = 1f;
    [SerializeField] private float waterUsagePerSecond = 1f;
    [SerializeField] private float maxWaterFill = 100f;
    [SerializeField] private float waterNeededToGrow = 150f;

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
        }

        print(state);
    }

    public void UpdatePumpkinScale()
    {
        float scale = Mathf.Lerp(0f, targetScale, currentGrow / waterNeededToGrow);

        pumpkin.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void BaseInteract()
    {
        print("BaseInteract on Pumpkin");

        switch (state)
        {
            case PumpkinState.Empty:
                StartGrowing();
                break;
            case PumpkinState.Growing:
                StartWatering();
                break;
            case PumpkinState.Ready:
                StartHarvesting();
                break;
        }
    }

    public void SpecialInteract()
    {

    }

    public void StartGrowing()
    {
        state = PumpkinState.Growing;
    }

    public void StartWatering()
    {
        currentWaterFill = maxWaterFill;
    }

    public void StartHarvesting()
    {
        state = PumpkinState.Empty;
    }
}
