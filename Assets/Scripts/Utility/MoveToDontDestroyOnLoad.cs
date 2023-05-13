using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToDontDestroyOnLoad : MonoBehaviour
{
    [Tooltip("If this scene is reloaded, then another copy will be added to DontDestroyOnLoad. This option prevents that from happening")]
    [SerializeField] private bool destroyIfDuplicated;
    [Tooltip("The ID is used to identify objects which should only exist once.")] //While I could use a singleton pattern, I don't want to extend the EventSystem to destroy itself for this project.
    [SerializeField] private int id;
    // Start is called before the first frame update
    void Start()
    {
        if(destroyIfDuplicated)
        {
            foreach (var obj in FindObjectsByType<MoveToDontDestroyOnLoad>(FindObjectsSortMode.InstanceID))
            {
                if (obj.gameObject == gameObject)
                    continue;

                if(obj.id == id)
                {
                    Destroy(gameObject);
                    return;
                }

            }
        }


        DontDestroyOnLoad(gameObject);
    }
}
