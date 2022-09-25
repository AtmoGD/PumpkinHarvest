using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void StartGame()
    {
        animator.SetTrigger("StartGame");
    }

    public void StartGameOnGameController()
    {
        GameController.instance.StartGame();
    }
}
