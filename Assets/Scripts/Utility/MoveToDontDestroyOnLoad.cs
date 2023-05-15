using UnityEngine;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// This class moves an object to the DontDestroyOnLoad scene, and destroys this object if another one already exists (in the case of a scene being reloaded)
    /// </summary>
    public class MoveToDontDestroyOnLoad : MonoBehaviour
    {
        [Tooltip("If this scene is reloaded, then another copy will be added to DontDestroyOnLoad. This option prevents that from happening")]
        [SerializeField] private bool destroyIfDuplicated;
        [Tooltip("The ID is used to identify objects which should only exist once.")] 
        [SerializeField] private int id;
        //While I could use a singleton pattern, I don't want to extend the EventSystem Component to destroy itself for this project.

        // Start is called before the first frame update
        void Start()
        {
            if (destroyIfDuplicated)
            {
                foreach (var obj in FindObjectsByType<MoveToDontDestroyOnLoad>(FindObjectsSortMode.InstanceID))
                {
                    if (obj.gameObject == gameObject)
                        continue;

                    if (obj.id == id)
                    {
                        Destroy(gameObject);
                        return;
                    }

                }
            }


            DontDestroyOnLoad(gameObject);
        }
    }
}
