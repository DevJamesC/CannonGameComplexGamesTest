using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOnStart : MonoBehaviour
{

    [SerializeField] private UIButtonMethods uiButtonMethods;
    private void Start()
    {
        uiButtonMethods.SetPauseTimescale();
    }
}
