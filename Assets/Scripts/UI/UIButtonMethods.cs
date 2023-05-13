using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIButtonMethods : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public static void LoadSceneStatic(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void ChangeInputMapToGamePlay()
    {
        PlayerInput playerInput = GetPlayerInput();
        playerInput.SwitchCurrentActionMap("Gameplay");
    }

    public void ChangeInputMapToMenu()
    {
        PlayerInput playerInput = GetPlayerInput();
        playerInput.SwitchCurrentActionMap("Menu");
    }

    private PlayerInput GetPlayerInput()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null)
            return null;
        return playerObj.GetComponent<PlayerInput>();
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
