using System.Collections;
using System.Collections.Generic;
using Assets.Code;
using Assets.Code.Networking;
using UnityEngine;

public class HitBoxSpawner : NetworkSingleton<HitBoxSpawner>
{
    public GameObject CirclePrefab;
    public GameObject BoxPrefab;

    public override void OnStartServer()
    {
        if (CirclePrefab == null)
            Debug.LogError($"no {nameof(CirclePrefab)} found");
        if (BoxPrefab == null)
            Debug.LogError($"no {nameof(BoxPrefab)} found");
    }

    public void SpawnCircle(int size, Vector2 position)
    {

    }
}
