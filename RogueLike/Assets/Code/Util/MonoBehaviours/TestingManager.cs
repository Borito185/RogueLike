using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
/// <summary>
/// class used in testing scene's allows for automatic session creation and helper functions for testing
/// </summary>
public class TestingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (ParrelSync.ClonesManager.IsClone())
        {
            NetworkManager.singleton.StartClient();
        }
        else
        {
            NetworkManager.singleton.StartHost();
        }
    }

    public void WriteToConsole(string text)
    {
        Debug.Log(text);
    }
}
