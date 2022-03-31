using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {
        EnterPlayMode();
    }

    
    private void EnterPlayMode()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }
}
