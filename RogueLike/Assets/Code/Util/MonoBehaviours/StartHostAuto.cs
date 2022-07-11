using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class StartHostAuto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.singleton.StartHost();
    }
}
