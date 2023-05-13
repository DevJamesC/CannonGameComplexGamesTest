using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void LevelWinManager_OnWin()
    {
        gameWinMenu.SetActive(true);
    }
}
