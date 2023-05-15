using UnityEngine;
using PixelCrushers;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// This class registers new Unity Input System actions with the Dialogue System For Unity. This is a key peice to integrate the two solutions
    /// </summary>
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
                InputDeviceManager.UnregisterInputAction("Interact");
            }

        }
    }
}
