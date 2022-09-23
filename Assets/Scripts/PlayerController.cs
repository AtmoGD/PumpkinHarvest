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
        currentControllable = farmer;
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        if (currentControllable)
            currentControllable.OnMove(_context.ReadValue<Vector2>());
    }
}
