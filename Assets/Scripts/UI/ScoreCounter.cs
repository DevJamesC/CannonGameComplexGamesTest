using System;
using TMPro;
using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// This component will display the current or target score to win on a text component
    /// </summary>
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private CounterType counterType;


        private LevelWinManager levelWinManager;
        private TextMeshProUGUI text;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            levelWinManager = FindFirstObjectByType<LevelWinManager>();
        }
        // Start is called before the first frame update
        void Start()
        {
            if (counterType == CounterType.PointsToWin)
                text.text = $"Points to Win: {levelWinManager.PointsToWin}";
            else
            {
                levelWinManager.OnGainPoints += LevelWinManager_OnGainPoints;
                text.text = $"Points: {levelWinManager.CurrentPoints}";
            }
        }

        private void LevelWinManager_OnGainPoints(int obj)
        {
            text.text = $"Points: {levelWinManager.CurrentPoints}";
        }

        [Serializable]
        private enum CounterType
        {
            Current,
            PointsToWin
        }
    }
}