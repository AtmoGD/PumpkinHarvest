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

    public void OnMove(InputAction.CallbackContext _context)
    {
        if (currentControllable)
            currentControllable.OnMove(_context.ReadValue<Vector2>());
    }

    public void OnBaseInteract(InputAction.CallbackContext _context)
    {
        if (currentControllable)
            currentControllable.OnBaseInteract();
    }

    public void OnSpecialInteract(InputAction.CallbackContext _context)
    {
        if (currentControllable)
            currentControllable.OnSpecialInteract();
    }
}
