using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Controllable farmer;
    [SerializeField] private Controllable truck;

    private Controllable currentControllable;

    private void Start()
    {
        farmer.InitPlayerController(this);
        truck.InitPlayerController(this);

        currentControllable = farmer;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (currentControllable)
            currentControllable.OnMove(context.ReadValue<Vector2>());
    }

    public void OnBaseInteract(InputAction.CallbackContext context)
    {
        if (context.started && currentControllable)
            currentControllable.OnBaseInteract();
    }

    public void OnSpecialInteract(InputAction.CallbackContext context)
    {
        if (context.started && currentControllable)
            currentControllable.OnSpecialInteract();
    }
}
