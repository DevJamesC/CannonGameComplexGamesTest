using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {

    }

    [Serializable]
    private enum CounterType
    {
        Current,
        PointsToWin
    }
}
