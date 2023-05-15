using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// This component allows a button to be virtually pressed when an input action is invoked
    /// </summary>
    public class ExecuteButtonPressOnInputAction : MonoBehaviour
    {
        [SerializeField] private string inputActionName;
        [SerializeField] private Button button;
        [SerializeField] private InputActionAsset inputActionAsset;


        private void OnEnable()
        {
            inputActionAsset[inputActionName].started += ExecuteButtonPressOnInputAction_started;
        }
        private void OnDisable()
        {
            inputActionAsset[inputActionName].started -= ExecuteButtonPressOnInputAction_started;
        }

        /// <summary>
        /// Presses the button
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteButtonPressOnInputAction_started(InputAction.CallbackContext obj)
        {
            button.onClick.Invoke();
        }
    }
}
