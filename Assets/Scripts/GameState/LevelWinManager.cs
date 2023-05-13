using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void CheckIfWin(int obj)
    {
        if (currentPoints < pointsToWin || hasWon)
            return;

        hasWon = true;
        OnWin.Invoke();
    }

    public void AddPoints(int points)
    {
        currentPoints += points;
        OnGainPoints.Invoke(currentPoints);
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3);
        if (nextLevelName != string.Empty)
            SceneManager.LoadSceneAsync(nextLevelName);
    }
}
