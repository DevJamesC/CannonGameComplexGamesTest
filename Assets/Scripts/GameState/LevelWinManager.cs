using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// Determin the win condition for the level, and hold data related to the players progress towards winning
    /// </summary>
    public class LevelWinManager : MonoBehaviour
    {
        public int CurrentPoints { get => currentPoints; }
        public int PointsToWin { get => pointsToWin; }

        public event Action<int> OnGainPoints = delegate { };
        public event Action OnWin = delegate { };

        [SerializeField] private int pointsToWin;
        [SerializeField] private string nextLevelName;

        private int currentPoints;
        bool hasWon;

        private void Start()
        {
            hasWon = false;
            OnGainPoints += CheckIfWin;
            OnWin += () => StartCoroutine(LoadNextLevel());
        }

        /// <summary>
        /// Every time a point is added, check if the current points equals or exeeds our goal
        /// </summary>
        /// <param name="obj"></param>
        private void CheckIfWin(int obj)
        {
            if (currentPoints < pointsToWin || hasWon)
                return;

            hasWon = true;
            OnWin.Invoke();
        }

        /// <summary>
        /// Add points to the current points counter
        /// </summary>
        /// <param name="points"></param>
        public void AddPoints(int points)
        {
            currentPoints += points;
            OnGainPoints.Invoke(currentPoints);
        }

        /// <summary>
        /// On victory, load the next level in a couple seconds
        /// </summary>
        /// <returns></returns>
        IEnumerator LoadNextLevel()
        {
            yield return new WaitForSeconds(3);
            if (nextLevelName != string.Empty)
                SceneManager.LoadSceneAsync(nextLevelName);
        }
    }
}
