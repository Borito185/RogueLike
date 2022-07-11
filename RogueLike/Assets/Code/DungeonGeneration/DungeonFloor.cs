using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.DungeonGeneration
{
    [CreateAssetMenu(fileName = "DungeonFloor", menuName = "Dungeon/Floor", order = 0)]
    public class DungeonFloor : ScriptableObject
    {
        public List<TileBase> GroundTiles;
    }
}