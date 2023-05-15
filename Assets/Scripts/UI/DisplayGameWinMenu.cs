using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// This enables a "You Win" menu when the GameWin manager invokes the game win event
    /// </summary>
    public class DisplayGameWinMenu : MonoBehaviour
    {
        [SerializeField] private GameObject gameWinMenu;
        private LevelWinManager levelWinManager;
        // Start is called before the first frame update
        void Start()
        {
            levelWinManager = FindFirstObjectByType<LevelWinManager>();
            levelWinManager.OnWin += LevelWinManager_OnWin;
        }

        /// <summary>
        /// Set the game win menu active
        /// </summary>
        private void LevelWinManager_OnWin()
        {
            gameWinMenu.SetActive(true);
        }
    }
}
