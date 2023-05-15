using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// This class provides utility methods for UI button OnClick events.
    /// NOTE: I am aware that this class violates Single Responsibility. Refactoring should occur.
    /// </summary>
    public class UIButtonMethods : MonoBehaviour
    {
        private float gameplayTimescale;

        /// <summary>
        /// Load a scene
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        /// <summary>
        /// Static method to load a scene. Access and call this method from anywhere.
        /// </summary>
        /// <param name="sceneName"></param>
        public static void LoadSceneStatic(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        /// <summary>
        /// Unloads a scene
        /// </summary>
        /// <param name="sceneName"></param>
        public void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        /// <summary>
        /// Loads a scene additively
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadSceneAdditive(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        /// <summary>
        /// Change input map to gameplay input
        /// </summary>
        public void ChangeInputMapToGamePlay()
        {
            PlayerInput playerInput = GetPlayerInput();
            playerInput.SwitchCurrentActionMap("Gameplay");
        }

        /// <summary>
        /// Change input map to menu input
        /// </summary>
        public void ChangeInputMapToMenu()
        {
            PlayerInput playerInput = GetPlayerInput();
            playerInput.SwitchCurrentActionMap("Menu");
        }

        /// <summary>
        /// Get the PlayerInput component. Used for the ChangeInputMap methods
        /// </summary>
        /// <returns></returns>
        private PlayerInput GetPlayerInput()
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj == null)
                return null;
            return playerObj.GetComponent<PlayerInput>();
        }

        /// <summary>
        /// Pause the game, and keep a record of the previous timescale
        /// </summary>
        public void SetPauseTimescale()
        {
            gameplayTimescale = Time.timeScale;
            Time.timeScale = 0;
        }
         /// <summary>
         /// Resume the game by applying the previous timescale
         /// </summary>
        public void SetGameplayTimescale()
        {
            Time.timeScale = gameplayTimescale;
        }

        /// <summary>
        /// Quit the application, or quit play mode if playing in the editor
        /// </summary>
        public void QuitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
