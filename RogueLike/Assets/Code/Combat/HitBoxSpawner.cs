using System.Collections;
using System.Collections.Generic;
using Assets.Code;
using Assets.Code.Networking;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class HitBoxSpawner : NetworkSingleton<HitBoxSpawner>
{
    public GameObject CirclePrefab;
    public GameObject BoxPrefab;

    public override void OnStartServer()
    {
        if (CirclePrefab == null || BoxPrefab == null)
        {
            Debug.LogError("no prefab found");
            return;
        }

        AddIfNotContained(CirclePrefab);
        AddIfNotContained(BoxPrefab);

        static void AddIfNotContained(GameObject value)
        {
            List<GameObject> prefabs = NetworkManager.singleton.spawnPrefabs;
            if (!prefabs.Contains(value))
            {
                prefabs.Add(value);
            }
        }
    }

    public void SpawnCircle(GameObject owner, int size, Vector2 position)
    {
        GameObject instance = Instantiate(CirclePrefab);
        instance.transform.position = position;
        Vector3 scale = instance.transform.localScale;
        scale.x *= size;
        scale.y *= size;
        instance.transform.localScale = scale;
        NetworkServer.Spawn(instance, owner);
    }
}
