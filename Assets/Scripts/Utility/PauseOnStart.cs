using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// Pauses the game on Start. Used by the Settings menu to pause the game when it is loaded.
    /// </summary>
    public class PauseOnStart : MonoBehaviour
    {

        [SerializeField] private UIButtonMethods uiButtonMethods;
        private void Start()
        {
            uiButtonMethods.SetPauseTimescale();
        }
    }
}
