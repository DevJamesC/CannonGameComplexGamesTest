using UnityEngine;
using UnityEngine.InputSystem;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// This class will set the current input map on start
    /// </summary>
    public class SwitchInputMapOnStart : MonoBehaviour
    {
        [SerializeField] private InputMaps inputMapToSwitchTo;
        // Start is called before the first frame update
        void Start()
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj == null)
                return;
            PlayerInput playerInput = playerObj.GetComponent<PlayerInput>();
            switch (inputMapToSwitchTo)
            {
                case InputMaps.Gameplay: playerInput.SwitchCurrentActionMap("Gameplay"); break;
                case InputMaps.Menu: playerInput.SwitchCurrentActionMap("Menu"); break;

            }

        }

        [SerializeField]
        private enum InputMaps
        {
            Gameplay,
            Menu
        }
    }
}
