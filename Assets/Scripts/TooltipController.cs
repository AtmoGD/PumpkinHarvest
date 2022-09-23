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
        animator.SetBool("Active", true);
    }

    public void HideTooltip()
    {
        animator.SetBool("Active", false);
    }
}
