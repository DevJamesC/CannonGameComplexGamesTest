using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using TMPro;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// This component procedurally generates level select buttons for a level select menu
    /// </summary>
    public class DisplayLevelSelectMenu : MonoBehaviour
    {
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private Transform buttonParent;

        private void Start()
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
                if (Regex.IsMatch(sceneName, "Level*"))
                {
                    Button newButton = Instantiate(buttonPrefab, buttonParent);
                    newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Level {Regex.Match(sceneName, @"\d+").Value}";
                    newButton.onClick.AddListener(() => UIButtonMethods.LoadSceneStatic(sceneName));
                }
            }

        }

    }
}
