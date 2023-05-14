using UnityEngine;
using UnityEngine.InputSystem;
using PixelCrushers;

public class DialogueSystemInputRegistration : MonoBehaviour
{
    protected static bool isRegistered = false;
    private bool didIRegister = false;
    private InputControls controls;
    void Awake()
    {
        controls = new InputControls();
    }
    void OnEnable()
    {
        if (!isRegistered)
        {
            isRegistered = true;
            didIRegister = true;
            controls.Enable();
            //InputDeviceManager.RegisterInputAction("Back", controls.Gameplay.Back);
            InputDeviceManager.RegisterInputAction("Interact", controls.Gameplay.Interact);
        }
    }
    void OnDisable()
    {
        if (didIRegister)
        {
            isRegistered = false;
            didIRegister = false;
            controls.Disable();
            //InputDeviceManager.UnregisterInputAction("Back");
            InputDeviceManager.UnregisterInputAction("Interact");
        }

    }

}
