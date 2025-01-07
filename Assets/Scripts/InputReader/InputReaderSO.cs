using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


[CreateAssetMenu(fileName = "InputReaderSO", menuName = "InputReaderSO", order = 0)]
public class InputReaderSO : ScriptableObject, InputManager.IPlayerActions
{
    private InputManager _inputManager;
    public event UnityAction<Vector2> moveAction = delegate { };
    public event UnityAction<Vector2> lookAction = delegate { };
    public event UnityAction jumpAction = delegate { };
    public event UnityAction shootAction = delegate { };
    public event UnityAction forwardGunAction = delegate { };
    public event UnityAction backwardGunAction = delegate { };
    private void OnEnable()
    {
        _inputManager = new InputManager();
        _inputManager.Player.SetCallbacks(this);
        _inputManager.Player.Enable();

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveAction?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            jumpAction?.Invoke();
        }
    }
    public Vector2 GetMousePosition()
    {
        return _inputManager.Player.Look.ReadValue<Vector2>();
    }
    public Vector2 GetPlayerPosition()
    {
        return _inputManager.Player.Move.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            lookAction?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            shootAction?.Invoke();
        }
    }

    public void OnChangeGunForward(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            forwardGunAction?.Invoke();
        }
    }

    public void OnChangeGunBackward(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            backwardGunAction?.Invoke();
        }
    }

    
}

