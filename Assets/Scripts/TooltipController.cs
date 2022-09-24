using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipController : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowTooltip()
    {
        if (animator != null)
            animator.SetBool("Active", true);
    }

    public void HideTooltip()
    {
        if (animator != null)
            animator.SetBool("Active", false);
    }
}
