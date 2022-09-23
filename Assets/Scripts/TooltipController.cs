using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipController : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowTooltip()
    {
        animator.SetBool("Active", true);
        print("ShowTooltip");
    }

    public void HideTooltip()
    {
        animator.SetBool("Active", false);
        print("HideTooltip");
    }
}
